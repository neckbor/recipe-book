using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.BindingModels
{
    public class SearchConditionBindigModel
    {
        public string recipeName { get; set; }
        public string nationality { get; set; }
        public string ingredient { get; set; }
        public string author { get; set; }
    }
}
