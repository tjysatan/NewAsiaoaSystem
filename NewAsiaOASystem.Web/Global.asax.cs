using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;

namespace NewAsiaOASystem.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string SendMessage = System.Web.Configuration.WebConfigurationManager.AppSettings["SendMessage"];
           
            DateTime Nowdate= DateTime.Parse(DateTime.Now.ToShortDateString());
            if (SendMessage == "ture")
            {
                //if (Senddate < Nowdate) 
                //{
                    HelpermyTimer.StartFunction(120000000);//1000代表1秒
                    //updateSenddate(Senddate);
                //}
            }
            
            //工程师接单逾期
            //HelpermyTimer.tqfsFunction(60000);
            ////工程师制图逾期
            //HelpermyTimer.ztyqFunction(60000);
            ////资料审核逾期
            //HelpermyTimer.zlshyqFunction(60000);
            ////箱体确认逾期
            //HelpermyTimer.xytqFunction(60000);
            ////其他物料确认逾期
            //HelpermyTimer.qtqryqFunction(60000);
            ////生产接单逾期
            //HelpermyTimer.scjdyqFunction(60000);
            ////生产逾期
            //HelpermyTimer.scyqFunction(60000);
            ////发货逾期
            //HelpermyTimer.fhyqFunction(60000);
       
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            string l4net = Server.MapPath("~/Web.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(l4net));
            RegisterView();
        }

        /// <summary>
        /// 注册自定义视图查找路由
        /// </summary>
        protected void RegisterView()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ViewRouteConfig());
        }
 
        protected void Application_Error(object sender, EventArgs e)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================以下为错误提示===================================");
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
            Exception ex = Server.GetLastError().GetBaseException();
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error(ex.Message);
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error(ex.TargetSite);
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("============================================================");
        }

       
        
 
    }
}