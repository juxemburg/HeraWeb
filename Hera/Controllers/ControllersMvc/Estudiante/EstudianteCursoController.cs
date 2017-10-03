using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Services.ApplicationServices;
using Hera.Services.UserServices;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Estudiante")]
    [Route("/Estudiante/Curso/{idCurso:int}/[action]")]
    public class EstudianteCursoController : Controller
    {
        private readonly UserService _usrService;
        private readonly EstudianteService _ctrlService;

        public EstudianteCursoController(UserService usrService,
            EstudianteService ctrlService)
        {
            _usrService = usrService;
            _ctrlService = ctrlService;
        }

        [HttpGet]
        [Route("/Estudiante/Curso/{idCurso:int}")]
        public async Task<IActionResult> Details(int idCurso)
        {
            try
            {
                var estId = _usrService.Get_EstudianteId(User.Claims);
                var model = await _ctrlService.Get_Curso(estId, idCurso);
                return View(model);
            }
            catch (ApplicationServicesException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }




        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Desafio(int idCurso,
            int idDesafio)
        {
            try
            {
                var estId = _usrService.Get_EstudianteId(User.Claims);
                var model = await _ctrlService
                    .Get_Desafio(estId, idCurso, idDesafio);
                this.GetAlerts();
                return View(model);
            }
            catch (ApplicationServicesException)
            {
                return NotFound();
            }
        }

        [HttpPost("{idDesafio:int}/Calificar")]
        public async Task<IActionResult> CalificarDesafio(
            int idCurso, int idDesafio, string projId)
        {
            try
            {
                var estId = _usrService.Get_EstudianteId(User.Claims);
                var res = await _ctrlService
                    .Do_CalificarDesafio(estId, idCurso, idDesafio,
                        projId);
                if (res > 0)
                    return RedirectToAction("DesafioCompletado",
                        new
                        {
                            idCurso,
                            idDesafio,
                            idCalificacion = res
                        });                
            }
            catch (ApplicationServicesException e)
            {
                this.SetAlerts("error-alerts", e.Message);
            }
            return RedirectToAction("Desafio",
                new
                {
                    idCurso,
                    idDesafio
                });

        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> DesafioCompletado(int idCurso,
            int idDesafio, int idCalificacion)
        {
            var idEstudiante = _usrService.Get_EstudianteId(User.Claims);
            var cursoTerminado = await _ctrlService.Esta_Finalizado_Curso(idEstudiante, idCurso);
            var model = await _ctrlService.Get_DesafioCompletadoViewModel(
                idEstudiante, idCurso, idDesafio, idCalificacion);
            if (cursoTerminado.Estado)
            {
                return View("~/Views/EstudianteCurso/CursoCompletado.cshtml", cursoTerminado);
            }
            else {                
                this.SetAlerts("warning-alerts", cursoTerminado.Mensaje);
                this.GetAlerts();
                return (model == null)
                ? (IActionResult)NotFound()
                : View(model);
            }
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> IniciarDesafio(
            int idCurso, int idDesafio)
        {
            var estId = _usrService.Get_EstudianteId(User.Claims);

            var res = await _ctrlService
                .IniciarDesafio(estId, idCurso, idDesafio);

            if (!res)
                return BadRequest();

            return RedirectToAction("Desafio", new { idDesafio, idCurso });

        }
    }
}