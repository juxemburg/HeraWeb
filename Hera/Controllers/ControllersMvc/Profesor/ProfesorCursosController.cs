using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Hera.Models.UtilityViewModels;
using Entities.Cursos;

namespace Hera.Controllers.ControllersMvc
{    
    [Route("/Profesor/Cursos/[action]")]
    [Authorize(Roles = "Profesor")]
    public class ProfesorCursosController : Controller
    {
        private IDataAccess _data;

        public ProfesorCursosController(IDataAccess data)
        {
            _data = data;
        }

        [HttpGet]
        [Route("/Profesor/Cursos/")]
        public async Task<IActionResult> Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));

            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos(profId) :
                _data.Autocomplete_Cursos(searchString, profId);

            return View(new PaginationViewModel<Curso>(model, skip, take));
        }

        

        
        
    }
}