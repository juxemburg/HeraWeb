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
using Hera.Services.ApplicationServices;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
    public class CursosController : Controller
    {
        private readonly IDataAccess _data;
        private readonly UserService _userService;
        private readonly ColorService _clrService;
        private readonly CursoService _ctrlService;

        public CursosController(IDataAccess data,
            UserService userService,
            ColorService clrService,
            CursoService ctrlService)
        {
            _clrService = clrService;
            _data = data;
            _userService = userService;
            _ctrlService = ctrlService;
        }




        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await _ctrlService.Get_Curso(id);
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
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
                    var res = await _ctrlService.Create_Curso(profId, model);
                    if (res)
                    {
                        this.SetAlerts("success-alerts",
                            "El curso se cre� exitosamente");
                        return RedirectToAction("Cursos", "Profesor");
                    }
                }
                catch (Exception e)
                {

                }
            }
            ModelState.AddModelError("", "Error en la creaci�n del curso");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var profId = _userService.Get_ProfesorId(User.Claims);

            var res = await _ctrlService.Delete_Curso(profId, id);
            if (res)
                this.SetAlerts("success-alerts",
                    "El curso se elimin� exitosamente");
            else
                this.SetAlerts("error-alerts",
                    "El curso no se pudo eliminar");


            return RedirectToAction("Index", "ProfesorCursos");
        }



        [HttpPost]
        public async Task<IActionResult> AddDesafio(AddDesafioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profId = _userService.Get_ProfesorId(User.Claims);
                    var res = await _ctrlService
                        .Add_DesafioCurso(profId, model);

                    if (res)
                        this.SetAlerts("success-alerts",
                            "El desaf�o se agreg� exitosamente");
                    else
                        this.SetAlerts("error-alerts",
                            "No se pudo agregar el desaf�o");
                }
                catch (ApplicationServicesException e)
                {
                    this.SetAlerts("error-alerts", e.Message);
                }
            }
            return RedirectToAction("Details", "ProfesorCurso",
                new { idCurso = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveDesafio(int desafioId,
            int cursoId)
        {
            try
            {
                var profId = _userService.Get_ProfesorId(User.Claims);
                var res = await _ctrlService.Remove_DesafioCurso(profId,
                    desafioId, cursoId);
                if (res)
                    this.SetAlerts("success-alerts",
                        "El desaf�o se removi� exitosamente");
            }
            catch (Exception)
            {
                this.SetAlerts("error-alerts",
                    "Error, no se pudo remover el desaf�o");
            }
            return RedirectToAction("Details", "ProfesorCurso",
                new { idCurso = cursoId });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ChangeStarter(
            ChangeStarterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var profId = _userService.Get_ProfesorId(User.Claims);
                    var res = await _ctrlService
                        .Edit_DesafioInicial(profId, model);
                    if (res)
                        this.SetAlerts("success-alerts", "Desaf�o inicial " +
                                                         "cambiado exitosamente");
                }
                else
                    this.SetAlerts("error-alerts", "No es posible" +
                                                   " cambiar el desaf�o");
            }
            catch (ApplicationServicesException e)
            {
                this.SetAlerts("error-alerts", e.Message);
            }
            return RedirectToAction("Details",
                "ProfesorCurso", new { idCurso = model.CursoId });
        }
    }
}

