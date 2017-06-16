using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HeraScratch.Valoration;
using HeraScratch.Objects;
using System.Text.RegularExpressions;

namespace HeraScratch.ObjectExtensions
{
    static class ScratchObjectExtensions
    {
        private static Dictionary<string, string> _BasicOperators
            = new Dictionary<string, string>()
            {
                ["+"] = "+",
                ["-"] = "-",
                ["*"] = "*",
                ["/"] = "/",
                ["concatenate:with:"] = "concatenate:with:"
            };

        private static Dictionary<string, string> _MediumOperators
            = new Dictionary<string, string>()
            {
                ["<"] = "<",
                ["="] = "=",
                [">"] = ">",
                ["randomFrom:to:"] = "randomFrom:to:"
            };

        private static Dictionary<string, string> _AdvancedOperators
            = new Dictionary<string, string>()
            {
                ["&"] = "&",
                ["|"] = "|",
                ["not"] = "not"
            };

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
                ["doBroadcastAndWait"] = "doBroadcastAndWait",
                ["procDef"] = "procDef",
                ["whenCloned"] = "whenCloned"
            };
        private static Dictionary<string, string> _BasicControlBlocks
            = new Dictionary<string, string>()
            {
                ["doIf"] = "doIf",
                ["stopScripts"] = "whenKeyPressed",
                ["doForever"] = "whenClicked",
                ["doRepeat"] = "doRepeat",
                ["wait:elapsed:from:"] = "wait:elapsed:from:"
            };
        private static Dictionary<string, string> _MediumControlBlocks
            = new Dictionary<string, string>()
            {
                ["doWaitUntil"] = "doWaitUntil",
                ["doUntil"] = "doUntil",
                ["doIfElse"] = "doIfElse"
            };

        private static Dictionary<string, string> _reservedBlocks
            = new Dictionary<string, string>()
            {
                { "a","" },
                { "b","" },
                { "c","" },
                { "d","" },
                { "e","" },
                { "f","" },
                { "g","" },
                { "h","" },
                { "i","" },
                { "j","" },
                { "k","" },
                { "l","" },
                { "m","" },
                { "n","" },
                { "o","" },
                { "p","" },
                { "q","" },
                { "r","" },
                { "s","" },
                { "t","" },
                { "u","" },
                { "v","" },
                { "x","" },
                { "y","" },
                { "z","" },
                { "up arrow",""},
                { "right arrow",""},
                { "left arrow",""},
                { "down arrow",""}

            };

        public static bool IsReservedBlock(string name)
        {
            return _reservedBlocks.ContainsKey(name);
        }
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
            var deadCode = scripts
                .Where(o =>o.Count > 0 &&
                !_EventBlocks.ContainsKey(Get_firstBlock(o)))
                .ToList();
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
                .Where(grp => grp.Count() > 1)
                .Select(grp => grp.Key)
                .Count(),
                DeadCode = deadCode,
                NonUnusedBlocks = deadCode.Count == 0,
                UserDefinedBlocks =
                    blocks.Any(b => b == "procDef"),
                CloneUse =
                     blocks.Any(b => b == "createCloneOf"),
                SecuenceUse =
                        scripts.Any(list =>
                        _EventBlocks.ContainsKey(Get_firstBlock(list))
                        && list.Count > 0),
                MultipleThreads =
                    scripts.Count > 1,
                //EventsUse=
                TwoGreenFlagTrhead =
                    scripts.Where(s =>
                    Get_firstBlock(s).Equals("whenGreenFlag"))
                    .Count() > 1,
                //MessageUse = 
                AdvancedEventUse =
                scripts.GroupBy(s
                        => Get_firstBlock(s))
                        .Count() > 1,
                UseSimpleBlocks =
                    blocks.Any(b => _BasicControlBlocks.ContainsKey(b)),
                UseMediumBlocks =
                    blocks.Any(b => _MediumControlBlocks.ContainsKey(b)),
                //UseNestedControl = 
                BasicInputUse = blocks
                        .Any(b => b == "touching" || b == "whenKeyPressed"),
                VariableUse = blocks
                        .Any(b => b == "setVar:to:"),
                SpriteSensing = blocks
                .Any(b => b == "touching:" || b == "touching"),
                //SaredVariables = 
                //ListUse = 
                BasicOperators = blocks
                .Any(b => _BasicOperators.ContainsKey(b)),

                MediumOperators = blocks
                .Any(b => _MediumOperators.ContainsKey(b)),

                AdvancedOperators = blocks
                .Any(b => _AdvancedOperators.ContainsKey(b)),


            };
        }

        
    }

}
