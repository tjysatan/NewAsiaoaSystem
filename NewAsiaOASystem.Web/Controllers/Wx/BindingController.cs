using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web.Controllers
{
    public class BindingController : Controller
    {
        //
        // GET: /Binding/
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        IWX_OpenIDDao _IWX_OpenIDDao = ContextRegistry.GetContext().GetObject("WX_OpenIDDao") as IWX_OpenIDDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
        IWx_FTUserbdopenIdinfoDao _IWx_FTUserbdopenIdinfoDao = ContextRegistry.GetContext().GetObject("Wx_FTUserbdopenIdinfoDao") as IWx_FTUserbdopenIdinfoDao;

        public ActionResult Index()
        {
            string OpenId = Request.QueryString["OpenId"];
            ViewData["message"] = OpenId;
            return View();
        }

        [HttpPost]
        public JsonResult Index(FormCollection form)
        {

            string message = form["OpenId"];
            string N = form["Name"];
            string P = form["pwd"];
            WX_OpenIDView o_model = new WX_OpenIDView();
            if (_IWX_OpenIDDao.GetCount_byOpenId(message,"0") == null)//检查微信号是否已经绑定其他帐号
            {
                Newasia.XYOffer model = new Newasia.XYOffer();
                string UId= model.GetUsers(N,P);
                if (UId != null)
                {
                    NACustomerinfoView Custmodel = _INACustomerinfoDao.GetCusModelbyDsUid(UId);
                    if (Custmodel != null)
                    { //检测是否存在该用户
                        if (_IWX_OpenIDDao.GetCount_byP_Id(Custmodel.Id,"0") == null)//检查帐号是否绑定其他微信号
                        {
                            o_model.OpenId = message;
                            o_model.Person_Id = Custmodel.Id;
                            o_model.Time = DateTime.Now;
                            o_model.Type = 0;
                            //o_model.UserType = 5;
                            if (_IWX_OpenIDDao.Ninsert(o_model))
                            {
                                return Json(new { result = "success" });//绑定成功
                            }
                            else
                            {

                                return Json(new { result = "error" });//绑定失败
                            }
                        }
                        else
                        {

                            return Json(new { result = "error1" });//绑定失败 该帐号已经绑定过了
                        }
                    }
                    else
                    {
                        return Json(new { result = "error3" });//绑定失败 系统不存在该客户
                    }
                }
                else
                {
                    return Json(new { result = "error" });
                }
            }
           //return Content("<script>alert('微信帐号已经绑定！');window.history.back();</script>");
            return Json(new { result = "error2" });
        }

        public ActionResult Remove()
        {
            string OpenId = Request.QueryString["OpenId"];
            ViewData["message"] = OpenId;
            return View();
        }

        [HttpPost]
        public JsonResult Remove(FormCollection form)
        {
            bool i = false;
            string message = form["OpenId"];
            if (_IWX_OpenIDDao.GetCount_byOpenId(message,"0") != null && _IWX_OpenIDDao.GetCount_byOpenId(message,"0").Count!=0)
            {
            WX_OpenIDView model = _IWX_OpenIDDao.GetCount_byOpenId(message,"0")[0];
        
                i = _IWX_OpenIDDao.NDelete(model);
                if (i != false)
                {
                   // return Content("<script>alert('成功解除绑定');WeixinJSBridge.call('closeWindow');</script>");
                    return Json(new { result = "success" });
                }
                else
                {
                    //return Content("<script>alert('对不起，解除失败！');window.history.back();</script>");
                    return Json(new { result = "error" });
                }
            }
            else
            {
               // return Content("<script>alert('此微信号，尚未绑定！');window.history.back();</script>");
                return Json(new { result = "error2" });
            }
        }

        #region //已经绑定微信的帐号用户
        public ActionResult Bingdinglist(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWX_OpenIDDao.SetPagerPageIndex(CurrentPageIndex);
            _IWX_OpenIDDao.SetPagerPageSize(15);
            PagerInfo<WX_OpenIDView> listmodel = _IWX_OpenIDDao.Search();
            return View(listmodel);
        } 
        #endregion

        /// <summary>
        /// 根据用户名获取ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string Getidby_name(string user)
        {
            IList<SysPersonView> list = _ISysPersonDao.GetModelByname(user);
            string value = string.Empty;
            if (list != null)
            {
                value = list[0].Id;
            }
            return value;
        }

        #region //删除图文
        public ActionResult Delete(string id)
        {
            bool i = false;
            List<WX_OpenIDView> sys = _IWX_OpenIDDao.NGetList_id(id) as List<WX_OpenIDView>;
            if (null != sys)
            {
                i = _IWX_OpenIDDao.NDelete(sys);
            }

            return RedirectToAction("Bingdinglist");
        }
        #endregion

        #region //操作提示
        public ActionResult TS()
        {
            return View();
        } 
        #endregion

        //
        public ActionResult SIDcheck()
        {
            string OpenId = Request.QueryString["OpenId"];
            //string OpenId = "ol6V6t4cTrw_Y2rxsH_wGg2w5Mv0";
            if (OpenId != null || OpenId != "")
            {
                int Allcount = _IWL_DkxInfoDao.WXGetcountdkxbyCusId(OpenId);//查询全部电控箱
                ViewData["Allcount"] = Allcount;
                int monthNewSX = _IWL_DkxInfoDao.WXdkxsxcountbyCusIdM(OpenId, FirstDayOfMonth(DateTime.Now), LastDayOfMonth(DateTime.Now));//本月上线数量
                ViewData["monthNewSX"] = monthNewSX;
                int allsxcount = _IWL_DkxInfoDao.WXdkxallsxbyCusId(OpenId);//累计上线
                ViewData["allsxcount"] = allsxcount;
                int profitM = _IWL_DkxInfoDao.WXdkxjfbyCusIdM(OpenId, FirstDayOfMonth(DateTime.Now), LastDayOfMonth(DateTime.Now));//本月收益
                ViewData["profitM"] = profitM;
                int profitcM = _IWL_DkxInfoDao.WXdkxjfbyCusIdCM(OpenId, LastDayOfCMonth(DateTime.Now));
                ViewData["profitcM"] = profitcM;
                int allprofit = _IWL_DkxInfoDao.WXdkxcountsybyCusId(OpenId);//累计收益
                ViewData["allprofit"] = allprofit;
                string CusName = _INACustomerinfoDao.GetCusNamebyOpenID(OpenId);//用户名称
                ViewData["CusName"] = CusName;
            }
            return View(); 
        }

        /// <summary>
        /// 获取当前的年月(弃用)
        /// </summary>
        /// <returns></returns>
        public string GetNewYearMonth()
        {
            string NowYear = DateTime.Now.Year.ToString(); ;
            string NowMouth = DateTime.Now.Month.ToString();
            if (NowMouth.Length == 1)
            {
                NowMouth = "0" + NowMouth;
            }
            string NowTime = NowYear +"-"+ NowMouth;
            return NowTime;
        }

        /// 取得本月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        private string FirstDayOfMonth(DateTime datetime)
        {
            string DATA = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            return DATA;
        }
        /**/
        /// <summary>
        /// 取得本月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        private string LastDayOfMonth(DateTime datetime)
        {
            string DATA = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            return DATA;
        }

        /// <summary>
        /// 获取次月的最后一天日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private string LastDayOfCMonth(DateTime datetime)
        {
            string DATA = datetime.AddDays(1 - datetime.Day).AddMonths(2).AddDays(-1).ToString("yyyy-MM-dd");
            return DATA;
        }

        public string GetNewYearCMonth()
        {
            string NowYear = DateTime.Now.Year.ToString(); ;
            string NowMouth = DateTime.Now.Month.ToString();
            if (NowMouth.Length == 1)
            {
                NowMouth = "0" + NowMouth;
            }
            else if (NowMouth == "12")
            {
                NowMouth = "1";
            }
            else
            {
                NowMouth = (Convert.ToInt32(NowMouth) + 1).ToString();
            }
            string NowTime = NowYear + "-" + NowMouth;
            return NowTime;
        }


        //内部员工绑定微信公众号模块

        //绑定页面
        public ActionResult InsideuserBdView()
        {
            string OpenId = Request.QueryString["OpenId"];
            ViewData["OpendId"] = OpenId;
            return View();
        }

        [HttpPost]
        public JsonResult Insideuserbd(FormCollection form)
        {
            string message = form["OpenId"];
            string N = form["Name"];
            string P = form["pwd"];
            WX_OpenIDView o_model = new WX_OpenIDView();
            if (_IWX_OpenIDDao.GetCount_byOpenId(message, "1") == null)//检查微信号是否已经绑定其他帐号
            {
                //SysPersonView syspmodel = _ISysPersonDao.GetModelByname(N)[0];
                IList<SysPersonView> syspmodellist = _ISysPersonDao.GetModelByname(N);
                if (syspmodellist != null)//检测帐号是否为空
                {
                    if (_IWX_OpenIDDao.GetCount_byP_Id(syspmodellist[0].Id,"1") == null)//检查帐号是否绑定其他微信号
                    {
                        o_model.OpenId = message;
                        o_model.Person_Id = syspmodellist[0].Id;
                        o_model.Time = DateTime.Now;
                        o_model.Type = 1;
                        if (_IWX_OpenIDDao.Ninsert(o_model))
                        {
                            return Json(new { result = "success" });//绑定成功
                        }
                        else
                        {

                            return Json(new { result = "error" });//绑定失败
                        }
                    }
                    else
                    {
                        return Json(new { result = "error1" });//绑定失败 该帐号已经绑定过了
                    }
                }
                else
                {
                    return Json(new { result = "error3" });//绑定失败 系统不存在该客户
                }
            }
            else
            {
                return Json(new { result = "error2" });//该微信号已经绑定帐号
            }
        }

        //返退货微信提现帐号绑定
        public ActionResult FT_userbdopenidView(string code)
        {
            JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
            if (obj.Count == 2)
            {
                return Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx44f57e0f1268190b&redirect_uri=http://wx.chinanewasia.com/Binding/FT_userbdopenidView&response_type=code&scope=snsapi_base&state=STATE&connect_redirect=1#wechat_redirect");
            }
            else
            {
                ViewData["openId"] = obj["openid"].ToString();
                Session["OpenidNewO"] = obj["openid"].ToString();
                return View();
            }
            //ViewData["openId"] ="223";
            //return View();
        }

        [HttpPost]
        public JsonResult FTuserbdopenEide(FormCollection form)
        {
            string message = form["OpenId"];
            string N = form["Name"];
            string P = form["pwd"];
            string bmtype = form["bmtype"];
            if (_IWx_FTUserbdopenIdinfoDao.GetCount_byOpenId(message) == null)//微信尚未被绑定
            {
                bool flay = _ISysPersonDao.login(N,P);
                if (flay)//帐号密码合法
                {
                   // string cusId = _ISysPersonDao.GetUesrIdbyUserName(N);//
                    IList<SysPersonView> syspmodellist = _ISysPersonDao.GetModelByname(N);
                    string cusId = syspmodellist[0].Id;
                    if (_IWx_FTUserbdopenIdinfoDao.GetCount_byUserId(cusId) == null)//帐号尚未被绑定
                    {
                        Wx_FTUserbdopenIdinfoView model = new Wx_FTUserbdopenIdinfoView();
                        model.UserId = cusId;
                        model.OpenId = message;
                        model.C_time = DateTime.Now;
                        model.Bmtype = Convert.ToInt32(bmtype);
                        if (_IWx_FTUserbdopenIdinfoDao.Ninsert(model))
                        {
                            return Json(new { result = "success" });//绑定成功
                        }
                        else
                        {
                            return Json(new { result = "error" });//绑定失败
                        }
                    }
                    else//帐号被绑定
                    {
                        return Json(new { result = "error1" });//绑定失败 该帐号已经绑定过了
                    }
                }
                else//帐号密码不合法
                {
                    return Json(new { result = "error3" });//绑定失败 帐号或密码有误
                }
            }
            else
            {
                return Json(new { result = "error2" });//该微信号已经绑定帐号
            }
        }

        public object Getaccess_tokenbycodeinface(string code)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx44f57e0f1268190b&secret=bcbfe205a0e5fad5a4ab7f2ebb90656d&code={0}&grant_type=authorization_code ", code);
            string json = HttpUtility11.GetData(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            return obj;
        }

        #region //内部员工帐号绑定微信数据列表
        public ActionResult wxbdlist(int? pageIndex)
        {
            PagerInfo<Wx_FTUserbdopenIdinfoView> listmodel = GetWXbdinfoPagelist(pageIndex, null, null);
            return View(listmodel);
        }

        #region //多条件查询
        public JsonResult WXBDSearchList()
        {
            string user=Request.Form["user"];//帐号
            string type=Request.Form["type"];//绑定部门或者类型
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<Wx_FTUserbdopenIdinfoView> listmodel = GetWXbdinfoPagelist(pageIndex, user, type);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //微信绑定分页数据
        private PagerInfo<Wx_FTUserbdopenIdinfoView> GetWXbdinfoPagelist(int? pageIndex, string user, string type)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWx_FTUserbdopenIdinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IWx_FTUserbdopenIdinfoDao.SetPagerPageSize(10);
            PagerInfo<Wx_FTUserbdopenIdinfoView> listmodel = _IWx_FTUserbdopenIdinfoDao.GetwxbdinfoPagerList(user, type);
            return listmodel;
        }
        #endregion
        #endregion

    }
}
