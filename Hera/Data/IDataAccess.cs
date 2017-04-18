using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Data
{
    public interface IDataAccess
    {
        // Operaciones Básicas
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();

        //Estudiantes
        void AddEstudiante(Estudiante model);
        IQueryable<Estudiante> GetAll_Estudiante();
        Task<Estudiante> Find_Estudiante(int id);

        //Profesores
        void AddProfesor(Profesor model);
        IQueryable<Profesor> GetAll_Profesor();
        Task<Profesor> Find_Profesor(int id);

        //Desafios
        void AddDesafio(Desafio model);
        IQueryable<Desafio> GetAll_Desafios();
        Task<Desafio> Find_Desafio();

        //Cursos
        void AddCurso(Curso model);
        IQueryable<Curso> GetAll_Cursos();
        Task<Curso> Find_Curso();

    }
}
