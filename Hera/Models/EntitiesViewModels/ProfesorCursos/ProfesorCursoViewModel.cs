using Entities.Calificaciones;
using Entities.Cursos;
using Hera.Models.EntitiesViewModels.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Usuarios;

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
            Curso = curso;
            RegistrosCurso = registroCurso;
            var numM = Curso.Estudiantes
                    .Count(rel => 
                    rel.Estudiante.Genero == Genero.Masculino);
            var numF =
                Curso.Estudiantes
                    .Count(rel => 
                    rel.Estudiante.Genero == Genero.Femenino);
            var grpActividad = registroCurso.Values
                .SelectMany(e =>
                    e.SelectMany(e2 => e2.Calificaciones))
                .Where(cal => cal.Tiempoinicio > DateTime.Now.AddDays(-7))
                .GroupBy(cal => cal.Tiempoinicio.Date)
                .OrderBy(grp => grp.Key);

            Info = new InfoCursoViewModel()
            {
                DistSexo = new List<SingleValueSeriesViewModel>()
                {
                    new SingleValueSeriesViewModel()
                    {
                        Data = numM,
                        Label = $"{ChartUtil.Percentage(numM, Curso.Estudiantes.Count)}%",
                        Name = "Masculino"
                    },
                    new SingleValueSeriesViewModel()
                    {
                        Data = numF,
                        Label = $"{ChartUtil.Percentage(numF, Curso.Estudiantes.Count)}%",
                        Name = "Femenino"
                    }
                },
                ActividadCurso = new Dictionary<string, MultiValueSeriesViewModel>()
                {
                    ["Número de Calificaciones"] = new MultiValueSeriesViewModel()
                    {
                        Data = grpActividad
                            .Select(grp => (float)grp.Count()),
                        Labels = grpActividad
                        .Select(grp => string.Format("{0:d}", grp.Key)),
                        Name= "Número de Calificaciones"
                    }
                }

            };
        }
    }
}
