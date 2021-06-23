using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Context.Support;
using System.Web.UI;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.IDao;
using Newtonsoft.Json;
using System.Net;
using NewAsiaOASystem.Web.Helper;
using System.Text;

namespace NewAsiaOASystem.Web
{
    public class LoginController : Controller
    {
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        ISysMenuDao _ISysMenuDao = ContextRegistry.GetContext().GetObject("SysMenuDao") as ISysMenuDao;
        ISysRoleDao _ISysRoleDao = ContextRegistry.GetContext().GetObject("SysRoleDao") as ISysRoleDao;
        ISysFunctionDao _ISysFunctionDao = ContextRegistry.GetContext().GetObject("SysFunctionDao") as ISysFunctionDao;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录 
        /// </summary>
        /// <param name="userId">用户名</param>
        /// <param name="passwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public string Login(string userId, string passwd)
        {
            try
            {
                //判断是否是ajax请求，如果不是的话直接返回登录页面
                if (!Request.IsAjaxRequest())
                {
                    Response.Redirect("~/Login");
                    return "3";
                }
                if (!"".Equals(userId) && !"".Equals(passwd))
                {
                    SysPerson model = _ISysPersonDao.GetModelByLogin(userId);
                    if (model != null)//存在
                    {
                        if (NAHelper.MD5Encrypt(model.Password, new UTF8Encoding()) == passwd)
                        {
                            Session[SessionHelper.User] = _ISysPersonDao.GetSessionuser(userId);
                            bool record = Log_records(userId);//记录用户登录次数和最后登录时间
                            bool login = SysLog_history(userId);//登录日志
                            return "2";
                        }
                        else
                        {
                            return "1";
                        }
                    }
                    else//不存在用户
                    {
                        return "1";
                    }
                        //if (_ISysPersonDao.login(userId, passwd))
                        //{
                        //    Session[SessionHelper.User] = _ISysPersonDao.GetSessionuser(userId);
                        //    bool record = Log_records(userId);//记录用户登录次数和最后登录时间
                        //    bool login = SysLog_history(userId);//登录日志
                        //    return "2";
                        //}
                        //else
                        //{
                        //    return "1";
                        //}
                }
                return "0";
            }
            catch(Exception x)
            {
                return "3";
            }
        }


        

        //定时器定时刷新Session
        public void loginSession() {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            _ISysPersonDao.GetSessionuser(user.UserName);
        }

        /// <summary>
        /// 记录用户登录的次数和最后登录的时间
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Log_records(string user)
        {
            //string value = Getidby_name(user);
            if (_ISysPersonDao.NUpdatedata(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户名获取ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string Getidby_name(string user)
        {
            IList<SysPersonView> list = _ISysPersonDao.GetModelByname(user);
            string value = string.Empty;
            if (list != null)
            {
                value = list[0].Id;
            }
            return value;
        }

        ISysLog_historyDao _ISysLog_historyDao = ContextRegistry.GetContext().GetObject("SysLog_historyDao") as ISysLog_historyDao;
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool SysLog_history(string user)
        {
            try
            {
                string value = Getidby_name(user);
                SysLog_historyView syslog_history = new SysLog_historyView();
                syslog_history.Personid = value;
                syslog_history.Personname = user;
                //获取客户端IP
                string kdIp = GetIP();//Request.ServerVariables.Get("Remote_Addr").ToString();
                //获取IP
                //string ip = Request.UserHostName;
                //IPHostEntry hostInfo = Dns.GetHostByAddress(ip); 
                //var entry = System.Net.Dns.GetHostEntry(ip);
                //获取客户端计算机名称
                //  var name = entry.HostName;
                //syslog_history.Client_name = hostInfo.HostName;
                syslog_history.LogonIP = kdIp;
                syslog_history.LogonTime = DateTime.Now;

                if (_ISysLog_historyDao.Ninsert(syslog_history))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public ActionResult Main()
        {
            return View("../Main");
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public string GetIP()
        {
            try
            {
                HttpRequest request = System.Web.HttpContext.Current.Request;
                //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
                string userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(userHostAddress))
                    userHostAddress = userHostAddress.ToString().Split(',')[0].Trim();
                //否则直接读取REMOTE_ADDR获取客户端IP地址
                else if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
                if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
                {
                    return userHostAddress;
                }

                return "127.0.0.1";
            }

            catch
            {
                return "127.0.0.1";
            }

        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }



        #region //K3 接口查询
        
        #endregion
    }
}
