using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Calificaciones;
using Entities.Cursos;

namespace Hera.Models.EntitiesViewModels
{
    public class CalificacionCualitativaViewModel
    {

        public CalificacionCualitativaViewModel(
            RegistroCalificacion registro,
            string nombreDesafio)
        {
            this.Registro = registro;
            this.Registro.Calificaciones = registro.Calificaciones
                .Where(cal => cal.ResultadoGeneral != null)
                .ToList();
            this.NombreDesafio = nombreDesafio;
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
            string nombreDesafio,
            CreateCalificacionCualitativaViewModel formModel)
            :this(registro, nombreDesafio)
        {
            this.FormModel = formModel;            
        }

        public RegistroCalificacion Registro { get; set; }
        public string NombreDesafio { get; set; }
        public bool Calificado { get; set; }
        public CreateCalificacionCualitativaViewModel FormModel { get; set; }
    }
}
