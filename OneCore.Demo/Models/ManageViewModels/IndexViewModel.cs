using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneCore.Demo.Models.ManageViewModels
{
    public class IndexViewModel
    {       
        public string Id { get; set; }

        [Required(ErrorMessage = "El campo Usuario es requerido")]
        [MinLength(7, ErrorMessage = "El campo Usuario debe contener al menos 7 caracteres.")]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        [Required(ErrorMessage = "El campo Email es requerido")]
        [EmailAddress(ErrorMessage = "El campo Email no es un correo electrónico válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Sexo es requerido")]
        [Display(Name = "Sexo")]
        public string Gender { get; set; }

        [StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} y máximo {1} caractéres de longitud.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La contraseña no coincide.")]        
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
