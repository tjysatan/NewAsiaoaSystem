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
    /// <summary>
    /// 返退货产品类型管理
    /// tjy_satan
    /// </summary>
    public class NQR_ProductController : Controller
    {
        INQR_ProductDao _INQR_ProductDao = ContextRegistry.GetContext().GetObject("NQR_ProductDao") as INQR_ProductDao;

        #region //数据列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQR_ProductView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        }


        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQR_ProductView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NQR_ProductView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQR_ProductDao.SetPagerPageIndex(CurrentPageIndex);
            _INQR_ProductDao.SetPagerPageSize(11);
            PagerInfo<NQR_ProductView> listmodel = _INQR_ProductDao.GetCinfoList(Name, Suser);
            return listmodel;
        } 
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQR_ProductView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Name = model.Name;
                    model.CreatePerson = Convert.ToString(user.UserName);
                    model.CreateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQR_ProductDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Name = model.Name;
                    model.UpdatePerson = model.UpdatePerson;
                    model.UpdateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQR_ProductDao.NUpdate(model);
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

        #region //增加跳转页面
        public ActionResult addPage()
        {
            return View("Edit", new NQR_ProductView() );
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NQR_ProductView sys = new NQR_ProductView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQR_ProductDao.NGetModelById(id);
            return View("Edit", sys);
        }
        #endregion

        #region //删除


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            try
            {
                //操作是否成功 
                string id = Request.QueryString["id"].ToString();
                List<NQR_ProductView> sys = _INQR_ProductDao.NGetList_id(id) as List<NQR_ProductView>;
                if (null != sys)
                {
                    if (_INQR_ProductDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NQR_Product/List");
        }
        #endregion

    }
}
