using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Models.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Hera.Services;
using Hera.Models.EntitiesViewModels;

namespace Hera.Controllers.ControllersMvc
{
    public class FileController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Info"] = "";
            return View();
        }



        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            FormValueProvider formModel;
            using (var stream = System.IO.File.Create("Files/Temp/myfile.temp"))
            {
                formModel = await Request.StreamFile(stream);
            }

            var viewModel = new CreateDesafioViewModel();

            var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
               valueProvider: formModel);

            if (!bindingSuccessful)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }

            ViewData["Info"] = "Archivo subudo con exito!";
            return RedirectToAction("Index");
        }
    }
}