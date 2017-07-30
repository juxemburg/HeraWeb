using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Hera.Services.UserServices;
using Hera.Services;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.UtilityViewModels;
using Hera.Models.NotificationViewModels;

namespace Hera.Controllers.ControllersMvc.Notifications
{
    [Authorize]
    public class NotificationsController : Controller
    {
        private UserService _usrService;
        private NotificationService _ns;

        public NotificationsController(UserService usrService,
            NotificationService ns)
        {
            _usrService = usrService;
            _ns = ns;
        }

        public async Task<IActionResult> Index(int skip, int take = 10)
        {
            var id = _usrService.Get_Id(User.Claims);
            var notifications = await _ns.Get_Notifications(id, skip, take);
            
            return View(new PaginationViewModel<NotificationDateViewModel>(
                notifications, skip, take));

        }
    }
}