using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Core_3._1.Models.BindingModels
{
    public class RecipeBindingModel
    {
        public int IDRecipe { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string FirstStep { get; set; }
    }
}
