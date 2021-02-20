using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_chuhuoinfonController : Controller
    {
        //
        // GET: /返退货处理 出货单 管理/
        INQ_chuhuoinfonDao _INQ_chuhuoinfonDao = ContextRegistry.GetContext().GetObject("NQ_chuhuoinfonDao") as INQ_chuhuoinfonDao;

        public ActionResult Index()
        {
            return View();
        }

    }
}
