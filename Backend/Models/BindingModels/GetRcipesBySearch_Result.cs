using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class GetRcipesBySearch_Result
    {
        public int idRecipe { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public TimeSpan? duration { get; set; }
        public string ingredient { get; set; }
        public string nationality { get; set; }

    }
}
