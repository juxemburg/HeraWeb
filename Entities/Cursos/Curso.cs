using Entities.Desafios;
using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Cursos
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public virtual List<Rel_CursoEstudiantes> Estudiantes { get; set; }
        public virtual List<Rel_DesafiosCursos> Desafios { get; set; }
    }
}
