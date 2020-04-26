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
        public List<StepBindingModel> steps { get; set; }
        public List<IngredientListBindingModel> ingredientList { get; set; }

        public FullInfoRecipeBindingModel()
        {
            steps = new List<StepBindingModel>();
            ingredientList = new List<IngredientListBindingModel>();
        }
    }

    public class StepBindingModel
    {
        public int idStep { get; set; }
        public string description { get; set; }
        public int orderIndex { get; set; }
    }

    public class IngredientListBindingModel
    {
        public int idIngredientList { get; set; }
        public int idIngredient { get; set; }
        public string amount { get; set; }
    }
}
