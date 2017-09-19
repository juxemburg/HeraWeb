using System;

namespace Entities.Calificaciones
{
    public class CalificacionCualitativa
    {
        public int Id { get; set; }
        public bool Completada { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; }

        public int CalificacionId { get; set; }
        public Calificacion Calificacion { get; set; }
    }
}
