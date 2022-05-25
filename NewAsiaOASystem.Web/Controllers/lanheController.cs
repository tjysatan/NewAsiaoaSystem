using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.DBModel;

using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using Newtonsoft.Json;
using System.Data.OleDb;
using System.Data;
using System.IO;
using NewAsiaOASystem.Web.Controllers.NewAsiaOASystem;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Xml;

namespace NewAsiaOASystem.Web.Controllers
{
    public class lanheController : Controller
    {
        //
        // GET: /lanhe/
        IWL_GcsinfoDao _IWL_GcsinfoDao = ContextRegistry.GetContext().GetObject("WL_GcsinfoDao") as IWL_GcsinfoDao;

        Ilanhe_QzCusinfoDao _Ilanhe_QzCusinfoDao = ContextRegistry.GetContext().GetObject("lanhe_QzCusinfoDao") as Ilanhe_QzCusinfoDao;

        public ActionResult Index()
        {
            return View();
        }

        #region //提交页面
        public ActionResult apply()
        {
            return View("QzCuslist");
        }

        //提交保存
        [HttpPost]
        public JsonResult SavesqinfoEide(string cusname,string lxname,string tel,string hy)
        {
            try
            {
                //检查电话号码是否查复提交了
                bool flay = _Ilanhe_QzCusinfoDao.checktelIscf(tel);
                if(!flay)
                    return Json(new { result = "error", msg ="该电话号码已经申请成功，不能重复提交" });
                lanhe_QzCusinfoView model = new lanhe_QzCusinfoView();
                model.Cusname = cusname;
                model.c_time = DateTime.Now;
                model.hy = hy;
                model.lxname = lxname;
                model.lxtle = tel;
                if(_Ilanhe_QzCusinfoDao.Ninsert(model))
                    return Json(new { result = "success", msg = "申请成功！我们会尽快与您联系。" });
                else
                    return Json(new { result = "error", msg = "网络异常，请重新再试。" });
            }
            catch (Exception x)
            {
                return Json(new { result = "error", msg = x });
            }
        }
        #endregion
    }
}
