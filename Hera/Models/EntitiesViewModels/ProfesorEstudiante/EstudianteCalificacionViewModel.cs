using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels
{
    public class EstudianteCalificacionViewModel
    {
        public EstudianteCalificacionViewModel(Rel_CursoEstudiantes model)
        {
            this.CursoId = model.CursoId;
            this.Estudiante = model.Estudiante;
            this.Calificaciones = model.Registros;
        }

        public int CursoId { get; set; }
        public Estudiante Estudiante { get; set; }
        public List<RegistroCalificacion> Calificaciones { get; set; }
    }
}
