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

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_BlxxinfoController : Controller
    {
        //不良现象
        // GET: /NQ_Blxxinfo/

        INQ_BlxxinfoDao _INQ_BlxxinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlxxinfoDao") as INQ_BlxxinfoDao;

        #region //数据列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQ_BlxxinfoView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_BlxxinfoView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取数据列表
        private PagerInfo<NQ_BlxxinfoView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_BlxxinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_BlxxinfoDao.SetPagerPageSize(11);
            PagerInfo<NQ_BlxxinfoView> listmodel = _INQ_BlxxinfoDao.GetblxxinfoList(Name, Suser);
            return listmodel;
        }
        #endregion

        #region //增加跳转页面
        public ActionResult addPage()
        {
           // AlbumDropdown(null);
            return View("Edit", new NQ_BlxxinfoView());
        }
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQ_BlxxinfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Name = model.Name;
                  //  model.CreatePerson = Convert.ToString(user.Id);
                    model.C_Data = DateTime.Now;
                    flay = _INQ_BlxxinfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Name = model.Name;
                    flay = _INQ_BlxxinfoDao.NUpdate(model);
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

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NQ_BlxxinfoView sys = new NQ_BlxxinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_BlxxinfoDao.NGetModelById(id);
            return View("Edit", sys);
        }
        #endregion

        #region //删除
        public ActionResult Delete()
        {
            try
            {
                //操作是否成功 
                string id = Request.QueryString["id"].ToString();
                List<NQ_BlxxinfoView> sys = _INQ_BlxxinfoDao.NGetList_id(id) as List<NQ_BlxxinfoView>;
                if (null != sys)
                {
                    if (_INQ_BlxxinfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NQ_Blxxinfo/List");
        }
        #endregion

        #region //查找全部的不良现象
        [HttpPost]
        public string GetAllParlist()
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_BlxxinfoDao.NGetList());
            return json;
        }
        #endregion

        #region //根据ID 查找不良现象
        [HttpPost]
        public string GetblXXmodelbyId(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_BlxxinfoDao.NGetModelById(Id));
            return json;
        }
        #endregion

    }
}
