using Entities.Calificaciones;

namespace Hera.Models.EntitiesViewModels.ProfesorEstudiante
{
    public class CalificacionesViewModel
    {
        public string NombreCurso { get; set; }
        public string NombreEstudiante { get; set; }
        public string NombreDesafio { get; set; }
        public RegistroCalificacion Registro { get; set; }


        public CalificacionesViewModel()
        {
            
        }

        public CalificacionesViewModel(string nombreCurso,
            string nombreEstudiante, string nombreDesafio,
            RegistroCalificacion registro)
        {
            NombreCurso = nombreCurso;
            NombreEstudiante = nombreEstudiante;
            NombreDesafio = nombreDesafio;
            Registro = registro;
        }
    }
}
