using Entities.Calificaciones;
using Entities.Cursos;
using Entities.Desafios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Evaluacion
{
    public class CalificacionViewModel
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public int EstudianteId { get; set; }
        public int DesafioId { get; set; }
        

        public Calificacion Calificacion { get; set; }
        public EvaluacionViewModel Evaluacion { get; set; }

        public CalificacionViewModel(Calificacion cal,
            InfoDesafio infoDesafio)
        {
            Id = cal.Id;
            CursoId = cal.CursoId;
            EstudianteId = cal.EstudianteId;
            DesafioId = cal.DesafioId;
            Calificacion = cal;
            Evaluacion = new EvaluacionViewModel(cal.ResultadoGeneral,
                infoDesafio);
        }
    }
}
