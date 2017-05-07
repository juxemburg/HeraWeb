using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hera.Services.ScratchServices
{
    public class EvaluationScratch : IEvaluation
    {
        public IValoration General { get; set; }

        public IEnumerable<IValoration> IndividualValorations { get; set; }

        public void Initialize(IValoration general,
            IEnumerable<IValoration> individualValorations)
        {
            General = general;
            IndividualValorations = individualValorations;
        }
    }
}
