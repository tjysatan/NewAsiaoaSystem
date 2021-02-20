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
    public class NQ_GysInfoController : Controller
    {
        //
        // GET: /NQ_GysInfo/
        INQ_GysInfoDao _INQ_GysInfoDao = ContextRegistry.GetContext().GetObject("NQ_GysInfoDao") as INQ_GysInfoDao;

        #region //数据列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQ_GysInfoView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        } 
        #endregion

        #region //查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQ_GysInfoView> listmodel = GetListPager(pageIndex, Name);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        } 
        #endregion
        

        #region //获取分页数据
        private PagerInfo<NQ_GysInfoView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQ_GysInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INQ_GysInfoDao.SetPagerPageSize(11);
            PagerInfo<NQ_GysInfoView> listmodel = _INQ_GysInfoDao.GetCinfoList(Name, Suser);
            return listmodel;
        } 
        #endregion


        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQ_GysInfoView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.G_Dm = model.G_Dm;
                    model.G_Name = model.G_Name;
                    model.G_Lxr = model.G_Lxr;
                    model.G_Dz = model.G_Dz;
                    model.G_Tel = model.G_Tel;
                    model.CreatePerson = Convert.ToString(user.UserName);
                    model.CreateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQ_GysInfoDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.G_Dm = model.G_Dm;
                    model.G_Name = model.G_Name;
                    model.G_Lxr = model.G_Lxr;
                    model.G_Dz = model.G_Dz;
                    model.G_Tel = model.G_Tel;
                    model.UpdatePerson = model.UpdatePerson;
                    model.UpdateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQ_GysInfoDao.NUpdate(model);
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
            return View("Edit", new NQ_GysInfoView());
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id)
        {
            NQ_GysInfoView sys = new NQ_GysInfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQ_GysInfoDao.NGetModelById(id);
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
                List<NQ_GysInfoView> sys = _INQ_GysInfoDao.NGetList_id(id) as List<NQ_GysInfoView>;
                if (null != sys)
                {
                    if (_INQ_GysInfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NQ_GysInfo/List");
        }
        #endregion

        #region //根据供应商代码查找供应商信息
        [HttpPost]
        public string Getgysinfobydm(string dm)
        {
            string json;
            json = _INQ_GysInfoDao.Getgysmodelbydm(dm);
            return json;
        } 
        #endregion
    }
}
