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
using Hera.Services.UserServices;

namespace Hera.Controllers.ControllersMvc
{
    
    [Route("/Estudiante/Cursos/[action]")]
    [Authorize(Roles = "Estudiante")]
    public class EstudianteCursosController : Controller
    {
        private IDataAccess _data;
        private UserService _usrService;

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

        
    }
}