using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Ratings
{
    public class RateViewModel
    {
        [Range(0,5, ErrorMessage ="el número debe estar entre {1} y {2}")]
        public int Rate { get; set; }
    }
}
