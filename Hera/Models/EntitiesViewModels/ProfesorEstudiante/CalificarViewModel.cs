using System;
using Entities.Calificaciones;

namespace Hera.Models.EntitiesViewModels.ProfesorEstudiante
{
    public class CalificarViewModel
    {
        public int? Id { get; set; }
        public bool Completada { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Descripcion { get; set; }
        public int? CalificacionId { get; set; }

        public CalificarViewModel()
        {
            
        }

        public CalificarViewModel(int id)
        {
            CalificacionId = id;
            Completada = false;
        }


        public CalificacionCualitativa Map()
        {
            return new CalificacionCualitativa
            {
                CalificacionId = CalificacionId.GetValueOrDefault(),
                FechaRegistro = DateTime.Now,
                Descripcion = Descripcion,
                Completada = Completada
            };
        }
    }
}
