using Entities.Calificaciones;
using Entities.Usuarios;
using System.Collections.Generic;

namespace Entities.Cursos
{
    public class Rel_CursoEstudiantes
    {
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public virtual List<RegistroCalificacion> Registros { get; set; }
    }
}
