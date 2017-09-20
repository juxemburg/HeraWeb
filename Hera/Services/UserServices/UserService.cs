using Hera.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hera.Services.UserServices
{
    public class UserService
    {
        private readonly IDataAccess _data;

        public UserService(IDataAccess data)
        {
            _data = data;
        }

        public int Get_UserId(IEnumerable<Claim> claims)
        {
            return _data.Get_UserId(claims);
        }

        public int Get_Id(IEnumerable<Claim> claims)
        {
            var claimsList = ToList(claims);

            if (Get_inRole(claimsList, "IsEstudiante"))
                return Get_EstudianteId(claimsList);
            if (Get_inRole(claimsList, "IsProfesor"))
                return Get_ProfesorId(claimsList);
            return -1;
        }

        public int Get_EstudianteId(IEnumerable<Claim> claims)
        {
            var enumerable = ToList(claims);
            var id = _data.Get_UserId(enumerable);
            return (id == -1 || !Get_inRole(enumerable, "IsEstudiante")) 
                ? -1 : _data.Find_EstudianteId(id).Result;
        }

        public async Task<int?> Get_EstudianteUserId(int id)
        {
            return (await _data.Find_Estudiante(id))?.UsuarioId;
        }

        public int Get_ProfesorId(IEnumerable<Claim> claims)
        {
            var claimsList = ToList(claims);
            var id = _data.Get_UserId(claimsList);
            return (id == -1 || !Get_inRole(claimsList, "IsProfesor")) 
                ? -1 : _data.Find_ProfesorId(id).Result;
        }

        public async Task<int?> Get_ProfesorUserId(int id)
        {
            return (await _data.Find_Profesor(id))?.UsuarioId;
        }

        private List<Claim> ToList(IEnumerable<Claim> enumerable)
        {
            return enumerable as List<Claim> ?? enumerable.ToList();
        }

        private bool Get_inRole(IEnumerable<Claim> claims, string role)
        {
            try
            {
                return claims.Any(c => c.Type.Equals(role));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
