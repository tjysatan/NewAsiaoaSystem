using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web
{
    public class ViewRouteConfig : RazorViewEngine
    {
        public ViewRouteConfig()
        {
            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Sys/{1}/{0}.cshtml",//自定义规则
                "~/Views/NewAsiaOASystem/{1}/{0}.cshtml",//自定义规则
                "~/Views/Wx/{1}/{0}.cshtml",//自定义规则
                "~/Views/Vote/{1}/{0}.cshtml",//自定义规则
                "~/Views/NAFlow/{1}/{0}.cshtml",//自定义规则
                "~/Views/DKX/{1}/{0}.cshtml",//自定义规则
                 "~/Views/KSGZ/{1}/{0}.cshtml",//自定义规则
                 "~/Views/sbycjk/{1}/{0}.cshtml",//自定义规则
            };
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }
}