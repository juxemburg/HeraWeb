using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Valoration
{
    public interface IValoration
    {
        bool GeneralValoration { get; set; }
        string SpriteName { get; set; }
        int ScriptCount { get; set; }
        int BlockCount { get; set; }
        int DeadCodeCount { get; set; }
        int DuplicateScriptCount { get; set; }
        List<Tuple<string, int>> BlockFrequency { get; set; }
    }
}
