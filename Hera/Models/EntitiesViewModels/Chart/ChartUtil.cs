using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hera.Models.UtilityViewModels;

namespace Hera.Models.EntitiesViewModels.Chart
{
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
            catch (InvalidOperationException)
            {
                return 4;
            }
        }

        public static PieChartViewModel GetPieChartViewModel(
            List<SingleValueSeriesViewModel> models,string id,
            string clss, string labelPosition, int labelOffset,
            bool showLabel = true)
        {
            return new PieChartViewModel()
            {
                Id = id,
                Class = clss,
                Models = models,
                ShowLabel = showLabel,
                LabelPosition = labelPosition,
                LabelOffset = labelOffset
            };
        }

        public static LineChartViewModel GetLineChartViewModel(
            Dictionary<string, MultiValueSeriesViewModel> models,
            string id, string clss)
        {
            return new LineChartViewModel()
            {
                Id = id,
                Class = clss,
                DataDictionary = models
            };
        }
    }
}
