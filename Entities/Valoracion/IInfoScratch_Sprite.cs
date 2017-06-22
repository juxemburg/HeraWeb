using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Valoracion
{
    public class IInfoScratch_Sprite : IInfoScratch
    {
        public int Id { get; set; }
        public int ResultadoScratchId { get; set; }
        public ResultadoScratch ResultadoScratch { get; set; }
        public bool NonUnusedBlocks { get; set; }
        public bool UserDefinedBlocks { get; set; }
        public bool CloneUse { get; set; }
        public bool SecuenceUse { get; set; }
        public bool MultipleThreads { get; set; }
        public bool TwoGreenFlagTrhead { get; set; }
        public bool AdvancedEventUse { get; set; }
        public bool UseSimpleBlocks { get; set; }
        public bool UseMediumBlocks { get; set; }
        public bool UseNestedControl { get; set; }
        public bool BasicInputUse { get; set; }
        public bool VariableUse { get; set; }
        public bool SpriteSensing { get; set; }
        public bool VariableCreation { get; set; }
        public bool BasicOperators { get; set; }
        public bool MediumOperators { get; set; }
        public bool AdvancedOperators { get; set; }
        
    }
}
