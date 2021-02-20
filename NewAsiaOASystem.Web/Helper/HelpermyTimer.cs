using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using NewAsiaOASystem.DBModel;
using System.Web.Configuration;
using System.Xml;


namespace NewAsiaOASystem.Web
{
    public class HelpermyTimer
    {
      //  public static ISysPewersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        //IWX_Message_NewsDao _IWX_Message_NewsDao = ContextRegistry.GetContext().GetObject("WX_Message_NewsDao") as IWX_Message_NewsDao;
        public static void StartFunction(int timer)
        {
            System.Timers.Timer time = new System.Timers.Timer(timer);
            time.Elapsed += new System.Timers.ElapsedEventHandler(time_Elapsed);
            time.Enabled = true;
        }

        /// <summary>
        /// 重新赋值Session
        /// </summary>
        /// <param name="timer"></param>
        public void loginSession1(int timer)
        {

            System.Timers.Timer time = new System.Timers.Timer(timer);
            time.Elapsed += new System.Timers.ElapsedEventHandler(time_login);
            time.Enabled = true;
        }

 

        static bool state = false;

        //static void time_loginsession(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    SessionUser user = HttpContext.Current.Session[SessionHelper.User] as SessionUser;
        //    ss.loginsession(user);
        //}

        static void time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string SendMessagedate = System.Web.Configuration.WebConfigurationManager.AppSettings["SendMessagedata"];
            DateTime Senddate = DateTime.Parse(SendMessagedate);
            DateTime Nowdate = DateTime.Parse(DateTime.Now.ToShortDateString());

            //if (DateTime.Now.Hour >= 12)
            //    state = false;
            if (Senddate < Nowdate)
            {
                int h = DateTime.Now.Hour;
                if (h >= 10 && h < 11 && !state)
                {
                    // state = true;
                    //执行程序
                    MassManager.Mb_S_Icmass();
                    updateSenddate(Senddate);

                }
            }
        }

        static void time_login(object sender, System.Timers.ElapsedEventArgs e)
        {
            MassManager.LoginSession();
        }

        #region //电控箱下单处理逾期定时发送
        public static void tqfsFunction(int timer)
        {
            System.Timers.Timer time = new System.Timers.Timer(timer);
            time.Elapsed += new System.Timers.ElapsedEventHandler(time_YQFSlapsed);
            time.Enabled = true;
        }
        //工程师接单逾期
       static void time_YQFSlapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.gcsjdyq();//工程师接单逾期
           //MassManager.gcsztyq();
           //MassManager.xtkcqryq();
           //MassManager.qtwlqryq();
           //MassManager.scjdyq();
           //MassManager.scyq();
           //MassManager.fhyq();
       }
       #region //工程师制图逾期
       //工程师制图逾期
       public static void ztyqFunction(int timer)
       {
           System.Timers.Timer time = new System.Timers.Timer(timer);
           time.Elapsed += new System.Timers.ElapsedEventHandler(time_ZTYQElapsed);
           time.Enabled = true;
       }
       static void time_ZTYQElapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.gcsztyq();//制图逾期
       } 
       #endregion

       #region //箱体确认逾期
       public static void xytqFunction(int timer)
       {
           System.Timers.Timer time = new System.Timers.Timer(timer);
           time.Elapsed += new System.Timers.ElapsedEventHandler(time_XTYQElapsed);
           time.Enabled = true;
       }
       static void time_XTYQElapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.xtkcqryq();//相同确认逾期
       }
       #endregion

       #region //其他物料确认逾期
       public static void qtqryqFunction(int timer)
       {
           System.Timers.Timer time = new System.Timers.Timer(timer);
           time.Elapsed += new System.Timers.ElapsedEventHandler(time_QTQRYQElapsed);
           time.Enabled = true;
       }
       static void time_QTQRYQElapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.qtwlqryq();//其他物料确认逾期
       }
       #endregion

       #region //资料审核逾期
       public static void zlshyqFunction(int timer)
       {
           System.Timers.Timer time = new System.Timers.Timer(timer);
           time.Elapsed += new System.Timers.ElapsedEventHandler(time_ZLSHYQElapsed);
           time.Enabled = true;
       }
       static void time_ZLSHYQElapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.zlshyq();
       }
       #endregion

       #region //生产接单逾期
       public static void scjdyqFunction(int timer)
       {
           System.Timers.Timer time = new System.Timers.Timer(timer);
           time.Elapsed += new System.Timers.ElapsedEventHandler(time_SCJDYQElapsed);
           time.Enabled = true;
       }
       static void time_SCJDYQElapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.scjdyq();
       }
       #endregion

       #region //生产逾期
       public static void scyqFunction(int timer)
       {
           System.Timers.Timer time = new System.Timers.Timer(timer);
           time.Elapsed += new System.Timers.ElapsedEventHandler(time_SCYQElapsed);
           time.Enabled = true;
       }
       static void time_SCYQElapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.scyq();//生产逾期
       }
       #endregion

       #region //发货逾期
       public static void fhyqFunction(int timer)
       {
           System.Timers.Timer time = new System.Timers.Timer(timer);
           time.Elapsed += new System.Timers.ElapsedEventHandler(time_FHYQElapsed);
           time.Enabled = true;
       }
       static void time_FHYQElapsed(object sender, System.Timers.ElapsedEventArgs e)
       {
           MassManager.fhyq();//发货逾期
       }
       #endregion
       #endregion

        public static void updateSenddate(DateTime oldtime)
        {
            string filename = HttpRuntime.AppDomainAppPath.ToString() + @"\Web.config";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            // XmlElement xe = xn as XmlElement;
            foreach (XmlNode element in topM)
            {
                //XmlElement xe = element as XmlElement;
                if (element.Name.ToLower() == "appsettings")
                {
                    XmlNodeList _node = element.ChildNodes;
                    if (_node.Count > 0)
                    {
                        foreach (XmlNode el in _node)
                        {
                            XmlElement xe = el as XmlElement;
                            if (xe != null)
                            {
                                string Keyname = xe.Attributes["key"].InnerXml;//键值 
                                switch (Keyname)
                                {
                                    case "SendMessagedata":
                                        xe.Attributes["value"].Value = DateTime.Now.ToShortDateString(); ;
                                        break;
                                }
                                //if (el.Attributes["key"].InnerXml.ToLower() == "SendMessagedata")
                                //{
                                //    el.Attributes["value"].Value = DateTime.Now.ToShortDateString();
                                //}
                            }
                        }
                    }
                }
            }
            xmldoc.Save(filename);
        }


    }
}