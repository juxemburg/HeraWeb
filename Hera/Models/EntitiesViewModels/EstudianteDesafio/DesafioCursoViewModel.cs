using System.Collections.Generic;
using System.Linq;
using Entities.Cursos;
using Entities.Desafios;
using Hera.Models.EntitiesViewModels.Chart;
using Hera.Models.UtilityViewModels;

namespace Hera.Models.EntitiesViewModels.EstudianteDesafio
{
    public class DesafioCursoViewModel
    {
        public List<SingleValueSeriesViewModel> DistActividad { get; set; }
        public Desafio Desafio { get; set; }
        public int CursoId { get; set; }

        public DesafioCursoViewModel(Desafio desafio, Curso curso)
        {
            CursoId = curso.Id;
            Desafio = desafio;
            var query = desafio.Calificaciones
                .Where(cal => cal.CursoId == CursoId)
                .ToList();
            var total = curso.Estudiantes.Count();
            var numT = query.Count(cal => cal.Terminada);
            
            DistActividad = new List<SingleValueSeriesViewModel>()
            {
                new SingleValueSeriesViewModel()
                {
                    Data = numT,
                    Label = $"{ChartUtil.Percentage(numT, total)}%",
                    Name = "Terminaron"
                },
                new SingleValueSeriesViewModel()
                {
                    Data = total - numT,
                    Label = $"{ChartUtil.Percentage(total - numT, total)}%",
                    Name = "Sin Terminar"
                },
            };
        }

        public PieChartViewModel GetDistActividad(string clss,
            string labelPosition, int labelOffset, bool showLabel = true)
        {
            return ChartUtil.GetPieChartViewModel(DistActividad,
                "chart-activity", clss, labelPosition, labelOffset, 
                showLabel);
        }

    }
}
