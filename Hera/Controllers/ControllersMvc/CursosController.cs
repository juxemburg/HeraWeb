using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;
using Entities.Cursos;

using Hera.Models.UtilityViewModels;
using Microsoft.EntityFrameworkCore;
using Hera.Services.UserServices;
using Hera.Services.UtilServices;

namespace Hera.Controllers.ControllersMvc
{

    public class CursosController : Controller
    {
        private IDataAccess _data;
        private UserService _userService;
        private ColorService _clrService;

        public CursosController(IDataAccess data,
            UserService userService,
            ColorService clrService)
        {
            _clrService = clrService;
            _data = data;
            _userService = userService;
        }

        
        [HttpGet]
        public IActionResult Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var model = (string.IsNullOrWhiteSpace(searchString))
                ? _data.GetAll_Cursos() :
                _data.Autocomplete_Cursos(searchString);

            return View(new PaginationViewModel<Curso>(model, skip, take));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _data.Find_Curso_Public(id);
            if(model != null)
            {
                return View(model);
            }
            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles ="Profesor")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [Authorize(Roles = "Profesor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCursoViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var profId = 
                        await _userService.Get_ProfesorId(User.Claims);
                    var desafio = await _data.Find_Desafio(model.DesafioId.GetValueOrDefault());
                    if (desafio != null)
                    {
                        _data.Add<Curso>(model.Map(profId, desafio,
                            _clrService.RandomColor));
                        var res = await _data.SaveAllAsync();
                        if (res)
                        {
                            this.SetAlerts("success-alerts", "El curso se creó exitosamente");
                            return RedirectToAction("Cursos", "Profesor");
                        }
                            
                    }
                }
                catch (Exception e)
                {

                }
            }
            ModelState.AddModelError("", "Error en la creación del curso");
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Estudiante")]
        public async Task<IActionResult> AddEstudiante(
            AddEstudianteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = _data.Get_UserId(User.Claims);
                var estudianteId = await _data.Find_EstudianteId(id);
                try
                {
                    var curso = await _data.Find_Curso(model.CursoId);
                    if (curso.Password.Equals(model.Password))
                    {
                        _data.Add<Rel_CursoEstudiantes>(model.Map(model.CursoId, estudianteId));
                        await _data.SaveAllAsync();
                        return RedirectToAction("Index", "EstudianteCursos");
                    }
                    else
                        this.SetAlerts("error-alerts", "Lo sentimos, la contraseña es inválida");
                }
                catch (Exception) {
                }
            }
            return RedirectToAction("Busqueda","EstudianteCursos");
        }
        
        [HttpPost]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> AddDesafio(AddDesafioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var desafio = (model.DesafioId != null) ?
                    await _data.Find_Desafio(
                        model.DesafioId.GetValueOrDefault()) :
                    model.Map();

                    if (model.DesafioId == null)
                        _data.AddDesafio(desafio);

                    _data.AddDesafio(model.Id, desafio);
                    var res = await _data.SaveAllAsync();
                    if(res)
                        this.SetAlerts("success-alerts", "El desafío se agregó exitosamente");
                }
                catch (Exception) { }
            }
            return RedirectToAction("Details","ProfesorCurso", new { idCurso = model.Id });
        }

        [HttpPost]
        [Authorize(Roles ="Profesor")]
        public async Task<IActionResult> RemoveDesafio(int desafioId,
            int cursoId)
        {
            try
            {
                if(await _data.Exist_Desafio(desafioId, cursoId))
                {
                    await _data.Delete_Desafio(cursoId, desafioId);
                    var res = await _data.SaveAllAsync();
                    if(res)
                        this.SetAlerts("success-alerts", "El desafío se removió exitosamente");
                }
            }
            catch (Exception) { }
            return RedirectToAction("Details", "ProfesorCurso", new { idCurso = cursoId });
        }


    }
}