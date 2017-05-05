using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Hera.Models.EntitiesViewModels;
using Entities.Calificaciones;

namespace Hera.Controllers.ControllersMvc.Profesor
{

    [Route("/Profesor/Curso/{idCurso:int}/Estudiante/{idEstudiante:int}/Desafio/{idDesafio:int}/[action]")]
    public class ProfesorEstudianteController : Controller
    {
        private IDataAccess _data;

        public ProfesorEstudianteController(IDataAccess data)
        {
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