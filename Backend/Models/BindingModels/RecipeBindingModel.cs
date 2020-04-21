using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.BindingModels
{
    public class RecipeBindingModel
    {
        public int idRecipe { get; set; }
        public string name { get; set; }
        public string nationality { get; set; }
        public string firstStep { get; set; }
        public int stepsCount { get; set; }
    }
}
