using System;

namespace Entities.Calificaciones
{
    public class Calificacion
    {
        public int Id { get; set; }

        public int BloquesRepetidos { get; set; }
        public int Inicializacion { get; set; }
        public DateTime Tiempoinicio { get; set; }
        public DateTime? TiempoFinal { get; set; }

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