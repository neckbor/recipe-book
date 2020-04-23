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
        public string mainIngredient { get; set; }
        public string nationality { get; set; }
        public List<Step> steps { get; set; }
        public List<IngredientList> ingredients  { get; set; }

        public RecipeBindingModel()
        {
            steps = new List<Step>();
            ingredients = new List<IngredientList>();
        }
    }
}
