using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HttpClient.Test
{
    [DataContract(Name = "proyecto")]
    class Proyecto
    {
        [DataMember(Name = "objName")]
        public string ObjName { get; set; }

        [DataMember(Name = "children")]
        public IEnumerable<Proyecto> Children { get; set; }
        
        [DataMember(Name = "info")]
        public InfoProyecto Info { get; set; }

        [DataMember(Name = "scripts")]
        public IEnumerable<object> Scripts { get; set; }

        [DataMember(Name ="variables")]
        public IEnumerable<Variable> Variables { get; set; }
    }
}
