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
using System.Xml.Linq;
using System.Net;
using System.Data.OleDb;
using System.Data;

namespace NewAsiaOASystem.Web.Controllers
{
    public class PP_ShouruandzhichuDetailsinfoController : Controller
    {
        //
        // GET: /PP_ShouruandzhichuDetailsinfo/
        IPP_ShouruandzhichuDetailsinfoDao _IPP_ShouruandzhichuDetailsinfoDao = ContextRegistry.GetContext().GetObject("PP_ShouruandzhichuDetailsinfoDao") as IPP_ShouruandzhichuDetailsinfoDao;

        #region //列表以及查询页面
        #region //分页列表页面(收入)
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<PP_ShouruandzhichuDetailsinfoView> listmodel = GetListPager(pageIndex, null, null, "0",null,null,null,null);
            return View(listmodel);
        }
        #endregion

        #region //分页列表页面（支出）
        public ActionResult zhichulist(int? pageIndex)
        {
            PagerInfo<PP_ShouruandzhichuDetailsinfoView> listmodel = GetListPager(pageIndex, null, null, "1", null, null, null, null);
            return View(listmodel);
        }
        #endregion

        //条件查询
        #region //条件查询（支出）
        public JsonResult zhichuSearchList()
        {
            string Name = Request.Form["Name"];//员工姓名
            string xmname = Request.Form["xmname"];//收入项明细
            string startc_time = Request.Form["startc_time"];//记录时间
            string endc_time = Request.Form["endc_time"];//记录时间
            string startwctime = Request.Form["startwctime"];//任务完成时间
            string endwctime = Request.Form["endwctime"];//任务完成时间
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<PP_ShouruandzhichuDetailsinfoView> listmodel = GetListPager(pageIndex, Name, xmname, "1", startc_time, endc_time, startwctime, endwctime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        //条件查询
        #region //条件查询
        public JsonResult shouruSearchList()
        {
            string Name = Request.Form["Name"];//员工姓名
            string xmname = Request.Form["xmname"];//收入项明细
            string startc_time=Request.Form["startc_time"];//记录时间
            string endc_time=Request.Form["endc_time"];//记录时间
            string startwctime=Request.Form["startwctime"];//任务完成时间
            string endwctime=Request.Form["endwctime"];//任务完成时间
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<PP_ShouruandzhichuDetailsinfoView> listmodel = GetListPager(pageIndex,Name,xmname,"0",startc_time,endc_time,startwctime,endwctime);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<PP_ShouruandzhichuDetailsinfoView> GetListPager(int? pageIndex, string name, string xmnaem, string type, string startc_time, string endc_time,
            string startwctime, string endwctime)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IPP_ShouruandzhichuDetailsinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IPP_ShouruandzhichuDetailsinfoDao.SetPagerPageSize(10);
            PagerInfo<PP_ShouruandzhichuDetailsinfoView> listmodel = _IPP_ShouruandzhichuDetailsinfoDao.Getshouruzhichupage(name, xmnaem, type, startc_time, endc_time, startwctime, endwctime,Suser);
            return listmodel;
        }
        #endregion
        #endregion

    }
}
