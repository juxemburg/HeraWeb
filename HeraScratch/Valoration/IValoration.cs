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

        //Abstraction
        bool NonUnusedBlocks { get; set; }
        bool UserDefinedBlocks { get; set; }
        bool CloneUse { get; set; }

        //Algotimthm Thinkin
        bool SecuenceUse { get; set; }

        //ProblemSolving
        bool MultipleThreads { get; set; }
        bool EventsUse { get; set; }

        //Sync
        bool TwoGreenFlagTrhead { get; set; }
        bool MessageUse { get; set; }
        bool AdvancedEventUse { get; set; }

        //Control
        bool UseSimpleBlocks { get; set; }
        bool UseMediumBlocks { get; set; }
        bool UseNestedControl { get; set; }

        //Input
        bool BasicInputUse { get; set; }
        bool VariableUse { get; set; }
        bool SpriteSensing { get; set; }

        //Representation
        bool VariableCreation { get; set; }
        bool SaredVariables { get; set; }
        bool ListUse { get; set; }

        //Analysis
        bool BasicOperators { get; set; }
        bool MediumOperators { get; set; }
        bool AdvancedOperators { get; set; }



        List<Tuple<string, int>> BlockFrequency { get; set; }
        List<Tuple<string, string>> DuplicatedScripts { get; set; }
        List<Tuple<string, string>> DeadScripts { get; set; }
    }
}
