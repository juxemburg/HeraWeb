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
                List<RegistroCalificacion>> registroCurso,
            List<string> criterios)
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
            var dates = Enumerable.Range(0, 7)
                .Select(d => DateTime.Now.AddDays((d * -1)));

            var califications = registroCurso.Values
                .SelectMany(e => e.SelectMany(e2 => e2.Calificaciones));

            var group = dates.GroupJoin(califications,
                d => d.Date, cal => cal.Tiempoinicio.Date,
                (d, cal) => new
                {
                    Key = string.Format("{0:d}", d),
                    Date = d,
                    Count = cal.Count()
                })
                .OrderBy(grp => grp.Date);

            
            

            Info = new InfoCursoViewModel()
            {
                CriteriosEvaluados = criterios,
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
                    
                    ["Número de Calificaciones"] 
                        = new MultiValueSeriesViewModel()
                    {
                        Data = group.Select(grp => (float)grp.Count),
                        Labels = group.Select(grp => grp.Key),
                        Name = "Número de Calificaciones"
                    }

                }

            };
        }
    }
}
