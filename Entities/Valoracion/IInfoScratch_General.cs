

namespace Entities.Valoracion
{
    public class IInfoScratch_General : IInfoScratch
    {
        
        public int Id { get; set; }

        
        public int ResultadoScratchId { get; set; }
        public ResultadoScratch ResultadoScratch { get; set; }

        public int SpriteCount { get; set; }

        public bool EventsUse { get; set; }
        public bool MessageUse { get; set; }
        public bool SharedVariables { get; set; }
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
        public int BasicOperators { get; set; }
        public int MediumOperators { get; set; }
        public int AdvancedOperators { get; set; }


        

    }
}
