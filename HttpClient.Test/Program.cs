using RestClient.Client;
using System;
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
            Console.WriteLine("Done!");
            Console.WriteLine(s.objName);
            if (s.info != null)
            {
                Console.WriteLine($"script count:" +
                    $" {s.info.scriptCount}");
            }
            Console.WriteLine("Clildren");
            foreach (var item in s.children)
            {
                Console.WriteLine(item.objName);
                if(item.info != null)
                {
                    Console.WriteLine($"script count:" +
                        $" {item.info.scriptCount}");
                }
            }
        }
    }
}