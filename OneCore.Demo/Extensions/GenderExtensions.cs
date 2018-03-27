using Microsoft.AspNetCore.Mvc.Rendering;
using OneCore.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCore.Demo.Extensions
{
    public class Gender
    {
        public string Label { get; set; }

        public char Value { get; set; }
    }

    public static class GenderExtensions
    {
        public static SelectList GetGenderSelectList()
        {
            return new SelectList(new List<Gender>()
            {
                new Gender(){ Label = "Masculino", Value = 'M' },
                new Gender(){ Label = "Femenino", Value = 'F'}
            }, "Value", "Label");
        }        
    }
}
