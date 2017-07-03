using Hera.Services.ScratchServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.ServicesViewModels.Valoration
{
    public class GeneralValorationViewModel
    {
        public GeneralValorationViewModel() { }

        public GeneralValorationViewModel(GeneralInfo info)
        {
            MultipleSpriteEvents = info.EventsUse;
            MessageUse = info.MessageUse;
            VariableUse = info.SharedVariables;
            ListUse = info.ListUse;

            var passFactor = 0.45f;

            //Abs
            NonUnusedBlocks =
                (info.NonUnusedBlocks / (float)info.SpriteCount) > passFactor;
            UserDefinedBlocks =
                (info.UserDefinedBlocks / (float)info.SpriteCount) > passFactor;
            CloneUse = (info.CloneUse / (float)info.SpriteCount) > passFactor;

            //Alg Thnk
            SecuenceUse =
                (info.SecuenceUse / (float)info.SpriteCount) > passFactor;
            MultipleThreads =
                (info.MultipleThreads / (float)info.SpriteCount) > passFactor;
            //Sync 
            TwoGreenFlagThread =
                (info.TwoGreenFlagTrhead / (float)info.SpriteCount) > passFactor;
            AdvancedEventUse =
                (info.AdvancedEventUse / (float)info.SpriteCount) > passFactor;

            //Flux
            UseSimpleBlocks =
                (info.UseSimpleBlocks / (float)info.SpriteCount) > passFactor;
            UseMediumBlocks =
                (info.UseMediumBlocks / (float)info.SpriteCount) > passFactor;
            UseNestedControl =
                (info.UseNestedControl / (float)info.SpriteCount) > passFactor;

            //Input
            BasicInputUse =
                (info.BasicInputUse / (float)info.SpriteCount) > passFactor;
            NonCreatedVariableUse =
                (info.VariableUse / (float)info.SpriteCount) > passFactor;
            SpriteSensisng =
                (info.SpriteSensing / (float)info.SpriteCount) > passFactor;

            //Var
            BasicOperators =
                (info.BasicOperators / (float)info.SpriteCount) > passFactor;
            MediumOperators =
                (info.MediumOperators / (float)info.SpriteCount) > passFactor;
            NestedOperators =
                (info.AdvancedOperators / (float)info.SpriteCount) > passFactor;



        }

        #region Valoration Attributes
        
        public bool MultipleSpriteEvents { get; set; }
        
        public bool VariableUse { get; set; }
        
        public bool MessageUse { get; set; }
        
        public bool ListUse { get; set; }
        
        public bool NonUnusedBlocks { get; set; }
        
        public bool UserDefinedBlocks { get; set; }
        
        public bool CloneUse { get; set; }
        
        public bool SecuenceUse { get; set; }
        
        public bool MultipleThreads { get; set; }

        public bool TwoGreenFlagThread { get; set; }

        public bool AdvancedEventUse { get; set; }
        
        public bool UseSimpleBlocks { get; set; }

        public bool UseMediumBlocks { get; set; }

        public bool UseNestedControl { get; set; }
        
        public bool BasicInputUse { get; set; }

        public bool NonCreatedVariableUse { get; set; }

        public bool SpriteSensisng { get; set; }
        
        public bool BasicOperators { get; set; }
        
        public bool MediumOperators { get; set; }
        
        public bool NestedOperators { get; set; }
        
        #endregion
    }
}
