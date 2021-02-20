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
using System.IO;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Data;
using System.Xml;

namespace NewAsiaOASystem.Web.Controllers
{
    public class KSAftersaleController : Controller
    {
        //
        // GET: /客诉售后跟踪模块控制器/
        IKS_CLPersonnelInfoDao _IKS_CLPersonnelInfoDao = ContextRegistry.GetContext().GetObject("KS_CLPersonnelInfoDao") as IKS_CLPersonnelInfoDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        IKS_ProblemTyleDao _IKS_ProblemTyleDao = ContextRegistry.GetContext().GetObject("KS_ProblemTyleDao") as IKS_ProblemTyleDao;

        #region //客诉处理人员信息管理

        #region //客诉处理人员数据列表
        public ActionResult KSCLRYList(int? pageIndex)
        {
            PagerInfo<KS_CLPersonnelInfoView> listmodel = GetKSCLRPagerList(pageIndex, null, null, null, null);
            return View(listmodel);
        } 
        #endregion

        #region //多条件查询
        public JsonResult KSCLRSearchList()
        {
            string Name = Request.Form["txt_name"];//姓名
            string zhname = Request.Form["txt_zhname"];//关联帐号
            string tel = Request.Form["txt_tel"];//联系电话
            string start = Request.Form["txt_start"];//是否禁用
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<KS_CLPersonnelInfoView> listmodel = GetKSCLRPagerList(pageIndex,Name,zhname,tel, start);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //客诉处理人员的分页数据
        private PagerInfo<KS_CLPersonnelInfoView> GetKSCLRPagerList(int? pageIndex, string name, string zhname, string tel, string start)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IKS_CLPersonnelInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IKS_CLPersonnelInfoDao.SetPagerPageSize(10);
            PagerInfo<KS_CLPersonnelInfoView> listmodel = _IKS_CLPersonnelInfoDao.KSGetCLRinfoPagerlist(name,zhname,tel,start);
            return listmodel;
        }
        #endregion

        #region //客诉处理人员编辑

        #region //增加跳转页面
        public ActionResult KSCLRaddPage()
        {
             AllgcsDropdown(null);
             return View("KSCLREdit",new KS_CLPersonnelInfoView());
        }
        #endregion

        #region //跳转到编辑页面
        public ActionResult KSCLREditPage(string id)
        {
            KS_CLPersonnelInfoView sys = new KS_CLPersonnelInfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IKS_CLPersonnelInfoDao.NGetModelById(id);
            AllgcsDropdown(sys.ZH_Id);
            return View("KSCLREdit", sys);
        }
        #endregion

        #region //客诉处理人数据保存提交
        public JsonResult KSCLREdit(KS_CLPersonnelInfoView model, FrameController from)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //操作是否成功
                bool flay = false;
                model.ZH_Id = Request.Form["zhListData"];
                 //判断Id是否存在 插入
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.C_name = user.Id;
                    model.C_time = DateTime.Now;
                    flay = _IKS_CLPersonnelInfoDao.Ninsert(model);
                }
                else
                {
                    model.Up_name = user.Id;
                    model.Up_time = DateTime.Now;
                    flay = _IKS_CLPersonnelInfoDao.NUpdate(model);
                }
                if (flay)
                {
                    return Json(new { result = "success" }, "text/html");
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

        //查询平台所有帐号
        public void AllgcsDropdown(string SelectedID)
        {
            List<SysPersonView> UnitList = new List<SysPersonView>();
            if (UnitList != null)
            {
                SysPersonView model = new SysPersonView();
                model.Name = "全部";
                model.Id = "";
                UnitList.Add(model);
                //List<SysPersonView> getUnitList = _ISysPersonDao.GetPersonbyRoletype("4") as List<SysPersonView>;//角色查询
                List<SysPersonView> getUnitList = _ISysPersonDao.NGetList() as List<SysPersonView>;
                if (getUnitList != null)
                    UnitList.AddRange(getUnitList);
                if (SelectedID != null)
                    ViewData["getADList"] = new SelectList(UnitList, "Id", "Name", SelectedID);
                else
                    ViewData["getADList"] = new SelectList(UnitList, "Id", "Name");
            }

        }
        #endregion
        #endregion

        #region //客诉问题分类信息管理

        #region //客诉问题分类数据列表
        public ActionResult KSWTtyleList(int? pageIndex)
        {
            PagerInfo<KS_ProblemTyleView> listmodel = GetKSProblemTypePager(pageIndex,null,null);
            return View(listmodel);
        }
        #endregion

        #region //多条件查询
        public JsonResult KSWTtyleSearchList()
        {
            string WTname = Request.Form["txtWTname"];//问题分类名称
            string start = Request.Form["txt_start"];//是否禁用
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<KS_ProblemTyleView> listmodel = GetKSProblemTypePager(pageIndex,WTname,start);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //客诉问题分类分页数据
        private PagerInfo<KS_ProblemTyleView> GetKSProblemTypePager(int? pageIndex, string WTName, string start)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                 CurrentPageIndex = 1;
            _IKS_ProblemTyleDao.SetPagerPageIndex(CurrentPageIndex);
            _IKS_ProblemTyleDao.SetPagerPageSize(10);
            PagerInfo<KS_ProblemTyleView> listmodel = _IKS_ProblemTyleDao.KSGetProblemTylePagerlist(WTName,start);
            return listmodel;
        }
        #endregion

        #region //客诉问题分类编辑

        #region //增加跳转页面
        public ActionResult KSWTaddPage()
        {
            return View("KSWTEide",new KS_ProblemTyleView());
        }
        #endregion

        #region //跳转到编辑页面
        public ActionResult KSWTEidePage(string id)
        {
            KS_ProblemTyleView sys = new KS_ProblemTyleView();
            if (!string.IsNullOrEmpty(id))
                sys = _IKS_ProblemTyleDao.NGetModelById(id);
            return View("KSWTEide",sys);
        }
        #endregion

        #region //客诉问题分类数据提交保存
        public JsonResult KSWTEide(KS_ProblemTyleView model, FrameController from)
        {
            try
            {
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                //操作是否成功
                bool flay = false;
                //判断Id是否存在 插入
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.C_name = user.Id;
                    model.C_time = DateTime.Now;
                    flay = _IKS_ProblemTyleDao.Ninsert(model);
                }
                else
                {
                    model.Up_name = user.Id;
                    model.Up_time = DateTime.Now;
                    flay = _IKS_ProblemTyleDao.NUpdate(model);
                }
                if (flay)
                {
                    return Json(new { result = "success" }, "text/html");
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
        #endregion
        #endregion

    }
}
