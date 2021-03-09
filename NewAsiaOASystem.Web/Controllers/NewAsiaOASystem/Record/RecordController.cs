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
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace NewAsiaOASystem.Web.Controllers
{
    public class RecordController : Controller
    {
        //
        // GET: /NA_CustomerRecord/
        //INA_QyinfoDao _INA_QyinfoDao = ContextRegistry.GetContext().GetObject("NA_QyinfoDao") as INA_QyinfoDao;
        INA_CustomerRecordDao _INA_CustomerRecordDao = ContextRegistry.GetContext().GetObject("NA_CustomerRecordDao") as INA_CustomerRecordDao;

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

        #region //备案新增编辑页面
        public ActionResult AddRecordView(string id)
        {
            NA_CustomerRecordView model = new NA_CustomerRecordView();
            if (!string.IsNullOrEmpty(id))
                model = _INA_CustomerRecordDao.NGetModelById(id);
            return View(model); 
        }
        #endregion

        #region //
        [HttpPost]
        public JsonResult Edit(NA_CustomerRecordView model)
        {
            try {
                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                if (!string.IsNullOrEmpty(model.Id)) { //修改
                    if (_INA_CustomerRecordDao.NUpdate(model))
                        return Json(new { resule = "success", msg = "新增成功。" });
                    else
                        return Json(new { resule = "error", msg = "新增保存失败。" });
                }
                else//新增
                {
                    model.c_time = DateTime.Now;//记录创建时间
                    model.Rdatatime = DateTime.Now;//备案时间
                    model.Rexpiredatetime = Convert.ToDateTime(DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(3).ToShortDateString());
                    model.Roverdue = 0;//未过期
                    model.Rname = Suser.RName;
                    model.RnameId = Suser.Id;
                    model.Rdeal = 0;//未成交
                    if (_INA_CustomerRecordDao.Ninsert(model))
                        return Json(new { resule = "success", msg = "新增成功。" });
                    else
                        return Json(new { resule = "error", msg = "新增保存失败。" });
                }
            } catch {
                return Json(new { result = "success",msg="操作异常，请联系管理员。" });
            }
            
        }
        #endregion

    }
}
