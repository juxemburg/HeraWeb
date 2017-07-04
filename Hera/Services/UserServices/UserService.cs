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


        public async Task<int> Get_EstudianteId(IEnumerable<Claim> claims)
        {
            var id = _data.Get_UserId(claims);
            return await _data.Find_EstudianteId(id);
        }

        public async Task<int> Get_ProfesorId(IEnumerable<Claim> claims)
        {
            var id = _data.Get_UserId(claims);
            return await _data.Find_ProfesorId(id);
        }
    }
}
