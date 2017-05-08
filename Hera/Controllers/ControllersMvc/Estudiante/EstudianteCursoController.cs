using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Entities.Calificaciones;
using Hera.Services;
using Hera.Services.ScratchServices;
using Hera.Models.EntitiesViewModels.EstudianteDesafio;

namespace Hera.Controllers.ControllersMvc
{
    [Route("/Estudiante/Curso/{idCurso:int}/[action]")]
    public class EstudianteCursoController : Controller
    {
        private IDataAccess _data;
        private ScratchService _evaluator;
        

        public EstudianteCursoController(IDataAccess data,
            ScratchService scratchService)
        {
            _evaluator = scratchService;
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
            return View(new CalificacionDesafioViewModel(model));
        }

        [HttpPost("{idDesafio:int}/Calificar")]
        public async Task<IActionResult> CalificarDesafio(
            int idCurso, int idDesafio, string projId)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));

            var model = await _data.Find_RegistroCalificacion(
                idCurso, estId,idDesafio);

            if (model != null && model.Iniciada)
            {
                var cal = model.CalificacionPendiente;
                cal.TerminarCalificacion(projId);
                var res = await _evaluator.Get_Evaluation(projId);

                var resultados = res.Select(val => val.Map(cal.Id))
                    .ToList();                
                
                _data.AddRange_ResultadoScratch(resultados);                
                _data.Edit<Calificacion>(cal);
                await _data.SaveAllAsync();
            }
            return RedirectToAction("Desafio",
                new { idCurso = idCurso, idDesafio = idDesafio });
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