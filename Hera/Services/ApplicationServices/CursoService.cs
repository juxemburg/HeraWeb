using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Cursos;
using Hera.Data;
using Hera.Models.EntitiesViewModels;
using Hera.Models.EntitiesViewModels.Cursos;
using Hera.Models.EntitiesViewModels.ProfesorCursos;
using Hera.Services.UtilServices;

namespace Hera.Services.ApplicationServices
{
    public class CursoService
    {
        private readonly IDataAccess _data;
        private readonly ColorService _clrService;

        public CursoService(IDataAccess data, ColorService clrService)
        {
            _data = data;
            _clrService = clrService;
        }


        public async Task<Curso> Get_Curso(int id)
        {
            var model = await _data.Find_Curso(id);
            if (model == null)
                throw new ApplicationServicesException(
                    "Curso no encontrado");
            return model;
        }

        public async Task<Curso> Get_Curso(int profId, int cursoId)
        {
            if(!await _data.Exist_Profesor_Curso(profId, cursoId))
                throw new ApplicationServicesException();
            return await _data.Find_Curso(cursoId);
        }

        public async Task<EditCursoViewModel> Get_CursoEditViewModel(
            int profId, int cursoId)
        {
            var model = await Get_Curso(profId, cursoId);
            return new EditCursoViewModel(model);
        }

        public async Task<bool> Create_Curso(int profId,
            CreateCursoViewModel model)
        {
            var desafio = await _data
                .Find_Desafio(model.DesafioId.GetValueOrDefault());
            if (desafio == null)
                throw new ApplicationServicesException(
                    "Error en la creación del curso");

            _data.AddCurso(model.Map(profId, desafio,
                _clrService.RandomColor));
            return await _data.SaveAllAsync();
        }

        public async Task<bool> Edit_Cruso(int profId,
            EditCursoViewModel model)
        {
            if (!await _data.Exist_Profesor_Curso(profId, model.Id))
                throw new ApplicationServicesException();
            var newCurso = await _data.Find_Curso(model.Id);
            newCurso.Nombre = model.Nombre;
            newCurso.Descripcion = model.Descripcion;
            newCurso.Password = model.Password;
            _data.Edit(newCurso);
            return await _data.SaveAllAsync();
        }

        public async Task<bool> Delete_Curso(int profId, int cursoId)
        {
            if (!await _data.Exist_Profesor_Curso(profId, cursoId))
                return false;

            await _data.Delete_Curso(cursoId);
            return await _data.SaveAllAsync();
        }

        public async Task<bool> Add_DesafioCurso(int profId,
            AddDesafioViewModel model)
        {
            try
            {
                if (!await _data.Exist_Profesor_Curso(profId, model.Id))
                    return false;

                if (await _data.Exist_Desafio(model.DesafioId, model.Id))
                    throw new ApplicationServicesException(
                        "El desafío ya se encuentra en el curso");

                var desafio = await _data.Find_Desafio(model.DesafioId);
                _data.AddDesafio(model.Id, desafio);
                return await _data.SaveAllAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationServicesException(
                    "Error en la creación de desafío", e);
            }
        }

        public async Task<bool> Remove_DesafioCurso(int profId,
            int desafioId, int cursoId)
        {
            try
            {
                if (!await _data.Exist_Profesor_Curso(profId, cursoId))
                    return false;

                if (await _data.Exist_Desafio(desafioId, cursoId))
                    throw new ApplicationServicesException(
                        "El desafío ya se encuentra en el curso");

                await _data.Delete_Desafio(cursoId, desafioId);
                return await _data.SaveAllAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationServicesException(
                    "Error en la eliminación de desafío", e);
            }
        }

        public async Task<bool> Edit_DesafioInicial(int profId,
            ChangeStarterViewModel model)
        {
            try
            {
                if (!await _data.Exist_Profesor_Curso(profId, model.CursoId))
                    return false;

                if (!await _data.Exist_Desafio(model.NewStarterId, model.CursoId)
                    && !await _data.Exist_Desafio(model.OldStarterId, model.CursoId))
                    throw new ApplicationServicesException(
                        "Error al cambiar el desafío");

                await _data.ChangeStarterDesafio(model.CursoId,
                    model.OldStarterId, model.NewStarterId);
                return await _data.SaveAllAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApplicationServicesException(
                    "Error en la eliminación de desafío", e);
            }
        }
    }
}

