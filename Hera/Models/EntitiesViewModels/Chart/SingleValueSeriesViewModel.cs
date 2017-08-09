using System;
using System.Collections.Generic;
using System.Linq;

namespace Hera.Models.EntitiesViewModels.Chart
{
    public class SingleValueSeriesViewModel
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

        public static float GetChartMax(IEnumerable<float> collection)
        {
            try
            {
                var max = collection.Max();
                return (max + (max * 0.15f));
            }
            catch(InvalidOperationException)
            {
                return 4;
            }
        }
    }
}
