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
                .Evaluate<ValorationTest>("159820016");
            foreach (var item in res)
            {
                Console.WriteLine($"script count: " +
                    $"{item.DuplicateScriptCount}");
            }
        }
    }
}