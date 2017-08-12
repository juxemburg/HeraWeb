using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Services.ApplicationServices;
using Hera.Services.UserServices;

namespace Hera.Controllers.ControllersMvc
{    
    [Route("/Profesor/Cursos/[action]")]
    [Authorize(Roles = "Profesor")]
    public class ProfesorCursosController : Controller
    {
        private readonly ProfesorService _ctrlService;
        private readonly UserService _usrService;

        public ProfesorCursosController(UserService usrService, 
            ProfesorService ctrlService)
        {
            _usrService = usrService;
            _ctrlService = ctrlService;
        }

        [HttpGet]
        [Route("/Profesor/Cursos/")]
        public async Task<IActionResult> Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var profId = _usrService.Get_ProfesorId(User.Claims);
            var model = await _ctrlService.GetAll_Cursos(profId, searchString,
                skip, take);
            this.GetAlerts();
            return View(model);
        }

        

        
        
    }
}