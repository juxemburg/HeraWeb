using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Valoration
{
    public class ScratchValoration : IValoration
    {
        public string SpriteName { get; set; }
        public bool GeneralValoration { get; set; }

        public int ScriptCount { get; set; }
        public int BlockCount { get; set; }
        public int DeadCodeCount { get; set; }
        public int DuplicateScriptCount { get; set; }

        public List<Tuple<string, int>> BlockFrequency { get; set; }
        public List<Tuple<string, string>> DuplicatedScripts { get; set; }
        public List<Tuple<string, string>> DeadScripts { get; set; }

        #region Valoration Variables

        #region Abstraction

        public bool NonUnusedBlocks { get; set; }
        public bool UserDefinedBlocks { get; set; }
        public bool CloneUse { get; set; }

        #endregion

        #region Algorithm Thinking

        public bool SecuenceUse { get; set; }

        #endregion

        public bool MultipleThreads { get; set; }
        public bool EventsUse { get; set; }
        public bool TwoGreenFlagTrhead { get; set; }
        public bool MessageUse { get; set; }
        public bool AdvancedEventUse { get; set; }
        public bool UseSimpleBlocks { get; set; }
        public bool UseMediumBlocks { get; set; }
        public bool UseNestedControl { get; set; }
        public bool BasicInputUse { get; set; }
        public bool VariableUse { get; set; }
        public bool SpriteSensing { get; set; }
        public bool VariableCreation { get; set; }
        public bool SaredVariables { get; set; }
        public bool ListUse { get; set; }
        public bool BasicOperators { get; set; }
        public bool MediumOperators { get; set; }
        public bool AdvancedOperators { get; set; }

        #endregion

        public List<List<object>> DeadCode { get; set; }
        

        public T Map<T>(string objName, bool generalVal = false)
            where T : IValoration, new()
        {
            return new T()
            {
                SpriteName = objName,
                ScriptCount = this.ScriptCount,
                DeadCodeCount = this.DeadCode.Count,
                BlockCount = this.BlockCount,
                BlockFrequency = this.BlockFrequency,
                DuplicateScriptCount = this.DuplicateScriptCount,
                GeneralValoration = generalVal,
                NonUnusedBlocks = NonUnusedBlocks,
                UserDefinedBlocks = UserDefinedBlocks,
                CloneUse = CloneUse,
                SecuenceUse = SecuenceUse,
                MultipleThreads = MultipleThreads,
                EventsUse = EventsUse,
                TwoGreenFlagTrhead = TwoGreenFlagTrhead,
                MessageUse = MessageUse,
                AdvancedEventUse = AdvancedEventUse,
                UseSimpleBlocks = UseSimpleBlocks,
                UseMediumBlocks = UseMediumBlocks,
                AdvancedOperators = AdvancedOperators,
                BasicOperators = BasicOperators,
                MediumOperators = MediumOperators
            };
        }

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
