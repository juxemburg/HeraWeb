using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public void AddCurso(Curso model)
        {
            _context.Entry<Curso>(model).State = EntityState.Added;
            if(model.Desafio != null)
            {
                _context.Entry<Desafio>(model.Desafio).State = EntityState.Added;
            }

        }

        public void AddDesafio(Desafio model)
        {
            Add<Desafio>(model);
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

        public Task<Curso> Find_Curso()
        {
            throw new NotImplementedException();
        }

        public async Task<Desafio> Find_Desafio(int id)
        {
            return await _context.Desafios.FindAsync(id);
        }

        public Task<Estudiante> Find_Estudiante(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> Find_Profesor(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Find_ProfesorId(int usuarioId)
        {
            var id = await _context.Profesores
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

        public IQueryable<Desafio> GetAll_Desafios()
        {
            return _context.Desafios;
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
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
