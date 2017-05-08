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
        public async Task<IEnumerable<U>> Evaluate<U>(string proyectId)            
            where U : IValoration, new()
        { 
            var result = await _client.Get<ScratchObject>(
                "internalapi/project", proyectId, "get");

            var gEval = result.GeneralEvaluation();
            var eval1 = result.Evaluate();
            var list = result
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
                        BlockFrequency = eval.BlockFrequency,
                        GeneralValoration = false
                    };
                })
                .Append(new U()
                {
                    SpriteName = "General",
                    ScriptCount = gEval.ScriptCount,
                    BlockCount = gEval.BlockCount,
                    BlockFrequency = gEval.BlockFrequency,
                    GeneralValoration = true
                })
                .Append(new U()
                {
                    SpriteName = "Stage",
                    ScriptCount = eval1.ScriptCount,
                    BlockCount = eval1.BlockCount,
                    BlockFrequency = eval1.BlockFrequency,
                    GeneralValoration = false
                });

            return list;
        }

    }
}
