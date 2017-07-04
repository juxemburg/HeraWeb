using Entities.Desafios;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels
{
    public class CreateDesafioViewModel : IValidatableObject
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
        
        [Required(ErrorMessage ="Campo obligatorio")]
        public string DirArchivo { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        #region Valoration Attributes

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


        

        #endregion

        public Desafio Map(int profesorId)
        {
            return new Desafio()
            {
                Nombre = this.Nombre,
                Descripcion = this.Descripcion,
                DirDesafioInicial = UrlEscenarioInicial,
                DirSolucion = DirArchivo,
                ProfesorId = profesorId,
                InfoDesafio = new InfoDesafio()
                {
                    MultipleSpriteEvents = MultipleSpriteEvents,
                    VariableUse = VariableUse,
                    MessageUse = MessageUse,
                    ListUse = ListUse,
                    NonUnusedBlocks = NonUnusedBlocks,
                    UserDefinedBlocks = UserDefinedBlocks,
                    CloneUse = CloneUse,
                    SecuenceUse = SecuenceUse,
                    MultipleThreads = MultipleThreads,
                    TwoGreenFlagThread = TwoGreenFlagThread,
                    AdvancedEventUse = AdvancedEventUse,
                    UseSimpleBlocks = UseSimpleBlocks,
                    UseMediumBlocks = UseMediumBlocks,
                    UseNestedControl = UseNestedControl,
                    BasicInputUse = BasicInputUse,
                    NonCreatedVariableUse = NonCreatedVariableUse,
                    SpriteSensisng = SpriteSensisng,
                    BasicOperators = BasicOperators,
                    MediumOperators = MediumOperators,
                    NestedOperators = NestedOperators
                }
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!(MultipleSpriteEvents || VariableUse || MessageUse ||
                ListUse || NonUnusedBlocks || UserDefinedBlocks || CloneUse
                || SecuenceUse || MultipleThreads || TwoGreenFlagThread
                || AdvancedEventUse || UseSimpleBlocks || UseMediumBlocks
                || UseNestedControl || BasicInputUse || NonCreatedVariableUse
                || SpriteSensisng  || BasicOperators || MediumOperators ||
                NestedOperators))
            {
                yield return new ValidationResult(
                    "No ha seleccionado ningún " +
                    "criterio de evaluación para el desafío",
                    new[] { "IdSolucion" });
            }
        }
    }
}
