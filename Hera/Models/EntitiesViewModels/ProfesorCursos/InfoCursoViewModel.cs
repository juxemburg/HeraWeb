using Hera.Models.EntitiesViewModels.Chart;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Hera.Models.UtilityViewModels;

namespace Hera.Models.EntitiesViewModels.ProfesorCursos
{
    public class InfoCursoViewModel
    {
        public List<ChartSeriesViewModel> DistSexo { get; set; }
        public Dictionary<string, int> ActividadCurso { get; set; }

        public int ActividadCursoMax =>
            ChartUtil.GetChartMax(ActividadCurso.Values);

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

        public string ActividadCursoJson => 
            JsonConvert.SerializeObject(
            new
            {
                labels = ActividadCurso.Keys.ToList(),
                series = new List<List<int>>()
                {
                    ActividadCurso.Values.ToList()
                }
            });
    }
}
