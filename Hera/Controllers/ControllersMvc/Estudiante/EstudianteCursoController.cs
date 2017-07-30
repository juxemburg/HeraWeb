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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels.EstudianteCurso;
using Hera.Services.DesafiosServices;
using Hera.Services.UserServices;
using HeraScratch.Exceptions;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Estudiante")]
    [Route("/Estudiante/Curso/{idCurso:int}/[action]")]
    public class EstudianteCursoController : Controller
    {
        private IDataAccess _data;
        private ScratchService _evaluator;
        private DesafioService _desafioService;
        private UserService _usrService;

        public EstudianteCursoController(IDataAccess data,
            ScratchService scratchService,
            DesafioService desafioService,
            UserService usrService)
        {
            _usrService = usrService;
            _desafioService = desafioService;
            _evaluator = scratchService;
            _data = data;
        }

        [HttpGet]
        [Route("/Estudiante/Curso/{idCurso:int}")]
        public async Task<IActionResult> Details(int idCurso)
        {
            var estId = _usrService.Get_EstudianteId(User.Claims);

            if (await _data.Exist_Estudiante_Curso(estId, idCurso))
            {
                var model = await _desafioService.Get_RelEstudianteCurso(
                    idCurso, estId);
                return View(model);
            }



            return NotFound();
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Desafio(int idCurso,
            int idDesafio)
        {
            var estId = _usrService.Get_EstudianteId(User.Claims);
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
            this.GetAlerts();
            return View(new CalificacionDesafioViewModel(model));
        }


        //[HttpGet]
        //[Route("/Estudiante/Curso/{idCurso:int}/Desafio/" +
        //    "{idDesafio:int}/Resultado/{idCalificacion:int}")]        
        //public async Task<IActionResult>Resultados(int idCurso,
        //    int idCalificacion, int idDesafio)
        //{
        //    var estId = await _data.Find_EstudianteId(
        //            _data.Get_UserId(User.Claims));
        //    if(await _data.Exist_Desafio(idDesafio, idCurso) &&
        //        await _data.Exist_Estudiante_Curso(estId, idCurso))
        //    {
        //        var model =await _data.Find_ResultadoScratchGeneral(
        //            idCalificacion);
        //        return View(new ResultadoGeneralViewModel(model));
        //    }
        //    return NotFound("asdad");
        //}

        [HttpPost("{idDesafio:int}/Calificar")]
        public async Task<IActionResult> CalificarDesafio(
            int idCurso, int idDesafio, string projId)
        {
            var estId = _usrService.Get_EstudianteId(User.Claims);

            var model = await _data.Find_RegistroCalificacion(
                idCurso, estId, idDesafio);

            try
            {
                if (model != null && model.Iniciada)
                {

                    var cal = model.CalificacionPendiente;
                    var res = await _evaluator.Get_Evaluation(projId);
                    var est = await _data.Find_Estudiante(estId);
                    var curso = await _data.Find_Curso(idCurso);
                    var resultados = res.Select(val => val.Map(cal.Id))
                        .ToList();

                    _data.Do_TerminarCalificacion(curso,
                        est, cal, resultados, projId);

                    var result = await _data.SaveAllAsync();
                    if (result)
                        return RedirectToAction("DesafioCompletado",
                        new
                        {
                            idCurso = idCurso,
                            idDesafio = idDesafio,
                            idCalificacion = cal.Id
                        });
                }
            }
            catch (EvaluationException)
            {
                this.SetAlerts("error-alerts", "id de desafío no válido");
            }
            return RedirectToAction("Desafio",
                new
                {
                    idCurso = idCurso,
                    idDesafio = idDesafio
                });

        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> DesafioCompletado(int idCurso,
            int idDesafio, int idCalificacion)
        {
            var idEstudiante = _usrService.Get_EstudianteId(User.Claims);

            if (await _data.Exist_Estudiante_Curso(idEstudiante, idCurso))
            {
                var desafio = await _desafioService
                    .Get_SiguienteDesafio(idCurso, idEstudiante);

                var resultado = await _data
                    .Find_ResultadoScratchGeneral(idCalificacion);

                return View(
                    new DesafioCompletadoViewModel(idCurso,
                    resultado, desafio));
            }
            return NotFound();
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> IniciarDesafio(
            int idCurso, int idDesafio)
        {
            var estId = _usrService.Get_EstudianteId(User.Claims);

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


            return RedirectToAction("Desafio"
                , new { idDesafio = idDesafio, idCurso = idCurso });

        }
    }
}