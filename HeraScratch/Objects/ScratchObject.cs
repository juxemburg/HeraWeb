﻿using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HeraScratch.Objects
{
    [DataContract(Name = "object")]
    class ScratchObject : IHttpObject
    {
        [DataMember(Name = "objName")]
        public string ObjName { get; set; }

        [DataMember(Name = "children")]
        public IEnumerable<ScratchObject> Children { get; set; }

        [DataMember(Name = "info")]
        public InfoObject Info { get; set; }

        [DataMember(Name = "scripts")]
        public IEnumerable<object> RawScripts { get; set; }

        public List<List<object>> Scripts { get; set; }
        public List<string> Blocks { get; set; }

        public List<string> ScriptsString { get; set; }

        [DataMember(Name = "variables")]
        public IEnumerable<Variable> Variables { get; set; }

        public void Initialize()
        {
            Do_setScripts();
            if (Children != null)
            {
                foreach (var item in Children)
                {
                    item.Initialize();
                }
            }
        }

        private void Do_setScripts()
        {
            if (RawScripts == null)
                return;
            Scripts = new List<List<object>>();
            Blocks = new List<string>();
            ScriptsString = new List<string>();
            var index = 1;
            foreach (object[] item in RawScripts)
            {
                var value = "";
                Scripts.Add((List<object>)Do_deserializeScript(
                    item, Blocks, 0,ref value));
                ScriptsString.Add(value);
                index++;
            }
        }

        private IEnumerable<object> Do_deserializeScript(object[] script,
            List<string> blocks, int depth, ref string stringScript)
        {
            List<object> array = new List<object>();
            var index = 0;
            var spaces = "";
            for (int i = 0; i < depth; i++)
            {
                spaces += "    ";
            }
            foreach (var item in script)
            {
                if (item == null)
                    continue;
                if (typeof(string) == item.GetType()
                    && index == 0
                    && (Variables == null ||
                    !Variables.Any(var => var.Name.Equals(item))))
                {
                    stringScript += item.ToString();
                    array.Add(item);
                    blocks.Add(item.ToString());
                }
                if (typeof(object[]) == item.GetType())
                {
                    stringScript += $"\n{spaces}";
                    var result = Do_deserializeScript((object[])item,
                        blocks, depth++,ref stringScript);
                    array.Add(result);
                }
                index++;
            }
            return array;
        }

        

    }
}
