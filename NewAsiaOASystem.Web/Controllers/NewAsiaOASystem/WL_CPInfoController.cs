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
    public class WL_CPInfoController : Controller
    {
        //
        // GET: /WL_CPInfo/
        IWL_CPInfoDao _IWL_CPInfoDao = ContextRegistry.GetContext().GetObject("WL_CPInfoDao") as IWL_CPInfoDao;

        //分页列表页面
        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<WL_CPInfoView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        }
        #endregion

        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["Qyname"];//区域名称
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<WL_CPInfoView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<WL_CPInfoView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWL_CPInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IWL_CPInfoDao.SetPagerPageSize(11);
            PagerInfo<WL_CPInfoView> listmodel = _IWL_CPInfoDao.GetWLcpinfoList(Name, Suser);
            return listmodel;
        }
        #endregion
        #endregion


        #region 保存处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(WL_CPInfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;

                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Name = model.Name;//产品名称
                    model.Dj = model.Dj;//单价
                    model.Sort = model.Sort;//排序
                    model.States = model.States;//是否启用
                    model.Beizhu = model.Beizhu;//备注
                    model.C_time = DateTime.Now;//创建时间
                    model.C_name = user.Id;//创建人
                    flay = _IWL_CPInfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Name = model.Name;//产品名称
                    model.Dj = model.Dj;//单价
                    model.Sort = model.Sort;//排序
                    model.States = model.States;//是否启用
                    model.Beizhu = model.Beizhu;//备注
                    flay = _IWL_CPInfoDao.NUpdate(model);
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
            return View("Edit", new WL_CPInfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            WL_CPInfoView sys = new WL_CPInfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IWL_CPInfoDao.NGetModelById(id);
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
                List<WL_CPInfoView> sys = _IWL_CPInfoDao.NGetList_id(id) as List<WL_CPInfoView>;
                if (null != sys)
                {
                    if (_IWL_CPInfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../WL_CPInfo/List");
        }
        #endregion
 

    }
}
