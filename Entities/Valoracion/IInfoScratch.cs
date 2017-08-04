
namespace Entities.Valoracion
{
    public interface IInfoScratch
    {
        int Id { get; set; }

        int ResultadoScratchId { get; set; }
        ResultadoScratch ResultadoScratch { get; set; }
    }
}
