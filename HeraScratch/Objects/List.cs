
using System.Runtime.Serialization;

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


