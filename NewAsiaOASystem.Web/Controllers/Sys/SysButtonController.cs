using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.Web;
using Spring.Context.Support;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;

namespace NewAsiaOASystem.Web.Controllers
{
    public class SysButtonController : Controller
    {
        ISysButtonDao _ISysButtonDao = ContextRegistry.GetContext().GetObject("SysButtonDao") as ISysButtonDao;
        public ActionResult Index(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _ISysButtonDao.SetPagerPageIndex(CurrentPageIndex);
            _ISysButtonDao.SetPagerPageSize(10);
            PagerInfo<SysButtonView> listmodel = _ISysButtonDao.Search();
            return View("SysButtonGrid",listmodel);
        }

        #region 获取数据

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public SysButtonView GetModelById(string ID)
        {
            return _ISysButtonDao.NGetModelById(ID);
        }

    
        #endregion

        #region 保存处理方法
     
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
       
        public JsonResult Edit()
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser; 
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                string ID = Request.Form["ID"];
                SysButtonView model = new SysButtonView();
                model.Name = model.Name;
                string sort = Request.Form["Sort"];
                if (string.IsNullOrEmpty(sort))
                {
                    sort = "0";
                }

                model.Sort = Convert.ToInt32(sort);
                model.Remark = Request.Form["Remark"];
                model.UpdateTime = DateTime.Now;
                model.Status = Convert.ToInt32(Request.Form["Status"]);
                model.UpdatePerson = Convert.ToString(Suser.UserName);
                HttpPostedFileBase imgFile = Request.Files["Ico"];
                string IcoUrl = Request.Form["HIco"];//保存的图片路径
                CommonClass cm = new CommonClass();
                if (null != imgFile)
                {
                    string url = cm.upLoadImg(imgFile);
                    if (!string.IsNullOrEmpty(url))
                    {
                        model.Ico = url;
                    }
                    else
                    {
                        model.Ico = IcoUrl;
                    }
                }
                //var webAppContent = ContextRegistry.GetContext();
                //SysButtonService = webAppContent.GetObject("SysButtonService") as ISysButtonService;

                bool flay = false;
                //添加
                if (string.IsNullOrEmpty(ID))
                {
                    model.CreatePerson = user.UserName;
                    model.CreateTime = DateTime.Now;
                    flay = _ISysButtonDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Id = ID;
                    model.Name = model.Name;
                    model.UpdateTime =Convert.ToDateTime( Request.Form["CreateTime"]);
                    model.UpdatePerson = user.UserName;
                    flay = _ISysButtonDao.NUpdate(model);
                }


                if (flay)
                {
                    return Json(new { result = "success" },"text/html");
                }

                else
                {
                    return Json(new { result = "error" }, "text/html");
                }
            }

            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
        }

        #endregion 

        public ActionResult Delete(string id)
        {
            bool i = false;
            List<SysButtonView> sys = _ISysButtonDao.NGetList_id(id) as List<SysButtonView>;
           if (null!=sys)
           {
               i = _ISysButtonDao.NDelete(sys);
           }

           return RedirectToAction("Index");
        }

        /// <summary>
        /// 跳转到添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {
            ViewBag.action = "addPage";
            ViewBag.result = new SysButtonView();
            return View("SysButtonEdit");
        }

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            ViewBag.action = "EditPage";
            SysButtonView sys = GetModelById(id);
            if (null==sys)
            {
                sys = new SysButtonView();
            }
            ViewBag.result = sys;
            return View("SysButtonEdit");
        }
    }
}
