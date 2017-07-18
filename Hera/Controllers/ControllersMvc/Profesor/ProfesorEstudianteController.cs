using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Hera.Models.EntitiesViewModels;
using Entities.Calificaciones;
using Hera.Models.EntitiesViewModels.EstudianteDesafio;
using Hera.Services.UserServices;
using Microsoft.AspNetCore.Authorization;

namespace Hera.Controllers.ControllersMvc.Profesor
{
    [Authorize(Roles ="Profesor")]
    [Route("/Profesor/Curso/{idCurso:int}/Estudiante/{idEstudiante:int}/Desafio/{idDesafio:int}/[action]")]
    public class ProfesorEstudianteController : Controller
    {
        private IDataAccess _data;
        private UserService _usrService;

        public ProfesorEstudianteController(IDataAccess data,
            UserService usrService)
        {
            _usrService = usrService;
            _data = data;
        }

        [HttpGet]
        public async Task<IActionResult> Calificar(int idCurso,
            int idEstudiante, int idDesafio)
        {
            var idProf = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));

            var model = await _data.Find_RegistroCalificacion(
                idCurso, idEstudiante,
                idDesafio, idProf);
            var desafio = await _data.Find_Desafio(idDesafio);

            if (model == null || desafio == null)
            {
                return NotFound();
            }
            
            var resultModel = new CalificacionCualitativaViewModel(
                model,
                desafio.Nombre);

            var formModel = await _data
                .Find_CalificacionCualitativa(idEstudiante,
                idCurso, idDesafio);
            if(formModel != null)
            {
                resultModel.FormModel =
                new CreateCalificacionCualitativaViewModel(formModel);
                resultModel.Calificado = true;
            }
            Console.WriteLine($"Calificaciones: {resultModel.Registro.Calificaciones.Count()}");
            return View(resultModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calificar(
            CreateCalificacionCualitativaViewModel model)
        {
            if (ModelState.IsValid)
            {
                _data.Add<CalificacionCualitativa>(model.Map());
                var res = await _data.SaveAllAsync();
                if (!res)
                    ModelState.AddModelError("", "Error al insertar " +
                        "la calificación");
            }
            return RedirectToAction("Calificar",
                new
                {
                    idCurso = model.CursoId,
                    idEstudiante = model.EstudianteId,
                    idDesafio = model.DesafioId
                });
        }

        [HttpGet]
        public async Task<IActionResult> EvaluacionCompleta(int idCurso,
            int idEstudiante, int idDesafio, int idCalificacion)
        {
            var cal = await _data.Find_Calificacion(idCalificacion);
            if(cal != null)
            {
                var model = new ResultadosScratchViewModel(cal.Resultados);
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCalificar(
            CreateCalificacionCualitativaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = await _data
                    .Find_CalificacionCualitativa(model.Id.Value);

                entity.Completada = model.Completada;
                entity.Descripcion = model.Descripcion;

                _data.Edit<CalificacionCualitativa>(entity);

                var res = await _data.SaveAllAsync();
                if (!res)
                    ModelState.AddModelError("", "Error al editar " +
                        "la calificación");
            }
            return RedirectToAction("Calificar",
                new
                {
                    idCurso = model.CursoId,
                    idEstudiante = model.EstudianteId,
                    idDesafio = model.DesafioId
                });
        }
    }
}