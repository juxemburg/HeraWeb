using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Evaluacion
{
    public class PropertyViewModel
    {
        public float Value { get; set; }
        public string Name { get; set; }

        public bool Visible { get => Value >= 0; }

        public PropertyViewModel(string name, float value)
        {
            Value = value;
            Name = name;
        }
    }
}
