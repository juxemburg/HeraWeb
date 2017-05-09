using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HeraScratch.Valoration;
using HeraScratch.Objects;

namespace HeraScratch.ObjectExtensions
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

            return Get_valoration(obj.Scripts,
                obj.Blocks,
                obj.ScriptsString);
        }
        public static ScratchValoration GeneralEvaluation(
            this ScratchObject obj)
        {
            if (obj.Children == null)
                return ScratchValoration.Default();

            var blocks = new List<string>();
            var scripts = new List<List<object>>();
            var scriptList = new List<string>();
            if(obj.RawScripts != null)
            {
                blocks.AddRange(obj.Blocks);
                scripts.AddRange(obj.Scripts);
                scriptList.AddRange(obj.ScriptsString);
            }
            foreach (var child in obj.Children)
            {
                if (child.Blocks != null )
                {
                    blocks.AddRange(child.Blocks);
                }
                if(child.Scripts != null)
                {
                    scripts.AddRange(child.Scripts);
                }
                if(child.ScriptsString != null)
                {
                    scriptList.AddRange(child.ScriptsString);
                }
            }
            return Get_valoration(scripts, blocks, scriptList);

        }

        private static string Get_firstBlock(List<object> script)
        {
            var i = ((IEnumerable<object>)script[0]).First();
            var item = ((IEnumerable<object>)i).FirstOrDefault();
            if (item != null && 
                typeof(string) == item.GetType())
            {
                var value = !_EventBlocks.ContainsKey(item.ToString());
                return item.ToString();
            }
            return "unknown";

        }
        private static ScratchValoration Get_valoration(
            List<List<object>> scripts, List<string> blocks,
            List<string> scriptList)
        {
            return new ScratchValoration()
            {
                ScriptCount = scripts.Count,
                BlockCount = blocks.Count(),
                BlockFrequency = blocks.GroupBy(b => b)
                .Select(b => new Tuple<string, int>(
                    b.Key,
                    b.Count()))
                .OrderByDescending(i => i.Item2)
                .ToList(),
                DuplicateScriptCount = scriptList
                .GroupBy(i => i)
                .Where(grp => grp.Count() >1)
                .Select(grp => grp.Key)
                .Count(),
                DeadCode = scripts
                               .Where(o =>
                               o.Count > 0 &&
                               !_EventBlocks.ContainsKey(Get_firstBlock(o)))
                               .ToList()

            };
        }
    }

}
