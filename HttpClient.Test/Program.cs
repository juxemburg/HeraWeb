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
        private static IEnumerable<Variable> _variables;
        static async Task RunAsync()
        {
            var client = new Client("https://projects.scratch.mit.edu");
            var s = await client.Get<Proyecto>("internalapi/project",
                "154614705", "get");
            _variables =  s.Variables.Concat(s.Children
                .Where(child => child.Variables != null)
                .SelectMany(child => child.Variables))
                .Distinct().ToList();

            var index = 0;
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
                if(item.Info != null)
                {
                    Console.WriteLine($"script count:" +
                        $" {item.Info.ScriptCount}");
                }
                if (item.Scripts == null)
                    continue;
                scriptn++;
                foreach (var script in item.Scripts)
                {
                    deserializeScript((object[])script);
                    Console.WriteLine("------------------------");
                }
            }
        }
        private static int blockCount = 0;
        private static void deserializeScript(object[] script)
        {

            var index = 0;
            foreach (var item in script)
            {
                if (item == null)
                    continue;
                if (typeof(object[]) == item.GetType())
                {
                    deserializeScript((object[])item);
                }
                if (typeof(string) == item.GetType()
                    && index == 0
                    && !_variables.Any(var => var.Name.Equals(item)))
                {
                    Console.WriteLine($"Block: {blockCount} {item}");
                    blockCount++;
                }
                index++;
            }
        }
    }
}