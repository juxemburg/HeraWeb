using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Microsoft.EntityFrameworkCore;
using Hera.Models.UtilityViewModels;
using Entities.Cursos;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Estudiante")]
    public class EstudianteController : Controller
    {
        private IDataAccess _data;

        public EstudianteController(IDataAccess data)
        {
            _data = data;
        }
        [HttpGet]
        public async Task<IActionResult> Cursos()
        {
            var estudianteId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var model = _data.GetAll_CursosEstudiante(estudianteId);

            return View(new PaginationViewModel<Curso>
                (await model.ToListAsync(), 0, 10));
        }

        [HttpGet]
        public async Task<IActionResult> Curso(int id)
        {
            var estId
                = await _data.Find_EstudianteId(
                    _data.Get_UserId(User.Claims));
            var model = await _data.Find_Curso(id);
            if (model == null || !model.ContieneEstudiante(estId))
                return NotFound();

            return View(model);
        }
    }
}