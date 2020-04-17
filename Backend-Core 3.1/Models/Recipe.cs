using System;
using System.Collections.Generic;

namespace Backend_Core_3._1.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            IngredientList = new HashSet<IngredientList>();
            Step = new HashSet<Step>();
        }

        public int Idrecipe { get; set; }
        public string Name { get; set; }
        public int Idingredient { get; set; }
        public int? Idnationality { get; set; }
        public string Author { get; set; }
        public DateTime DtimeCreate { get; set; }
        public TimeSpan? Duration { get; set; }

        public virtual Ingredient IdingredientNavigation { get; set; }
        public virtual Nationality IdnationalityNavigation { get; set; }
        public virtual ICollection<IngredientList> IngredientList { get; set; }
        public virtual ICollection<Step> Step { get; set; }
    }
}
