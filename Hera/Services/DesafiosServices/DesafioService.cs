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

        /// <summary>
        /// Método que obtiene el modelo de la relación
        /// entre uun estudiante y curso
        /// </summary>
        /// <param name="idCurso">identificador del curso</param>
        /// <param name="idEstudiante">identificador del estudiante</param>
        /// <returns>Modelo EstudianteCursoViewModel</returns>
        public async Task<EstudianteCursoViewModel>
            Get_RelEstudianteCurso(int idCurso,
            int idEstudiante)
        {
            var model = await _data
                    .Find_Rel_CursoEstudiantes(idCurso, idEstudiante);
            var desafiosRealizados = model.Registros
                .Where(rel => rel.Terminada
                && rel.Desafio != null)
                .Select(rel => rel.Desafio)                
                .ToList();
            var desafiosNoCompletados = model.Registros
                .Where(rel => !rel.Terminada
                && rel.Desafio != null)
                .Select(rel => rel.Desafio)
                .ToList();

            if (desafiosRealizados == null ||
                desafiosRealizados.Count == 0)
            {
                return new EstudianteCursoViewModel(model.Curso,
                    desafiosRealizados, 
                    desafiosNoCompletados,
                    model.Curso.Desafio);
            }
            else
            {
                var desafio = await Get_SiguienteDesafio(idCurso,
                    idEstudiante);

                return new EstudianteCursoViewModel(model.Curso,
                    desafiosRealizados, desafiosNoCompletados, desafio);
            }
        }

        /// <summary>
        /// Método que retorna el siguiente desafío para un
        /// estudiante, dentro de un curso
        /// </summary>
        /// <param name="idCurso">identificador del curso</param>
        /// <param name="idEstudiante">identificador del estudiante</param>
        /// <returns></returns>
        public async Task<Desafio> Get_SiguienteDesafio(int idCurso,
            int idEstudiante)
        {
            var desafios = await _data.GetAll_Desafios(idCurso)
                .ToListAsync();
            return desafios.ElementAt(_random.Next(desafios.Count));
        }
    }
}
