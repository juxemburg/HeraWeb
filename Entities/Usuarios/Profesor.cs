using Entities.Cursos;
using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Usuarios
{
    public class Profesor : IUsuario
    {
        public virtual List<Curso> Cursos { get; set; }
        public virtual List<Rel_Rating> Ratings { get; set; }
    }
}
