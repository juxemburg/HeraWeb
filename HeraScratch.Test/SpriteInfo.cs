using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Test
{
    class SpriteInfo : ISpriteValoration
    {
        public bool NonUnusedBlocks { get ; set ; }
        public bool UserDefinedBlocks { get ; set ; }
        public bool CloneUse { get ; set ; }
        public bool SecuenceUse { get ; set ; }
        public bool MultipleThreads { get ; set ; }
        public bool TwoGreenFlagTrhead { get ; set ; }
        public bool AdvancedEventUse { get ; set ; }
        public bool UseSimpleBlocks { get ; set ; }
        public bool UseMediumBlocks { get ; set ; }
        public bool UseNestedControl { get ; set ; }
        public bool BasicInputUse { get ; set ; }
        public bool VariableUse { get ; set ; }
        public bool SpriteSensing { get ; set ; }
        public bool VariableCreation { get ; set ; }
        public bool SaredVariables { get ; set ; }
        public bool ListUse { get ; set ; }
        public bool BasicOperators { get ; set ; }
        public bool MediumOperators { get ; set ; }
        public bool AdvancedOperators { get ; set ; }

        public string Info => $"Non unusedBlocks: {NonUnusedBlocks}\n" +
                $"user Defined Blocks: {UserDefinedBlocks}\n" +
                $"Clone use: {CloneUse}\n" +
                $"Secuence use: {SecuenceUse}\n" +
                $"Multiple Threads: {MultipleThreads}\n" +
                $"Two Green Flag Thread: {TwoGreenFlagTrhead}\n" +
                $"TwoGreenFlagThread: {MultipleThreads}\n" +
                $"Advanced Events Use: {AdvancedEventUse}\n" +
                $"Use of simple blocks: {UseSimpleBlocks}\n" +
                $"Use of medium blocks: {UseMediumBlocks}\n" +
                $"Use of nested control: {UseNestedControl}\n" +
                $"Use of basic input use: {BasicInputUse}\n" +
                $"Use of variables: {VariableUse}\n" +
                $"Use of sprite sensing blocks: {SpriteSensing}\n" +
                $"Use of sprite sensing blocks: {VariableCreation}\n" +
                $"Use of sprite sensing blocks: {SaredVariables}\n" +
                $"Use of sprite sensing blocks: {ListUse}\n" +
                $"Basic Operators: {BasicOperators}\n" +
                $"Medium Operators: {MediumOperators}\n" +
                $"Advanced Operators: {AdvancedOperators}\n";
    }
}
