using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.ProfesorCursos
{
    public class ChangeStarterViewModel : IValidatableObject
    {
        public int CursoId { get; set; }

        public int NewStarterId { get; set; }
        public int OldStarterId { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if(NewStarterId.Equals(OldStarterId))
            {
                yield return new ValidationResult(
                    "No se puede reemplazar éste valor",
                    new[] { "NewStarterId" });
            }
        }
    }
}
