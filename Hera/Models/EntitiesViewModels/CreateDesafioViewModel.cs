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

        [Display(Name ="Nombre del Desafio")]
        public string Nombre { get; set; }

        public int Dificultad { get; set; }

        public string Descripcion { get; set; }

        public string DirArchivo { get; set; }


        public Desafio Map()
        {
            return new Desafio()
            {
                Nombre = this.Nombre,
                Dificultad = this.Dificultad,
                Descripcion = this.Descripcion,
                DirArchivo = this.DirArchivo
            };
        }
    }
}
