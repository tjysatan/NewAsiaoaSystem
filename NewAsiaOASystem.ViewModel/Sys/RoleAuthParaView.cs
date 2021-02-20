using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class RoleAuthParaView
    {
        private string roleid;
        public string RoleId { get { return roleid; } set { roleid = value; } }
        public string MenuId { get; set; }
        public string ButtonId { get; set; }
        public string AuthId { get; set; }
        public string FieldId { get; set; }
    }
}
