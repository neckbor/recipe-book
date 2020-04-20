﻿using System;
using System.Collections.Generic;

namespace Backend_Core_3._1.Models
{
    public partial class Step
    {
        public int Idstep { get; set; }
        public int Idrecipe { get; set; }
        public int OrderIndex { get; set; }
        public string Description { get; set; }

        public virtual Recipe IdrecipeNavigation { get; set; }
    }
}
