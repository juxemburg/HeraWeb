
using System.Runtime.Serialization;

namespace HeraScratch.Objects
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
