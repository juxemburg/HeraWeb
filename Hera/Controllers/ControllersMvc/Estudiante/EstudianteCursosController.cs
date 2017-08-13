using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;
using Hera.Services.ApplicationServices;
using Hera.Services.UserServices;

namespace Hera.Controllers.ControllersMvc
{
    
    [Route("/Estudiante/Cursos/[action]")]
    [Authorize(Roles = "Estudiante")]
    public class EstudianteCursosController : Controller
    {
        private readonly UserService _usrService;
        private EstudianteService _ctrlService;

        public EstudianteCursosController(UserService usrService,
            EstudianteService ctrlService)
        {
            _ctrlService = ctrlService;
            _usrService = usrService;
        }

        [HttpGet]        
        public async Task<IActionResult> Busqueda(string searchString = "",
            int skip = 0, int take = 10)
        {
            var estId =  _usrService.Get_EstudianteId(User.Claims);
            var model = await _ctrlService
                .Search_Curso(estId, searchString, skip, take);
            this.GetAlerts();
            return View(model);
        }


        [Route("/Estudiante/Cursos/")]
        public async Task<IActionResult> Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var estudianteId = _usrService.Get_EstudianteId(User.Claims);
            var model = await _ctrlService.GetAll_Curso(estudianteId,
                searchString, skip, take);
            this.GetAlerts();
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddEstudiante(
            AddEstudianteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = _usrService.Get_EstudianteId(User.Claims);
                try
                {
                    var res = await _ctrlService
                        .Do_MatricularEstudiante(id, model);
                    if(res)
                        return RedirectToAction("Index");
                }
                catch (ApplicationServicesException e)
                {
                    this.SetAlerts("error-alerts", e.Message);
                }
            }
            return RedirectToAction("Busqueda");
        }


    }
}