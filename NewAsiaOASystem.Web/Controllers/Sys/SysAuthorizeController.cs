using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Context.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.Web.Controllers
{
    public class SysAuthorizeController : Controller
    {

        ISysAuthorizeDao _ISysAuthorizeDao = ContextRegistry.GetContext().GetObject("SysAuthorizeDao") as ISysAuthorizeDao; 
        public ActionResult Index(int? pageIndex)
        {
            string json = _ISysAuthorizeDao.GetTreeAuthorize();
            ViewBag.result = json;
            return View("SysAuthorizeGrid");
        }

        #region 获取数据

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public SysAuthorizeView GetModelById(string ID)
        {
            return _ISysAuthorizeDao.NGetModelById(ID);
        } 
        #endregion

        #region 保存处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysAuthorizeView model)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.CreateTime = DateTime.Now;
                    model.CreatePerson = user.UserName;
                    flay = _ISysAuthorizeDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Id = model.Id;
                    model.UpdateTime = DateTime.Now;
                    model.UpdatePerson = user.UserName;
                    flay = _ISysAuthorizeDao.NUpdate(model);
                }

                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
               
            }

            catch {
                return Json(new { result = "error" });
            }
        }

        #endregion 

        public ActionResult Delete(string id)
        {
            bool i = false;
            List<SysAuthorizeView> sys = _ISysAuthorizeDao.NGetList_id(id) as List<SysAuthorizeView>;
           if (null!=sys)
           {
               i = _ISysAuthorizeDao.NDelete(sys);
           }

           return RedirectToAction("Index");
        }  

        /// <summary>
        /// 跳转到添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {
            return View("SysAuthorizeEdit", new SysAuthorizeView());
        }

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            SysAuthorizeView sys = new SysAuthorizeView();
            if (!string.IsNullOrEmpty(id))
                sys = GetModelById(id);
            //根据上级ID获取到上级记录
            if (!string.IsNullOrEmpty(sys.ParentId))
            {
                SysAuthorizeView Sys1 = GetModelById(sys.ParentId);
                ViewBag.result = Sys1.Name;
            }
            return View("SysAuthorizeEdit", sys);
        }
    }
}
