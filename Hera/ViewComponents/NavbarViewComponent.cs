using Hera.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private IDataAccess _data;

        public NavbarViewComponent(IDataAccess data)
        {
            _data = data;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roleValue = 0;
            if (User.IsInRole("Profesor"))
                roleValue = 1;
            else if (User.IsInRole("Estudiante"))
                roleValue = 2;

            return View(roleValue);
        }


    }
}
