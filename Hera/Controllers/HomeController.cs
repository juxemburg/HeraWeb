using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Services;
using Hera.Models.EntitiesViewModels.EstudianteDesafio;

namespace Hera.Controllers
{
    public class HomeController : Controller
    {
        private ScratchService _evaluator;

        public HomeController(ScratchService evaluator)
        {
            _evaluator = evaluator;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Profesor"))
            {
                return RedirectToAction("Index", "ProfesorCursos");
            }
            else if (User.IsInRole("Estudiante"))
            {
                return RedirectToAction("Index", "EstudianteCursos");
            }   
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Evaluacion(string idProyecto)
        {
            if(!string.IsNullOrWhiteSpace(idProyecto))
            {
                var model= (await _evaluator
                    .Get_Evaluation(idProyecto))
                    .Select(val => val.Map());
                return View(new ResultadosScratchViewModel(model));
            }
            return RedirectToAction("Index");
        }
    }
}
