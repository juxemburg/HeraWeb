using Entities.Colors;
using Entities.Desafios;
using Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Cursos
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Color Color { get; set; }
        public String ColorName { get => ColorHelper.Get_ColorName(Color); }
        public int ProfesorId { get; set; }
        public Profesor Profesor { get; set; }

        public string Password { get; set; }

        public int DesafioId { get; set; }
        public Desafio Desafio { get; set; }

        public virtual List<Rel_CursoEstudiantes> Estudiantes { get; set; }
        public virtual List<Rel_DesafiosCursos> Desafios { get; set; }

        public bool ContieneEstudiante(int estId)
        {
            var query = Estudiantes
                .Where(rel => rel.EstudianteId == estId)
                .FirstOrDefault();
            return query != null;                
        }

        public bool ContieneDesafio(int desafioId)
        {
            var query = Desafios
                .Where(rel => rel.DesafioID == desafioId)
                .FirstOrDefault();
            return (query != null || Desafio.Id == desafioId);
        }

        
    }
}
