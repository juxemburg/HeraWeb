using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Valoracion
{
    public interface IInfoScratch
    {
        int Id { get; set; }

        int ResultadoScratchId { get; set; }
        ResultadoScratch ResultadoScratch { get; set; }
    }
}
