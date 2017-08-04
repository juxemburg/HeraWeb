using System;

namespace Entities.Calificaciones
{
    public class CalificacionCualitativa
    {
        public int Id { get; set; }
        public bool Completada { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; }

        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }
        public RegistroCalificacion RegistroCalificacion { get; set; }
    }
}
