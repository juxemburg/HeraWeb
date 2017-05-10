using Entities.Cursos;
using Entities.Desafios;
using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.EstudianteCurso
{
    public class EstudianteCursoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        public Profesor Profesor { get; set; }

        public List<Desafio> DesafiosRealizados { get; set; }
        public Desafio DesafioPendiente { get; set; }

        public EstudianteCursoViewModel(Curso model,
            List<Desafio> desafiosRealizados, 
            Desafio desafioPendiente)
        {
            this.Id = model.Id;
            this.Nombre = model.Nombre;
            this.Descripcion = model.Descripcion;
            this.Profesor = model.Profesor;
            this.DesafiosRealizados =
                desafiosRealizados != null ? desafiosRealizados
                : new List<Desafio>();
            this.DesafioPendiente = desafioPendiente;
            
        }
    }
}
