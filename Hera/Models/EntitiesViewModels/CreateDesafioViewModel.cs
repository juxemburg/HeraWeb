using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels
{
    public class CreateDesafioViewModel
    {

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name ="Nombre del Desafio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Dificultad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }

        


        public Desafio Map(string filePath)
        {
            return new Desafio()
            {
                Nombre = this.Nombre,
                Dificultad = this.Dificultad,
                Descripcion = this.Descripcion,
                DirArchivo = filePath
            };
        }
    }
}
