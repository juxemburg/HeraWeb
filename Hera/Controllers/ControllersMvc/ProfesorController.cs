using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Hera.Models.UtilityViewModels;
using Entities.Cursos;
using Microsoft.AspNetCore.Authorization;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles ="Profesor")]
    public class ProfesorController : Controller
    {
        private IDataAccess _data;

        public ProfesorController(IDataAccess  data)
        {
            _data = data;
        }


        [HttpGet]
        public async Task<IActionResult> Cursos(string searchString = "",
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