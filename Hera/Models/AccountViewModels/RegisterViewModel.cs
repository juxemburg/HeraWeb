using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage = "La {0} Debe tener al menos {2} y, como mucho,  {1} caracteres.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma tu contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombres Completos")]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Apellidos Completos")]
        public string Apellidos { get; set; }

        [Required]
        [Range(7, 100,
            ErrorMessage = "Error la edad debe estar entre 7 y 100 años")]
        public int Edad { get; set; }

        [Required]
        [Display(Name = "Soy un: ")]
        public string Role { get; set; }
    }
}
