using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Desafios
{
    public class InfoDesafio
    {
        public int Id { get; set; }

        #region Valoration Attributes

        //General Valoration
        public bool MultipleSpriteEvents { get; set; }
        
        public bool VariableUse { get; set; }
        
        public bool MessageUse { get; set; }
        
        public bool ListUse { get; set; }

        //Sprite Valoration
        //Abstraction
        public bool NonUnusedBlocks { get; set; }
        
        public bool UserDefinedBlocks { get; set; }
        
        public bool CloneUse { get; set; }

        //Algorithm Thinking
        public bool SecuenceUse { get; set; }


        //Problem Solving
        public bool MultipleThreads { get; set; }

        //Sync
        public bool TwoGreenFlagThread { get; set; }
        
        public bool AdvancedEventUse { get; set; }

        //Control
        public bool UseSimpleBlocks { get; set; }
        
        public bool UseMediumBlocks { get; set; }
        
        public bool UseNestedControl { get; set; }

        //Input
        public bool BasicInputUse { get; set; }
        
        public bool NonCreatedVariableUse { get; set; }
        
        public bool SpriteSensisng { get; set; }

        //Analysis
        public bool BasicOperators { get; set; }
        
        public bool MediumOperators { get; set; }
        
        public bool NestedOperators { get; set; }
        
        #endregion

        public int DesafioId { get; set; }
        public Desafio Desafio { get; set; }

    }
}
