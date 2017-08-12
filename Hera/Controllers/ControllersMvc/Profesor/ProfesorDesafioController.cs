using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Hera.Services.UserServices;
using Hera.Models.UtilityViewModels;
using Hera.Models.EntitiesViewModels.Desafios;
using Hera.Services.ApplicationServices;
using Microsoft.EntityFrameworkCore;

namespace Hera.Controllers.ControllersMvc.Profesor
{
    [Authorize(Roles="Profesor")]
    [Route("/Profesor/Desafios")]
    public class ProfesorDesafioController : Controller
    {
        private readonly UserService _usrService;
        private readonly ProfesorService _ctrlService;

        public ProfesorDesafioController(UserService usrService,
            ProfesorService ctrlService)
        {
            _usrService = usrService;
            _ctrlService = ctrlService;
        }

        public async Task<IActionResult> Index(
            SearchDesafioViewModel searchModel,
            int skip = 0, int take = 10)
        {
            var profId = _usrService.Get_ProfesorId(User.Claims);
            var model = await _ctrlService.GetAll_Desafios(profId, searchModel,
                skip, take);
            this.GetAlerts();
            return View(model);
        }
        

        
    }
}