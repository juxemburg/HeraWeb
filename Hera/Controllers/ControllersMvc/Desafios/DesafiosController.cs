using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;
using Hera.Services.UserServices;
using Hera.Models.EntitiesViewModels.Desafios;
using Hera.Models.EntitiesViewModels.Ratings;
using Hera.Services.ApplicationServices;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
    [Route("[controller]/[action]")]
    public class DesafiosController : Controller
    {
        private readonly UserService _userService;
        private readonly DesafioService _ctrlService;

        public DesafiosController(UserService userService,
            DesafioService ctrlService)
        {
            _userService = userService;
            _ctrlService = ctrlService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            SearchDesafioViewModel searchModel, int skip = 0,
            int take = 10)
        {
            var profId = _userService.Get_ProfesorId(User.Claims);
            var model = await _ctrlService.GetAll_Desafios(profId,
                searchModel, skip, take);
            return View(model);
        }

        [HttpGet("{desafioId}")]
        public async Task<IActionResult> Details(int desafioId)
        {
            var model = await _ctrlService.Get_Desafio(desafioId);

            this.GetAlerts();
            return (model == null)
                ? (IActionResult)NotFound()
                : View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            this.GetAlerts();
            return View(new CreateDesafioViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateDesafioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var profId = _userService.Get_ProfesorId(User.Claims);
                    var res = await _ctrlService
                        .Create_Desafio(profId, model);
                    if (res)
                    {
                        this.SetAlerts("sucess-alerts",
                            "Desaf�o creado Correctamente");
                        return RedirectToAction("Index", "ProfesorDesafio");
                    }
                    this.SetAlerts("error-alerts",
                        "Error en la creaci�n del desaf�o");

                }
                catch (ApplicationServicesException e)
                {
                    this.SetAlerts("error-alerts", e.Message);
                }
            }
            return View(model);
        }


        [HttpPost("{desafioId}")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Rate(int desafioId,
            RateViewModel model)
        {
            var idProfesor = _userService.Get_ProfesorId(User.Claims);
            if (ModelState.IsValid)
            {
                var res = await
                    _ctrlService.Do_CalificarDesafio(idProfesor, desafioId,
                    model);
                if(res)
                    this.SetAlerts("success-alerts","se calific� el desaf�o exitosamente");
                else
                    this.SetAlerts("error-alerts", "error al calificar el desaf�o");
            }
            return RedirectToAction("Details",
                new { desafioId });
        }

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var profId = _userService.Get_ProfesorId(User.Claims);

                var res = await _ctrlService.Delete_Desafio(profId, id);
                if (res)
                    this.SetAlerts("success-alerts",
                        "El desaf�o se elimin� exitosamente");
                else
                    this.SetAlerts("error-alerts",
                        "El desaf�o no se puede eliminar," +
                        " revisa que no est� siendo usado previamente");
                return RedirectToAction("Index", "ProfesorDesafio");
            }
            catch (ApplicationServicesException )
            {
                return NotFound();
            }
        }
    }
}