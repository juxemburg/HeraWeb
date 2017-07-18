using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Hera.Models.UtilityViewModels;
using Entities.Desafios;
using Hera.Models.EntitiesViewModels;
using Hera.Services.UserServices;
using Hera.Models.EntitiesViewModels.Desafios;
using Microsoft.EntityFrameworkCore;
using Hera.Models.EntitiesViewModels.Ratings;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
    [Route("[controller]/[action]")]
    public class DesafiosController : Controller
    {
        private IDataAccess _data;
        private UserService _userService;

        public DesafiosController(IDataAccess data,
            UserService userService)
        {
            _data = data;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SearchDesafioViewModel searchModel,
            int skip = 0, int take = 10)
        {
            var searchString = searchModel.SearchString;
            var model = _data.GetAll_Desafios(null, null,
                searchString, searchModel.Map(), searchModel.EqualSearchModel)
                .AsNoTracking();
            var profId = await _userService.Get_ProfesorId(User.Claims);
            if (profId > 0)
            {
                model = model.Where(d => d.ProfesorId != profId);
            }

            var list = model.Select(m =>
                new DesafioDetailsViewModel(m))
                .ToList();
            return View(new PaginationViewModel<DesafioDetailsViewModel>(
                list, skip, take));
        }

        [HttpGet("{desafioId}")]
        public async Task<IActionResult> Details(int desafioId)
        {
            var model = await _data.Find_Desafio(desafioId);

            if (model != null)
                return View(new DesafioDetailsViewModel(model));
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateDesafioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDesafioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profId = await _userService.Get_ProfesorId(User.Claims);
                    _data.AddDesafio(model.Map(profId));
                    await _data.SaveAllAsync();
                    return RedirectToAction("Index", "ProfesorDesafio");
                }
                catch (Exception) { }
            }
            return View(model);
        }


        [HttpPost("{desafioId}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Rate(int desafioId,
            RateViewModel model)
        {
            var idProfesor = await _userService.Get_ProfesorId(User.Claims);
            var value = await _data.Exist_Desafio(desafioId);
            if (!value)
                return NotFound();
            if (ModelState.IsValid)
            {
                await _data.Calificar_Desafio(desafioId, idProfesor,
                        model.Rate);
                await _data.SaveAllAsync();
            }
            return RedirectToAction("Details",
                new { desafioId = desafioId });
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var profId = await _userService.Get_ProfesorId(User.Claims);
            if (await _data.Exist_DesafioP(id, profId))
            {
                await _data.Delete_Desafio(id);
                var res = await _data.SaveAllAsync();
                return RedirectToAction("Index", "ProfesorDesafio");
            }
            return NotFound();
        }
    }
}