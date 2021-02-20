using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;
using System.Net;

namespace NewAsiaOASystem.Web.Controllers
{
    public class WxconfigController : Controller
    {
        //
        // GET: /Wxconfig/
        INAReturnListDao _INAReturnListDao = ContextRegistry.GetContext().GetObject("NAReturnListDao") as INAReturnListDao;
        IWx_configinfoDao _IWx_configinfoDao = ContextRegistry.GetContext().GetObject("Wx_configinfoDao") as IWx_configinfoDao;

        //平台微信公众号参数设置页面
        public ActionResult WeixinConfigView()
        {
            Wx_configinfoView model = new Wx_configinfoView();
            model = _IWx_configinfoDao.Getweixinpayconfig();
            if (model == null)
            {
                model = new Wx_configinfoView();
            }
            return View("Edit", model);
        }

        //提交平台微信公众号的参数
        [HttpPost]
        public JsonResult Edit(Wx_configinfoView model, FormCollection from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.C_datetime = DateTime.Now;
                    model.C_name = user.Id;
                    model.Type = 0;//平台公众号参数
                    flay = _IWx_configinfoDao.Ninsert(model);
                }
                else//修改
                {
                    model.Up_datetime = DateTime.Now;
                    model.Up_name = user.Id;
                    flay = _IWx_configinfoDao.NUpdate(model);
                }
                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            }
            catch
            {
                return Json(new { result = "error" });//提交失败
            }
        }
    }
}
