using HeraScratch.Objects;
using HeraScratch.ObjectExtensions;
using HeraScratch.Valoration;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeraScratch
{
    public class Evaluator
    {
        private Client _client;

        public Evaluator()
        {
            _client = new Client("https://projects.scratch.mit.edu");
        }
        public async Task<ScratchValoration> Evaluate(string proyectId)
        { 
            var result = await _client.Get<ScratchObject>(
                "internalapi/project","159291183", "get");
            return result.GeneralEvaluation();
        }

    }
}
