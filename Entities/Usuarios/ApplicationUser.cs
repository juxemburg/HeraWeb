
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Entities.Usuarios
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int UsuarioId { get; set; }
    }
}
