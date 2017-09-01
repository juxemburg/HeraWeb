using System;

using System.ComponentModel.DataAnnotations;
using Entities.Desafios;

namespace Hera.Models.EntitiesViewModels.Desafios
{
    public class EditDesafioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Nombre del Desafio")]
        public string Nombre { get; set; }

        [Display(Name = "Url del escenario inicial")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Url(ErrorMessage = "Digite una url válida")]
        public string UrlEscenarioInicial { get; set; }
        
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public EditDesafioViewModel()
        {
            
        }
        public EditDesafioViewModel(Desafio model)
        {
            Id = model.Id;
            Nombre = model.Nombre;
            UrlEscenarioInicial = model.DirDesafioInicial;
            Descripcion = model.Descripcion;
        }
    }
}
