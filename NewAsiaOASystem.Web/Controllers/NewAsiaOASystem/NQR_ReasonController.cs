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
    /// 返退货原因管理
    /// tjy_satan
    /// </summary>
    public class NQR_ReasonController : Controller
    {
        //
        // GET: /NQR_Reason/
        INQR_ReasonDao _INQR_ReasonDao = ContextRegistry.GetContext().GetObject("NQR_ReasonDao") as INQR_ReasonDao;
    

        #region //数据列表页面

        public ActionResult List(int? pageIndex)
        {
            PagerInfo<NQR_ReasonView> listmodel = GetListPager(pageIndex, null);
            return View(listmodel);
        }


        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NQR_ReasonView> listmodel = GetListPager(pageIndex, Name);
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
        private PagerInfo<NQR_ReasonView> GetListPager(int? pageIndex, string Name)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INQR_ReasonDao.SetPagerPageIndex(CurrentPageIndex);
            _INQR_ReasonDao.SetPagerPageSize(11);
            PagerInfo<NQR_ReasonView> listmodel = _INQR_ReasonDao.GetCinfoList(Name, Suser);
            return listmodel;
        }
        #endregion

        #region //增加跳转页面
        public ActionResult addPage()
        {
            AlbumDropdown(null);
            return View("Edit", new NQR_ReasonView());
        }
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(NQR_ReasonView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                model.ParentId = Request.Form["ReasonListData"];
                if (model.ParentId == null||model.ParentId=="")
                    model.ParentId = "0";
                //添加
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Name = model.Name;
                    model.CreatePerson = Convert.ToString(user.UserName);
                    model.CreateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQR_ReasonDao.Ninsert(model);
                }
                //修改
                else
                {
                    model.Name = model.Name;
                    model.UpdatePerson = model.UpdatePerson;
                    model.UpdateTime = DateTime.Now;
                    model.Status = model.Status;
                    model.Sort = model.Sort;
                    flay = _INQR_ReasonDao.NUpdate(model);
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
            NQR_ReasonView sys = new NQR_ReasonView();
            if (!string.IsNullOrEmpty(id))
                sys = _INQR_ReasonDao.NGetModelById(id);
            AlbumDropdown(sys.ParentId);
            return View("Edit", sys);
        }
        #endregion


        #region 初始化设置下拉框值
       /// <summary>
        /// 初始化设置下拉框值
       /// </summary>
       /// <param name="SelectedPID">设置默认上级菜单</param>
        public void AlbumDropdown( string SelectedPID)
        {
            List<NQR_ReasonView> ReasonlistView = _INQR_ReasonDao.GetlistisPar() as List<NQR_ReasonView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (ReasonlistView != null)
            {
                foreach (var item in ReasonlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != "0")
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["ReasonList"] = new SelectList(Reasonlist, "Id", "Name");

        }
          #endregion






    }
}
