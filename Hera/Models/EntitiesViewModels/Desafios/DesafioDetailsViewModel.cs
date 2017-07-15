using Entities.Calificaciones;
using Entities.Desafios;
using Entities.Usuarios;
using Hera.Models.EntitiesViewModels.Ratings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Desafios
{
    public class DesafioDetailsViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DirDesafioInicial { get; set; }
        public string DirSolucion { get; set; }
        public int Popularidad { get; set; }
        public RatingViewModel Valoracion { get; set; }

        public Profesor Profesor { get; set; }
        public InfoDesafio InfoDesafio { get; set; }
        public virtual List<RegistroCalificacion> Calificaciones { get; set; }


        public DesafioDetailsViewModel()
        {

        }
        public DesafioDetailsViewModel(Desafio desafio)
        {
            this.Id = desafio.Id;
            this.Nombre = desafio.Nombre;
            this.Descripcion = desafio.Descripcion;
            this.DirDesafioInicial = desafio.DirDesafioInicial;
            this.DirSolucion = desafio.DirSolucion;
            this.Popularidad = desafio.Popularity;
            this.Valoracion = new RatingViewModel()
            {
                Average = desafio.AverageRating,
                ReviewCount = desafio.RatingCount
            };
            this.Profesor = desafio.Profesor;
            this.InfoDesafio = desafio.InfoDesafio;
            this.Calificaciones = desafio.Calificaciones;
        }

        public Desafio Map()
        {
            return new Desafio()
            {
                Id = this.Id,
                Nombre = Nombre,
                Descripcion = Descripcion,
                DirDesafioInicial = DirDesafioInicial,
                DirSolucion = DirSolucion,
                Calificaciones = Calificaciones,
                InfoDesafio = InfoDesafio,
                Profesor = Profesor,
                ProfesorId = Profesor.Id
            };
        }

    }
}
