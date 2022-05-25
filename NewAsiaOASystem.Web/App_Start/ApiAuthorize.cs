using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Web.Security;
using System.Collections.ObjectModel;
using System.Threading;
using System.Security.Principal;

namespace NewAsiaOASystem.Web
{
    public class ApiAuthorize: AuthorizationFilterAttribute
    {

        private const string UnauthorizedMessage = "请求未授权，拒绝访问。";
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //tanjianyun
            //token

            var author=actionContext.Request.Headers.Authorization;
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count() == 2)
            {
                return;
            }



            if (author==null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(UnauthorizedMessage, Encoding.UTF8) };
                return;
            }else
            {
                
                //解析toten
                Authorizationuser Authoruser = JwtHelp.GetJwtDecode(Convert.ToString(author));
                if (Authoruser.username == "admin" && Authoruser.userId == "erpadmin")
                {

                }
                else
                {//进行时间验证
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(UnauthorizedMessage, Encoding.UTF8) };
                    return;
                }
            }

            //if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
            //{
            //    base.OnAuthorization(actionContext);
            //    return;
            //}

            //if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    base.OnAuthorization(actionContext);
            //    return;
            //}

            //var cookies = actionContext.Request.Headers.GetCookies();
            //if (cookies == null || cookies.Count < 1)
            //{
            //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(UnauthorizedMessage, Encoding.UTF8) };
            //    return;
            //}

            //FormsAuthenticationTicket ticket = GetTicket(cookies);
            //if (ticket == null)
            //{
            //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(UnauthorizedMessage, Encoding.UTF8) };
            //    return;
            //}

            //这里可以对FormsAuthenticationTicket对象进行进一步验证

            //var principal = new GenericPrincipal(new FormsIdentity(ticket), null);
            //HttpContext.Current.User = principal;
            //Thread.CurrentPrincipal = principal;

            base.OnAuthorization(actionContext);
        }

        private FormsAuthenticationTicket GetTicket(Collection<CookieHeaderValue> cookies)
        {
            FormsAuthenticationTicket ticket = null;
            foreach (var item in cookies)
            {
                var cookie = item.Cookies.SingleOrDefault(c => c.Name == FormsAuthentication.FormsCookieName);
                if (cookie != null)
                {
                    ticket = FormsAuthentication.Decrypt(cookie.Value);
                    break;
                }
            }
            return ticket;
        }
    }
}