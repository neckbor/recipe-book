using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Core_3._1.Models
{
    public class GetRcipesBySearch_Result
    {
        public int IDRecipe { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Ingredient { get; set; }
        public string Nationality { get; set; }

    }
}
