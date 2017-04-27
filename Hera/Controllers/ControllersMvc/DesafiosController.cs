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

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
    public class DesafiosController : Controller
    {
        private IDataAccess _data;

        public DesafiosController(IDataAccess data)
        {
            _data = data;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDesafioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _data.AddDesafio(model.Map(""));
                    await _data.SaveAllAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception) { }
            }
            return View(model);
        }


        [AllowAnonymous]
        public async Task<FileResult> DownloadEscenario(int desafioId)
        {
            var desafio = await _data.Find_Desafio(desafioId);
            if (desafio != null)
            {

                var filepath = desafio.DirArchivo;
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                return File(fileBytes, "application/x-msdownload", "Escenario.sb2");
            }
            return null;
        }
    }
}