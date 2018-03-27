using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneCore.Demo.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Usuario es requerido")]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }
    }
}
