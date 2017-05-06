using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.Test
{
    public class Proyecto
    {
        public string objName { get; set; }
        public IEnumerable<Proyecto> children { get; set; }
        public InfoProyecto info { get; set; }
    }
}
