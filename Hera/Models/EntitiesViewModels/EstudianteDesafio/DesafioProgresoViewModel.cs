using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Calificaciones;

namespace Hera.Models.EntitiesViewModels.EstudianteDesafio
{
    public class DesafioProgresoViewModel
    {
        public int CursoId { get; set; }
        public string NombreCurso { get; set; }
        public string NombreDesafio { get; set; }

        public List<Calificacion> Calificaciones { get; set; }
    }
}
