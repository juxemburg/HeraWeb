using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels
{
    public class ProgresoDesafioViewModel
    {
        public double promedioTiempos { get; set; }
        public List<Estudiante> estudiantesQueFinalizaron { get; set; }
        public List<Estudiante> estudiantesQueNoFinalizaron { get; set; }

        public ProgresoDesafioViewModel() {
            this.promedioTiempos = 0;
            this.estudiantesQueFinalizaron = new List<Estudiante>();
            this.estudiantesQueNoFinalizaron = new List<Estudiante>(); 
        }
    }
}
