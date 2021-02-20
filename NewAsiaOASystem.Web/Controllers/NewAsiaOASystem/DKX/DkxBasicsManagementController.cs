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
using System.Data.OleDb;

namespace NewAsiaOASystem.Web.Controllers
{
    public class DkxBasicsManagementController : Controller
    {
        //
        // GET: /电控箱生产的基础信息管理控制器/
        //生产订单退单原因
        INewChargebackReasonDao _INewChargebackReasonDao = ContextRegistry.GetContext().GetObject("NewChargebackReasonDao") as INewChargebackReasonDao;
 
        #region //订单退单原因
        public ActionResult ChargebackReasonlistView(int? pageIndex)
        {
            IsRemarksDropdown(null);
            IsDisDropdown(null);
            PagerInfo<NewChargebackReasonView> listmodel = GetChargebackReasonlistpage(pageIndex, null, "0",null,"0");
            return View(listmodel);
        }

        #region //多条件查询
        public JsonResult ChargebackReasonSearchList()
        {
            string name = Request.Form["txtSearch_Name"];//退单原因名称
            string type = "0";//默认生产退单
            string IsRemarks = Request.Form["txt_IsRemarks"];//是否需要备注
            string IsDis = Request.Form["txt_IsDis"];//是否禁用
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NewChargebackReasonView> listmodel = GetChargebackReasonlistpage(pageIndex, name, type,IsRemarks,IsDis);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });

        }
        #endregion

        #region //获取分页数据
        private PagerInfo<NewChargebackReasonView> GetChargebackReasonlistpage(int? pageIndex, string name, string type, string IsRemarks, string IsDis)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INewChargebackReasonDao.SetPagerPageIndex(CurrentPageIndex);
            _INewChargebackReasonDao.SetPagerPageSize(10);
            PagerInfo<NewChargebackReasonView> listmodel = _INewChargebackReasonDao.GetChargebackReasonlistpage(name, type, IsRemarks, IsDis);
            return listmodel;
        }
        #endregion

        #region //保存提交的方法
        [HttpPost]
        public JsonResult CBREdit(NewChargebackReasonView model, FrameController from)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                if (string.IsNullOrEmpty(model.Id))//新增
                {
                    model.IsRemarks = Convert.ToInt32(Request.Form["IsRemarks"]);
                    model.Type = Convert.ToInt32(Request.Form["txttype"]);
                    model.IsDis = Convert.ToInt32(Request.Form["txtIsDis"]);
                    model.C_userId = user.Id;
                    model.C_time = DateTime.Now;
                    flay = _INewChargebackReasonDao.Ninsert(model);
                }
                else
                { //修改
                    model.IsRemarks = Convert.ToInt32(Request.Form["IsRemarks"]);
                    model.Type = Convert.ToInt32(Request.Form["txttype"]);
                    model.IsDis = Convert.ToInt32(Request.Form["txtIsDis"]);
                    model.Up_userId = user.Id;
                    model.Up_time = DateTime.Now;
                    flay = _INewChargebackReasonDao.NUpdate(model);
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
        public ActionResult CBRaddPage()
        {
            IsRemarksDropdown(null);
            CBTYPEDropdown(null);
            IsDisDropdown(null);
            return View("CBREdit", new NewChargebackReasonView());
        }
        #endregion
        #region //编辑跳转页面
        public ActionResult CBREditPage(string id)
        {
            NewChargebackReasonView sys = new NewChargebackReasonView();
            if (!string.IsNullOrEmpty(id))
                sys = _INewChargebackReasonDao.NGetModelById(id);
            IsRemarksDropdown(sys.IsRemarks.ToString());
            CBTYPEDropdown(sys.Type.ToString());
            IsDisDropdown(sys.IsDis.ToString());
            return View("CBREdit", sys);
        }
        #endregion

        #region //是否需要备注
        public void IsRemarksDropdown(string SelectedPID)
        {
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Id = "0";
            Reasonmodel.Name = "不需要";
            Reasonlist.Add(Reasonmodel);
            Reasonmodel = new GetReasonlist();
            Reasonmodel.Id = "1";
            Reasonmodel.Name = "需要";
            Reasonlist.Add(Reasonmodel);
            if (SelectedPID != null)
                ViewData["IsRemarks"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["IsRemarks"] = new SelectList(Reasonlist, "Id", "Name");

        }
        #endregion
        #region //订单退回环节
        public void CBTYPEDropdown(string SelectedPID)
        {
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Id = "0";
            Reasonmodel.Name = "生产退单";
            Reasonlist.Add(Reasonmodel);
            if (SelectedPID != null)
                ViewData["CBTYPE"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["CBTYPE"] = new SelectList(Reasonlist, "Id", "Name");
        }

        #endregion
        #region //是否禁用的下来选项
        public void IsDisDropdown(string SelectedPID)
        {
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Id = "0";
            Reasonmodel.Name = "启用";
            Reasonlist.Add(Reasonmodel);
            Reasonmodel = new GetReasonlist();
            Reasonmodel.Id = "1";
            Reasonmodel.Name = "禁用";
            Reasonlist.Add(Reasonmodel);
            if (SelectedPID != null)
                ViewData["IsDis"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["IsDis"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion
        #endregion

    }
}
