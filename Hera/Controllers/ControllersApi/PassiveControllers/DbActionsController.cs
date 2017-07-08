using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Hera.Controllers.ControllersApi.PassiveControllers
{
    [Authorize(Roles ="Admin")]
    [Produces("application/json")]
    [Route("api/DbActions")]
    public class DbActionsController : Controller
    {
        private ApplicationDbContext _context;
        private IDataAccess _data;

        public DbActionsController(ApplicationDbContext context,
            IDataAccess data)
        {
            _data = data;
            _context = context;
        }

        [HttpGet("DeleteCourseInfo")]
        public async Task<IActionResult> DeleteCourseInfo()
        {
            try
            {
                _context.Database
                .ExecuteSqlCommand("Delete from [InfoGenerales]");
                _context.Database
                .ExecuteSqlCommand("Delete from [InfoSprites]");
                _context.Database
                .ExecuteSqlCommand("Delete from [BloquesScratch]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [ResultadosScratch]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [Calificaciones]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [CalificacionesCualitativas]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [RegistroCalificaiones]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [Rel_Cursos_Desafios]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [Cursos]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [InfoDesafios]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [Desafios]");
                await _data.SaveAllAsync();
                return Ok("Deleted");
            }
            catch(Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet("DeleteValorationInfo")]
        public async Task<IActionResult> DeleteValorationInfo()
        {
            try
            {
                _context.Database
                    .ExecuteSqlCommand("Delete from [InfoGenerales]");
                _context.Database
                    .ExecuteSqlCommand("Delete from [InfoSprites]");
                _context.Database
                    .ExecuteSqlCommand("Delete from [BloquesScratch]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [ResultadosScratch]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [Calificaciones]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [CalificacionesCualitativas]");
                _context.Database
                    .ExecuteSqlCommand("Delete From [RegistroCalificaiones]");
                await _data.SaveAllAsync();
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
    }
}