using Entities.Valoracion;
using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services.ScratchServices
{
    public class SpriteInfo : ISpriteValoration, IScratchAdapter
    {

        public bool HasEvents { get; set; }
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
        public bool BasicOperators { get ; set ; }
        public bool MediumOperators { get ; set ; }
        public bool AdvancedOperators { get ; set ; }

        public string Info => "";

        public IInfoScratch Map()
        {
            return new IInfoScratch_Sprite()
            {
                //Abst
                NonUnusedBlocks = this.NonUnusedBlocks,
                UserDefinedBlocks = this.UserDefinedBlocks,
                CloneUse = this.CloneUse,

                //Alg Thnk
                SecuenceUse = this.SecuenceUse,

                //Sync
                MultipleThreads = this.MultipleThreads,

                //Probl solv
                TwoGreenFlagTrhead = this.TwoGreenFlagTrhead,                
                AdvancedEventUse = this.AdvancedEventUse,

                //Flux Control
                UseSimpleBlocks = this.UseSimpleBlocks,
                UseMediumBlocks = this.UseMediumBlocks,
                UseNestedControl = this.UseNestedControl,

                //Input
                BasicInputUse = this.BasicInputUse,
                VariableUse = this.VariableUse,
                SpriteSensing = this.SpriteSensing,
                
                //Analysis
                BasicOperators = this.BasicOperators,
                MediumOperators = this.MediumOperators,
                AdvancedOperators = this.AdvancedOperators
            };
        }

        
    }
}
