using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HeraScratch.Objects
{
    [DataContract(Name = "info")]
    class InfoObject
    {
        [DataMember(Name = "projectID")]
        public int Id { get; set; }

        [DataMember(Name = "scriptCount")]
        public int ScriptCount { get; set; }
    }
}
