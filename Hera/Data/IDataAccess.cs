using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Desafios;
using Entities.Notifications;
using Entities.Usuarios;
using Entities.Valoracion;
using Hera.Services.NotificationServices.NotificationBuilders;
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
        Task<Rel_CursoEstudiantes> Find_Estudiante(int idEstudiante,
            int idCurso, int idProfesor);

        //Profesores
        void AddProfesor(Profesor model);
        IQueryable<Profesor> GetAll_Profesor();
        Task<int> Find_ProfesorId(int usuarioId);
        Task<int> Find_EstudianteId(int usuarioId);
        Task<Profesor> Find_Profesor(int id);

        //Desafios
        void AddDesafio(Desafio model);
        void AddDesafio(int cursoId, Desafio model);
        IQueryable<Desafio> GetAll_Desafios(int? cursoId = null,
            int? profesorId = null, string searchString = "",
            InfoDesafio similarInfo = null, bool equality = false);
        IQueryable<Desafio> Autocomplete_Desafios(string queryString);
        Task<Desafio> Find_Desafio(int id);
        Task<Rel_DesafiosCursos> Find_Rel_DesafiosCursos(int desafioId, int cursoId);
        Task<bool> Exist_Desafio(int id);
        Task<bool> Exist_Desafio(int idDesafio, int idCurso);
        Task<bool> Exist_DesafioP(int id, int idProfesor);
        Task Delete_Desafio(int id);
        Task Delete_Desafio(int cursoId, int desafioId);
        Task Calificar_Desafio(int desafioId, int profesorId, int calificacion);

        //Cursos
        void AddCurso(Curso model);        
        IQueryable<Curso> Autocomplete_Cursos(string queryString,
            int? prodId = null);
        IQueryable<Curso> GetAll_Cursos();
        IQueryable<Curso> GetAll_Cursos(int profId);
        IQueryable<Curso> GetAll_CursosEstudiante(int idEst,
            string courseName = "",
            bool inverse = false);
        Task<Curso> Find_Curso(int id);
        Task<Curso> Find_Curso_Public(int id);
        Task ChangeStarterDesafio(int cursoId, int oldId, int newId);
        Task Delete_Curso(int id);

        //Rel_Cursos_Estudiantes
        Task<Rel_CursoEstudiantes> Find_Rel_CursoEstudiantes(int idCurso,
            int idEstudiante);


        //Rel_Desafio_Profesor
        Task<Rel_Rating> Find_Rel_Rating(int desafioId, int profesorId);

        //RegistroCalificacion
        Task<RegistroCalificacion> Find_RegistroCalificacion(
            int cursoId, int estudianteId,
            int desafioId, int? profesorId = null);
        IQueryable<RegistroCalificacion> GetAll_RegistroCalificacion(
            int? cursoId = null, int? estudianteId = null,
            int? desafioId = null);
        Task Delete_RegistroCalificacion(int cursoId, int estId,
            int desafioId);


        //Calificacion
        Task<Calificacion> Find_Calificacion(int calificacionId);
        Task<Calificacion> Find_Calificacion(int calificacionId,
            int estudianteId, int cursoId, int desafioId);
        void AddCalificacion(Calificacion calificacion);
        void EditFinalizarCalificacion(int calificacionId);
        void Do_TerminarCalificacion(Curso curso, Calificacion calificacion,
            List<ResultadoScratch> resultados, string projId);

        //Calificacion Cualitativa
        Task<CalificacionCualitativa> Find_CalificacionCualitativa
            (int calificacionId);

        Task<CalificacionCualitativa> Find_CalificacionCualitativa
            (int estudianteId, int cursoId, int desafioId);

        //Cantidad de estudiantes que finalizaron y no finalizaron los desafios
        IQueryable<Estudiante> Find_Estudiantes_Finalizaron
            (int desafioId, int cursoId);
        IQueryable<Estudiante> Find_Estudiantes_No_Finalizaron
            (int desafioId, int cursoId);

        //Resultados Scratch
        void AddRange_ResultadoScratch(
            IEnumerable<ResultadoScratch> resultados);
        void Add_ResultadoScratch(
            ResultadoScratch resultado);
        IQueryable<ResultadoScratch> GetAll_ResultadoScratch(
            int calificacionId);
        Task<ResultadoScratch> Find_ResultadoScratchGeneral(
            int calificacionId);
        void Add_InfoScratch(IInfoScratch info);
        

        //Validacion
        Task<bool> Exist_Profesor_Curso(int profesorId,
            int cursoId);
        Task<bool> Exist_Estudiante_Curso(int estudianteId,
            int cursoId);

        //Notifications
        void Do_PushNotification(NotificationType type,
            int userId,
            Dictionary<string, string> values);
        void Add_Notification(Notification model);
        void Edit_Notification(Notification model);
        Task<Notification> Find_Notification(int id);
        Task<Notification> Find_Notification(int userId, string key);

    }
}
