using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.Test
{
    class ScratchValoration
    {
        public int ScriptCount { get; set; }
        public int BlockCount { get; set; }
        public List<Tuple<string,int>> BlockFrequency { get; set; }
        public List<List<object>> DeadCode { get; set; }

        public static ScratchValoration Default()
        {
            return new ScratchValoration()
            {
                ScriptCount = 0,
                BlockCount = 0,
                BlockFrequency = new List<Tuple<string, int>>()
            };
        }
    }

    
}
