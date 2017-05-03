using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Hera.Models.UtilityViewModels;
using Entities.Cursos;
using Microsoft.AspNetCore.Authorization;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles ="Profesor")]
    public class ProfesorController : Controller
    {
        private IDataAccess _data;

        public ProfesorController(IDataAccess  data)
        {
            _data = data;
        }


        

        

    }
}