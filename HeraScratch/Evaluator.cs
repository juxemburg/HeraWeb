using HeraScratch.Objects;
using HeraScratch.ObjectExtensions;
using HeraScratch.Valoration;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace HeraScratch
{
    public class Evaluator
    {
        private Client _client;

        public Evaluator()
        {
            _client = new Client("https://projects.scratch.mit.edu");
        }
        public async Task<T> Evaluate<T, U>(string proyectId)
            where T : IEvaluation, new()
            where U : IValoration, new()
        { 
            var result = await _client.Get<ScratchObject>(
                "internalapi/project", proyectId, "get");

            var evaluation = new T();
            var list = (IEnumerable<IValoration>)result
                .Children
                .Where(child => child.RawScripts != null &&
                !string.IsNullOrWhiteSpace(child.ObjName))
                .Select(child =>
                {
                    var eval = child.Evaluate();
                    return new U()
                    {
                        SpriteName = child.ObjName,
                        ScriptCount = eval.ScriptCount,
                        BlockCount = eval.BlockCount,
                        BlockFrequency = eval.BlockFrequency
                    };
                });
            var childEvaluations = new List<IValoration>();
            var gEval = result.GeneralEvaluation();
            var gResult = new U()
            {
                SpriteName = "General",
                ScriptCount = gEval.ScriptCount,
                BlockCount = gEval.BlockCount,
                BlockFrequency = gEval.BlockFrequency
            };

            evaluation.Initialize(gResult,
                list);
            return evaluation;
        }

    }
}
