using Entities.Valoracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.EstudianteDesafio
{
    public class ResultadosScratchViewModel
    {
        public ResultadoScratch ResultadoGeneral { get; set; }
        public List<ResultadoScratch> ScriptsResultados { get; set; }

        public ResultadosScratchViewModel(
            IEnumerable<ResultadoScratch> resultados)
        {
            ResultadoGeneral = resultados
                .FirstOrDefault(res => res.General);
            ScriptsResultados = resultados
                .Where(res => !res.General)
                .OrderBy(res => res.Nombre)
                .ToList();
        }
    }
}
