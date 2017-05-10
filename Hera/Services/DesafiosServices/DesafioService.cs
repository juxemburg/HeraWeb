using Entities.Desafios;
using Hera.Data;
using Hera.Models.EntitiesViewModels.EstudianteCurso;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services.DesafiosServices
{
    public class DesafioService
    {
        private IDataAccess _data;
        private Random _random;

        public DesafioService(IDataAccess data)
        {
            _data = data;
            _random = new Random(4);
        }

        public async Task<EstudianteCursoViewModel>
            Get_RelEstudianteCurso(int idCurso,
            int idEstudiante)
        {
            var model = await _data
                    .Find_Rel_CursoEstudiantes(idCurso, idEstudiante);
            var desafiosRealizados = model.Registros
                .Where(rel => rel.Desafio != null)
                .Select(rel => rel.Desafio)                
                .ToList();
            if (desafiosRealizados == null ||
                desafiosRealizados.Count == 0)
            {
                return new EstudianteCursoViewModel(model.Curso,
                    desafiosRealizados, model.Curso.Desafio);
            }
            else
            {
                var desafio = await Get_SiguienteDesafio(idCurso,
                    idEstudiante);

                return new EstudianteCursoViewModel(model.Curso,
                    desafiosRealizados, desafio);
            }
        }

        public async Task<Desafio> Get_SiguienteDesafio(int idCurso,
            int idEstudiante)
        {
            var desafios = await _data.GetAll_Desafios(idCurso)
                    .ToListAsync();

            return desafios.ElementAt(_random.Next(desafios.Count));
        }
    }
}
