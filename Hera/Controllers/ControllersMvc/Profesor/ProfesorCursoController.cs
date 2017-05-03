using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;

namespace Hera.Controllers.ControllersMvc.Profesor
{
    [Route("/Profesor/Curso/{idCurso:int}/[action]")]
    [Authorize(Roles="Profesor")]
    public class ProfesorCursoController : Controller
    {
        private IDataAccess _data;

        public ProfesorCursoController(IDataAccess data)
        {
            _data = data;
        }

        [HttpGet]
        [Route("/Profesor/Curso/{idCurso:int}")]
        public async Task<IActionResult> Details(int idCurso)
        {
            var model = await _data.Find_Curso(idCurso);
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));

            if (model == null || model.ProfesorId != profId)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Desafio(int idCurso, int idDesafio)
        {
            var curso = await _data.Find_Curso(idCurso);
            var model = await _data.Find_Desafio(idDesafio);
            if(model == null || curso == null
                || curso.ContieneDesafio(idDesafio))
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpGet("{idEstudiante:int}")]
        public async Task<IActionResult> Estudiante(int idCurso,
            int idEstudiante)
        {
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));
            var model = await _data.Find_Estudiante(idEstudiante,
                idCurso, profId);
            if(model == null)
            {
                return NotFound();
            }

            return View(new EstudianteCalificacionViewModel(model));
        }
    }
}