﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Data.SqlClient;
using Entities.Calificaciones;

namespace Hera.Data
{
    public class DataAccess_Sql : IDataAccess
    {
        private ApplicationDbContext _context;

        public DataAccess_Sql(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Get_UserId(IEnumerable<Claim> claims)
        {
            var res = claims
                .Where(c => c.Type.Equals("UsuarioId"))
                .Select(c => c.Value)
                .FirstOrDefault();
            int id = Convert.ToInt32(res);
            return id;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Added;
        }
        public void Edit<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void AddCurso(Curso model)
        {
            _context.Entry<Curso>(model).State = EntityState.Added;
            if (model.Desafio != null)
            {
                _context.Entry<Desafio>(model.Desafio).State = EntityState.Added;
            }

        }

        public void AddDesafio(Desafio model)
        {
            Add<Desafio>(model);
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

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist_Desafio(int id)
        {
            return (await _context.Desafios.FindAsync(id)) != null;
        }

        public async Task<Curso> Find_Curso(int id)
        {
            return await _context.Cursos
                .Where(c => c.Id == id)
                .Include(c => c.Desafio)
                .Include(c => c.Profesor)
                .Include(c => c.Desafios)
                .ThenInclude(c => c.Desafio)
                .Include(c => c.Estudiantes)
                .ThenInclude(rel => rel.Estudiante)
                .FirstOrDefaultAsync();
        }
        public async Task<Curso> Find_Curso_Public(int id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<Desafio> Find_Desafio(int id)
        {
            return await _context.Desafios.FindAsync(id);
        }

        public async Task<Estudiante> Find_Estudiante(int id)
        {
            return await _context.Estudiantes.FindAsync(id);
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
        public async Task<Profesor> Find_Profesor(int id)
        {
            return await _context.Profesores.FindAsync(id);
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
                .Include(c => c.Profesor);
        }
        public IQueryable<Curso> GetAll_Cursos(int profId)
        {
            return _context.Cursos
                .Where(c => c.ProfesorId == profId)
                .Include(c => c.Profesor);
        }
        public IQueryable<Curso> GetAll_CursosEstudiante(int idEst)
        {
            var ids = _context.Rel_Cursos_Estudiantes
                .Where(rel => rel.EstudianteId == idEst)
                .Select(rel => rel.CursoId);

            var query = _context.Cursos
                .Where(cur => ids.Contains(cur.Id))
                .Include(cur => cur.Profesor);
            return query;

        }
        public IQueryable<Curso> Autocomplete_Cursos(string queryString)
        {
            return Autocomplete_Cursos(queryString, null);
        }
        public IQueryable<Curso> Autocomplete_Cursos(string queryString,
            int? profId)
        {
            var query = (profId == null) ? GetAll_Cursos() :
                GetAll_Cursos(profId.GetValueOrDefault());
            query = query.Where(c => c.Nombre.Contains(queryString));
            return query;
        }

        public IQueryable<Desafio> GetAll_Desafios()
        {
            return _context.Desafios;
        }
        public IQueryable<Desafio> Autocomplete_Desafios(string queryString)
        {
            return GetAll_Desafios()
                .Where(d => d.Nombre.Contains(queryString));
        }

        public IQueryable<Estudiante> GetAll_Estudiante()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Profesor> GetAll_Profesor()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<RegistroCalificacion> Find_RegistroCalificacion(
            int cursoId, int estudianteId, int desafioId)
        {
            var query = await _context.RegistroCalificaiones
                .Where(reg => reg.CursoId == cursoId
                && reg.EstudianteId == reg.EstudianteId
                && reg.DesafioId == desafioId)
                .Include(reg => reg.Desafio)
                .Include(reg => reg.Calificaciones)
                .FirstOrDefaultAsync();

            return query;
        }

        public IQueryable<RegistroCalificacion> GetAll_RegistroCalificacion(
            int? cursoId, int? estudianteId, int? desafioId)
        {
            var query =
                (IQueryable<RegistroCalificacion>)
                _context.RegistroCalificaiones;

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

        public async Task<Calificacion> Find_Calificacion(
            int calificacionId)
        {
            return await _context.Calificaciones.FindAsync(calificacionId);
        }
    }
}
