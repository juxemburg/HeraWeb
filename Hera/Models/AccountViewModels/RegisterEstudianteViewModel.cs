using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.AccountViewModels
{
    public class RegisterEstudianteViewModel : RegisterViewModel
    {
        [Range(1, 11,
            ErrorMessage = "Por favor, seleccione un grado entre 1 y 11")]
        public int Grado { get; set; }

        public Genero Genero { get; set; }

        [Display(Name ="Materia Favorita")]
        public EstudianteInfo_MateriaFavorita MateriaFavorita { get; set; }

        [Display(Name ="¿Qué actividades realizas cuando usas el PC?")]
        public EstudianteInfo_ActividadesPc ActividadesPc { get; set; }

        [Display(Name ="¿Qué tan frecuente usas el computador?")]
        public EstudianteInfo_UsoPc FrecuenciaPc { get; set; }

        [Display(Name ="¿Cómo consideras que manejas los computadores?")]
        [Range(1, 10, ErrorMessage =
            "Error, escoge un número entre 1 y 10")]
        public int ManejoComputador { get; set; }

        [Display(Name ="¿Qué tanto conozco del " +
            "manejo de los computadores?")]
        [Range(1, 10, ErrorMessage =
            "Error, escoge un número entre 1 y 10")]
        public int ConocimientoComputador { get; set; }


        public Estudiante Map(int usuarioId)
        {
            return new Estudiante()
            {
                UsuarioId = usuarioId,
                Nombres = this.Nombres,
                Apellidos = this.Apellidos,
                Edad = this.Edad,
                Grado = this.Grado,
                Genero = this.Genero,
                MateriaFavorita = this.MateriaFavorita,
                ActividadesPc = this.ActividadesPc,
                FrecuenciaPc = this.FrecuenciaPc,
                ManejoComputador = this.ManejoComputador,
                ConocimientoComputador = this.ConocimientoComputador
            };
        }


    }
}
