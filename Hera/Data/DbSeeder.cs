using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hera.Data
{
    public class DbSeeder
    {
        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<ApplicationUser> _userMgr;
        private IDataAccess _data;
        private ApplicationDbContext _context;

        public DbSeeder(UserManager<ApplicationUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            ApplicationDbContext context,
            IDataAccess dataAccess)
        {
            _data = dataAccess;
            _roleMgr = roleMgr;
            _userMgr = userMgr;
            _context = context;
        }

        public async Task ClearDatabase()
        {
            _context.Database
                .ExecuteSqlCommand("Delete from [BloquesScratch]");
            _context.Database
                .ExecuteSqlCommand("Delete From [ResultadosScratch]");
            _context.Database
                .ExecuteSqlCommand("Delete From [Calificaciones]");
            _context.Database
                .ExecuteSqlCommand("Delete From [CalificacionesCualitativas]");
            _context.Database
                .ExecuteSqlCommand("Delete From [RegistroCalificaiones]");
            await _data.SaveAllAsync();
        }

        public async Task Seed()
        {
            await seedRoles();
            var user = await _userMgr.FindByEmailAsync("juanmontano@unicauca.edu.co");
            if(user == null)
                await seedUser("juanmontano@unicauca.edu.co");

            //await seedData();
        }

        private async Task seedData()
        {
            for(int i =1; i<= 40; i++)
            {
                var curso = new Curso()
                {
                    Nombre = "Curso " + i,
                    Desafio = new Desafio()
                    {
                        Nombre = "Desafio " + i,
                        Dificultad = 4,
                        DirDesafioInicial = "/dir/dir/dir"
                    },
                    Password = "pass",
                    ProfesorId = 1,
                    Descripcion = "desc curso " + i
                };
                _data.AddCurso(curso);
                await _data.SaveAllAsync();
                
            }
        }

        private async Task seedUser(string email)
        {
            var user = new ApplicationUser()
            {
                UserName = email,
                Email = email
            };

            var userResult = 
                await _userMgr.CreateAsync(user, "le5Ux1>76");
            var roleResult = 
                await _userMgr.AddToRoleAsync(user, "Admin");
            var claimResult = 
                await _userMgr.AddClaimAsync(user,
                new Claim("SuperUser", "True"));

            if (!userResult.Succeeded || 
                !roleResult.Succeeded || 
                !claimResult.Succeeded)
            {
                throw new InvalidOperationException("Error en la construccion de usuarios");
            }
        }

        private async Task seedRoles()
        {
            
            if (!(await _roleMgr.RoleExistsAsync("Admin")))
            {
                var role = new IdentityRole("Admin");
                role.Claims.Add(
                    new IdentityRoleClaim<string>()
                    { ClaimType = "IsAdmin", ClaimValue = "True" });
                await _roleMgr.CreateAsync(role);
            }

            if (!(await _roleMgr.RoleExistsAsync("Profesor")))
            {
                var role = new IdentityRole("Profesor");
                role.Claims.Add(
                    new IdentityRoleClaim<string>()
                    { ClaimType = "IsProfesor", ClaimValue = "True" });
                await _roleMgr.CreateAsync(role);
            }

            if (!(await _roleMgr.RoleExistsAsync("Estudiante")))
            {
                var role = new IdentityRole("Estudiante");
                role.Claims.Add(
                    new IdentityRoleClaim<string>()
                    { ClaimType = "IsEstudiante", ClaimValue = "True" });
                await _roleMgr.CreateAsync(role);
            }
        }

       
    }
}
