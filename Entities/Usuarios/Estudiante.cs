using Entities.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Usuarios
{
    public class Estudiante: IUsuario
    {
        public int Edad { get; set; }

        public virtual List<Rel_CursoEstudiantes> Cursos { get; set; }

    }
}
