using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.EstudianteDesafio
{
    public class CalificacionDesafioViewModel
    {
        public int CursoId { get { return Rel_CursoEstudiantes.CursoId; } }
        public int DesafioId { get { return Desafio.Id; } }
        public int EstudianteId { get { return Rel_CursoEstudiantes.EstudianteId; } }
        public Rel_CursoEstudiantes Rel_CursoEstudiantes { get; set; }        
        public Desafio Desafio { get; set; }
        public virtual List<ResultadoDesafioViewModel> Calificaciones { get; set; }
        public virtual CalificacionCualitativa CalificacionCualitativa { get; set; }

        public bool Iniciada
        {
            get
            {
                return (Calificaciones != null) ?
                Calificaciones
                    .Where(cal => cal.EnCurso)
                    .Count() > 0
                    : true;

            }
        }

        public bool Terminada
        {
            get
            {
                return (Calificaciones != null) ?
                (Calificaciones
                    .Where(cal => !cal.EnCurso)
                    .Count() > 0
                    && Calificaciones.Count > 0)
                    : false;
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
            CalificacionCualitativa = model.CalificacionCualitativa;
        }
    }
}
