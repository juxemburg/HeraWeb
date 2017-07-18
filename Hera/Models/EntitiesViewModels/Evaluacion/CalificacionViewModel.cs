using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Evaluacion
{
    public class CalificacionViewModel
    {
        public Calificacion Calificacion { get; set; }
        public EvaluacionViewModel Evaluacion { get; set; }

        public CalificacionViewModel(Calificacion cal,
            InfoDesafio infoDesafio)
        {
            Calificacion = cal;
            Evaluacion = new EvaluacionViewModel(cal.ResultadoGeneral,
                infoDesafio);
        }
    }
}
