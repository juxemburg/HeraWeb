using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Controllers
{
    public static class ControllerExtensions
    {
        public static void SetAlerts(this Controller _this,
            string type, string message)
        {
            if (_this.TempData[type] == null)
                _this.TempData[type] = new List<string>();

            ((List<string>)_this.TempData[type]).Add(message);
        }

        public static void GetAlerts(this Controller _this)
        {
            _this.ViewData["success-alerts"] = _this.TempData["success-alerts"];
            _this.ViewData["warning-alerts"] = _this.TempData["warning-alerts"];
            _this.ViewData["error-alerts"] = _this.TempData["error-alerts"];
        }
    }
}
