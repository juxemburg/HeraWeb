using Entities.Cursos;
using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
