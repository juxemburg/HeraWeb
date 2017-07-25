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
        private IDataAccess _data;

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
            var id = _data.Get_UserId(claims);
            if (Get_inRole(claims, "IsEstudiante"))
                return Get_EstudianteId(claims);
            if (Get_inRole(claims, "IsProfesor"))
                return Get_ProfesorId(claims);
            return -1;
        }

        public int Get_EstudianteId(IEnumerable<Claim> claims)
        {
            var id = _data.Get_UserId(claims);
            return (id == -1 || !Get_inRole(claims, "IsEstudiante")) 
                ? -1 : _data.Find_EstudianteId(id).Result;
        }

        public int Get_ProfesorId(IEnumerable<Claim> claims)
        {
            var id = _data.Get_UserId(claims);
            return (id == -1 || !Get_inRole(claims, "IsProfesor")) 
                ? -1 : _data.Find_ProfesorId(id).Result;
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
