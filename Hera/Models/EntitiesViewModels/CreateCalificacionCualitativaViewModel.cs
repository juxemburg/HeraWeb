using Entities.Calificaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels
{
    public class CreateCalificacionCualitativaViewModel
    {
        public int? Id { get; set; }
        
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }

        [Display(Name="El estudiante completó satisfacoriamente los objetivos del desafío?")]
        public bool Completada { get; set; }

        [Display(Name ="¿Por Qué?")]
        public string Descripcion { get; set; }

        public CreateCalificacionCualitativaViewModel(CalificacionCualitativa map)
        {

        }

        public CalificacionCualitativa Map()
        {
            return new CalificacionCualitativa()
            {
                Completada = this.Completada,
                FechaRegistro = DateTime.Now,
                Descripcion = this.Descripcion,
                CursoId = this.CursoId,
                EstudianteId = this.EstudianteId,
                DesafioId = this.DesafioId
            };
        }
    }
}
