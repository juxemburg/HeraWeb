using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Models.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Hera.Services;
using Hera.Models.EntitiesViewModels;
using Hera.Data;

namespace Hera.Controllers.ControllersMvc
{
    public class FileController : Controller
    {
        private FileManagerService _fileManager;
        private IDataAccess _data;

        public FileController(FileManagerService service,
            IDataAccess data)
        {
            _data = data;
            _fileManager = service;
        }
        public IActionResult Index()
        {
            ViewData["Info"] = "";
            return View();
        }



        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> Upload()
        {
            string fileName 
                = "Files/Temp/" + _fileManager.GetFilePath() + ".sb2";
            FormValueProvider formModel;
            using (var stream = System.IO.File.Create(fileName))
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

            _data.AddDesafio(viewModel.Map(fileName));
            await _data.SaveAllAsync();
            
            return RedirectToAction("Index");
        }
    }
}