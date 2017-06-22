using HeraScratch.Objects;
using HeraScratch.ObjectExtensions;
using HeraScratch.Valoration;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using HeraScratch.Exceptions;

namespace HeraScratch
{
    public class Evaluator
    {
        private Client _client;

        public Evaluator()
        {
            _client = new Client("https://projects.scratch.mit.edu");
        }
        public async Task<IEnumerable<T>> Evaluate<T,U,S>(string proyectId)
            where T : IValoration, new()
            where U : ISpriteValoration, new()
            where S : IGeneralValoration, new()
        {
            try
            {
                var result = await _client.Get<ScratchObject>(
                "internalapi/project", proyectId, "get");

                
                var list = result
                    .Children
                    .Where(child => child.RawScripts != null &&
                    !string.IsNullOrWhiteSpace(child.ObjName))
                    .Select(child => 
                    child.Evaluate<T,U,S>(child.ObjName));
                var previousList =
                    list.Select(item => (U)item.AdditionalInfo)
                    .ToList();
                var gEval =
                    result.GeneralEvaluation<T, U, S>("General",
                    previousList);
                list = list.Append(gEval);

                return list;
            }
            catch (Exception inner)
            {
                throw new EvaluationException("Proyect not found", inner);
            }
        }

    }
}
