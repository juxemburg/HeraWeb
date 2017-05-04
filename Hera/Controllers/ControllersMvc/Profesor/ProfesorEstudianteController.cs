using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Hera.Models.EntitiesViewModels;

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

        public async Task<IActionResult> Calificar(int idCurso,
            int idEstudiante, int idDesafio)
        {
            var idProf = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));

            var model = await _data.Find_RegistroCalificacion(
                idCurso,idEstudiante, 
                idDesafio, idProf);

            if(model == null)
            {
                return NotFound();
            }
            return View(new CalificacionCualitativaViewModel(model));
        }
    }
}