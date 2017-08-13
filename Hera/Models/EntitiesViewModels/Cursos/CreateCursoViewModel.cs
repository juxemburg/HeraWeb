using Entities.Colors;
using Entities.Cursos;
using Entities.Desafios;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hera.Models.EntitiesViewModels
{
    public class CreateCursoViewModel
    {
        [Required(ErrorMessage ="Este campo es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public int? DesafioId { get; set; }

        

        

        public Curso Map(int profesorId,Desafio desafioInicial,
            Color color = Color.Lightblue)
        {
            return new Curso()
            {
                Nombre = Nombre,
                Descripcion = Descripcion,
                ProfesorId = profesorId,
                Password = Password,
                Desafios = new List<Rel_DesafiosCursos>()
                {
                    new Rel_DesafiosCursos()
                    {
                        Initial = true,
                        DesafioId = desafioInicial.Id,
                        Desafio = desafioInicial
                    }
                },
                Color = color
            };
        }
        

    }
}
