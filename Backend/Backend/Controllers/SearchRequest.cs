using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Controllers
{
    public class SearchRequest
    {
        public string Name { get; set; }
        public string MainIngredient { get; set; }
        public string Nationality { get; set; }
    }
}