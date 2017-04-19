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
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int ProfesorId { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public int DesafioId { get; set; }

        public CreateDesafioViewModel Desafio { get; set; }

        public Curso Map(Desafio desafioInicial)
        {
            return new Curso()
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                ProfesorId = this.ProfesorId,
                Password = this.Password,
                DesafioId = desafioInicial.Id,
                Desafio = desafioInicial
            };
        }

        public Curso Map()
        {
            return new Curso()
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                ProfesorId = this.ProfesorId,
                Password = this.Password,
                Desafio = this.Desafio.Map()
            };
        }

    }
}
