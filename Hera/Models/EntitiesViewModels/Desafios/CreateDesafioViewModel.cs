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
        
        [Display(Name = "Url del escenario inicial")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Url(ErrorMessage = "Digite una url válida")]
        public string UrlEscenarioInicial { get; set; }

        [Display(Name = "Id del Proyecto")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdSolucion { get; set; }

        [Display(Name = "Url de la solución")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Solucion { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        //General Valoration
        [Display(Name ="Más de un sprite tiene eventos")]
        public bool MultipleSpriteEvents { get; set; }

        [Display(Name = "Uso, y creación, de variables")]
        public bool VariableUse { get; set; }

        [Display(Name ="Uso correcto de mensajes")]
        public bool MessageUse { get; set; }

        [Display(Name ="Uso y creación de listas")]
        public bool ListUse { get; set; }

        //Sprite Valoration
        //Abstraction
        [Display(Name ="Se usan todos los bloques")]
        public bool NonUnusedBlocks { get; set; }

        [Display(Name ="Creación de bloques propios")]
        public bool UserDefinedBlocks { get; set; }

        [Display(Name ="Uso de clones")]
        public bool CloneUse { get; set; }

        //Algorithm Thinking
        [Display(Name ="Uso de secuencias")]
        public bool SecuenceUse { get; set; }


        //Problem Solving
        [Display(Name ="usa más de un hilo por sprite")]
        public bool MultipleThreads { get; set; }

        //Sync
        [Display(Name ="Posee, al menos, dos hilos que empiezan con bandera verde")]
        public bool TwoGreenFlagThread { get; set; }

        [Display(Name ="Usa más de un tipo de evento")]
        public bool AdvancedEventUse { get; set; }

        //Control
        [Display(Name ="Uso de bloques simples")]
        public bool UseSimpleBlocks { get; set; }

        [Display(Name ="Uso de bloques complejos")]
        public bool UseMediumBlocks { get; set; }

        [Display(Name ="Uso de bloques anidados")]
        public bool UseNestedControl { get; set; }

        //Input
        [Display(Name ="Uso de bloques de entrada")]
        public bool BasicInputUse { get; set; }

        [Display(Name ="Uso de variables no creadas")]
        public bool NonCreatedVariableUse { get; set; }

        [Display(Name ="Uso de sensores de sprite")]
        public bool SpriteSensisng { get; set; }

        //Analysis
        [Display(Name ="Uso de operadores básicos")]
        public bool BasicOperators { get; set; }

        [Display(Name ="Uso de operadores complejos")]
        public bool MediumOperators { get; set; }

        [Display(Name ="Uso de operadores anidados")]
        public bool NestedOperators { get; set; }



        //

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
                Dificultad = 0,
                Descripcion = this.Descripcion,
                DirDesafioInicial = UrlEscenarioInicial,
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
