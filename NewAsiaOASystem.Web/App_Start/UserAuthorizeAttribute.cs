using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using System.Web.Routing;

namespace NewAsiaOASystem.Web
{
    //AuthorizeAttribute权限过滤器，他会在所有过滤器之前优先执行
    public class UserAuthorizeAttribute : ActionFilterAttribute//ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取session["role"] 角色的功能 action
            HttpContextBase context = filterContext.HttpContext;
            SessionUser Suser = context.Session[SessionHelper.User] as SessionUser;
            //获取点击的controller和action
            string ControllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            if (ControllerName != "Weixin" && ControllerName != "DisPerformanceAppraisal"
                && ControllerName != "Binding" && ControllerName != "Gis" && ControllerName != "FirstBack" && ControllerName != "Vote_Subject" && ControllerName != "ExpressPrinting" && ControllerName != "publicAPI"
                && ControllerName != "activity" && ControllerName != "Act_SignNamelistinfo" && ControllerName != "Personalprofit" && ControllerName!="Iot"&& ControllerName != "Aftersale"&&ControllerName!= "lanhe")
            {
                if (Suser == null && ControllerName != "Login")
                {
                   context.Response.Write("<script language=javascript>parent.location.href='/Login/Login';</script>");
                   // context.Response.Write("<script language=javascript>parent.location.href='/Login/Index';</script>");
                }
                if (Suser != null && Suser.RoleType != "0")
                {
                    if (Suser.Funlist != null)
                    {
                        List<string> actionurl = Suser.Funlist;
                        string temp = actionurl.Find(x => x == "/" + ControllerName + "/" + actionName);
                        if (temp != null)
                        {
                            ErrorRedirect(filterContext);
                        }
                    }
                }
            }
        }

        // 错误处理方法
        private void ErrorRedirect(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Admin", action = "Err404" }));
        }

    }
}