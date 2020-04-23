using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.BindingModels
{
    public class FullInfoRecipeBindingModel
    {
        public int idRecipe { get; set; }
        public string name { get; set; }
        public int idIngredient { get; set; }
        public int idNationality { get; set; }
        public string author { get; set; }
        public string duration { get; set; }
        public List<Step> steps { get; set; }
        public List<IngredientList> ingredientList { get; set; }

        public FullInfoRecipeBindingModel()
        {
            steps = new List<Step>();
            ingredientList = new List<IngredientList>();
        }
    }
}
