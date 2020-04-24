using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.BindingModels
{
    public class IngredientBindingModel
    {
        public int idIngredient { get; set; }
        public string name { get; set; }
    }

    public class NationalityBindingModel
    {
        public int idNationality { get; set; }
        public string name { get; set; }
    }
}
