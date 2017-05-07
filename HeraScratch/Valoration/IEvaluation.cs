using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Valoration
{
    public interface IEvaluation
    {
        IValoration General { get; set; }

        IEnumerable<IValoration> IndividualValorations { get; set; }

        void Initialize(IValoration general,
            IEnumerable<IValoration> individualValorations);
    }
}
