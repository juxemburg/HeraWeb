using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Services.ApplicationServices;

namespace Hera.Controllers.ControllersMvc.Admin
{


    [Authorize(Roles = "Admin")]
    [Route("/Admin/Profesores/[action]")]
    public class AdminProfesoresController : Controller
    {
        private readonly AdminService _ctrlService;

        public AdminProfesoresController(AdminService adminService)
        {
            _ctrlService = adminService;
        }

        [HttpGet("/Admin/Profesores")]
        public async Task<IActionResult> Index(string searchString = "",
            int skip = 0, int take = 10)
        {
            var model = await _ctrlService
                .Get_Profesores(searchString, skip, take);
            return View(model);
        }
    }
}