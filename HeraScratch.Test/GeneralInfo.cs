using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Test
{
    class GeneralInfo : IGeneralValoration
    {
        public int SpriteCount { get; set; }
        public bool SharedVariables { get; set; }
        public bool EventsUse { get; set; }
        public bool MessageUse { get; set; }
        public bool ListUse { get; set; }

        public int NonUnusedBlocks { get; set; }
        public int UserDefinedBlocks { get; set; }
        public int CloneUse { get; set; }
        public int SecuenceUse { get; set; }
        public int MultipleThreads { get; set; }
        public int TwoGreenFlagTrhead { get; set; }
        public int AdvancedEventUse { get; set; }
        public int UseSimpleBlocks { get; set; }
        public int UseMediumBlocks { get; set; }
        public int UseNestedControl { get; set; }
        public int BasicInputUse { get; set; }
        public int VariableUse { get; set; }
        public int SpriteSensing { get; set; }
        public int VariableCreation { get; set; }
        public int SaredVariables { get; set; }
        public int BasicOperators { get; set; }
        public int MediumOperators { get; set; }
        public int AdvancedOperators { get; set; }

        public string Info =>
                $"Shared Variables: {SharedVariables}\n" +
                $"Events used: {EventsUse}\n" +
                $"Messages Used: {MessageUse}\n" +
                $"List Used: {ListUse}\n" +
                $"====================================\n" +
                $"Non unusedBlocks: {NonUnusedBlocks}\n" +
                $"user Defined Blocks: {UserDefinedBlocks}\n" +
                $"Clone use: {CloneUse}\n" +
                $"Secuence use: {SecuenceUse}\n" +
                $"Multiple Threads: {MultipleThreads}\n" +
                $"Two Green Flag Thread: {TwoGreenFlagTrhead}\n" +
                $"Advanced Events Use: {AdvancedEventUse}\n" +
                $"Use of simple blocks: {UseSimpleBlocks}\n" +
                $"Use of medium blocks: {UseMediumBlocks}\n" +
                $"Use of nested control: {UseNestedControl}\n" +
                $"Use of basic input use: {BasicInputUse}\n" +
                $"Use of variables: {VariableUse}\n" +
                $"Use of sprite sensing blocks: {SpriteSensing}\n" +
                $"Use of Variable creation: {VariableCreation}\n" +
                $"Use of sprite sensing blocks: {SaredVariables}\n" +
                $"Use of sprite sensing blocks: {ListUse}\n" +
                $"Basic Operators: {BasicOperators}\n" +
                $"Medium Operators: {MediumOperators}\n" +
                $"Advanced Operators: {AdvancedOperators}\n";

        
    }
}
