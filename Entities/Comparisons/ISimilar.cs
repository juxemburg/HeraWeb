using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Comparisons
{
    interface ISimilar
    {
        bool IsEqualTo(object obj);
        bool IsSimilarTo(object obj);
    }
}
