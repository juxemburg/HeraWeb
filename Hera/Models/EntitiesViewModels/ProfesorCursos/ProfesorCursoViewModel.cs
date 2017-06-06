using Entities.Calificaciones;
using Entities.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.ProfesorCursos
{
    public class ProfesorCursoViewModel
    {
        public Curso Curso { get; set; }

        public Dictionary<Tuple<int,int>, List<RegistroCalificacion>>
            RegistrosCurso { get; set; }

        public ProfesorCursoViewModel(Curso curso, 
            Dictionary<Tuple<int, int>,
                List<RegistroCalificacion>> registroCurso)
        {
            this.Curso = curso;
            this.RegistrosCurso = registroCurso;
        }
    }
}
