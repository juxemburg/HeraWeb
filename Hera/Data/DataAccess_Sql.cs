using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Hera.Data
{
    public class DataAccess_Sql : IDataAccess
    {
        private ApplicationDbContext _context;

        public DataAccess_Sql(ApplicationDbContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Entry<T>(entity).State = EntityState.Added;
        }

        public void AddCurso(Curso model)
        {
            throw new NotImplementedException();
        }

        public void AddDesafio(Desafio model)
        {
            throw new NotImplementedException();
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

        public Task<Curso> Find_Curso()
        {
            throw new NotImplementedException();
        }

        public Task<Desafio> Find_Desafio()
        {
            throw new NotImplementedException();
        }

        public Task<Estudiante> Find_Estudiante(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Profesor> Find_Profesor(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Curso> GetAll_Cursos()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Desafio> GetAll_Desafios()
        {
            throw new NotImplementedException();
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
