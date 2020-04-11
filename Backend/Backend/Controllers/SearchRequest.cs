using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Controllers
{
    public class SearchRequest
    {
        public string name { get; set; }
        public string mainIngredient { get; set; }
        public string nationality { get; set; }
        public string author { get; set; }
    }
}