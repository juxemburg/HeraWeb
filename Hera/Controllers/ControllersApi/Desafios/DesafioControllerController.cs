using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hera.Services;

namespace Hera.Controllers.ControllersApi.Desafios
{
    [Produces("application/json")]
    [Route("api/DesafioController")]
    public class DesafioControllerController : Controller
    {
        private ScratchService _scratchService;

        public DesafioControllerController(ScratchService service)
        {
            _scratchService = service;
        }


    }
}