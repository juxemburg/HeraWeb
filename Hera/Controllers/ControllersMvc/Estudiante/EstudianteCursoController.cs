using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Entities.Calificaciones;

namespace Hera.Controllers.ControllersMvc
{
    [Route("/Estudiante/Curso/{idCurso:int}/[action]")]
    public class EstudianteCursoController : Controller
    {
        private IDataAccess _data;

        public EstudianteCursoController(IDataAccess data)
        {
            _data = data;
        }

        [HttpGet]
        [Route("/Estudiante/Curso/{idCurso:int}")]
        public async Task<IActionResult> Details(int idCurso)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var model = await _data.Find_Curso(idCurso);
            if (model == null || !model.ContieneEstudiante(estId))
                return NotFound();

            return View(model);
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Desafio(int idCurso,
            int idDesafio)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var curso = await _data.Find_Curso(idCurso);
            if (curso == null || !curso.ContieneEstudiante(estId)
                || !curso.ContieneDesafio(idDesafio))
                return NotFound();

            var model = await _data.Find_RegistroCalificacion(
                idCurso, estId, idDesafio);
            if (model == null)
            {
                model = new RegistroCalificacion()
                {
                    CursoId = idCurso,
                    DesafioId = idDesafio,
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

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> IniciarDesafio(
            int idCurso, int idDesafio)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));

            var model = new Calificacion()
            {
                BloquesRepetidos = 0,
                Inicializacion = 0,
                Tiempoinicio = DateTime.Now,
                CursoId = idCurso,
                EstudianteId = estId,
                DesafioId = idDesafio
            };
            _data.Add<Calificacion>(model);
            bool res = await _data.SaveAllAsync();
            if (!res)
                return BadRequest();

            return RedirectToAction("DownloadEscenario", "File"
                , new { desafioId = idDesafio });

        }
    }
}