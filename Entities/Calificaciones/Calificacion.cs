using Entities.Valoracion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Calificaciones
{
    public class Calificacion
    {
        public int Id { get; set; }

        public DateTime Tiempoinicio { get; set; }
        public DateTime? TiempoFinal { get; set; }

        public virtual List<ResultadoScratch> Resultados { get; set; }

        public int? CalificacionCualitativaId { get; set; }
        public CalificacionCualitativa CalificacionCualitativa { get; set; }

        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }
        public RegistroCalificacion RegistroCalificacion { get; set; }
        public string DirArchivo { get; set; }


        public TimeSpan Duracion => (TiempoFinal - Tiempoinicio).GetValueOrDefault();
        public bool EnCurso => TiempoFinal == null;

        public void TerminarCalificacion(string dirArchivo)
        {
            DirArchivo = dirArchivo;
            TiempoFinal = DateTime.Now;
        }
        public ResultadoScratch ResultadoGeneral
        {
            get
            {
                return Resultados != null ? Resultados
                    .FirstOrDefault(res => res.General) : null;
            }
        }

    }
}