using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Test
{
    class ValorationTest : IValoration
    {
        public bool GeneralValoration {get; set;}
        public string SpriteName {get; set;}

        public int ScriptCount {get; set;}
        public int BlockCount {get; set;}
        public int DeadCodeCount {get; set;}
        public int DuplicateScriptCount { get; set; }

        public List<Tuple<string, int>> BlockFrequency {get; set;}
        public List<Tuple<string, string>> DuplicatedScripts { get; set; }
        public List<Tuple<string, string>> DeadScripts { get; set; }

        public bool NonUnusedBlocks { get; set; }
        public bool UserDefinedBlocks { get; set; }
        public bool CloneUse { get; set; }
        public bool SecuenceUse { get; set; }

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
    }
}
