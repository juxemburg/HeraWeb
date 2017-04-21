using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.EntityFrameworkCore;

namespace Hera.Controllers
{
    [Produces("application/json")]
    [Route("api/Cursos")]
    public class CursosController : Controller
    {
        private IDataAccess _data;

        public CursosController(IDataAccess data)
        {
            _data = data;
        }

        [HttpGet("autocomplete/{search}")]
        public async Task<IActionResult> AutoComplete(string search)
        {
            var res = await _data.Autocomplete_Desafios(search)
                .Select(r => new
                {
                    Id= r.Id,
                    Nombre = r.Nombre,
                    Dificultad = r.Dificultad
                }).ToListAsync();

            return Ok(res);
        }
    }
}