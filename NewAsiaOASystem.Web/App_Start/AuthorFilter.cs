using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.Web
{
    public class AuthorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ControllerName = filterContext.RouteData.Values["controller"].ToString(); 
            HttpContextBase context = filterContext.HttpContext;
            if (!ControllerName.Equals("Weixin"))
            {
                context.Response.Write("<script language=javascript>parent.location.href='/Login/Login';</script>");
 
            }
        } 
     
    }

    
}