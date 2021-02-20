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
    public class Flow_PleasepurchaseinfoController : Controller
    {
        //name:元器件请购单controller
        //author:tjy_satan
        // GET: /Flow_Pleasepurchaseinfo/
        IFlow_PleasepurchaseinfoDao _IFlow_PleasepurchaseinfoDao = ContextRegistry.GetContext().GetObject("Flow_PleasepurchaseinfoDao") as IFlow_PleasepurchaseinfoDao;
        //分页列表页面

        #region //列表以及查询页面
        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<Flow_PleasepurchaseinfoView> listmodel = GetListPager(pageIndex, null, null, null, null, null,null);
            return View(listmodel);
        }
        #endregion

        //一号采购员 列表
        public ActionResult OneList(int? pageIndex)
        {
            PagerInfo<Flow_PleasepurchaseinfoView> listmodel = GetListPager(pageIndex, null, null, null, null, null, "0");
            return View(listmodel);
        }

        //二号采购员 列表
        public ActionResult TweList(int? pageIndex)
        {
            PagerInfo<Flow_PleasepurchaseinfoView> listmodel = GetListPager(pageIndex, null, null, null, null, null, "1");
            return View(listmodel);
        }

        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["CPname"];//产品名称
            string wlbianhao = Request.Form["wl_bm"];//物料编号
            string Isscing = Request.Form["Isscing"];//状态
            string starttime = Request.Form["starttime"];//下单开始时间
            string endtime = Request.Form["endtime"];//下单结束时间
            string cgy = Request.Form["cgy"];//下单结束时间
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<Flow_PleasepurchaseinfoView> listmodel = GetListPager(pageIndex, Name, wlbianhao, Isscing, starttime, endtime, cgy);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<Flow_PleasepurchaseinfoView> GetListPager(int? pageIndex, string Name, string wl_bm, string Isscing, string starttime, string endtime,string cgy)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IFlow_PleasepurchaseinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IFlow_PleasepurchaseinfoDao.SetPagerPageSize(11);
            PagerInfo<Flow_PleasepurchaseinfoView> listmodel = _IFlow_PleasepurchaseinfoDao.GetCinfoList(Name, wl_bm, Isscing, starttime, endtime,cgy);
            return listmodel;
        }
        #endregion
        #endregion

        #region //交期回复页面
        //回访交期页面
        public ActionResult JQview(string Id, string type)
        {
            ViewData["Id"] = Id;
            ViewData["type"] = type;
            return View();
        }

        //提交交期
        public string Updatejq(string Id, string jqdatetime)
        {
            Flow_PleasepurchaseinfoView model = _IFlow_PleasepurchaseinfoDao.NGetModelById(Id);
            model.Jqtime = Convert.ToDateTime(jqdatetime);
            model.Type = 1;
            if (_IFlow_PleasepurchaseinfoDao.NUpdate(model))
            {
                return "0";//设置成功
            }
            else
            {
                return "1";//设置失败
            }
        }
        
        #endregion

        #region //采购完成
        //采购完成页面
        public ActionResult Wcview(string Id, string type)
        {
            ViewData["Id"] = Id;
            ViewData["type"] = type;
            return View();
        }

        //提交 完成信息
        public string Updatewcinfo(string Id, string sjcgsl, string wc_datetime)
        {
            Flow_PleasepurchaseinfoView model = _IFlow_PleasepurchaseinfoDao.NGetModelById(Id);
            model.wc_datetime = Convert.ToDateTime(wc_datetime);
            model.Type = 2;
            model.sjcgsl = Convert.ToDecimal(sjcgsl);
            if (_IFlow_PleasepurchaseinfoDao.NUpdate(model))
            {
                return "0";//设置成功
            }
            else
            {
                return "1";//设置失败
            }
        } 
        #endregion
      
    }
}
