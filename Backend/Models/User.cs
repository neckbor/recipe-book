using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class User
    {
        public int Iduser { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PassworgHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Idrole { get; set; }

        public virtual Role IdroleNavigation { get; set; }
    }
}
