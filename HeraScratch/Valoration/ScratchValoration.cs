using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Valoration
{
    public class ScratchValoration : IValoration
    {
        public int ScriptCount { get; set; }
        public int BlockCount { get; set; }
        public List<Tuple<string,int>> BlockFrequency { get; set; }
        public List<List<object>> DeadCode { get; set; }
        public string SpriteName { get; set; }

        public override string ToString()
        {
            var res = $"Script Count: {ScriptCount}\n" +
                $"Block Count:{BlockCount}\n" +
                $"Dead Code: {DeadCode.Count}\n" +
                $"Frecuency: \n";
            foreach (var item in BlockFrequency)
            {
                res += $"Block: {item.Item1} |=> {item.Item2} \n";
            }

            return res;
        }

        public static ScratchValoration Default()
        {
            return new ScratchValoration()
            {
                ScriptCount = 0,
                BlockCount = 0,
                BlockFrequency = new List<Tuple<string, int>>(),
                DeadCode = new List<List<object>>()
            };
        }
    }

    
}
