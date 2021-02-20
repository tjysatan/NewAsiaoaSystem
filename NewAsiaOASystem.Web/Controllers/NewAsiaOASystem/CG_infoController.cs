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
    public class CG_infoController : Controller
    {
        //
        // GET: /CG_info/
        ICG_infoDao _ICG_infoDao = ContextRegistry.GetContext().GetObject("CG_infoDao") as ICG_infoDao;
        ICG_DetailinfoDao _ICG_DetailinfoDao = ContextRegistry.GetContext().GetObject("CG_DetailinfoDao") as ICG_DetailinfoDao;
        INQ_GysInfoDao _INQ_GysInfoDao = ContextRegistry.GetContext().GetObject("NQ_GysInfoDao") as INQ_GysInfoDao;
        INQ_YJinfoDao _INQ_YJinfoDao = ContextRegistry.GetContext().GetObject("NQ_YJinfoDao") as INQ_YJinfoDao;

        #region //列表以及查询页面
        //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<CG_infoView> listmodel = GetListPager(pageIndex, null, null, null, null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["gysId"];
            //string yname = Request.Form["txtSearch_Yname"];
            string Isdh = Request.Form["Isdh"];
            string Stratrldate = Request.Form["Stratrldate"];//开始下单时间
            string Endrldate = Request.Form["Endrldate"];//结束下单时间
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<CG_infoView> listmodel = GetListPager(pageIndex, Name, Isdh, Stratrldate, Endrldate);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //分页数据
        private PagerInfo<CG_infoView> GetListPager(int? pageIndex, string Name, string Isdh, string Stratrldate, string Endrldate)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _ICG_infoDao.SetPagerPageIndex(CurrentPageIndex);
            _ICG_infoDao.SetPagerPageSize(11);
            PagerInfo<CG_infoView> listmodel = _ICG_infoDao.GetCginfoList(Name, Isdh, Stratrldate, Endrldate, Suser);
            return listmodel;
        }
        #endregion 
        #endregion

        #region //根据供应商ID 查找供应商信息
        [HttpPost]
        public string GetgysinfoJson(string Id)
        {
            string Json;
            Json = JsonConvert.SerializeObject(_INQ_GysInfoDao.NGetModelById(Id));
            return Json;
        } 
        #endregion

        #region //查看采购单详情
        public ActionResult CkcgdView(string Id)
        {
            ViewData["gysId"] = Id;//供应商Id
            return View();
        }
        #endregion

        #region //根据Id 查找采购单数据
        [HttpPost]
        public string GetCGdjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_ICG_infoDao.NGetModelById(Id));
            return json;
        } 
        #endregion

        #region //根据采购单ID 查找采购单明细
        [HttpPost]
        public string Getcgmxjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_ICG_DetailinfoDao.Getcgmxlist(Id));
            return json;
        } 
        #endregion

        #region //根据元器件ID查找元器件信息
        public string Getyqjmodeljson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INQ_YJinfoDao.NGetModelById(Id));
            return json;
        } 
        #endregion
        
        //预定时间设定页面
        public ViewResult SGtimeview(string Id)
        {
            CG_infoView model = new CG_infoView();
            if(!string.IsNullOrEmpty(Id))
             model = _ICG_infoDao.NGetModelById(Id);  
            return View("SGtimeview", model);
        }


        //保存生管需求交期
        [HttpPost]
        public JsonResult Editsgtime(CG_infoView model, FrameController from)
        {
            bool flay = false;
            if (model.Cg_Isdh == 0)
            {
                model.Cg_sgjqtime = model.Cg_sgjqtime;//生管预计交期
                model.Cg_jqqrtime = model.Cg_jqqrtime;//
                flay = _ICG_infoDao.NUpdate(model);
                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            }
            else
            {
                return Json(new { result = "error1" });//已经到货部能修改时间
            }
           
        }


        //到货时间确定
        public ViewResult dhtimeview(string Id) {
            CG_infoView model = new CG_infoView();
            if (!string.IsNullOrEmpty(Id))
                model = _ICG_infoDao.NGetModelById(Id);
            return View("dhtimeview", model);
        }

        [HttpPost]
        public JsonResult Editdhtime(CG_infoView model, FrameController from)
        {
            bool flay = false;
            model.Cg_dhtime = model.Cg_dhtime;//到货时间
            model.Cg_Isdh = 1;//到货状态
            flay = _ICG_infoDao.NUpdate(model);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }

        //采购数量确定
        public ViewResult cgslView(string Id)
        {
            CG_DetailinfoView model = new CG_DetailinfoView();
            if (!string.IsNullOrEmpty(Id))
                model = _ICG_DetailinfoDao.NGetModelById(Id);
            return View("cgslView", model);
        }

        //保存采购数量
        [HttpPost]
        public JsonResult Editcgsl(CG_DetailinfoView model, FrameController from)
        {
            bool flay = false;
            model.sjcgsl = model.sjcgsl;//实际采购数量
            flay = _ICG_DetailinfoDao.NUpdate(model);
            if (flay)
                return Json(new { result = "success" });//返回设置成功
            else
                return Json(new { result = "error" });//返回设置不成功
        }

        //删除
        [HttpPost]
        public JsonResult cgmxdelete(string Id)
        {
            bool flay = false;
            CG_DetailinfoView model = new CG_DetailinfoView();
            model = _ICG_DetailinfoDao.NGetModelById(Id);
            flay = _ICG_DetailinfoDao.NDelete(model);
            if (flay)
                return Json(new { result = "success" });//返回设置成功
            else
                return Json(new { result = "error" });//返回设置不成功
        }
    }
}
