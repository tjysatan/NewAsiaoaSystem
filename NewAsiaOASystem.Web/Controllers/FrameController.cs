using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web.Controllers
{
    public class FrameController : Controller
    {
        /// <summary>
        /// 显示左边菜单栏
        /// </summary>
        /// <returns></returns>
        public ActionResult left()
        {
            return View("../left");
        }

        /// <summary>
        /// 显示头部菜单
        /// </summary>
        /// <returns></returns>

        public ActionResult top()
        {
            return View("../top");
        }


        public ActionResult Index()
        {

            return View();
        }

    }
}
