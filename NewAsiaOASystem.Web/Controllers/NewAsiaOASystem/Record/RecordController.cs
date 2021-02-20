using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web.Controllers
{
    public class RecordController : Controller
    {
        //
        // GET: /NA_CustomerRecord/

        public ActionResult Index()
        {
            return View();
        }

        #region //列表
        public ActionResult list()
        {
            return View();
        }
        #endregion

    }
}
