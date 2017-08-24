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
        
    }
}
