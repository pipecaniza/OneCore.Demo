using OneCore.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCore.Demo.Extensions
{
    public static class ApplicationUserExtensions
    {
        public static string GetGender(this ApplicationUser user)
        {
            return user.Gender == "M" ? "Masculino" : "Femenino";
        }

        public static string GetStatus(this ApplicationUser user)
        {
            return user.Status ? "Activo" : "Inactivo";
        }
    }
}
