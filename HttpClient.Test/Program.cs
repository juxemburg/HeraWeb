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
            
            //var s = await client.Get<ScratchObject>("internalapi/project",
            //    "159291183", "get");
            Console.WriteLine("Done!");
            Console.WriteLine($"==============Sprite: {s.ObjName}");
            if (s.Info != null)
            {
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