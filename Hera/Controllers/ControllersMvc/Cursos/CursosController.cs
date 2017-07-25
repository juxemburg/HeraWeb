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
using Hera.Models.EntitiesViewModels.ProfesorCursos;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
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
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCursoViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var profId = _userService.Get_ProfesorId(User.Claims);
                    var desafio = await _data.Find_Desafio(model.DesafioId.GetValueOrDefault());
                    if (desafio != null)
                    {
                        _data.AddCurso(model.Map(profId, desafio,
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var profId = _userService.Get_ProfesorId(User.Claims);
            if (await _data.Exist_Profesor_Curso(profId, id))
            {
                await _data.Delete_Curso(id);
                var res = await _data.SaveAllAsync();
                if (res)
                    this.SetAlerts("success-alerts",
                        "El curso se eliminó exitosamente");
            }
            else
                this.SetAlerts("error-alerts",
                    "El curso no se pudo eliminar");

            return RedirectToAction("Index", "ProfesorCursos");

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddEstudiante(
            AddEstudianteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = _data.Get_UserId(User.Claims);
                try
                {
                    var curso = await _data.Find_Curso(model.CursoId);
                    var estudiante = await _data.Find_Estudiante(id);
                    _data.Do_MatricularEstudiante(curso, estudiante,
                        model.Map(curso.Id, estudiante.Id), model.Password);

                    if (await _data.SaveAllAsync())
                        return RedirectToAction("Index", "EstudianteCursos");
                    else
                        this.SetAlerts("error-alerts",
                            "Lo sentimos, la contraseña es inválida");
                }
                catch (Exception) {
                }
            }
            return RedirectToAction("Busqueda","EstudianteCursos");
        }
        
        [HttpPost]
        public async Task<IActionResult> AddDesafio(AddDesafioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!(await
                        _data.Exist_Desafio(model.DesafioId, model.Id)))
                    {
                        var desafio =
                        await _data.Find_Desafio(model.DesafioId);
                        _data.AddDesafio(model.Id, desafio);
                        var res = await _data.SaveAllAsync();
                        if (res)
                            this.SetAlerts("success-alerts",
                                "El desafío se agregó exitosamente");
                    }
                    else
                        this.SetAlerts("error-alerts",
                            "El desafío ya se encuntra en el curso!");
                }
                catch (Exception) { }
            }
            return RedirectToAction("Details","ProfesorCurso", new { idCurso = model.Id });
        }

        [HttpPost]
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
            return RedirectToAction("Details", "ProfesorCurso",
                new { idCurso = cursoId });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangeStarter(
            ChangeStarterViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _data.ChangeStarterDesafio(model.CursoId,
                    model.OldStarterId, model.NewStarterId);
                var res = await _data.SaveAllAsync();
                if (res)
                    this.SetAlerts("success-alerts", "Desafío inicial " +
                        "cambiado exitosamente");
            }
            else
                this.SetAlerts("error-alerts", "No es posible" +
                    " cambiar el desafío");
            return RedirectToAction("Details",
                "ProfesorCurso", new { idCurso = model.CursoId });
        }
    }
}
