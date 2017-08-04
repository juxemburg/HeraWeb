

namespace HeraScratch.Valoration
{
    public interface ISpriteValoration : IValorationInfo
    {
        bool HasEvents { get; set; }

        //Abstraction

        bool NonUnusedBlocks { get; set; }
        bool UserDefinedBlocks { get; set; }
        bool CloneUse { get; set; }

        //Algotimthm Thinkin
        bool SecuenceUse { get; set; }

        //ProblemSolving
        bool MultipleThreads { get; set; }

        //Sync
        bool TwoGreenFlagTrhead { get; set; }
        bool AdvancedEventUse { get; set; }

        //Control
        bool UseSimpleBlocks { get; set; }
        bool UseMediumBlocks { get; set; }
        bool UseNestedControl { get; set; }// To-do

        //Input
        bool BasicInputUse { get; set; }
        bool VariableUse { get; set; }
        bool SpriteSensing { get; set; }

        //Representation
        bool VariableCreation { get; set; }//To-do

        //Analysis
        bool BasicOperators { get; set; }
        bool MediumOperators { get; set; }
        bool AdvancedOperators { get; set; }
    }
}
