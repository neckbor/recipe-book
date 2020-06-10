using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models.BindingModels
{
    public class UserInfo : UserLoginString
    {
        public string role { get; set; }
        public string email { get; set; }
        public string oldLogin { get; set; }
        public string password { get; set; }
    }
}
