using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using Entities.Valoracion;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services.ScratchServices
{
    public class Valoration_Scatch : IValoration
    {
        public int ScriptCount { get; set; }
        public int BlockCount { get; set; }
        public List<Tuple<string, int>> BlockFrequency { get; set; }
        public string SpriteName { get; set; }

        public ResultadoScratch Map()
        {
            return new ResultadoScratch()
            {
                Nombre = SpriteName,
                NumBloques = BlockCount,
                NumScripts = ScriptCount,
                Bloques = BlockFrequency
                .Select(b =>
                {
                    return new BloqueScratch()
                    {
                        Nombre = b.Item1,
                        Frecuencia = b.Item2
                    };
                }).ToList()
            };
        }
        public ResultadoScratch Map(int calId)
        {
            return new ResultadoScratch()
            {
                CalificacionId = calId,
                Nombre = SpriteName,
                NumBloques = BlockCount,
                NumScripts = ScriptCount,
                Bloques = BlockFrequency
                .Select(b =>
                {
                    return new BloqueScratch()
                    {
                        Nombre = b.Item1,
                        Frecuencia = b.Item2
                    };
                }).ToList()
            };
        }
    }
}
