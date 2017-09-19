using System.Threading.Tasks;
using Entities.Calificaciones;
using Microsoft.AspNetCore.Mvc;
using Hera.Models.EntitiesViewModels.ProfesorEstudiante;
using Hera.Services.ApplicationServices;
using Hera.Services.UserServices;
using Microsoft.AspNetCore.Authorization;

namespace Hera.Controllers.ControllersMvc.Profesor
{
    [Authorize(Roles = "Profesor")]
    [Route("/Profesor/Curso/{idCurso:int}/Estudiante/{idEstudiante:int}/Desafio/{idDesafio:int}/[action]")]
    public class ProfesorEstudianteController : Controller
    {
        private readonly UserService _usrService;
        private readonly ProfesorService _ctrlService;

        public ProfesorEstudianteController(UserService usrService,
            ProfesorService ctrlService)
        {
            _usrService = usrService;
            _ctrlService = ctrlService;
        }

        [HttpGet]
        public async Task<IActionResult> Calificar(int idCurso,
            int idEstudiante, int idDesafio)
        {
            var idProf = _usrService.Get_ProfesorId(User.Claims);


            var model = await _ctrlService.Get_CalificacionModel(
                idProf, idCurso, idEstudiante, idDesafio);

            if (model == null)
                return NotFound();

            this.GetAlerts();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calificar(int idCurso, 
            int idEstudiante, int idDesafio, CalificarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var profId = _usrService.Get_ProfesorId(User.Claims);
                var res = await _ctrlService.Do_Calificar(profId, idCurso,
                    idEstudiante, model);
                if (!res)
                    this.SetAlerts("error-alerts", "Error al insertar " +
                                                   "la calificación");
                    
            }
            return RedirectToAction("Calificar",
                new
                {
                    idCurso,
                    idEstudiante,
                    idDesafio
                });
        }

        [HttpGet]
        public async Task<IActionResult> EvaluacionCompleta(int idCurso,
            int idEstudiante, int idDesafio, int idCalificacion)
        {
            var profId = _usrService.Get_ProfesorId(User.Claims);
            var model = await _ctrlService.Get_EvaluacionModel(profId,
                idCurso, idEstudiante, idDesafio, idCalificacion);

            return model == null ? (IActionResult) NotFound() 
                : View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCalificar(int idCurso,
            int idEstudiante, int idDesafio, CalificacionCualitativa model)
        {
            if (ModelState.IsValid)
            {
                var profId = _usrService.Get_ProfesorId(User.Claims);
                var res = await _ctrlService
                    .Do_EditCalificar(profId, idCurso, idEstudiante,
                     idDesafio, model);
                if (!res)
                    this.SetAlerts("error-alerts","Error al editar" +
                                                  " la calificación");
            }
            return RedirectToAction("Calificar",
                new
                {
                    idCurso,
                    idEstudiante,
                    idDesafio
                });
        }
    }


}