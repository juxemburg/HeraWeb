using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Desafios
{
    public class InfoDesafio
    {
        public int Id { get; set; }

        public Desafio_Abstraccion Abstraccion { get; set; }

        public Desafio_PensamientoAlgoritmico 
            PensamientoAlgoritmico { get; set; }

        public Desafio_DescomposicionProblemas 
            DescomposicionProblemas { get; set; }

        public Desafio_Paralelismo Paralelismo { get; set; }

        public Desafio_ControlFlujo ControlFlujo { get; set; }

        public Desafio_Interaccion Interaccion { get; set; }

        public Desafio_Representacion Representacion { get; set; }

        public Desafio_Analisis Analisis { get; set; }

    }
}
