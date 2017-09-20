using Entities.Desafios;
using Entities.Valoracion;

namespace Hera.Models.EntitiesViewModels.Evaluacion
{
    public class EvaluacionViewModel
    {
        public int SpriteCount { get; set; }

        public bool EventsUse { get; set; }
        public bool MessageUse { get; set; }
        public bool SharedVariables { get; set; }
        public bool ListUse { get; set; }

        public float NonUnusedBlocks { get; set; }
        public float UserDefinedBlocks { get; set; }
        public float CloneUse { get; set; }
        public float SecuenceUse { get; set; }
        public float MultipleThreads { get; set; }
        public float TwoGreenFlagTrhead { get; set; }
        public float AdvancedEventUse { get; set; }
        public float UseSimpleBlocks { get; set; }
        public float UseMediumBlocks { get; set; }
        public float UseNestedControl { get; set; }
        public float BasicInputUse { get; set; }
        public float VariableUse { get; set; }
        public float SpriteSensing { get; set; }
        public float VariableCreation { get; set; }
        public float BasicOperators { get; set; }
        public float MediumOperators { get; set; }
        public float AdvancedOperators { get; set; }

        

        public EvaluacionViewModel(ResultadoScratch resultadoGeneral,
            InfoDesafio infoDesafio)
        {
            var infoGeneral = resultadoGeneral.IInfoScratch_General;

            SpriteCount = infoGeneral.SpriteCount;

            EventsUse = infoGeneral.EventsUse;
            MessageUse = infoGeneral.MessageUse;
            SharedVariables = infoGeneral.SharedVariables;
            ListUse = infoGeneral.ListUse;

            float Trans(int n, bool value)
            {
                return (value) ? (n / (float) SpriteCount) : -1f;
            }

            NonUnusedBlocks = Trans(infoGeneral.NonUnusedBlocks,
                infoDesafio.NonUnusedBlocks);
            UserDefinedBlocks = Trans(infoGeneral.UserDefinedBlocks,
                infoDesafio.UserDefinedBlocks);
            CloneUse = Trans(infoGeneral.CloneUse,
                infoDesafio.CloneUse);
            SecuenceUse = Trans(infoGeneral.SecuenceUse,
                infoDesafio.SecuenceUse);
            MultipleThreads = Trans(infoGeneral.MultipleThreads,
                infoDesafio.MultipleThreads);
            TwoGreenFlagTrhead = Trans(infoGeneral.TwoGreenFlagTrhead,
                infoDesafio.TwoGreenFlagThread);
            AdvancedEventUse = Trans(infoGeneral.AdvancedEventUse,
                infoDesafio.AdvancedEventUse);
            UseSimpleBlocks = Trans(infoGeneral.UseSimpleBlocks,
                infoDesafio.UseSimpleBlocks);
            UseMediumBlocks = Trans(infoGeneral.UseMediumBlocks,
                infoDesafio.UseMediumBlocks);
            UseNestedControl = Trans(infoGeneral.UseNestedControl,
                infoDesafio.UseNestedControl);
            BasicInputUse = Trans(infoGeneral.BasicInputUse,
                infoDesafio.BasicInputUse);
            VariableUse = Trans(infoGeneral.VariableUse,
                infoDesafio.VariableUse);
            SpriteSensing = Trans(infoGeneral.SpriteSensing,
                infoDesafio.SpriteSensisng);
            VariableCreation = Trans(infoGeneral.VariableCreation,
                infoDesafio.NonCreatedVariableUse);
            BasicOperators = Trans(infoGeneral.BasicOperators,
                infoDesafio.BasicOperators);
            MediumOperators = Trans(infoGeneral.MediumOperators,
                infoDesafio.MediumOperators);
            AdvancedOperators = Trans(infoGeneral.AdvancedOperators,
                infoDesafio.NestedOperators);

        }

        
    }
}
