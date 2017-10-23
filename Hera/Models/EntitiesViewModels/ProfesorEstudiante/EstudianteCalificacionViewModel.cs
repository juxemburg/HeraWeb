using System.Collections.Generic;
using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Usuarios;

namespace Hera.Models.EntitiesViewModels.ProfesorEstudiante
{
    public class EstudianteCalificacionViewModel
    {
        public EstudianteCalificacionViewModel(Rel_CursoEstudiantes model)
        {
            CursoId = model.CursoId;
            Estudiante = model.Estudiante;
            Calificaciones = model.Registros;
        }

        public int CursoId { get; set; }
        public Estudiante Estudiante { get; set; }
        public List<RegistroCalificacion> Calificaciones { get; set; }
    }
}
