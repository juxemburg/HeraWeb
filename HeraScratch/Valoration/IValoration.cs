using System;
using System.Collections.Generic;

namespace HeraScratch.Valoration
{
    public interface IValoration
    {
        bool generalValoration { get; set; }
        string SpriteName { get; set; }

        int ScriptCount { get; set; }
        int BlockCount { get; set; }
        int DeadCodeCount { get; set; }
        int DuplicateScriptCount { get; set; }

        IValorationInfo AdditionalInfo { get; set; }

        List<Tuple<string, int>> BlockFrequency { get; set; }
        List<Tuple<string, string>> DuplicatedScripts { get; set; }
        List<Tuple<string, string>> DeadScripts { get; set; }

        
    }

    public static class ValorationHelper
    {
        public static IValoration Get_DefaultSingle<T,U>(
            string name = "Stage")
            where T : IValoration, new()
            where U : ISpriteValoration, new()
        {
            return new T()
            {
                SpriteName = name,
                generalValoration = false,
                BlockFrequency = new List<Tuple<string, int>>(),
                DuplicatedScripts = new List<Tuple<string, string>>(),
                DeadScripts = new List<Tuple<string, string>>(),
                AdditionalInfo = new U() { }
            };
        }
    }
}
