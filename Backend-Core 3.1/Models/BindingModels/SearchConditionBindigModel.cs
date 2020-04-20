using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Core_3._1.Models.BindingModels
{
    public class SearchConditionBindigModel
    {
        public string RecipeName { get; set; }
        public int IDNationality { get; set; }
        public int IDIngredient { get; set; }
        public string Author { get; set; }
    }
}
