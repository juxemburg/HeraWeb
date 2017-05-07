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


            var scriptn = 0;
            Console.WriteLine("Done!");
            Console.WriteLine(s.ObjName);
            if (s.Info != null)
            {
                Console.WriteLine($"script count:" +
                    $" {s.Info.ScriptCount}");
            }
            Console.WriteLine("Clildren");
            foreach (var item in s.Children)
            {
                if (item == null)
                    continue;
                Console.WriteLine(item.ObjName);
                
                if (item.RawScripts == null)
                    continue;
                var evaluation = item.Evaluate();
                Console.WriteLine($"Dead Scripts" +
                    $": {evaluation.DeadCode.Count}");
                scriptn++;
                
            }
        }
    }
}