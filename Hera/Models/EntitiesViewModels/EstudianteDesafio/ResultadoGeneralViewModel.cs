using Entities.Valoracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.EstudianteDesafio
{
    public class ResultadoGeneralViewModel
    {
        public string Nombre { get; set; }
        public int NumScripts { get; set; }
        public int NumBloques { get; set; }
        public int DuplicateScriptsCount { get; set; }
        public int DeadCodeCount { get; set; }

        public ResultadoGeneralViewModel(ResultadoScratch model)
        {
            this.Nombre = model.Nombre;
            this.NumScripts = model.NumScripts;
            this.NumBloques = model.NumBloques;
            this.DuplicateScriptsCount = model.DuplicateScriptsCount;
            this.DeadCodeCount = model.DeadCodeCount;
        }
    }
}
