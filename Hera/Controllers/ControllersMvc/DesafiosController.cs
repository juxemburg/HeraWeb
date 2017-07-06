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

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
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
        public IActionResult Index(SearchDesafioViewModel searchModel,
            int skip = 0, int take = 10)
        {
            var searchString = searchModel.SearchString;
            var model = _data.GetAll_Desafios(null,null,
                searchString,searchModel.Map(),searchModel.EqualSearchModel);
            return View(
                new PaginationViewModel<Desafio>(model, skip, take));
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
    }
}