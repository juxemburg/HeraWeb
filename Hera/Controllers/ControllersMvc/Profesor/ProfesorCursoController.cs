using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;
using Hera.Models.EntitiesViewModels.EstudianteDesafio;
using Microsoft.EntityFrameworkCore;
using Hera.Models.EntitiesViewModels.ProfesorCursos;
using Hera.Services.ApplicationServices;
using Hera.Services.UserServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hera.Controllers.ControllersMvc.Profesor
{
    [Route("/Profesor/Curso/{idCurso:int}/[action]")]
    [Authorize(Roles = "Profesor")]
    public class ProfesorCursoController : Controller
    {
        private readonly IDataAccess _data;
        private readonly UserService _usrService;
        private readonly ProfesorService _ctrlService;

        public ProfesorCursoController(IDataAccess data,
            UserService userService, ProfesorService ctrlService)
        {
            _ctrlService = ctrlService;
            _usrService = userService;
            _data = data;
        }

        [HttpGet]
        [Route("/Profesor/Curso/{idCurso:int}")]
        public async Task<IActionResult> Details(int idCurso)
        {
            try
            {
                var profId = await _data.Find_ProfesorId(
                    _data.Get_UserId(User.Claims));

                var model = await _ctrlService.Get_Curso(profId, idCurso);

                ViewData["select-desafios"] =
                    await _ctrlService
                    .GetAll_DesafiosSelectList(profId, idCurso);

                this.GetAlerts();
                return View(model);
            }
            catch (ApplicationServicesException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Desafio(int idCurso, int idDesafio)
        {
            var idProfesor = _usrService.Get_ProfesorId(User.Claims);
            try
            {

                var model =
                    await _ctrlService.Get_Desafio(idProfesor, idCurso, idDesafio);
                return View(model);
            }
            catch (ApplicationServicesException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        [HttpGet("{idEstudiante:int}")]
        public async Task<IActionResult> Estudiante(int idCurso,
            int idEstudiante)
        {
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));

            try
            {
                var model = await _ctrlService.Get_Estudiante(profId,
                    idCurso, idEstudiante);

                return View(model);
            }
            catch (ApplicationServicesException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

    }
}