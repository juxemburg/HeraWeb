using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Services;
using HeraScratch.Exceptions;
using Hera.Models.ServicesViewModels.Valoration;
using Hera.Services.ScratchServices;

namespace Hera.Controllers.ControllersApi.Desafios
{
    [Produces("application/json")]
    [Route("api/Desafios")]
    public class DesafiosController : Controller
    {
        private readonly ScratchService _scratchService;

        public DesafiosController(ScratchService service)
        {
            _scratchService = service;
        }


        [HttpGet("GeneralValoration/{projectjId}")]
        public async Task<IActionResult> GeneralValoration(string projectjId)
        {
            try
            {
                var res = 
                    await _scratchService.Get_GeneralEvaluation(projectjId);
                var model
                    = new GeneralValorationViewModel(
                        (GeneralInfo)res.AdditionalInfo);
                return Ok(model);
            }
            catch (EvaluationException)
            {
                return BadRequest("Id de proyecto inválido");
            }
        }


    }
}