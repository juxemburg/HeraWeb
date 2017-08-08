using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Services.UserServices;
using Hera.Services;

namespace Hera.Controllers.ControllersApi
{
    [Produces("application/json")]
    [Route("api/Notifications")]
    [Authorize]
    public class NotificationsController : Controller
    {
        private readonly UserService _usrService;
        private readonly NotificationService _ns;

        public NotificationsController(UserService usrService,
            NotificationService ns)
        {
            _usrService = usrService;
            _ns = ns;
        }


        [HttpPost("Count")]
        public async Task<IActionResult> Get_NotificationCount()
        {
            var userId = _usrService.Get_UserId(User.Claims);
            if (userId > 0)
            {
                return Ok(new
                {
                    Count = await _ns.Get_UnreadNotificationsCount(userId)
                });
            }
            return BadRequest();
        }

        [HttpPost("Resume")]
        public async Task<IActionResult> Get_NotificationsResume()
        {
            var userId = _usrService.Get_UserId(User.Claims);

            if (userId <= 0) return BadRequest();

            var model = await _ns.GetResumedNotifications(userId);
            return Ok(model);
        }
    }
}