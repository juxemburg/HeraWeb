
namespace Entities.Valoracion
{
    public class BloqueScratch
    {
        public int Id { get; set; }
        public int ResultadoScratchId { get; set; }
        public ResultadoScratch ResultadoScratch { get; set; }

        public string Nombre { get; set; }
        public int Frecuencia { get; set; }
    }
}
