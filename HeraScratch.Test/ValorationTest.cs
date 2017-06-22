using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Test
{
    class ValorationTest : IValoration
    {
        public bool generalValoration { get; set; }
        public string SpriteName { get; set; }

        public int ScriptCount { get; set; }
        public int BlockCount { get; set; }
        public int DeadCodeCount { get; set; }
        public int DuplicateScriptCount { get; set; }

        public IValorationInfo AdditionalInfo { get; set; }

        public List<Tuple<string, int>> BlockFrequency { get; set; }
        public List<Tuple<string, string>> DuplicatedScripts { get; set; }
        public List<Tuple<string, string>> DeadScripts { get; set; }
    }
}
