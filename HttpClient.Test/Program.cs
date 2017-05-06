using RestClient.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var s = await client.Get<Proyecto>("internalapi/project",
                "10128431", "get");
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
                Console.WriteLine($"-------------scripts{scriptn}------------");
                foreach (object[] codeblock in item.Scripts)
                {
                    if (codeblock == null)
                        continue;
                    var blocks = (object[])codeblock;
                    foreach (object[] block in (IEnumerable)blocks[2])
                    {
                        if (block == null)
                            continue;

                        Console.WriteLine($"block num:{index} {block[0]}");
                        index++;
                    }
                }
            }
        }
    }
}