using Entities.Calificaciones;
using Entities.Cursos;
using Hera.Models.EntitiesViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.ProfesorCursos
{
    public class ProfesorCursoViewModel
    {
        public Curso Curso { get; set; }

        public Dictionary<Tuple<int,int>, List<RegistroCalificacion>>
            RegistrosCurso { get; set; }

        public InfoCursoViewModel Info { get; set; }

        public ProfesorCursoViewModel(Curso curso, 
            Dictionary<Tuple<int, int>,
                List<RegistroCalificacion>> registroCurso)
        {
            this.Curso = curso;
            this.RegistrosCurso = registroCurso;
            var numM = Curso.Estudiantes
                    .Where(rel =>
                    rel.Estudiante.Genero ==
                    Entities.Usuarios.Genero.Masculino)
                    .Count();
            var numF =
                Curso.Estudiantes
                    .Where(rel =>
                    rel.Estudiante.Genero ==
                    Entities.Usuarios.Genero.Femenino)
                    .Count();
            this.Info = new InfoCursoViewModel()
            {
                
                NumNinos = new ChartSeriesViewModel()
                {
                    Data = numM,
                    Label= $"{ChartUtil.Percentage(numM,Curso.Estudiantes.Count)}%",
                    Name="Masculino"
                },
                NumNinas = new ChartSeriesViewModel()
                {
                    Data = numF,
                    Label = $"{ChartUtil.Percentage(numF,Curso.Estudiantes.Count)}%",
                    Name = "Femenino"
                }

            };
        }
    }
}
