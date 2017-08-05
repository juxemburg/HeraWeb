using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Chart
{
    public class MultiValueSeriesViewModel
    {
        public IEnumerable<float> Data { get; set; }
        public IEnumerable<string> Labels { get; set; }
        public string Name { get; set; }

        public float Max => ChartUtil.GetChartMax(Data);
    }
}
