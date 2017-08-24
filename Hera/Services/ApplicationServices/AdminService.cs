using System.Linq;
using System.Threading.Tasks;
using Entities.Usuarios;
using Hera.Data;
using Hera.Models.UtilityViewModels;
using Microsoft.EntityFrameworkCore;

namespace Hera.Services.ApplicationServices
{
    public class AdminService
    {
        private readonly IDataAccess _data;

        public AdminService(IDataAccess data)
        {
            _data = data;
        }

        public async Task<PaginationViewModel<Profesor>>
            Get_Profesores(string searchStrng, int skip, int take)
        {
            var model = await _data.GetAll_Profesor(searchStrng)
                .OrderBy(p => p.NombreCompleto)
                .ToListAsync();

            return new PaginationViewModel<Profesor>(model, skip, take);
        }

        public async Task<bool> Activate_Profesor(int usuarioId,
            bool value)
        {
            if (!await _data.Exist_Profesor(usuarioId))
                return false;

            var profesor = await _data.Find_ProfesorU(usuarioId);
            profesor.Activo = value;
            return await _data.SaveAllAsync();
        }
        
    }
}
