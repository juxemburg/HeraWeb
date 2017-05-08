using Entities.Valoracion;
using System;
using System.Collections.Generic;

namespace Entities.Calificaciones
{
    public class Calificacion
    {
        public int Id { get; set; }

        public DateTime Tiempoinicio { get; set; }
        public DateTime? TiempoFinal { get; set; }

        //Resultado General
        public int? ResultadoScratchId { get; set; }
        public ResultadoScratch ResultadoScratch { get; set; }

        public virtual List<ResultadoScratch> Resultados { get; set; }

        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }
        public RegistroCalificacion RegistroCalificacion { get; set; }
        public string DirArchivo { get; set; }


        public TimeSpan Duracion { get { return (TiempoFinal - Tiempoinicio).GetValueOrDefault(); } }
        public bool EnCurso
        {
            get
            {
                return TiempoFinal == null;
            }
        }
        public void TerminarCalificacion(string dirArchivo)
        {
            this.DirArchivo = dirArchivo;
            TiempoFinal = DateTime.Now;
        }

    }
}