using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hera.Data
{
    public interface IDataAccess
    {
        // Operaciones Básicas
        void Add<T>(T entity) where T : class;
        void Edit<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();

        //Claims
        int Get_UserId(IEnumerable<Claim> claims);

        //Estudiantes
        void AddEstudiante(Estudiante model);
        IQueryable<Estudiante> GetAll_Estudiante();
        Task<Estudiante> Find_Estudiante(int id);

        //Profesores
        void AddProfesor(Profesor model);
        IQueryable<Profesor> GetAll_Profesor();
        Task<int> Find_ProfesorId(int usuarioId);
        Task<int> Find_EstudianteId(int usuarioId);
        Task<Profesor> Find_Profesor(int id);

        //Desafios
        void AddDesafio(Desafio model);
        void AddDesafio(int cursoId, Desafio model);
        IQueryable<Desafio> GetAll_Desafios();
        IQueryable<Desafio> Autocomplete_Desafios(string queryString);
        Task<Desafio> Find_Desafio(int id);
        Task<bool> Exist_Desafio(int id);

        //Cursos
        void AddCurso(Curso model);        
        IQueryable<Curso> Autocomplete_Cursos(string queryString);
        IQueryable<Curso> Autocomplete_Cursos(string queryString,
            int? prodId);
        IQueryable<Curso> GetAll_Cursos();
        IQueryable<Curso> GetAll_Cursos(int profId);
        IQueryable<Curso> GetAll_CursosEstudiante(int idEst);
        Task<Curso> Find_Curso(int id);


        //RegistroCalificacion
        Task<RegistroCalificacion> Find_RegistroCalificacion(int cursoId,
            int estudianteId, int desafioId);
        IQueryable<RegistroCalificacion> GetAll_RegistroCalificacion(
            int? cursoId, int? estudianteId, int? desafioId);


        //RegistroCalificacion
        Task<Calificacion> Find_Calificacion(int calificacionId);
        void AddCalificacion(Calificacion calificacion);
        void EditFinalizarCalificacion(int calificacionId);

        


    }
}
