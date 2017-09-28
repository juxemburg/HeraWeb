using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.AccountViewModels
{
    public class RegisterProfesorViewModel : RegisterViewModel
    {
        public Profesor Map(int usuarioId)
        {
            return new Profesor()
            {
                UsuarioId = usuarioId,
                Nombres = this.Nombres,
                Apellidos = this.Apellidos                
            };
        }
    }
}
