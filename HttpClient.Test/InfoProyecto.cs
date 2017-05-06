using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HttpClient.Test
{
    [DataContract(Name = "info")]
    class InfoProyecto
    {
        [DataMember(Name = "projectID")]
        public int Id { get; set; }

        [DataMember(Name = "scriptCount")]
        public int ScriptCount { get; set; }
    }
}
