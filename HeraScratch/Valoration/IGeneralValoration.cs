using System;
using System.Collections.Generic;
using System.Text;

namespace HeraScratch.Valoration
{
    public interface IGeneralValoration : IValorationInfo
    {

        bool EventsUse { get; set; }// General Variable
        bool MessageUse { get; set; }// To-do General Variable
        bool SharedVariables { get; set; }//To-do
        bool ListUse { get; set; }//To-do

        //Abstraction
        int NonUnusedBlocks { get; set; }
        int UserDefinedBlocks { get; set; }
        int CloneUse { get; set; }

        //Algotimthm Thinkin
        int SecuenceUse { get; set; }

        //ProblemSolving
        int MultipleThreads { get; set; }

        //Sync
        int TwoGreenFlagTrhead { get; set; }
        int AdvancedEventUse { get; set; }

        //Control
        int UseSimpleBlocks { get; set; }
        int UseMediumBlocks { get; set; }
        int UseNestedControl { get; set; }// To-do

        //Input
        int BasicInputUse { get; set; }
        int VariableUse { get; set; }
        int SpriteSensing { get; set; }

        //Representation
        int VariableCreation { get; set; }//To-do

        //Analysis
        int BasicOperators { get; set; }
        int MediumOperators { get; set; }
        int AdvancedOperators { get; set; }
    }
}
