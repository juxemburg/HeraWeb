using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hera.Data;
using Hera.Services.UserServices;
using Hera.Models.UtilityViewModels;
using Entities.Desafios;
using Hera.Models.EntitiesViewModels.Desafios;

namespace Hera.Controllers.ControllersMvc.Profesor
{
    [Authorize(Roles="Profesor")]
    [Route("/Profesor/Desafios")]
    public class ProfesorDesafioController : Controller
    {
        private IDataAccess _data;
        private UserService _usrService;

        public ProfesorDesafioController(IDataAccess data,
            UserService usrService)
        {
            _usrService = usrService;
            _data = data;
        }

        public async Task<IActionResult> Index(
            SearchDesafioViewModel searchModel,
            int skip = 0, int take = 10)
        {
            var profId = await _usrService.Get_ProfesorId(User.Claims);
            var model = _data.GetAll_Desafios(null, profId,
                searchModel.SearchString, searchModel.Map(), true);
            
            return View(new PaginationViewModel<Desafio>(model, skip, take));
        }

        [HttpGet("{idDesafio}")]
        public IActionResult Details(int idDesafio)
        {
            return View();
        }

        
    }
}