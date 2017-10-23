using Entities.Calificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels
{
    public class AddCalificacionViewModel
    {
        public int BloquesRepetidos { get; set; }
        public int Inicializacion { get; set; }
        public DateTime Tiempoinicio { get; set; }
        public DateTime? TiempoFinal { get; set; }

        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }
        public RegistroCalificacion RegistroCalificacion { get; set; }
        public string DirArchivo { get; set; }

        public AddCalificacionViewModel Map()
        {
            return new AddCalificacionViewModel()
            {
                BloquesRepetidos = this.BloquesRepetidos,
                Inicializacion = this.Inicializacion,
                Tiempoinicio = this.Tiempoinicio,
                CursoId = this.CursoId,
                EstudianteId = this.EstudianteId,
                DesafioId = this.DesafioId
            };
        }

        
    }
}
