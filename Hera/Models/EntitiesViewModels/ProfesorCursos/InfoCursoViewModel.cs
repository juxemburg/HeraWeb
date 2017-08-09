using Hera.Models.EntitiesViewModels.Chart;
using System.Collections.Generic;
using Hera.Models.UtilityViewModels;

namespace Hera.Models.EntitiesViewModels.ProfesorCursos
{
    public class InfoCursoViewModel
    {
        public List<SingleValueSeriesViewModel> DistSexo { get; set; }
        public Dictionary<string, MultiValueSeriesViewModel> ActividadCurso { get; set; }

        

        public PieChartViewModel GetDistribucionSexo(string clss,
            string labelPosition, int labelOffset, bool showLabel = true)
        {
            return ChartUtil.GetPieChartViewModel(DistSexo, "chart-sex",
                clss, labelPosition, labelOffset, showLabel);
        }

        public LineChartViewModel GetActividadCurso(string clss)
        {
            return ChartUtil.GetLineChartViewModel(ActividadCurso, 
                "chart-activity", clss);
        }

        
    }
}
