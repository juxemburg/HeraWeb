using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Desafios
{
    public class Rel_Rating
    {
        public int DesafioId { get; set; }
        public Desafio Desafio { get; set; }

        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public int Rating { get; set; }
    }
}
