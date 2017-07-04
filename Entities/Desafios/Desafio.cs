using Entities.Calificaciones;
using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Desafios
{
    public class Desafio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DirDesafioInicial { get; set; }
        public string DirSolucion { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public InfoDesafio InfoDesafio { get; set; }
        public virtual List<RegistroCalificacion> Calificaciones { get; set; }

    }
}
