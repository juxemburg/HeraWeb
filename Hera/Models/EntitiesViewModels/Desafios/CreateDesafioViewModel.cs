using Entities.Desafios;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Display(Name = "Nombre del Desafio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Dificultad { get; set; }

        [Display(Name = "Url del escenario inicial")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Url(ErrorMessage = "Digite una url válida")]
        public string EscenarioInicial { get; set; }

        [Display(Name = "Url de la solución")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Url(ErrorMessage = "Digite una url válida")]
        public string Solucion { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Abstracción")]
        public Desafio_Abstraccion Abstraccion { get; set; }

        [Display(Name ="Pensamiento algorítmico")]
        public Desafio_PensamientoAlgoritmico
            PensamientoAlgoritmico
        { get; set; }

        [Display(Name ="Descomposición de problemas")]
        public Desafio_DescomposicionProblemas
            DescomposicionProblemas
        { get; set; }

        [Display(Name ="Paralelismo")]
        public Desafio_Paralelismo Paralelismo { get; set; }

        [Display(Name="Control de flujo")]
        public Desafio_ControlFlujo ControlFlujo { get; set; }

        [Display(Name ="Interacción")]
        public Desafio_Interaccion Interaccion { get; set; }

        [Display(Name ="Representación")]
        public Desafio_Representacion Representacion { get; set; }

        [Display(Name= "Análisis")]
        public Desafio_Analisis Analisis { get; set; }

        public Desafio Map()
        {
            return new Desafio()
            {
                Nombre = this.Nombre,
                Dificultad = this.Dificultad,
                Descripcion = this.Descripcion,
                DirDesafioInicial = EscenarioInicial,
                DirSolucion = Solucion,
                InfoDesafio = new InfoDesafio()
                {
                    Abstraccion = this.Abstraccion,
                    Analisis = this.Analisis,
                    ControlFlujo = this.ControlFlujo,
                    DescomposicionProblemas = this.DescomposicionProblemas,
                    Interaccion = this.Interaccion,
                    Paralelismo = this.Paralelismo,
                    PensamientoAlgoritmico = this.PensamientoAlgoritmico,
                    Representacion = this.Representacion
                }
            };
        }

        
    }
}
