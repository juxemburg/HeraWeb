using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;
using Microsoft.EntityFrameworkCore;
using Hera.Models.EntitiesViewModels.ProfesorCursos;

namespace Hera.Controllers.ControllersMvc.Profesor
{
    [Route("/Profesor/Curso/{idCurso:int}/[action]")]
    [Authorize(Roles = "Profesor")]
    public class ProfesorCursoController : Controller
    {
        private IDataAccess _data;

        public ProfesorCursoController(IDataAccess data)
        {
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

            
            
            
            return View(new ProfesorCursoViewModel(model, registros));
        }

        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Desafio(int idCurso, int idDesafio)
        {
            var curso = await _data.Find_Curso(idCurso);
            var model = await _data.Find_Desafio(idDesafio);
            if (model == null || curso == null
                || curso.ContieneDesafio(idDesafio))
            {
                return NotFound();
            }
            return View(model);
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
        [HttpGet("{idDesafio:int}")]
        public async Task<IActionResult> Progres(int idCurso,
        int idDesafio)
        {
            var profId = await _data.Find_ProfesorId(
                _data.Get_UserId(User.Claims));
            if (!(await _data.Exist_Profesor_Curso(profId, idCurso))
                && await _data.Exist_Desafio(idDesafio, idCurso))
            {
                return NotFound();
            }
            var calificaciones = _data
                .GetAll_RegistroCalificacion(idCurso, null, idDesafio);

            var nuevoProgreso = new ProgresoDesafioViewModel()
            {
                estudiantesQueFinalizaron = await _data
                .Find_Estudiantes_Finalizaron(idDesafio, idCurso).ToListAsync(),

                estudiantesQueNoFinalizaron = await _data
                .Find_Estudiantes_No_Finalizaron(idDesafio, idCurso).ToListAsync(),

                promedioTiempos = (calificaciones.Count() > 0) ? calificaciones
                .Select(c => c.Calificaciones.Average(c1 => c1.Duracion.TotalSeconds))
                .Average()
                : 0
            };
            return View("Progres", nuevoProgreso);
        }
    }
}