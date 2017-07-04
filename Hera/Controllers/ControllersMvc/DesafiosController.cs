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
        public IActionResult Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var model = (string.IsNullOrWhiteSpace(searchString)) ?
                _data.GetAll_Desafios() :
                _data.Autocomplete_Desafios(searchString);
            return View(new PaginationViewModel<Desafio>(model, skip, take));
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
                    return RedirectToAction("Index");
                }
                catch (Exception) { }
            }
            return View(model);
        }


        
        
    }
}