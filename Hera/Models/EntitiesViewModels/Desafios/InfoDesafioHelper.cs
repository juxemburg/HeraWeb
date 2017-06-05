using Entities.Desafios;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Desafios
{
    public static class InfoDesafioHelper
    {
        public static List<SelectListItem> 
            AbstraccionItems { get; }
            = new List<SelectListItem>()
            {
                new SelectListItem
                { Value= ""+(int)Desafio_Abstraccion.Ninguno,
                    Text ="Ninguno"},
                new SelectListItem
                { Value= ""+(int)Desafio_Abstraccion.NoBloquesNoUsados,
                    Text ="No bloques no usados"},
                new SelectListItem
                { Value= ""+(int)Desafio_Abstraccion.CreacionBloquesPropios,
                    Text ="Creación de bloques propios"},
                new SelectListItem
                { Value= ""+(int)Desafio_Abstraccion.UsoDeClones,
                    Text ="Uso de clones"},
            };

        public static List<SelectListItem>
            PensamientoAlgoritmicoItems
        { get; }
            = new List<SelectListItem>()
            {
                new SelectListItem
                { Value= ""+(int)Desafio_PensamientoAlgoritmico.Ninguno,
                    Text ="Ninguno"},
                new SelectListItem
                { Value= ""+(int)Desafio_PensamientoAlgoritmico.UsoSecuencias,
                    Text ="Uso de Secuencias"}
            };

        public static List<SelectListItem>
             DescomposicionProblemasItems
        { get; }
        = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ""+(int)Desafio_DescomposicionProblemas.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_DescomposicionProblemas.DosHilosPorSprite,
                Text = "Dos hilos por sprite"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_DescomposicionProblemas.MultiplesEventosPorSprite,
                Text = "Múltiples eventos por sprite"
            }
        };

        public static List<SelectListItem>
             ParalelismoItems
        { get; }
        = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ""+(int)Desafio_Paralelismo.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Paralelismo.DosHilosBanderaVerde,
                Text = "Dos hilos con bandera verde"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Paralelismo.EnvioYRecepcionMensajes,
                Text = "Envío y recepción de mensajes"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Paralelismo.DosProgramasEnOtrosEventos,
                Text = "Dos programas en otros eventos"
            }
        };

        public static List<SelectListItem>
             ControlFlujoItems
        { get; }
        = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ""+(int)Desafio_ControlFlujo.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_ControlFlujo.FlujoSimple,
                Text = "Uso de flujos simples"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_ControlFlujo.FlujoComplejo,
                Text = "Uso de flujos complejos"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_ControlFlujo.FlujoAnidado,
                Text = "Uso de flujos anidados"
            }
        };

        public static List<SelectListItem>
             InteraccionItems
        { get; }
        = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ""+(int)Desafio_Interaccion.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Interaccion.InputBasico,
                Text = "Uso de bloques básicos de interacción"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Interaccion.UsoVariables,
                Text = "Uso de variables (no propias)"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Interaccion.SensoresSprite,
                Text = "Uso de sensores de sprite"
            }
        };

        public static List<SelectListItem>
             RepresentacionItems
        { get; }
        = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ""+(int)Desafio_Representacion.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Representacion.CreacionVariables,
                Text = "Creación de variables (propias)"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Representacion.UsoCompartidoVariables,
                Text = "Uso de variables en múltiples sprites"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Representacion.UsoListas,
                Text = "Uso de listas"
            }
        };

        public static List<SelectListItem>
             AnalisisItems
        { get; }
        = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = ""+(int)Desafio_Analisis.Ninguno,
                Text = "Ninguno"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Analisis.OperadoresBasicos,
                Text = "Uso de operadores básicos"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Analisis.OperadoresComplejos,
                Text = "Uso de operadores complejos"
            },
            new SelectListItem
            {
                Value = ""+(int)Desafio_Analisis.OperadoresAnidados,
                Text = "Uso de operadores anidados"
            }
        };
    }
}
