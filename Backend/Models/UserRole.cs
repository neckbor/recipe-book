using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class UserRole
    {
        public int IduserRole { get; set; }
        public int Iduser { get; set; }
        public int Idrole { get; set; }

        public virtual Role IdroleNavigation { get; set; }
        public virtual User IduserNavigation { get; set; }
    }
}
