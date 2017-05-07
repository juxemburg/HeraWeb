using RestClient.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HttpClient.Test
{
    class Program
    {
        static void Main()
        {
            RunAsync().Wait();
            Console.ReadKey();
        }
        
        static async Task RunAsync()
        {
            var client = new Client("https://projects.scratch.mit.edu");
            var s = await client.Get<ScratchObject>("internalapi/project",
                "159291183", "get");
            //var s = await client.Get<ScratchObject>("internalapi/project",
            //    "159291183", "get");
            Console.WriteLine("Done!");
            Console.WriteLine(s.ObjName);
            if (s.Info != null)
            {
                Console.WriteLine($"script count:" +
                    $" {s.Info.ScriptCount}");
                var eval = s.GeneralEvaluation();
                Console.WriteLine($"Evaluation:\n\n {eval}");
            }
            Console.WriteLine("----------------Clildren");
            foreach (var item in s.Children)
            {
                if (item == null || item.RawScripts == null)
                    continue;

                Console.WriteLine(item.ObjName);
                var evaluation = item.Evaluate();
                Console.WriteLine($"Evaluation: \n\n{evaluation}");
            }
        }
    }
}