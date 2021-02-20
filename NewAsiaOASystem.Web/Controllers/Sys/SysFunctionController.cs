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
    public class SysFunctionController : Controller
    {

        ISysFunctionDao _ISysFunctionDao = ContextRegistry.GetContext().GetObject("SysFunctionDao") as ISysFunctionDao;
        public ActionResult Index(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _ISysFunctionDao.SetPagerPageIndex(CurrentPageIndex);
            _ISysFunctionDao.SetPagerPageSize(11);
            PagerInfo<SysFunctionView> listmodel = _ISysFunctionDao.Search();
            AlbumDropdown(null);
            return View("SysFunctionGrid", listmodel);
        }

        #region 获取数据

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public SysFunctionView GetModelById(string ID)
        {
            return _ISysFunctionDao.NGetModelById(ID);
        }
        #endregion

        #region 保存处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysFunctionView model)
        {
            try
            {
                if (Request.Form["Name"] == "")
                {
                    return Json(new { result = "error" });
                }
                model.ActionUrl = Request.Form["ActionData"];
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                bool flay = false;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    //先检验action是否已经存在
                    if (!_ISysFunctionDao.CheckAction(model.ActionUrl))
                        return Json(new { result = "error" });
                    model.CreateTime = DateTime.Now;
                    model.CreatePerson = user.UserName;
                    flay = _ISysFunctionDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Id = model.Id;
                    model.UpdateTime = DateTime.Now;
                    model.UpdatePerson = user.UserName;
                    flay = _ISysFunctionDao.NUpdate(model);
                }

                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });

            }

            catch
            {
                return Json(new { result = "error" });
            }
        }

        #endregion

        public ActionResult Delete(string id)
        {
            bool i = false;
            List<SysFunctionView> sys = _ISysFunctionDao.NGetList_id(id) as List<SysFunctionView>;
            if (null != sys)
            {
                i = _ISysFunctionDao.NDelete(sys);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 跳转到添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult addPage()
        {
            AlbumDropdown(null);
            return View("SysFunctionEdit", new SysFunctionView());
        }

        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            SysFunctionView sys = new SysFunctionView();
            if (!string.IsNullOrEmpty(id))
            {
                sys = GetModelById(id);
                AlbumDropdown(sys.ActionUrl);
            }
            return View("SysFunctionEdit", sys);
        }

        /// <summary>
        /// 设置下拉框值
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        public void AlbumDropdown(string SelectedID)
        {
            string url = "";
            GetControllerInfo ControllerInfo = new GetControllerInfo();
            List<GetControllerInfoView> controller = ControllerInfo.GetFromControllerInfo();
            List<GetControllerInfoView> controllerUrl = new List<GetControllerInfoView>();

            foreach (var Conitem in controller)
            {
                url = "/" + Conitem.ControllerName + "/" + Conitem.ActionName;
                GetControllerInfoView ControllerInfoView = new GetControllerInfoView();
                ControllerInfoView.ControllerName = url;
                ControllerInfoView.ActionName = url;
                controllerUrl.Add(ControllerInfoView);
            }
            if (SelectedID != null)
                ViewData["ActionUrl"] = new SelectList(controllerUrl, "ControllerName", "ActionName", SelectedID);
            else
                ViewData["ActionUrl"] = new SelectList(controllerUrl, "ControllerName", "ActionName");
        }

    }
}
