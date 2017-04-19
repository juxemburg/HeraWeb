using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Hera.Models.EntitiesViewModels;
using Entities.Cursos;

namespace Hera.Controllers.ControllersMvc
{
    [Authorize(Roles = "Profesor")]
    public class CursosController : Controller
    {
        private IDataAccess _data;

        public CursosController(IDataAccess data)
        {
            _data = data;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var model = await _data.GetAll_Cursos().ToListAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCursoViewModel model)
        {
            
            if(ModelState.IsValid)
            {
                try
                {
                    var id = _data.Get_UserId(User.Claims); 
                    var desafio = await _data.Find_Desafio(model.DesafioId);
                    if (desafio != null)
                        _data.Add<Curso>(model.Map(id, desafio));
                    else
                        _data.AddCurso(model.Map(id));

                    var res = await _data.SaveAllAsync();
                    if (res)
                        return RedirectToAction("Index");
                }
                catch (Exception e) { }

                
                
            }
            return View(model);
        }



    }
}