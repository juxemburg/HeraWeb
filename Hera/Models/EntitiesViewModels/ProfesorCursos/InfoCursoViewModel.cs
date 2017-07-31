using Hera.Models.EntitiesViewModels.Chart;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.ProfesorCursos
{
    public class InfoCursoViewModel
    {
        public ChartSeriesViewModel NumNinos { get; set; }
        public ChartSeriesViewModel NumNinas { get; set; }

        public Dictionary<string, int> ActividadCurso { get; set; }
        public int ActividadCursoMax
        {
            get => ChartUtil.GetChartMax(ActividadCurso.Values);
        }
        public string ActividadCursoJson
        {
            get => JsonConvert.SerializeObject(
                new
                {
                    labels = ActividadCurso.Keys.ToList(),
                    series = new List<List<int>>() { ActividadCurso.Values.ToList() }
                });
        }
    }
}
