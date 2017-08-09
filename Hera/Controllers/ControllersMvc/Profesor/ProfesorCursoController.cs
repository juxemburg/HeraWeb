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

        public ProfesorCursoController(IDataAccess data,
            UserService userService)
        {
            _usrService = userService;
            _data = data;
        }

        [HttpGet]
        [Route("/Profesor/Curso/{idCurso:int}")]
        public async Task<IActionResult> Details(int idCurso)
        {
            var model = await _data.Find_Curso(idCurso);
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));
            if (model == null || model.ProfesorId != profId)
            {
                return NotFound();
            }
            var registros =
                await _data.GetAll_RegistroCalificacion(idCurso)
                .GroupBy(reg => new
                {
                    reg.DesafioId,
                    reg.EstudianteId
                })
                .ToDictionaryAsync(reg => 
                new Tuple<int,int>(reg.Key.DesafioId, reg.Key.EstudianteId)
                , reg => reg.ToList());
            model.Desafios = model.Desafios
                .OrderByDescending(d => d.Initial)
                .ToList();
            ViewData["select-desafios"] =
                model.Desafios.Select(d =>
                new SelectListItem()
                {
                    Value = d.DesafioId.ToString(),
                    Text = d.Desafio.Nombre
                });

            this.GetAlerts();
            return View(new ProfesorCursoViewModel(model, registros));
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Desafio(int idCurso, int idDesafio)
        {
            var idProfesor = _usrService.Get_ProfesorId(User.Claims);

            if (!await _data.Exist_Desafio(idDesafio, idCurso, idProfesor))
                return NotFound();

            var desafio = await _data.Find_Desafio(idDesafio);
            var curso = await _data.Find_Curso(idCurso);
            return View(new DesafioCursoViewModel(desafio, curso));
        }

        [HttpGet("{idEstudiante:int}")]
        public async Task<IActionResult> Estudiante(int idCurso,
            int idEstudiante)
        {
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));
            var model = await _data.Find_Estudiante(idEstudiante,
                idCurso, profId);
            if (model == null)
            {
                return NotFound();
            }

            return View(new EstudianteCalificacionViewModel(model));
        }
        
    }
}