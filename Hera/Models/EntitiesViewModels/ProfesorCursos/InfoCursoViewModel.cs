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
            return new PieChartViewModel()
            {
                Id = "chart-sex",
                Class = clss,
                Models =  DistSexo,
                ShowLabel = showLabel,
                LabelPosition = labelPosition,
                LabelOffset = labelOffset
            };
        }

        public LineChartViewModel GetActividadCurso(string clss)
        {
            return new LineChartViewModel()
            {
                Id="cart-activity",
                Class = clss,
                DataDictionary = ActividadCurso
            };
        }

        
    }
}
