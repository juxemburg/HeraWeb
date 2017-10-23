using System.Collections.Generic;
using Entities.Calificaciones;
using Hera.Models.EntitiesViewModels.Evaluacion;

namespace Hera.Models.EntitiesViewModels.ProfesorEstudiante
{
    public class CalificacionesViewModel
    {
        public string NombreCurso { get; set; }
        public string NombreEstudiante { get; set; }
        public string NombreDesafio { get; set; }
        public RegistroCalificacion Registro { get; set; }
        public List<CalificacionViewModel> CalificacionList { get; set; }


        public CalificacionesViewModel()
        {
            
        }

        public CalificacionesViewModel(string nombreCurso,
            string nombreEstudiante, string nombreDesafio,
            List<CalificacionViewModel> calificacionList,
            RegistroCalificacion registro)
        {
            NombreCurso = nombreCurso;
            NombreEstudiante = nombreEstudiante;
            NombreDesafio = nombreDesafio;
            Registro = registro;
            CalificacionList = calificacionList;
        }
    }
}
