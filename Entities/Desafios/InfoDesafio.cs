using Entities.Comparisons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Desafios
{
    public class InfoDesafio: ISimilar
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


        public List<string> ActiveProperties()
        {
            var properties = new List<string>();
            if (MultipleSpriteEvents)
                properties.Add("Más de un sprite tiene eventos");
            if (VariableUse)
                properties.Add("Uso y creación de variables");
            if (MessageUse)
                properties.Add("Uso correcto de mensajes");
            if (ListUse)
                properties.Add("Uso y creación de listas");

            if (NonUnusedBlocks)
                properties.Add("Se usan todos los bloques");
            if (UserDefinedBlocks)
                properties.Add("Creación de bloques propios");
            if (CloneUse)
                properties.Add("Uso de clones");

            if (SecuenceUse)
                properties.Add("Uso de secuencias");

            if (MultipleThreads)
                properties.Add("Usa más de un hilo por sprite");

            if (UseSimpleBlocks)
                properties.Add("Uso de bloques simples");
            if (UseMediumBlocks)
                properties.Add("Uso de bloques complejos");
            if (UseNestedControl)
                properties.Add("Uso de bloques anidados");

            if (BasicInputUse)
                properties.Add("Uso de bloques de entrada");
            if (NonCreatedVariableUse)
                properties.Add("Uso de variables no creadas");
            if (SpriteSensisng)
                properties.Add("Uso de sensores de sprite");

            if (BasicOperators)
                properties.Add("Uso de operadores básicos");
            if (MediumOperators)
                properties.Add("Uso de operadores complejos");
            if (NestedOperators)
                properties.Add("Uso de operadores anidados");

            return properties;
        }

        public bool IsFalse
        {
            get => (MultipleSpriteEvents == false &&
                    VariableUse == false &&
                    MessageUse == false &&
                    ListUse == false &&
                    NonUnusedBlocks == false &&
                    UserDefinedBlocks == false &&
                    CloneUse == false &&
                    SecuenceUse == false &&
                    MultipleThreads == false &&
                    TwoGreenFlagThread == false &&
                    AdvancedEventUse == false &&
                    UseSimpleBlocks == false &&
                    UseMediumBlocks == false &&
                    UseNestedControl == false &&
                    BasicInputUse == false &&
                    NonCreatedVariableUse == false &&
                    SpriteSensisng == false &&
                    BasicOperators == false &&
                    MediumOperators == false &&
                    NestedOperators == false); 
        }

        public bool IsEqualTo(object obj)
        {

            if (obj is InfoDesafio info)
            {
                return (
                    MultipleSpriteEvents == info.MultipleSpriteEvents &&
                    VariableUse == info.VariableUse &&
                    MessageUse == info.MessageUse &&
                    ListUse == info.ListUse &&
                    NonUnusedBlocks == info.NonUnusedBlocks &&
                    UserDefinedBlocks == info.UserDefinedBlocks &&
                    CloneUse == info.CloneUse &&
                    SecuenceUse == info.SecuenceUse &&
                    MultipleThreads == info.MultipleThreads &&
                    TwoGreenFlagThread == info.TwoGreenFlagThread &&
                    AdvancedEventUse == info.AdvancedEventUse &&
                    UseSimpleBlocks == info.UseSimpleBlocks &&
                    UseMediumBlocks == info.UseMediumBlocks &&
                    UseNestedControl == info.UseNestedControl &&
                    BasicInputUse == info.BasicInputUse &&
                    NonCreatedVariableUse == info.NonCreatedVariableUse &&
                    SpriteSensisng == info.SpriteSensisng &&
                    BasicOperators == info.BasicOperators &&
                    MediumOperators == info.MediumOperators &&
                    NestedOperators == info.NestedOperators
                    );
            }
            return false;
        }

        public bool IsSimilarTo(object obj)
        {
            if (obj is InfoDesafio info)
            {
                return (
                    MultipleSpriteEvents == info.MultipleSpriteEvents ||
                    VariableUse == info.VariableUse ||
                    MessageUse == info.MessageUse ||
                    ListUse == info.ListUse ||
                    NonUnusedBlocks == info.NonUnusedBlocks ||
                    UserDefinedBlocks == info.UserDefinedBlocks ||
                    CloneUse == info.CloneUse ||
                    SecuenceUse == info.SecuenceUse ||
                    MultipleThreads == info.MultipleThreads ||
                    TwoGreenFlagThread == info.TwoGreenFlagThread ||
                    AdvancedEventUse == info.AdvancedEventUse ||
                    UseSimpleBlocks == info.UseSimpleBlocks ||
                    UseMediumBlocks == info.UseMediumBlocks ||
                    UseNestedControl == info.UseNestedControl ||
                    BasicInputUse == info.BasicInputUse ||
                    NonCreatedVariableUse == info.NonCreatedVariableUse ||
                    SpriteSensisng == info.SpriteSensisng ||
                    BasicOperators == info.BasicOperators ||
                    MediumOperators == info.MediumOperators ||
                    NestedOperators == info.NestedOperators
                    );
            }
            return false;
        }
    }
}
