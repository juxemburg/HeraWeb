using Entities.Calificaciones;
using System.Collections.Generic;

namespace Entities.Valoracion
{
    public class ResultadoScratch
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NumScripts { get; set; }
        public int NumBloques { get; set; }
        public int DuplicateScriptsCount { get; set; }
        public int DeadCodeCount { get; set; }
        public virtual List<BloqueScratch> Bloques { get; set; }        
        public bool General { get; set; }

        public int CalificacionId { get; set; }
        public Calificacion Calificacion { get; set; }
    }
}
