using Entities.Cursos;
using Entities.Desafios;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Calificaciones
{
    public class RegistroCalificacion
    {
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public Rel_CursoEstudiantes Rel_CursoEstudiantes { get; set; }

        public int DesafioId { get; set; }
        public Desafio Desafio { get; set; }

        public virtual List<Calificacion> Calificaciones { get; set; }
        public virtual CalificacionCualitativa CalificacionCualitativa { get; set; }

        public bool Iniciada
        {
            get
            {
                return (Calificaciones == null) || Calificaciones
                           .Any(cal => cal.EnCurso);
            }
        }

        public bool Terminada
        {
            get
            {
                return (Calificaciones != null) 
                    && (Calificaciones.Any(cal => !cal.EnCurso)
                    && Calificaciones.Count > 0);
            }
        }

        public Calificacion CalificacionPendiente
        {
            get
            {
                return Calificaciones
                    .Where(cal => cal.EnCurso)
                    .FirstOrDefault();
            }
        }

    }
}
