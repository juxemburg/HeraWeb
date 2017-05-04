using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Calificaciones;
using Entities.Cursos;

namespace Hera.Models.EntitiesViewModels
{
    public class CalificacionCualitativaViewModel
    {
        public CalificacionCualitativaViewModel(RegistroCalificacion model)
        {
            this.Registro = model;
        }

        public RegistroCalificacion Registro { get; set; }
    }
}
