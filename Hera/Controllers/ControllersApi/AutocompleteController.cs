using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Data;
using Microsoft.EntityFrameworkCore;

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
                    r.Id,
                    r.Nombre,
                    r.Descripcion,
                    Autor = r.Profesor.NombreCompleto
                }).ToListAsync();

            return Ok(res);
        }

        
    }
}