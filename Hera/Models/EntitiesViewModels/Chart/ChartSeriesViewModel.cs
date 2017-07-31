using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Chart
{
    public class ChartSeriesViewModel
    {
        public int Data { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
    }

    public static class ChartUtil
    {
        public static double Percentage(int amount, int total)
        {
            return Math.Round((amount / (float)total * 100), 2);
        }
    }
}
