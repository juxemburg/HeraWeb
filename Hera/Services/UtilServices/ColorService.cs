using Entities.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services.UtilServices
{
    public class ColorService
    {
        private static Random _random = new Random();
        private static Color[] _colors 
            = (Color[])Enum.GetValues(typeof(Color));

        public Color RandomColor {
            get => _colors[_random.Next(_colors.Length)];
        }

    }
}
