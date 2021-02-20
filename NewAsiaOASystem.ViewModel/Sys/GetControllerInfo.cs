using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class GetControllerInfoView
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Params { get; set; }
    }

    public class GetMenuList
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class GetReasonlist
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

}
