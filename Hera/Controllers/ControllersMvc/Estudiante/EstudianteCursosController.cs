using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Hera.Models.UtilityViewModels;
using Entities.Cursos;
using Microsoft.EntityFrameworkCore;
using Entities.Calificaciones;
using Hera.Models.EntitiesViewModels;
using Hera.Services.UserServices;

namespace Hera.Controllers.ControllersMvc
{
    
    [Route("/Estudiante/Cursos/[action]")]
    [Authorize(Roles = "Estudiante")]
    public class EstudianteCursosController : Controller
    {
        private readonly IDataAccess _data;
        private readonly UserService _usrService;

        public EstudianteCursosController(IDataAccess data,
            UserService usrService)
        {
            _usrService = usrService;
            _data = data;
        }

        [HttpGet]        
        public async Task<IActionResult> Busqueda(string searchString = "",
            int skip = 0, int take = 10)
        {
            var estId =  _usrService.Get_EstudianteId(User.Claims);
            var model = _data.GetAll_CursosEstudiante(estId, 
                searchString, true);
            this.GetAlerts();
            return View(new PaginationViewModel<Curso>(model, skip, take));
        }


        [Route("/Estudiante/Cursos/")]
        public async Task<IActionResult> Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var estudianteId = _usrService.Get_EstudianteId(User.Claims);
            var model = _data.GetAll_CursosEstudiante(estudianteId,
                searchString);
            this.GetAlerts();
            return View(new PaginationViewModel<Curso>
                (await model.ToListAsync(), 0, 10));
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddEstudiante(
            AddEstudianteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = _usrService.Get_EstudianteId(User.Claims);
                try
                {
                    var curso = await _data.Find_Curso(model.CursoId);
                    var estudiante = await _data.Find_Estudiante(id);
                    _data.Do_MatricularEstudiante(curso, estudiante,
                        model.Map(curso.Id, estudiante.Id), model.Password);

                    if (await _data.SaveAllAsync())
                        return RedirectToAction("Index");
                    else
                        this.SetAlerts("error-alerts",
                            "Lo sentimos, la contraseña es inválida");
                }
                catch (Exception)
                {
                    this.SetAlerts("error-alerts",
                        "Error en el proceso de matrícula del curso");
                }
            }
            return RedirectToAction("Busqueda");
        }


    }
}