using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private UserService _usrService;
        private NotificationSerivce _ns;

        public NotificationsController(UserService usrService,
            NotificationSerivce ns)
        {
            _usrService = usrService;
            _ns = ns;
        }


        [HttpPost("Count")]
        public async Task<IActionResult> Get_NotificationCount()
        {
            var userId = _usrService.Get_Id(User.Claims);
            if (userId > 0)
            {
                return Ok(new
                {
                    Count = await _ns.Get_UnreadNotificationsCount(userId)
                });
            }
            return BadRequest();

        }
    }
}