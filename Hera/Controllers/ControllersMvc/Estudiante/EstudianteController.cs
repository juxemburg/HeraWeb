using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Microsoft.EntityFrameworkCore;
using Hera.Models.UtilityViewModels;
using Entities.Cursos;
using Entities.Calificaciones;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Estudiante")]
    public class EstudianteController : Controller
    {
        private IDataAccess _data;

        public EstudianteController(IDataAccess data)
        {
            _data = data;
        }
        
        

        
    }
}