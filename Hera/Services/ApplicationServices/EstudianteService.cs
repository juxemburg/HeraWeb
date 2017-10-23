using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Calificaciones;
using Entities.Cursos;
using Hera.Data;
using Hera.Models.EntitiesViewModels;
using Hera.Models.EntitiesViewModels.EstudianteCurso;
using Hera.Models.EntitiesViewModels.EstudianteDesafio;
using Hera.Models.UtilityViewModels;
using Hera.Services.DesafiosServices;
using HeraScratch.Exceptions;
using Microsoft.EntityFrameworkCore;
using Hera.Models.ServicesViewModels.Estudiante;

namespace Hera.Services.ApplicationServices
{
    public class EstudianteService
    {
        private readonly IDataAccess _data;
        private readonly DesafioEstudianteService _desafioService;
        private readonly ScratchService _evaluator;

        public EstudianteService(IDataAccess data, ScratchService evaluator,
            DesafioEstudianteService desafioService)
        {
            _data = data;
            _desafioService = desafioService;
            _evaluator = evaluator;
        }

        public async Task<PaginationViewModel<Curso>> Search_Curso(int estId,
            string searchString, int skip, int take)
        {
            var model = await _data
                .Search_CursosEstudiante(estId, searchString)
                .ToListAsync();
            return new PaginationViewModel<Curso>(model, skip, take);
        }

        public async Task<PaginationViewModel<Curso>> GetAll_Curso(
            int estId, string searchString, int skip, int take)
        {
            var model = await _data.GetAll_CursosEstudiante(estId,
                searchString).ToListAsync();
            return new PaginationViewModel<Curso>(model, skip, take);
        }

        public async Task<bool> Do_MatricularEstudiante(int estId,
            AddEstudianteViewModel model)
        {
            try
            {
                var curso = await _data.Find_Curso(model.CursoId);
                var estudiante = await _data.Find_Estudiante(estId);
                _data.Do_MatricularEstudiante(curso, estudiante,
                    model.Map(curso.Id, estudiante.Id), model.Password);

                if (!await _data.SaveAllAsync())
                    throw new ApplicationServicesException(
                        "Lo sentimos, la contraseña es inválida");

                return true;
            }
            catch (Exception)
            {
                throw  new ApplicationServicesException(
                    "Contraseña inválida");
            }
        }

        public async Task<EstudianteCursoViewModel> Get_Curso(int estId,
            int cursoId)
        {
            if(!await _data.Exist_Estudiante_Curso(estId, cursoId))
                throw new ApplicationServicesException();

            var model = await _desafioService
                .Get_RelEstudianteCurso(cursoId, estId);
            return model;
        }

        public async Task<CalificacionDesafioViewModel> Get_Desafio(
            int estId, int cursoId, int desafioId)
        {
            var curso = await _data.Find_Curso(cursoId);
            if (curso == null || !curso.ContieneEstudiante(estId)
                || !curso.ContieneDesafio(desafioId))
                throw new ApplicationServicesException("");

            var model = await _data.Find_RegistroCalificacion(
                cursoId, estId, desafioId);
            if (model == null)
            {
                model = new RegistroCalificacion()
                {
                    CursoId = cursoId,
                    DesafioId = desafioId,
                    EstudianteId = estId,
                    Calificaciones = new List<Calificacion>()
                };
                _data.Add(model);
                if(!await _data.SaveAllAsync())
                    throw new ApplicationServicesException("");
            }
            return new CalificacionDesafioViewModel(model);
        }

        public async Task<DesafioProgresoViewModel> Get_DesafioProgreso(
            int estId, int cursoId, int desafioId)
        {
            var registro = await _data.Find_RegistroCalificacion(cursoId,
                    estId, desafioId);
            if(registro == null)
                throw new ApplicationServicesException("");

            var desafio = await _data.Find_Desafio(desafioId);
            var curso = await _data.Find_Curso(cursoId);

            return new DesafioProgresoViewModel
            {
                Calificaciones = registro.Calificaciones
                    .Where(c => !c.EnCurso)
                    .ToList(),
                CursoId = cursoId,
                NombreCurso = curso.Nombre,
                NombreDesafio = desafio.Nombre
            };
        }

        public async Task<int> Do_CalificarDesafio(int estId,
            int idCurso, int idDesafio, string idProj)
        {
            var model = await _data.Find_RegistroCalificacion(
                idCurso, estId, idDesafio);

            try
            {
                if (model == null || !model.Iniciada) return -1;

                var cal = model.CalificacionPendiente;
                var res = await _evaluator.Get_Evaluation(idProj);
                var est = await _data.Find_Estudiante(estId);
                var curso = await _data.Find_Curso(idCurso);
                var resultados = res.Select(val => val.Map(cal.Id))
                    .ToList();

                _data.Do_TerminarCalificacion(curso,
                    est, cal, resultados, idProj);
                await _data.SaveAllAsync();
                return cal.Id;
            }
            catch (EvaluationException)
            {
                throw new ApplicationServicesException(
                    "Id de desafío inválido");
            }
        }

        public async Task<DesafioCompletadoViewModel>
            Get_DesafioCompletadoViewModel(int idEst, int idCurso,
                int idDesafio, int idCalificacion)
        {
            if (!await _data.Exist_Estudiante_Curso(idEst, idCurso))
                return null;

            var desafio = await _desafioService
                .Get_SiguienteDesafio(idCurso, idEst);

            var resultado = await _data
                .Find_ResultadoScratchGeneral(idCalificacion);

            return new DesafioCompletadoViewModel(idCurso, resultado,
                desafio);
        }

        public async Task<bool> IniciarDesafio(int idEst, int idCurso,
            int idDesafio)
        {
            var model = new Calificacion()
            {
                Tiempoinicio = DateTime.Now,
                CursoId = idCurso,
                EstudianteId = idEst,
                DesafioId = idDesafio
            };
            _data.Add(model);
            return await _data.SaveAllAsync();
        }

        public async Task<CursoCompletadoViewModel> Esta_Finalizado_Curso( int idEst, int idCurso) {
            CursoCompletadoViewModel cursoCompletado = new CursoCompletadoViewModel();
            cursoCompletado.Estado = await _data.The_Course_Is_Over(idEst, idCurso);
            var curso = await _data.Find_Curso(idCurso);
            if (cursoCompletado.Estado)
                cursoCompletado.Mensaje = "Tu curso " + curso.Nombre + " Ha sido finalizado con exito";
            else
                cursoCompletado.Mensaje = "Aún te quedan unos cuantos desafios por terminar en el curos"+ curso.Nombre +", sigue adelante";
            return cursoCompletado;
        }
    }
}

