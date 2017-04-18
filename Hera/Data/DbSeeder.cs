using Entities.Usuarios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSeeder(UserManager<ApplicationUser> userMgr,
            RoleManager<IdentityRole> roleMgr,
            ApplicationDbContext context,
            IDataAccess dataAccess)
        {
            _roleMgr = roleMgr;
            _userMgr = userMgr;
        }

        public async Task Seed()
        {
            await seedRoles();
            var user = await _userMgr.FindByEmailAsync("juanmontano@unicauca.edu.co");
            if(user == null)
                await seedUser("juanmontano@unicauca.edu.co");

            await seedData();
        }

        private async Task seedData()
        {
            
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
