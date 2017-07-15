﻿using Entities.Cursos;
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

        

        

        public Curso Map(int ProfesorId,Desafio desafioInicial)
        {
            return new Curso()
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                ProfesorId = ProfesorId,
                Password = this.Password,
                DesafioId = desafioInicial.Id,
                Desafio = desafioInicial
            };
        }
        

    }
}