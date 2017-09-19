using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Entities.Calificaciones;
using Entities.Valoracion;
using Hera.Services;
using Entities.Notifications;
using Hera.Services.NotificationServices.NotificationBuilders;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using Remotion.Linq.Utilities;

namespace Hera.Data
{
    public class DataAccess_Sql : IDataAccess
    {
        private readonly ApplicationDbContext _context;
        private readonly FileManagerService _fmService;
        private readonly NotificationDbContext _notificationCtx;

        public DataAccess_Sql(ApplicationDbContext context,
            FileManagerService fmService,
            NotificationDbContext notificationCtx)
        {
            _notificationCtx = notificationCtx;
            _fmService = fmService;
            _context = context;
        }

        public int Get_UserId(IEnumerable<Claim> claims)
        {
            try
            {
                var res = claims
                    .FirstOrDefault(c => c.Type.Equals("UsuarioId"))
                    .Value;
                
                return Convert.ToInt32(res);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Added;
        }
        public void Edit<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Deleted;
        }

        public void AddCurso(Curso model)
        {
            Add<Curso>(model);
            foreach (var item in model.Desafios)
            {
                Add<Rel_DesafiosCursos>(item);
            }

        }

        public void AddDesafio(Desafio model)
        {
            Add<Desafio>(model);
            if (model.InfoDesafio != null)
                Add<InfoDesafio>(model.InfoDesafio);
        }
        public void AddDesafio(int cursoId, Desafio model)
        {
            var rel = new Rel_DesafiosCursos()
            {
                CursoId = cursoId,
                Desafio = model
            };

            Add<Rel_DesafiosCursos>(rel);


        }

        public void AddEstudiante(Estudiante model)
        {
            Add<Estudiante>(model);
        }

        public void AddProfesor(Profesor model)
        {
            Add<Profesor>(model);
        }


        public async Task<bool> Exist_Desafio(int idDesafio)
        {
            return await _context.Desafios
                .AnyAsync(d => d.Id == idDesafio);
        }
        public async Task<bool> Exist_Desafio(int idDesafio, int idCurso)
        {
            return await _context.Rel_Cursos_Desafios
                .AnyAsync(des => des.CursoId == idCurso &&
                des.DesafioId == idDesafio);
        }
        public async Task<bool> Exist_Desafio(int idDesafio, int idCurso,
            int profesorId)
        {
            return await _context.Rel_Cursos_Desafios
                .Include(rel => rel.Curso)
                .AnyAsync(des => des.CursoId == idCurso &&
                                 des.DesafioId == idDesafio
                                 && des.Curso.ProfesorId == profesorId);
        }
        public async Task<bool> Exist_DesafioP(int id, int idProfesor)
        {
            return await _context.Desafios
                .AnyAsync(d => d.Id == id && d.ProfesorId == idProfesor);
        }
        public async Task Delete_Desafio(int id)
        {
            var desafio = await Find_Desafio(id);
            if (desafio != null && desafio.Popularity == 0)
            {
                _fmService.DeleteFile(desafio.DirSolucion);
                Delete<Desafio>(desafio);
            }

        }
        public async Task Delete_Desafio(int cursoId, int desafioId)
        {
            var rel = await Find_Rel_DesafiosCursos(desafioId, cursoId);
            if (rel != null)
            {
                Delete<Rel_DesafiosCursos>(rel);
                var calificaciones =
                    GetAll_RegistroCalificacion(cursoId, null, desafioId);
                foreach (var calificacion in calificaciones)
                {
                    Delete<RegistroCalificacion>(calificacion);
                }
            }
        }
        public async Task ChangeStarterDesafio(int cursoId, int oldId,
            int newId)
        {
            if (await Exist_Desafio(oldId, cursoId)
                && await Exist_Desafio(newId, cursoId))
            {
                var curso = await Find_Curso(cursoId);
                var desafioNew = curso.Desafios
                    .First(d => d.DesafioId == newId);
                var desafioOld = curso.Desafios
                    .First(d => d.DesafioId == oldId);
                desafioNew.Initial = true;
                desafioOld.Initial = false;
                Edit<Rel_DesafiosCursos>(desafioOld);
                Edit<Rel_DesafiosCursos>(desafioNew);
            }
        }

        public async Task<Curso> Find_Curso(int id)
        {
            return await _context.Cursos
                .Where(c => c.Id == id)
                .Include(c => c.Profesor)
                .Include(c => c.Desafios)
                .ThenInclude(c => c.Desafio)
                .Include(c => c.Estudiantes)
                .ThenInclude(rel => rel.Estudiante)
                .Include("Estudiantes.Registros")
                .FirstOrDefaultAsync();
        }
        public async Task<Curso> Find_Curso_Public(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }
        public async Task Delete_Curso(int id)
        {
            var curso = await Find_Curso(id);
            Delete(curso);
        }

        public async Task<Rel_CursoEstudiantes> Find_Rel_CursoEstudiantes(int idCurso,
            int idEstudiante)
        {
            return await _context.Rel_Cursos_Estudiantes
                .Include("Curso")
                .Include("Curso.Desafios")
                .Include("Curso.Desafios.Desafio")
                .Include("Curso.Desafios.Desafio.InfoDesafio")
                .Include("Curso.Profesor")
                .Include(cur => cur.Registros)
                .ThenInclude(reg=>  reg.Calificaciones)
                .FirstOrDefaultAsync(rel => rel.CursoId == idCurso &&
                rel.EstudianteId == idEstudiante);

        }
        public async Task<Rel_DesafiosCursos> Find_Rel_DesafiosCursos
            (int desafioId, int cursoId)
        {
            return await _context.Rel_Cursos_Desafios
                .FirstAsync(r => r.CursoId == cursoId &&
                r.DesafioId == desafioId);
        }

        public async Task<Desafio> Find_Desafio(int id)
        {
            return await _context.Desafios
                .Include(d => d.InfoDesafio)
                .Include(d => d.Profesor)
                .Include(d => d.Ratings)
                .Include(d => d.Cursos)
                .Include(d => d.Calificaciones)
                .ThenInclude(c => c.Calificaciones)
                .FirstAsync(d => d.Id == id);
        }

        public async Task Calificar_Desafio(int desafioId, int profesorId,
            int calificacion)
        {
            var rating = await Find_Rel_Rating(desafioId,
                profesorId);
            if (rating != null)
            {
                rating.Rating = calificacion;
                Edit<Rel_Rating>(rating);
            }
            else
            {
                rating = new Rel_Rating()
                {
                    DesafioId = desafioId,
                    ProfesorId = profesorId,
                    Rating = calificacion
                };
                Add<Rel_Rating>(rating);
            }
        }

        public async Task<Rel_Rating> Find_Rel_Rating(int desafioId,
            int profesorId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.DesafioId == desafioId
                && r.ProfesorId == profesorId);
        }

        public async Task<Estudiante> Find_Estudiante(int id)
        {
            return await _context.Estudiantes
                .FirstAsync(e => e.Id.Equals(id));
        }

        public async Task<Rel_CursoEstudiantes> Find_Estudiante(int idEstudiante,
            int idCurso, int idProfesor)
        {

            var query = await _context
                .Rel_Cursos_Estudiantes
                .Include(rel => rel.Curso)
                .Include(rel => rel.Estudiante)
                .Include("Registros.Calificaciones")
                .Include("Registros.Desafio")
                .Where(rel =>
                rel.Curso.ProfesorId == idProfesor &&
                rel.CursoId == idCurso &&
                rel.EstudianteId == idEstudiante)
                .FirstOrDefaultAsync();

            return query;
        }

        public void Do_MatricularEstudiante(Curso curso,
            Estudiante estudiante,
            Rel_CursoEstudiantes model, string password)
        {
            if (curso.Password.Equals(password))
            {
                Add<Rel_CursoEstudiantes>(model);
                Do_PushNotification(NotificationType.NotificationNuevoEstudiante,
                    curso.Profesor.UsuarioId,
                    new Dictionary<string, string>()
                    {
                        ["IdCurso"] = $"{curso.Id}",
                        ["NombreCurso"] = curso.Nombre,
                        ["NombreEstudiante"] = estudiante.NombreCompleto
                    });
            }
        }

        public async Task<bool> Exist_Profesor(int usuarioId)
        {
            return await _context.Profesores
                .AnyAsync(p => p.UsuarioId == usuarioId);
        }
        public async Task<Profesor> Find_Profesor(int id)
        {
            return await _context.Profesores.FindAsync(id);
        }

        public async Task<Profesor> Find_ProfesorU(int usuarioId)
        {
            try
            {
                var id = await Find_ProfesorId(usuarioId);
                return await Find_Profesor(id);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<int> Find_ProfesorId(int usuarioId)
        {
            var id = await _context.Profesores
                .Where(p => p.UsuarioId == usuarioId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
            return id;
        }
        public async Task<int> Find_EstudianteId(int usuarioId)
        {
            var id = await _context.Estudiantes
                .Where(p => p.UsuarioId == usuarioId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
            return id;
        }

        public IQueryable<Curso> GetAll_Cursos()
        {
            return _context.Cursos
                .Where(c => c.Activo)
                .Include(c => c.Desafios)
                .Include(c => c.Profesor);
        }
        public IQueryable<Curso> GetAll_Cursos(int profId, 
            bool active = true)
        {
            return _context.Cursos
                .Where(c => c.ProfesorId == profId
                && c.Activo == active)
                .Include(c => c.Profesor);
        }

        public IQueryable<Curso> Search_CursosEstudiante(int idEst, 
            string searchString = "")
        {
            var ids = _context.Rel_Cursos_Estudiantes
                .Where(rel => rel.EstudianteId == idEst)
                .Select(rel => rel.CursoId);
            var query = Enumerable.Empty<Curso>().AsQueryable();
            query =  _context.Cursos
                .Where(c => !ids.Contains(c.Id)
                && c.Activo)
                .Include(c => c.Profesor);
            if (!string.IsNullOrWhiteSpace(searchString))
                query = query.Where(c => c.Nombre.Contains(searchString));
            return query;
        }
        public IQueryable<Curso> GetAll_CursosEstudiante(int idEst,
            string courseName = "", bool inverse = false)
        {
            var query = Enumerable.Empty<Curso>().AsQueryable();

            var ids = _context.Rel_Cursos_Estudiantes
                .Where(rel => rel.EstudianteId == idEst)
                .Select(rel => rel.CursoId);
            query = _context.Cursos
                .Where(cur => ids.Contains(cur.Id)
                && cur.Activo)
                .Include(c => c.Profesor);
            
            if (!string.IsNullOrWhiteSpace(courseName))
                query = query.Where(c => c.Nombre.Contains(courseName));

            return query;

        }

        public IQueryable<Curso> Autocomplete_Cursos(string queryString,
            int? profId = null)
        {
            var query = (profId == null) ? GetAll_Cursos() :
                GetAll_Cursos(profId.GetValueOrDefault());
            query = query.Where(c => c.Nombre.Contains(queryString));
            return query;
        }

        public IQueryable<Curso> Autocomplete_CursosI(string queryString,
            int profId)
        {
            return GetAll_Cursos(profId, false)
                .Where(c => c.Nombre.Contains(queryString));
        }

        public IQueryable<Desafio> GetAll_Desafios(int? cursoId = null,
            int? profesorId = null, string searchString = "",
            InfoDesafio similarInfo = null, bool equality = false,
            float avgValoration = 0)
        {
            IQueryable<Desafio> query = Enumerable.Empty<Desafio>()
                .AsQueryable();
            if (cursoId != null)
            {
                query = _context
                    .Rel_Cursos_Desafios
                    .Include(rel => rel.Desafio)
                    .Include("Desafio.InfoDesafio")
                    .Include("Desafio.Profesor")
                    .Include("Desafio.Ratings")
                    .Include("Desafio.Cursos")
                    .Where(rel => rel.CursoId == cursoId)
                    .Select(rel => rel.Desafio);
            }
            else
            {
                query = _context.Desafios
                    .Include(d => d.InfoDesafio)
                    .Include(d => d.Profesor)
                    .Include(d => d.Ratings)
                    .Include(d => d.Cursos)
                    .Where(d => d.AverageRating >= avgValoration);
            }
            

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query
                    .Where(d => d.Nombre.Contains(searchString));
            }

            if (similarInfo != null && !similarInfo.IsFalse)
            {
                if (equality)
                    query = query
                        .Where(d => d.InfoDesafio.IsEqualTo(similarInfo));
                else
                    query = query
                        .Where(d => d.InfoDesafio.IsSimilarTo(similarInfo));
            }

            if (profesorId != null)
                query = query
                    .Where(d => d.ProfesorId == profesorId);
            
            return query.OrderByDescending(d => d.AverageRating)
                .ThenByDescending(d => d.RatingCount)
                .ThenBy(d => d.Nombre);
        }
        public IQueryable<Desafio> Autocomplete_Desafios(string queryString)
        {
            return GetAll_Desafios()
                .Where(d => d.Nombre.Contains(queryString))
                .Include(d => d.Profesor);
        }

        public IQueryable<Estudiante> GetAll_Estudiante()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Profesor> GetAll_Profesor(
            string searchString = "")
        {
            IQueryable<Profesor> query = _context.Profesores;
            if (!string.IsNullOrWhiteSpace(searchString))
                query = query.Where(p =>
                    p.NombreCompleto.Contains(searchString));
            return query;
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<RegistroCalificacion> Find_RegistroCalificacion(
            int cursoId, int estudianteId, int desafioId,
            int? profesorId = null)
        {
            var query = Enumerable
                .Empty<RegistroCalificacion>().AsQueryable();

            if (profesorId != null
                && !(await Exist_Profesor_Curso(profesorId.Value, cursoId)))
            {
                return null;
            }
            query = _context.RegistroCalificaiones
                .Where(reg => reg.CursoId == cursoId
                && reg.EstudianteId == estudianteId
                && reg.DesafioId == desafioId)
                .Include(reg => reg.Desafio)
                .Include("Calificaciones.Resultados")
                .Include("Calificaciones.CalificacionCualitativa")
                .Include("Calificaciones.Resultados.Bloques")
                .Include("Calificaciones.Resultados.IInfoScratch_General")
                .Include("Calificaciones.Resultados.IInfoScratch_Sprite");

            return await query.FirstOrDefaultAsync();
        }

        public IQueryable<RegistroCalificacion> GetAll_RegistroCalificacion(
            int? cursoId = null, int? estudianteId = null,
            int? desafioId = null)
        {
            var query =
                (IQueryable<RegistroCalificacion>)
                _context.RegistroCalificaiones
                .Include(reg => reg.Calificaciones)
                .ThenInclude(cal => cal.CalificacionCualitativa)
                .Where(cal => !cal.Iniciada);

            if (cursoId != null)
                query = query
                    .Where(reg =>
                    reg.CursoId == cursoId.GetValueOrDefault());

            if (estudianteId != null)
                query = query
                    .Where(reg =>
                    reg.EstudianteId == estudianteId.GetValueOrDefault());

            if (desafioId != null)
                query = query
                    .Where(reg =>
                    reg.DesafioId == desafioId.GetValueOrDefault());

            return query;
        }
        public async Task Delete_RegistroCalificacion(int cursoId, int estId,
            int desafioId)
        {
            var model = await Find_RegistroCalificacion(cursoId, estId,
                desafioId);
            Delete(model);
        }

        public void AddCalificacion(Calificacion calificacion)
        {
            Add<Calificacion>(calificacion);
        }

        public async void EditFinalizarCalificacion(int calificacionId)
        {
            var model = await Find_Calificacion(calificacionId);
            if (model != null)
            {
                model.TiempoFinal = DateTime.Now;
                Edit<Calificacion>(model);
            }
        }
        public void Do_TerminarCalificacion(Curso curso,
            Estudiante estudiante,
            Calificacion calificacion,
            List<ResultadoScratch> resultados, string projId)
        {

            calificacion.TerminarCalificacion(projId);
            AddRange_ResultadoScratch(resultados);
            Edit<Calificacion>(calificacion);
            Do_PushNotification(
                NotificationType.NotificationNuevaCalificacion,
                curso.Profesor.UsuarioId,
                new Dictionary<string, string>()
                {
                    ["IdCurso"] = $"{curso.Id}",
                    ["NombreCurso"] = curso.Nombre,
                    ["NombreEstudiante"] = estudiante.NombreCompleto
                });
        }

        public async Task<Calificacion> Find_Calificacion(
            int calificacionId)
        {

            return await _context.Calificaciones
                .Include(cal => cal.Resultados)
                .Include("Resultados.Bloques")
                .Include("Resultados.IInfoScratch_General")
                .Include("Resultados.IInfoScratch_Sprite")
                .FirstOrDefaultAsync(cal => cal.Id == calificacionId);
        }

        public async Task<Calificacion> Find_Calificacion(int calificacionId,
            int estudianteId, int cursoId, int desafioId)
        {
            var model = await _context.Calificaciones
                .Where(cal => cal.EstudianteId == estudianteId &&
                cal.Id == calificacionId && cal.DesafioId == desafioId &&
                cal.CursoId == cursoId)
                .Include(cal => cal.Resultados)
                .Include("Resultados.Bloques")
                .Include("Resultados.IInfoScratch_General")
                .Include("Resultados.IInfoScratch_Sprite")
                .FirstOrDefaultAsync();
            return model;
        }

        public async Task<CalificacionCualitativa>
            Find_CalificacionCualitativa(int calificacionId)
        {
            return await _context.CalificacionesCualitativas
                .FindAsync(calificacionId);
        }

        public async Task<CalificacionCualitativa>
            Find_CalificacionCualitativa(int estudianteId,
            int cursoId, int desafioId)
        {
            var model = await _context.Calificaciones
                .Where(cal => cal.DesafioId == desafioId &&
                cal.CursoId == cursoId && cal.EstudianteId == estudianteId)
                .FirstOrDefaultAsync();
            return model.CalificacionCualitativa;
        }

        //Resultados Scratch
        public void Add_ResultadoScratch(ResultadoScratch resultado)
        {
            Add<ResultadoScratch>(resultado);
            if (resultado.General)
                Add_InfoScratch(resultado.IInfoScratch_General);
            else
                Add_InfoScratch(resultado.IInfoScratch_Sprite);

            foreach (var bloque in resultado.Bloques)
            {
                Add<BloqueScratch>(bloque);
            }
        }
        public void AddRange_ResultadoScratch(
            IEnumerable<ResultadoScratch> resultados)
        {
            foreach (var item in resultados)
            {
                Add_ResultadoScratch(item);
            }
        }
        public IQueryable<ResultadoScratch> GetAll_ResultadoScratch(
            int calificacionId)
        {
            var query = _context.ResultadosScratch
                .Where(res => res.CalificacionId == calificacionId)
                .Include(res => res.Bloques)
                .OrderBy(res => res.General)
                .ThenBy(res => res.Nombre);
            return query;
        }
        public async Task<ResultadoScratch> Find_ResultadoScratchGeneral(
            int calificacionId)
        {
            return await _context
                .ResultadosScratch
                .Include(res => res.Bloques)
                .Include(res => res.IInfoScratch_General)
                .Include(res => res.IInfoScratch_Sprite)
                .FirstOrDefaultAsync(res =>
                res.CalificacionId == calificacionId &&
                res.General);
        }
        public void Add_InfoScratch(IInfoScratch info)
        {
            if (info is IInfoScratch_General generalInfo)
                Add<IInfoScratch_General>(generalInfo);
            if (info is IInfoScratch_Sprite spriteInfo)
                Add(spriteInfo);
        }


        //Validacion
        public async Task<bool> Exist_Profesor_Curso(int profesorId,
            int cursoId)
        {
            return await _context.Cursos
                .AnyAsync(cur => cur.Id == cursoId
                && cur.ProfesorId == profesorId);

        }

        public async Task<bool> Exist_Estudiante_Curso(int estudianteId,
            int cursoId)
        {
            return await _context.Rel_Cursos_Estudiantes
                .Include(rel => rel.Curso)
                .AnyAsync(rel => rel.EstudianteId == estudianteId &&
                rel.CursoId == cursoId && rel.Curso.Activo);

        }

        public IQueryable<Estudiante> Find_Estudiantes_Finalizaron(int desafioId, int cursoId)
        {
            var consulta = _context.RegistroCalificaiones
                .Where(y => y.DesafioId == desafioId &&
                y.CursoId == cursoId)
                .Select(e => e.EstudianteId);
            var query = _context.Estudiantes
                .Where(est => consulta.Contains(est.Id));
            return query;
        }

        public IQueryable<Estudiante> Find_Estudiantes_No_Finalizaron(int desafioId, int cursoId)
        {

            var est = _context.Rel_Cursos_Estudiantes
                .Include(rel => rel.Estudiante)
                .Where(rel => rel.CursoId == cursoId)
                .Select(rel => rel.Estudiante);

            var estSi = Find_Estudiantes_Finalizaron(desafioId, cursoId);
            var query = est
                .Where(e => !estSi.Contains(e));
            return query.AsQueryable();



        }

        //Notifications

        public void Do_PushNotification(NotificationType type,
            int userId,
            Dictionary<string, string> values)
        {
            Task.Run(() =>
            {
                var not = NotificationBuilder.CreateNotification(type,
                    userId, values);
                Add_Notification(not);
                _notificationCtx.SaveChangesAsync().Wait();
            });
        }
        public void Add_Notification(Notification model)
        {
            _notificationCtx.Add(model);
        }

        public void Edit_Notification(Notification model)
        {
            _notificationCtx.Entry(model).State =
                EntityState.Modified;;
        }

        public async Task<Notification> Find_Notification(int id)
        {
            var model = await _notificationCtx.Notifications
                .FirstOrDefaultAsync(n => n.Id == id);

            return model;
        }
        public async Task<Notification> Find_Notification
            (int userId, string key)
        {
            var model = await _notificationCtx.Notifications
                .FirstOrDefaultAsync(n => n.UsuarioId == userId &&
                n.Key.Equals(key) && n.Unread);

            return model;
        }
        public IQueryable<Notification> GetAll_Notifications(int userId,
            bool unread = true)
        {
            return _notificationCtx.Notifications
                .Where(c => c.UsuarioId.Equals(userId) &&
                c.Unread == unread)
                .OrderByDescending(c => c.Date);
        }
        public async Task Do_MarkAsRead(
            IEnumerable<Notification> notifications)
        {
            foreach (var item in notifications)
            {
                item.Unread = false;
                Edit_Notification(item);
            }
            await _notificationCtx.SaveChangesAsync();
        }
    }
}
