using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAsiaOASystem.Web
{
    public class UserLogin
    {
        public string username { get; set; }
        public string userpass { get; set; }
    }

    public class UserUpdatePassword
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}