using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientList = new HashSet<IngredientList>();
            Recipe = new HashSet<Recipe>();
        }

        public int Idingredient { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IngredientList> IngredientList { get; set; }
        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
