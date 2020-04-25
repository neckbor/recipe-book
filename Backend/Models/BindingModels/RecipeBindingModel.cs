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
        public List<StepBindingModel> steps { get; set; }
        public List<IngredientListBindingModel> ingredients  { get; set; }

        public RecipeBindingModel()
        {
            steps = new List<StepBindingModel>();
            ingredients = new List<IngredientListBindingModel>();
        }
    }
}
