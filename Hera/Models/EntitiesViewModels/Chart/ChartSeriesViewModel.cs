using System;
using System.Collections.Generic;
using System.Linq;

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

        public static int GetChartMax(IEnumerable<int> collection)
        {
            try
            {
                var max = collection.Max();
                return (int)(max + (max * 0.1f) + 1);
            }
            catch(InvalidOperationException)
            {
                return 4;
            }
        }
    }
}
