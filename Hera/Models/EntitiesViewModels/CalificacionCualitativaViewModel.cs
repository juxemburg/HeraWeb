using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Calificaciones;
using Entities.Cursos;
using Hera.Models.EntitiesViewModels.Evaluacion;
using Entities.Desafios;

namespace Hera.Models.EntitiesViewModels
{
    public class CalificacionCualitativaViewModel
    {

        public CalificacionCualitativaViewModel(
            RegistroCalificacion registro,
            Desafio desafio)
        {
            this.Registro = registro;
            this.Registro.Calificaciones = registro.Calificaciones
                .Where(cal => cal.ResultadoGeneral != null)
                .ToList();
            Calificaciones = registro
                .Calificaciones
                .Select(cal =>
                new CalificacionViewModel(cal, desafio.InfoDesafio))
                .ToList();
            this.NombreDesafio = desafio.Nombre;
            this.Calificado = false;
            this.FormModel = new CreateCalificacionCualitativaViewModel()
            {
                CursoId = Registro.CursoId,
                EstudianteId = Registro.EstudianteId,
                DesafioId = Registro.DesafioId
            };
        }
        public CalificacionCualitativaViewModel(
            RegistroCalificacion registro,
            Desafio desafio,
            CreateCalificacionCualitativaViewModel formModel)
            : this(registro, desafio)
        {
            this.FormModel = formModel;
        }

        public RegistroCalificacion Registro { get; set; }
        public List<CalificacionViewModel> Calificaciones { get; set; }
        public string NombreDesafio { get; set; }
        public bool Calificado { get; set; }
        public CreateCalificacionCualitativaViewModel FormModel { get; set; }
        public EvaluacionViewModel Model { get; set; }
    }
}
