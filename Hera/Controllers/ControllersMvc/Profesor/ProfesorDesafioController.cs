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
using Microsoft.EntityFrameworkCore;

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
            var profId = _usrService.Get_ProfesorId(User.Claims);
            var model = _data.GetAll_Desafios(null, profId,
                searchModel.SearchString, searchModel.Map(),
                searchModel.EqualSearchModel)
                .AsNoTracking()
                .Select(m => 
                new DesafioDetailsViewModel(m))
                .ToList();
            this.GetAlerts();
            return View(new PaginationViewModel<DesafioDetailsViewModel>(
                model, skip, take));
        }

        [HttpGet("{idDesafio}")]
        public IActionResult Details(int idDesafio)
        {
            return View();
        }

        
    }
}