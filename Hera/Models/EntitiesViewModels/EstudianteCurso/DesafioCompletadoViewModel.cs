using Entities.Desafios;
using Entities.Valoracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.EstudianteCurso
{
    public class DesafioCompletadoViewModel
    {
        public int CursoId { get; set; }
        public ResultadoScratch Resultado { get; set; }
        public Desafio SiguienteDesafio { get; set; }

        public DesafioCompletadoViewModel(int cursoId,
            ResultadoScratch resultado, Desafio siguienteDesafio)
        {
            this.CursoId = cursoId;
            this.Resultado = resultado;
            this.SiguienteDesafio = siguienteDesafio;
        }
    }
}
