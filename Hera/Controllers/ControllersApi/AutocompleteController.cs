using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.EntityFrameworkCore;
using Hera.Services;

namespace Hera.Controllers
{
    [Produces("application/json")]
    [Route("api/Autocomplete")]
    public class AutocompleteController : Controller
    {
        private IDataAccess _data;

        public AutocompleteController(IDataAccess data)
        {
            _data = data;
        }

        [HttpGet("Desafios/{search}")]
        public async Task<IActionResult> AutoComplete(string search)
        {
            var res = await _data.Autocomplete_Desafios(search)
                .Select(r => new
                {
                    Id= r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    Dificultad = 0
                }).ToListAsync();

            return Ok(res);
        }

        
    }
}