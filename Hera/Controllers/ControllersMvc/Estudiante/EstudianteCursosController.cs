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

namespace Hera.Controllers.ControllersMvc
{
    
    [Route("/Estudiante/Cursos/[action]")]
    [Authorize(Roles = "Estudiante")]
    public class EstudianteCursosController : Controller
    {
        private IDataAccess _data;

        public EstudianteCursosController(IDataAccess data)
        {
            _data = data;
        }

        [HttpGet]        
        public IActionResult Busqueda(string searchString = "",
            int skip = 0, int take = 10)
        {
            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos() :
                _data.Autocomplete_Cursos(searchString);

            return View(new PaginationViewModel<Curso>(model, skip, take));
        }


        [Route("/Estudiante/Cursos/")]
        public async Task<IActionResult> Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var estudianteId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var model = _data.GetAll_CursosEstudiante(estudianteId);

            return View(new PaginationViewModel<Curso>
                (await model.ToListAsync(), 0, 10));
        }

        
    }
}