using Entities.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Usuarios
{
    public class Profesor : IUsuario
    {
        public virtual List<Curso> Cursos { get; set; }
    }
}
