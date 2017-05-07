using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Valoration
{
    public interface IValoration
    {
        string SpriteName { get; set; }
        int ScriptCount { get; set; }
        int BlockCount { get; set; }
        List<Tuple<string, int>> BlockFrequency { get; set; }
    }
}
