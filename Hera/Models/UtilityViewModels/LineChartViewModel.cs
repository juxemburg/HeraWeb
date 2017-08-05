using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hera.Models.UtilityViewModels
{
    public class LineChartViewModel
    {
        public List<string> Labels { get; set; }
        public List<List<int>> Series { get; set; }

        public string ToJson => JsonConvert.SerializeObject(
            new
            {
                labels = Labels,
                series = Series
            });
    }
}
