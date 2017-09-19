using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Desafios;
using System.Collections.Generic;
using System.Linq;

namespace Hera.Models.EntitiesViewModels.EstudianteDesafio
{
    public class CalificacionDesafioViewModel
    {
        public int CursoId => Rel_CursoEstudiantes.CursoId;
        public int DesafioId => Desafio.Id;
        public int EstudianteId => Rel_CursoEstudiantes.EstudianteId;
        public Rel_CursoEstudiantes Rel_CursoEstudiantes { get; set; }        
        public Desafio Desafio { get; set; }
        public virtual List<ResultadoDesafioViewModel> Calificaciones { get; set; }

        public bool Iniciada
        {
            get
            {
                return (Calificaciones == null) || 
                    Calificaciones.Any(cal => cal.EnCurso);

            }
        }

        public bool Terminada
        {
            get
            {
                return (Calificaciones != null) 
                    && (Calificaciones.Any(cal => !cal.EnCurso)
                    && Calificaciones.Count > 0);
            }
        }

        public ResultadoDesafioViewModel CalificacionPendiente
        {
            get
            {
                return Calificaciones
                    .Where(cal => cal.EnCurso)
                    .FirstOrDefault();
            }
        }

        public CalificacionDesafioViewModel(RegistroCalificacion model)
        {
            Rel_CursoEstudiantes = model.Rel_CursoEstudiantes;
            Desafio = model.Desafio;
            Calificaciones = model.Calificaciones
                .Select(cal => new ResultadoDesafioViewModel(cal))
                .ToList();
        }
    }
}
