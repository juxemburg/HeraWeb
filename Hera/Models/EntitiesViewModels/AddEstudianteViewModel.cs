using Entities.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels
{
    public class AddEstudianteViewModel
    {        
        public string Password { get; set; }        
        public int CursoId { get; set; }


        public Rel_CursoEstudiantes Map(int cursoId, int estudianteId) {
            return new Rel_CursoEstudiantes()
            {
                CursoId = cursoId,
                EstudianteId = estudianteId
            };
        }
    }

    
}
