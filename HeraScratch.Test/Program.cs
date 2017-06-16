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
                .Evaluate<ValorationTest>("166106578");
            foreach (var item in res)
            {
                Console.WriteLine($"=============item name " +
                    $"{item.SpriteName}");
                Console.WriteLine($"dead code {item.DeadCodeCount}");
                Console.WriteLine($"script count: " +
                    $"{item.DuplicateScriptCount}");
                Console.WriteLine($"Non unusedBlocks: {item.NonUnusedBlocks}");
                Console.WriteLine($"user Defined Blocks: {item.UserDefinedBlocks}");
                Console.WriteLine($"Clone use: {item.CloneUse}");
                Console.WriteLine($"Secuence use: {item.SecuenceUse}");
                Console.WriteLine($"Multiple Threads: {item.MultipleThreads}");
                Console.WriteLine($"EventsUse: {item.EventsUse}");
                Console.WriteLine($"TwoGreenFlagThread: {item.MultipleThreads}");
                Console.WriteLine($"Message Use: {item.MessageUse}");
                Console.WriteLine($"Advanced Events Use: {item.AdvancedEventUse}");
                Console.WriteLine($"Use of simple blocks: {item.UseSimpleBlocks}");
                Console.WriteLine($"Use of medium blocks: {item.UseMediumBlocks}"); 
                Console.WriteLine($"Basic Operators: {item.BasicOperators}");
                Console.WriteLine($"Medium Operators: {item.MediumOperators}");
                Console.WriteLine($"Advanced Operators: {item.AdvancedOperators}");
            }
        }
    }
}