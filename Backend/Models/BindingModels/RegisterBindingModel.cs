using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.BindingModels
{
    public class RegisterBindingModel : LoginBindingModel
    {
        [Required]
        public string email { get; set; }
    }
}
