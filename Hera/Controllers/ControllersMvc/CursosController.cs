using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;
using Entities.Cursos;

using Hera.Models.UtilityViewModels;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
    public class CursosController : Controller
    {
        private IDataAccess _data;

        public CursosController(IDataAccess data)
        {
            _data = data;
        }

        [Authorize]
        public IActionResult Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos() :
                _data.Autocomplete_Cursos(searchString);

            return View(new PaginationViewModel<Curso>(model, skip, take));
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _data.Find_Curso(id);
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));
            if (model != null && model.ProfesorId == profId)
            {
                return View(model);
            }
            else
                return NotFound();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCursoViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var id = _data.Get_UserId(User.Claims);
                    var profId = await _data.Find_ProfesorId(id);
                    var desafio = await _data.Find_Desafio(model.DesafioId.GetValueOrDefault());
                    if (desafio != null)
                        _data.Add<Curso>(model.Map(profId, desafio));
                    else
                        _data.AddCurso(model.Map(profId));

                    var res = await _data.SaveAllAsync();
                    if (res)
                        return RedirectToAction("Index");
                }
                catch (Exception e) { }
            }
            ModelState.AddModelError("", "Error de cosos ");
            return View(model);
        }


        public async Task<IActionResult> AddDesafio(AddDesafioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var desafio = (model.DesafioId != null) ?
                    await _data.Find_Desafio(
                        model.DesafioId.GetValueOrDefault()) :
                    model.Map();

                if (model.DesafioId == null)
                    _data.AddDesafio(desafio);

                _data.AddDesafio(model.Id, desafio);
                await _data.SaveAllAsync();
            }
            return RedirectToAction("Details", new { id = model.Id });
        }


    }
}