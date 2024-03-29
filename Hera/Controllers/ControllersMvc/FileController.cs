using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hera.Models.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Hera.Services;
using Hera.Models.EntitiesViewModels;
using Hera.Data;
using Entities.Calificaciones;
using Microsoft.AspNetCore.Authorization;
using Hera.Services.UserServices;
using Hera.Services.ApplicationServices;

namespace Hera.Controllers.ControllersMvc
{
    public class FileController : Controller
    {
        private readonly FileManagerService _fileManager;
        private readonly IDataAccess _data;
        private readonly UserService _userService;
        private readonly DesafioService _desafioService;

        public FileController(FileManagerService service,
            IDataAccess data, UserService userService,
            DesafioService desafioService)
        {
            _data = data;
            _fileManager = service;
            _userService = userService;
            _desafioService = desafioService;

        }

        [HttpPost]
        [DisableFormValueModelBinding]
        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> UploadDesafio()
        {
            string fileName
                = "Files/Temp/" + _fileManager.GetFilePath() + ".sb2";
            FormValueProvider formModel;
            var viewModel = new CreateDesafioViewModel();
            using (var stream = System.IO.File.Create(fileName))
            {
                formModel = await Request.StreamFile(stream);
                if (stream.Length > 0)
                {
                    viewModel.DirArchivo = fileName;
                }
            }

            var bindingSuccessful = await TryUpdateModelAsync(viewModel,
                "", formModel);

            if (!bindingSuccessful)
            {
                _fileManager.DeleteFile(fileName);
                return View("../Desafios/Create", viewModel);
            }
            var profesorId =  _userService.Get_ProfesorId(User.Claims);
            var res = (await _desafioService.Create_Desafio(profesorId,
                viewModel));
            if(res)
                this.SetAlerts("success-alerts",
                    "El desaf�o se cre� exitosamente");
            else
                this.SetAlerts("error-alerts",
                    "Error al crear el desaf�o");
            return RedirectToAction("Index", "ProfesorDesafio");

        }



        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadResultado()
        {

            string fileName
                = "Files/Temp/" + _fileManager.GetFilePath() + ".sb2";
            FormValueProvider formModel;
            using (var stream = System.IO.File.Create(fileName))
            {
                formModel = await Request.StreamFile(stream);
            }
            var viewModel = new TerminarDesafioViewModel();

            var bindingSuccessful = await TryUpdateModelAsync(viewModel, prefix: "",
               valueProvider: formModel);

            if (!bindingSuccessful || !ModelState.IsValid)
            {
                _fileManager.DeleteFile(fileName);
                return BadRequest(ModelState);
            }

            var model = await _data.Find_RegistroCalificacion(
                viewModel.CursoId, viewModel.EstudianteId,
                viewModel.DesafioId);
            if (model != null && model.Iniciada)
            {
                var cal = model.CalificacionPendiente;
                cal.TerminarCalificacion(fileName);
                _data.Edit<Calificacion>(cal);
                await _data.SaveAllAsync();
            }



            return RedirectToAction("Desafio", "EstudianteCurso",
                new { idCurso = model.CursoId, idDesafio = model.DesafioId });
        }

        [Authorize(Roles = "Profesor")]
        public async Task<FileResult> DownloadEscenario(int desafioId)
        {
            var desafio = await _data.Find_Desafio(desafioId);
            if (desafio != null)
            {
                return getFile(desafio.DirSolucion,
                    desafio.Nombre);
            }
            return null;
        }

        [Authorize(Roles = "Estudiante, Profesor")]
        public async Task<FileResult> DownloadResultado(int estudianteId,
            int cursoId, int desafioId, int idCalificacion)
        {
            var calificacion = await _data.Find_Calificacion(idCalificacion,
                estudianteId, cursoId, desafioId);
            if (calificacion != null)
            {
                var fileName =
                    $"calificacion_{desafioId}_{cursoId}_{estudianteId}";
                return getFile(calificacion.DirArchivo, fileName);
            }
            return null;
        }

        private FileResult getFile(string filePath, string fileName,
            string ext = "sb2")
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/x-msdownload",
                $"{fileName}.{ext}");
        }
    }
}