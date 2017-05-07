using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.Test
{
    static class ScratchObjectExtensions
    {
        private static Dictionary<string, string> _EventBlocks
            = new Dictionary<string, string>()
            {
                ["whenGreenFlag"] = "whenGreenFlag",
                ["whenKeyPressed"] = "whenKeyPressed",
                ["whenClicked"] = "whenClicked",
                ["whenIReceive"] = "whenIReceive",
                ["whenSceneStarts"] = "whenSceneStarts",
                ["whenSensorGreaterThan"] = "whenSensorGreaterThan",
                ["broadcast:"] = "broadcast:",
                ["doBroadcastAndWait"] = "doBroadcastAndWait"
            };
        public static ScratchValoration Evaluate(this ScratchObject obj)
        {
            if (obj.RawScripts == null)
            {
                return ScratchValoration.Default();
            }


            return new ScratchValoration()
            {
                ScriptCount = obj.Scripts.Count,
                BlockCount = obj.Blocks.Count(),
                BlockFrequency = obj.Blocks.GroupBy(b => b)
                .Select(b => new Tuple<string, int>(
                    b.Key,
                    b.Count()))
                .OrderByDescending(i => i.Item2)
                .ToList(),
                DeadCode = obj.Scripts
                               .Where(o =>
                               o.Count >0 &&
                               !_EventBlocks.ContainsKey(Get_firstBlock(o)))
                               .ToList()

            };
        }
        private static string Get_firstBlock(List<object> script)
        {
            var i = ((IEnumerable<object>)script[0]).First();
            var item = ((IEnumerable<object>)i).FirstOrDefault();
            if(typeof(string) == item.GetType())
            {
                var value = !_EventBlocks.ContainsKey(item.ToString());
                return item.ToString();
            }
            return "";

        }
    }

}
