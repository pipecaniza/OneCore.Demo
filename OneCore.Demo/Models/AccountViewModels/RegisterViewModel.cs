using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneCore.Demo.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo Usuario es requerido.")]
        [MinLength(7, ErrorMessage = "El campo Usuario debe contener al menos 7 caracteres.")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo Email es requerido.")]
        [EmailAddress(ErrorMessage = "El campo Email no es un correo electrónico válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Sexo es requerido.")]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es requerido.")]
        [StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} y máximo {1} caractéres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña.")]
        [Compare("Password", ErrorMessage = "La contraseña no coincide.")]
        public string ConfirmPassword { get; set; }
    }
}
