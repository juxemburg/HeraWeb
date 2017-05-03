using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Hera.Models.UtilityViewModels;
using Entities.Cursos;
using Microsoft.EntityFrameworkCore;
using Entities.Calificaciones;

namespace Hera.Controllers.ControllersMvc
{
    
    [Route("/Estudiante/Cursos/[action]")]
    [Authorize(Roles = "Estudiante")]
    public class EstudianteCursoController : Controller
    {
        private IDataAccess _data;

        public EstudianteCursoController(IDataAccess data)
        {
            _data = data;
        }

        [HttpGet]        
        public IActionResult Busqueda(string searchString = "",
            int skip = 0, int take = 10)
        {
            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos() :
                _data.Autocomplete_Cursos(searchString);

            return View(new PaginationViewModel<Curso>(model, skip, take));
        }


        [Route("/Estudiante/Cursos/")]
        public async Task<IActionResult> Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var estudianteId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var model = _data.GetAll_CursosEstudiante(estudianteId);

            return View(new PaginationViewModel<Curso>
                (await model.ToListAsync(), 0, 10));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var model = await _data.Find_Curso(id);
            if (model == null || !model.ContieneEstudiante(estId))
                return NotFound();

            return View(model);
        }

        [HttpGet("/Estudiante/Cursos/{cursoId}/Desafio/{desafioId}")]        
        public async Task<IActionResult> Desafio(int cursoId, int desafioId)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var curso = await _data.Find_Curso(cursoId);
            if (curso == null || !curso.ContieneEstudiante(estId)
                || !curso.ContieneDesafio(desafioId))
                return NotFound();

            var model = await _data.Find_RegistroCalificacion(
                cursoId, estId, desafioId);
            if (model == null)
            {
                model = new RegistroCalificacion()
                {
                    CursoId = cursoId,
                    DesafioId = desafioId,
                    EstudianteId = estId,
                    Calificaciones = new List<Calificacion>()
                };
                _data.Add<RegistroCalificacion>(model);
                var res = await _data.SaveAllAsync();
                if (!res)
                    return BadRequest();
            }

            return View(model);

        }

        public async Task<IActionResult> IniciarDesafio(
            int cursoId, int desafioId)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));

            var model = new Calificacion()
            {
                BloquesRepetidos = 0,
                Inicializacion = 0,
                Tiempoinicio = DateTime.Now,
                CursoId = cursoId,
                EstudianteId = estId,
                DesafioId = desafioId
            };
            _data.Add<Calificacion>(model);
            bool res = await _data.SaveAllAsync();
            if (!res)
                return BadRequest();

            return RedirectToAction("DownloadEscenario", "File"
                , new { desafioId = desafioId });

        }
    }
}