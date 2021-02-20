using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using Spring.Context.Support;


namespace NewAsiaOASystem.Web
{
    public class AdminController : Controller
    {

        public ActionResult Index()
        {
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            if (Suser == null)
            {
                return RedirectToAction("../Login/Login");
            }
            else
            {
                SysPerson person = _ISysPersonDao.GetModelByLogin(Suser.UserName);
                ViewBag.user = Convert.ToString(person.UserName);
            }
            return View();
        }

        /// <summary>
        /// 获取右侧导航菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetleftnavMenu()
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            string Menu = Convert.ToString(Suser.MenuLeft);
            return Json(new { result = Menu });
        }

        //登录后显示的右侧页面
        public ActionResult h_right()
        {
            //SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            //IDisNoFloatingChild_StatisticsDao _IDisFloatingChild_StatisticsDao =
            //    ContextRegistry.GetContext().GetObject("DisNoFloatingChild_StatisticsDao") as IDisNoFloatingChild_StatisticsDao;
            //DisIndex_StatisticsView view = _IDisFloatingChild_StatisticsDao.GetIndexData(Suser);
            return View();
        }

        //错误跳转页面
        public ActionResult Err404()
        {
            return View();
        }

        public void loginsession()
        {
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            _ISysPersonDao.loginsession(Suser);
        }

    }
}
