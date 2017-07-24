using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Usuarios
{
    public abstract class IUsuario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public string NombreCompleto
        {
            get
            {
                return Nombres + " " + Apellidos;
            }
        }
        

    }
}
