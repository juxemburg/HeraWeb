using Entities.Colors;
using Entities.Cursos;
using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        

        

        public Curso Map(int ProfesorId,Desafio desafioInicial,
            Color color = Color.Lightblue)
        {
            return new Curso()
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                ProfesorId = ProfesorId,
                Password = this.Password,
                Desafios = new List<Rel_DesafiosCursos>()
                {
                    new Rel_DesafiosCursos()
                    {
                        Initial = true,
                        DesafioID = desafioInicial.Id,
                        Desafio = desafioInicial
                    }
                },
                Color = color
            };
        }
        

    }
}
