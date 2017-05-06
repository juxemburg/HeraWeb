using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HttpClient.Test
{
    [DataContract(Name="variable")]
    class Variable
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public object Value { get; set; }

        [DataMember(Name = "isPersistent")]
        public bool Persistent { get; set; }
    }
}
