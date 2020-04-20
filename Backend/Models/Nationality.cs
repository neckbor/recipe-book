using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Recipe = new HashSet<Recipe>();
        }

        public int Idnationality { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
