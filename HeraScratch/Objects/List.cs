using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HeraScratch.Objects
{
    [DataContract(Name = "list")]
    class ScratchList
    {
        [DataMember(Name ="listName")]
        public string ListName { get; set; }

        [DataMember(Name ="visible")]
        public bool Visible { get; set; }
    }
}


