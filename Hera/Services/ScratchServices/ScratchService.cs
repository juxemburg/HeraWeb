﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraScratch.Valoration;
using HeraScratch;
using Hera.Services.ScratchServices;

namespace Hera.Services
{
    public class ScratchService
    {
        private Evaluator _evaluator;

        public ScratchService()
        {
            _evaluator = new Evaluator();
        }
        public async Task<EvaluationScratch> Get_Evaluation(string projId)           
        {
            var result = await _evaluator
                .Evaluate<EvaluationScratch, Valoration_Scatch>(projId);
            return result;
        }
    }
}