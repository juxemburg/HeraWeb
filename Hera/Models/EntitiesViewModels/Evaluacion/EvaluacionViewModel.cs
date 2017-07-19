using Entities.Desafios;
using Entities.Valoracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            this.SpriteCount = infoGeneral.SpriteCount;

            this.EventsUse = infoGeneral.EventsUse;
            this.MessageUse = infoGeneral.MessageUse;
            this.SharedVariables = infoGeneral.SharedVariables;
            this.ListUse = infoGeneral.ListUse;

            Func<int, bool, float> trans = (n, value) => {
                return (value) ? (n / (float)SpriteCount) : -1f;
            };

            this.NonUnusedBlocks = trans(infoGeneral.NonUnusedBlocks,
                infoDesafio.NonUnusedBlocks);
            this.UserDefinedBlocks = trans(infoGeneral.UserDefinedBlocks,
                infoDesafio.UserDefinedBlocks);
            this.CloneUse = trans(infoGeneral.CloneUse,
                infoDesafio.CloneUse);
            this.SecuenceUse = trans(infoGeneral.SecuenceUse,
                infoDesafio.SecuenceUse);
            this.MultipleThreads = trans(infoGeneral.MultipleThreads,
                infoDesafio.MultipleThreads);
            this.TwoGreenFlagTrhead = trans(infoGeneral.TwoGreenFlagTrhead,
                infoDesafio.TwoGreenFlagThread);
            this.AdvancedEventUse = trans(infoGeneral.AdvancedEventUse,
                infoDesafio.AdvancedEventUse);
            this.UseSimpleBlocks = trans(infoGeneral.UseSimpleBlocks,
                infoDesafio.UseSimpleBlocks);
            this.UseMediumBlocks = trans(infoGeneral.UseMediumBlocks,
                infoDesafio.UseMediumBlocks);
            this.UseNestedControl = trans(infoGeneral.UseNestedControl,
                infoDesafio.UseNestedControl);
            this.BasicInputUse = trans(infoGeneral.BasicInputUse,
                infoDesafio.BasicInputUse);
            this.VariableUse = trans(infoGeneral.VariableUse,
                infoDesafio.VariableUse);
            this.SpriteSensing = trans(infoGeneral.SpriteSensing,
                infoDesafio.SpriteSensisng);
            this.VariableCreation = trans(infoGeneral.VariableCreation,
                infoDesafio.NonCreatedVariableUse);
            this.BasicOperators = trans(infoGeneral.BasicOperators,
                infoDesafio.BasicOperators);
            this.MediumOperators = trans(infoGeneral.MediumOperators,
                infoDesafio.MediumOperators);
            this.AdvancedOperators = trans(infoGeneral.AdvancedOperators,
                infoDesafio.NestedOperators);

        }


    }
}
