using HeraScratch.Valoration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Valoracion;

namespace Hera.Services.ScratchServices
{
    public class GeneralInfo : IGeneralValoration, IScratchAdapter
    {
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

        public string Info => "";

        public IInfoScratch Map()
        {
            return new IInfoScratch_General()
            {
                EventsUse = EventsUse,
                MessageUse = MessageUse,
                SharedVariables = SharedVariables,
                ListUse = ListUse,

                NonUnusedBlocks = NonUnusedBlocks,
                UserDefinedBlocks = UserDefinedBlocks,
                CloneUse = CloneUse,
                SecuenceUse = SecuenceUse,
                MultipleThreads = MultipleThreads,
                TwoGreenFlagTrhead = TwoGreenFlagTrhead,
                AdvancedEventUse = AdvancedEventUse,
                UseSimpleBlocks = UseSimpleBlocks,
                UseMediumBlocks = UseMediumBlocks,
                UseNestedControl = UseNestedControl,
                BasicInputUse = BasicInputUse,
                VariableUse = VariableUse,
                SpriteSensing = SpriteSensing,
                VariableCreation = VariableCreation,
                BasicOperators = BasicOperators,
                MediumOperators = MediumOperators,
                AdvancedOperators = AdvancedOperators
            };
        }
    }
}
