using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Desafios;
using Hera.Data;
using Hera.Models.EntitiesViewModels;
using Hera.Models.EntitiesViewModels.Desafios;
using Hera.Models.EntitiesViewModels.EstudianteDesafio;
using Hera.Models.EntitiesViewModels.ProfesorCursos;
using Hera.Models.UtilityViewModels;
using Hera.Services.UserServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hera.Services.ApplicationServices
{
    public class ProfesorService
    {
        private readonly IDataAccess _data;

        public ProfesorService(IDataAccess data)
        {
            _data = data;
        }

        public async Task<PaginationViewModel<DesafioDetailsViewModel>>
            GetAll_Desafios(int profId, SearchDesafioViewModel searchModel,
            int skip, int take = 10)
        {
            var model = await _data.GetAll_Desafios(null, profId,
                    searchModel.SearchString, searchModel.Map(),
                    searchModel.EqualSearchModel)
                .AsNoTracking()
                .Select(m =>
                    new DesafioDetailsViewModel(m))
                .ToListAsync();
            return new PaginationViewModel<DesafioDetailsViewModel>(
                model, skip, take);
        }

        public async Task<IEnumerable<Desafio>> GetAll_Desafios(int profId,
            int cursoId)
        {
            if (await _data.Exist_Profesor_Curso(profId, cursoId))
            {
                var curso = await _data.Find_Curso(cursoId);
                return curso.Desafios
                    .Select(d => d.Desafio);
            }
            throw new ApplicationServicesException("El desafío no existe");
        }

        public async Task<IEnumerable<SelectListItem>>
            GetAll_DesafiosSelectList(int profId, int cursoId)
        {
            return (await GetAll_Desafios(profId, cursoId))
                .Select(d =>
                    new SelectListItem()
                    {
                        Value = d.Id.ToString(),
                        Text = d.Nombre
                    });
        }


        public async Task<ProfesorCursoViewModel> Get_Curso(int profId,
            int cursoId)
        {
            var model = await _data.Find_Curso(cursoId);
            if (model == null || model.ProfesorId != profId)
            {
                throw new ApplicationServicesException("Curso no encontrado");
            }
            var registros =
                await _data.GetAll_RegistroCalificacion(cursoId)
                    .GroupBy(reg => new
                    {
                        reg.DesafioId,
                        reg.EstudianteId
                    })
                    .ToDictionaryAsync(reg =>
                            new Tuple<int, int>(
                                reg.Key.DesafioId, reg.Key.EstudianteId),
                                reg => reg.ToList());
            model.Desafios = model.Desafios
                .OrderByDescending(d => d.Initial)
                .ToList();
            return new ProfesorCursoViewModel(model, registros);
        }

        public async Task<DesafioCursoViewModel> Get_Desafio(int profId,
            int cursoId, int desafioId)
        {
            if (!await _data.Exist_Desafio(desafioId, cursoId, profId))
                throw  new ApplicationServicesException("Desafío no encontrado");

            var desafio = await _data.Find_Desafio(desafioId);
            var curso = await _data.Find_Curso(cursoId);

            return new DesafioCursoViewModel(desafio, curso);
        }

        public async Task<EstudianteCalificacionViewModel>
            Get_Estudiante(int profId,int cursoId, int estudianteId)
        {
            var model = await _data.Find_Estudiante(estudianteId,
                cursoId, profId);
            if(model == null)
                throw new ApplicationServicesException("Estudiante no encontrado");
            return new EstudianteCalificacionViewModel(model);
        }

    }

    
}
