using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraScratch.Valoration;
using HeraScratch;
using Hera.Services.ScratchServices;
using HeraScratch.Exceptions;

namespace Hera.Services
{
    public class ScratchService
    {
        private Evaluator _evaluator;

        public ScratchService()
        {
            _evaluator = new Evaluator();
        }


        public async Task<IEnumerable<Valoration_Scatch>> 
            Get_Evaluation(string projId)
        {
            try
            {
                var result = await _evaluator
                .Evaluate<Valoration_Scatch, SpriteInfo,
                GeneralInfo>(projId);
                return result;
            }
            catch (EvaluationException)
            {
                throw;
            }
        }

        public async Task<Valoration_Scatch> 
            Get_GeneralEvaluation(string projectId)
        {
            try
            {
                var res = await _evaluator
                    .GeneralEvaluate<Valoration_Scatch, SpriteInfo,
                    GeneralInfo>(projectId);
                return res;
            }
            catch(EvaluationException)
            {
                throw;
            }
        }


    }
}
