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

        
        public Desafio Desafio
        {
            get => Desafios.FirstOrDefault(d => d.Initial).Desafio;
        }

        public virtual List<Rel_CursoEstudiantes> Estudiantes { get; set; }
        public virtual List<Rel_DesafiosCursos> Desafios { get; set; }

        public bool ContieneEstudiante(int estId)
        {
            return Estudiantes
                .Any(rel => rel.EstudianteId == estId);
        }

        public bool ContieneDesafio(int desafioId)
        {
            return Desafios.Any(rel => rel.DesafioID == desafioId);
        }

        
    }
}
