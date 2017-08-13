using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities.Colors;
using Entities.Cursos;
using Entities.Desafios;

namespace Hera.Models.EntitiesViewModels.Cursos
{
    public class EditCursoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public EditCursoViewModel()
        {
            
        }
        public EditCursoViewModel(Curso model)
        {
            Id = model.Id;
            Nombre = model.Nombre;
            Descripcion = model.Descripcion;
            Password = model.Password;
            ConfirmPassword = "";
        }
        

    }
}
