using System;
using System.Collections.Generic;

namespace Backend_Core_3._1.Models
{
    public partial class IngredientList
    {
        public int IdingredientList { get; set; }
        public int Idrecipe { get; set; }
        public int Idingredient { get; set; }
        public string Amount { get; set; }

        public virtual Ingredient IdingredientNavigation { get; set; }
        public virtual Recipe IdrecipeNavigation { get; set; }
    }
}
