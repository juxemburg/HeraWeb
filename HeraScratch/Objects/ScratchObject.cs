using HeraScratch.ObjectExtensions;
using RestClient.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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

        private int _deadCodeCount = 0;
        public int DeadCodeCount { get => _deadCodeCount; }

        public List<string> ScriptsString { get; set; }

        public List<string> MessagesSent { get; set; }
        public List<string> MessagesRecieved { get; set; }

        public bool NestedControl { get; set; }
        public bool NestedOperator { get; set; }

        [DataMember(Name = "variables")]
        public IEnumerable<Variable> Variables { get; set; }
        
        [DataMember(Name = "lists")]
        public IEnumerable<ScratchList> Lists { get; set; }

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
            MessagesSent = new List<string>();
            MessagesRecieved = new List<string>();
            foreach (object[] item in RawScripts)
            {
                var value = "";
                if (ScratchObjectExtensions
                    .Get_ValidScript((object[])item[2])
                    && ScratchObjectExtensions
                    .Get_ScriptLength((object[])item[2]) > 1)
                {
                    Scripts.Add((List<object>)Do_deserializeScript(
                        item, Blocks, 0, ref value));
                    ScriptsString.Add(value);
                }
                else
                    _deadCodeCount++;

            }
        }

        private void IsMessageBlock(object[] script)
        {
            try
            {
                if (script[0] is string block &&
                block.Equals("broadcast:"))
                {
                    if (script[1] is string message &&
                        !MessagesSent.Contains(message))
                    {
                        MessagesSent.Add(message);
                    }
                }
                if (script[0] is string block2 &&
                    block2.Equals("whenIReceive"))
                {
                    if (script[1] is string message &&
                        !MessagesRecieved.Contains(message))
                    {
                        MessagesRecieved.Add(message);
                    }
                }
            }
            catch (Exception) { }
        }
        private void IsNestedOperator(object[] script)
        {
            if (NestedOperator)
                return;
            try
            {
                if(script[0] is string block
                    && this.IsOperatorBlock(block))
                {
                    var s1 = Get_firstBlock(script[1]);
                    var s2 = Get_firstBlock(script[2]);
                    NestedOperator = this.IsOperatorBlock(s1) ||
                        this.IsOperatorBlock(s2);
                }
            }
            catch (Exception) { }
        }
        private void IsNestedControl(object[] script)
        {
            if (NestedControl)
                return;
            try
            {
                if (script[0] is string block
                && this.IsControlBlock(block))
                {
                    var s1 = Get_firstBlock(script[1]);
                    var s2 = Get_firstBlock(script[2]);
                    NestedControl = this.IsControlBlock(s1) ||
                        this.IsControlBlock(s2);
                }
            }
            catch (Exception e)
            { }
        }

        private IEnumerable<object> Do_deserializeScript(object[] script,
            List<string> blocks, int depth, ref string stringScript)
        {
            List<object> array = new List<object>();
            var index = 0;
            var spaces = "";
            for (int i = 0; i < depth; i++)
            {
                spaces += "____";
            }
            foreach (var item in script)
            {
                if (item == null)
                    continue;
                if (item is string stringItem
                    && index == 0
                    && (Variables == null ||
                    !ScratchObjectExtensions
                    .IsReservedBlock(item.ToString()) ||
                    !Variables.Any(var => var.Name.Equals(item))))
                {
                    stringScript += stringItem;
                    array.Add(item);
                    blocks.Add(stringItem);
                    continue;
                }
                if (item != null && item is string itemString)
                {
                    stringScript += $" {itemString}";
                    continue;
                }
                if (item is object[] objectArray)
                {
                    IsMessageBlock(objectArray);
                    IsNestedControl(objectArray);
                    IsNestedOperator(objectArray);
                    stringScript += $"\n{spaces}";
                    var result = Do_deserializeScript(objectArray,
                        blocks, depth++, ref stringScript);
                    array.Add(result);
                }
                index++;
            }
            return array;
        }

        private static string Get_firstBlock(object obj)
        {
            if(obj is object[] script)
            {
                if (script.Length <= 0)
                    return "";
                if (script[0] is string name)
                    return name;
                else
                {
                    return (script[0] is object[] subScript) ?
                        Get_firstBlock(subScript) : "";
                }
            }
            return "";
        }



    }
}
