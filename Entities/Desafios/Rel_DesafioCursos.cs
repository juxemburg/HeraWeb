using Entities.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Desafios
{
    public class Rel_DesafiosCursos
    {
        public int DesafioID { get; set; }
        public Desafio Desafio { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
