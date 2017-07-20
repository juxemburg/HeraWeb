using System;

namespace HeraScratch.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync();
            Console.ReadKey();
        }
        private static async void RunAsync()
        {
            Evaluator _evaluator = new Evaluator();

            var res = await _evaluator
                .Evaluate<ValorationTest, SpriteInfo, GeneralInfo>("167997841");
            foreach (var item in res)
            {
                Console.WriteLine($"=============item name " +
                    $"{item.SpriteName}");
                Console.WriteLine($"dead code {item.DeadCodeCount}");
                Console.WriteLine($"script count: " +
                    $"{item.DuplicateScriptCount}");
                Console.WriteLine(item.AdditionalInfo.Info);

                
            }
        }
    }
}