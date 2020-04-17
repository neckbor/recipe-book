using System;
using System.Collections.Generic;

namespace Backend_Core_3._1.Models
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
