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
    /// 免疫点管理
    /// </summary>
    public class DisImmuneCenterController : Controller
    {
        IDisImmuneCenterDao _IDisImmuneCenterDao = ContextRegistry.GetContext().GetObject("DisImmuneCenterDao") as IDisImmuneCenterDao;

        #region 获取数据列表
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<DisImmuneCenterView> listmodel = GetListPager(pageIndex, null, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            string CommId = Request.Form["CommId"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DisImmuneCenterView> listmodel = GetListPager(pageIndex, Name, CommId);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">社区名称</param>
        /// <param name="CommID">所属区域ID</param>
        /// <returns></returns>
        private PagerInfo<DisImmuneCenterView> GetListPager(int? pageIndex, string Name, string CommID)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDisImmuneCenterDao.SetPagerPageIndex(CurrentPageIndex);
            _IDisImmuneCenterDao.SetPagerPageSize(11);
            PagerInfo<DisImmuneCenterView> listmodel = _IDisImmuneCenterDao.GetIcList(Name, CommID, Suser);
            //AlbumDropdownList(null);
            return listmodel;
        }

        #endregion

        #region 增删改
        /// <summary>
        /// 编辑处理的方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public JsonResult Edit(DisImmuneCenterView model)
        {
            try
            {
                //model.ParentId = Request.Form["SelectComm"];
                //if (Request.Form["SelectComm"] == "" || Request.Form["Name"] == "")
                //{
                //    return Json(new { result = "Nameerror" }, "text/html");
                //}
                //if (Request.Form["D_Lat"] == "" || Request.Form["D_lon"] == "")
                //{
                //    return Json(new { result = "Pointerror" }, "text/html");
                //}

                SessionUser Suser = Session[SessionHelper.User] as SessionUser;
                //操作是否成功
                bool flay = false;
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.CreateTime = DateTime.Now;
                    model.CreatePerson = Convert.ToString(Suser.UserName);
                    flay = _IDisImmuneCenterDao.Ninsert(model);
                }
                else
                {
                    model.UpdateTime = DateTime.Now;
                    model.UpdatePerson = Convert.ToString(Suser.UserName);
                    flay = _IDisImmuneCenterDao.NUpdate(model);
                }

                if (flay)
                    return Json(new { result = "success" }, "text/html");

                else
                    return Json(new { result = "error" }, "text/html");
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
        }


        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View("Edit", new DisImmuneCenterView());
        }


        /// <summary>
        /// 修改跳转页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            ViewBag.action = "EditPage";
            string id = Request.QueryString["id"].ToString();
            DisImmuneCenterView sys = _IDisImmuneCenterDao.NGetModelById(id);
            ViewBag.result = sys;
            return View("Edit", sys);
        }


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
                List<DisImmuneCenterView> sys = _IDisImmuneCenterDao.NGetList_id(id) as List<DisImmuneCenterView>;
                if (null != sys)
                {
                    if (_IDisImmuneCenterDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../DisImmuneCenter/List");

        }

        #endregion

        /// <summary>
        /// 设置下拉框值(编辑页面时)
        /// </summary>
        /// <param name="SelectedID">需要选中的Value值</param>
        public JsonResult AlbumDropdown(string commId)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            string json = _IDisImmuneCenterDao.AlbumDropdown(commId, Suser);
            return Json(new { result = json }, "text/html");
        }


        /// <summary>
        /// 获取当前登录用户所能查看到的社区数据
        /// </summary>
        /// <returns></returns>
        //public JsonResult GetSheQuCenter()
        //{
        //    SessionUser user = Session[SessionHelper.User] as SessionUser;
        //   // string json = _IDisImmuneCenterDao.GetSheQu(user);
        //    return Json(new { result = json });
        //}
    }
}
