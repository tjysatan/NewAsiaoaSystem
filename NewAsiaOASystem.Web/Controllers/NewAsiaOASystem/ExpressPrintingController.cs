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
    public class ExpressPrintingController : Controller
    {
        //快递单打印
        // GET: /ExpressPrinting/

        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        IEP_jlinfoDao _IEP_jlinfoDao = ContextRegistry.GetContext().GetObject("EP_jlinfoDao") as IEP_jlinfoDao;

        public ActionResult Index()
        {
            return View();
        }

        //打印快递单 信息管理
        public ActionResult printlist(int? pageIndex)
        {
            PagerInfo<NACustomerinfoView> listmodel = GetListPager(pageIndex, null,null,"1",null,null);
            return View(listmodel);
        }

        //条件查询
        public JsonResult SearchList()
        {
            string Name = Request.Form["txtSearch_Name"];//单位名称
            string lxrName = Request.Form["txtSearch_LXname"];//联系人姓名
            string Isjy=Request.Form["Isjy"];//是否禁用
            string Tel=Request.Form["Tel"];//电话
            string Isds=Request.Form["Isds"];//是否电商
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<NACustomerinfoView> listmodel = GetListPager(pageIndex, Name,lxrName,Isjy,Tel,Isds);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }

        #region //获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="Name">客户名称</param>
        /// <returns></returns>
        private PagerInfo<NACustomerinfoView> GetListPager(int? pageIndex, string Name,string lxrname,string Isjy,string Tel,string Isds)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _INACustomerinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _INACustomerinfoDao.SetPagerPageSize(11);
            PagerInfo<NACustomerinfoView> listmodel = _INACustomerinfoDao.GetCinfoList(Name,lxrname,null,Isjy,Tel,Isds,Suser);
            return listmodel;
        }
        #endregion

        #region //平台快递单打印

        /// <summary>
        /// 中通快递单打印
        /// </summary>
        /// <returns></returns>
        public ActionResult ztPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = id;//客户信息ID
            return View();
        }

        /// <summary>
        /// 德邦快递单打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult dbPrint()
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            EP_jlinfoView model = new EP_jlinfoView();
            if(Session["epId"].ToString() != "" && Session["epId"] != null)
            {
                var Id = Session["epId"].ToString();
                 model = _IEP_jlinfoDao.NGetModelById(Id);
            }
            return View(model);
        }

        public ActionResult dbwlPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;
            return View();
        }

        /// <summary>
        /// 顺丰快递单打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult sfPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        public ActionResult sfYPrint()
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            EP_jlinfoView model = new EP_jlinfoView();
            if (Session["epId"].ToString() != "" && Session["epId"] != null)
            {
                var Id = Session["epId"].ToString();
                model = _IEP_jlinfoDao.NGetModelById(Id);
            }
            return View(model);
        }

        /// <summary>
        /// 顺丰快递（新版）打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult sfxPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
            //SessionUser user = Session[SessionHelper.User] as SessionUser;
            //EP_jlinfoView model = new EP_jlinfoView();
            //if (Session["epId"].ToString() != "" && Session["epId"] != null)
            //{
            //    var Id = Session["epId"].ToString();
            //    model = _IEP_jlinfoDao.NGetModelById(Id);
            //}
            //return View(model);
        }

        /// <summary>
        /// 盛辉快递单打印
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult shPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        /// <summary>
        /// 天地华宇
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult tdhyPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        /// <summary>
        /// 远程快运
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult yckyPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        /// <summary>
        /// 中通快运
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ztkyPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        public ActionResult anPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }

        /// <summary>
        /// 佳邦
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult jiabangPrint(string id)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户id
            ViewData["Id"] = id;//客户Id
            return View();
        }


        /// <summary>
        /// 打印跳转页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintPage(string id, string kdgs)
        {
            return View();
        }
        
        #endregion

        #region //平台打印电商平台
        //中通快递打印
        public ActionResult DsztPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }

        //德邦快递单打印
        public ActionResult DsdbPrint()
        {
            //SessionUser user = Session[SessionHelper.User] as SessionUser;
            //ViewData["UID"] = user.Id;//当前登录用户
            //ViewData["Id"] = kdId;//客户信息ID
            //ViewData["addId"] = addId;
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            EP_jlinfoView model = new EP_jlinfoView();
            if (Session["epId"].ToString() != "" && Session["epId"] != null)
            {
                var Id = Session["epId"].ToString();
                model = _IEP_jlinfoDao.NGetModelById(Id);
            }
            return View(model);
            return View();
        }

        //德邦物流单打印
        public ActionResult DsdbwlPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }
        //顺丰快递单打印
        public ActionResult DssfPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }
        //盛辉快递单打印
        public ActionResult DsshPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }

        //天地华宇
        public ActionResult DstdhyPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }

        /// <summary>
        /// 远程快运
        /// </summary>
        /// <param name="kdId"></param>
        /// <param name="addId"></param>
        /// <returns></returns>
        public ActionResult DsyckyPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }

        //中通快运
        public ActionResult DsztkyPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }


        public ActionResult DsanPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }

        //佳邦
        public ActionResult DsjiabangPrint(string kdId, string addId)
        {
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            ViewData["UID"] = user.Id;//当前登录用户
            ViewData["Id"] = kdId;//客户信息ID
            ViewData["addId"] = addId;
            return View();
        }
        #endregion

        /// <summary>
        /// 获取当前的服务器的时间json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Getdatenewjson()
        { 
           string flay = DateTime.Now.ToString();
           return "{\"status\":\"" + flay + "\"}";
        }
    }
}
