using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneCore.Demo.Models
{
    public class UserViewModel
    {        
        public string Id { get; set; }
        [Display(Name = "Usuario")]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Sexo")]
        public string Gender { get; set; }
        [Display(Name = "Estatus")]
        public string Status { get; set; }
    }
}
