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
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;

namespace NewAsiaOASystem.Web.Controllers
{ 
    public class EP_jlinfoController : Controller
    {
        //快递打印记录
        // GET: /EP_jlinfo/
        INAReturnListDao _INAReturnListDao = ContextRegistry.GetContext().GetObject("NAReturnListDao") as INAReturnListDao;
        IEP_jlinfoDao _IEP_jlinfoDao = ContextRegistry.GetContext().GetObject("EP_jlinfoDao") as IEP_jlinfoDao;
        IWX_OpenIDDao _IWX_OpenIDDao = ContextRegistry.GetContext().GetObject("WX_OpenIDDao") as IWX_OpenIDDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;
        
        /// <summary>
        /// 插入打印记录（平台客户答应）
        /// </summary>
        /// <param name="sjId"></param>  
        /// <param name="kdgs"></param>
        /// <returns></returns>
        [HttpPost]
        public string InsEPModel(string sjId, string kdgs)
        {
            EP_jlinfoView model = new EP_jlinfoView();
            string json;
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            model.JjId = Suser.Id;//寄货人
            model.SjId = sjId;//收件人
            if (kdgs == "sfx")
            {
                model.Kdgs = "sf";
            }
            else
            {
                model.Kdgs = kdgs;//快递公司
            }
            model.Jjdatetime = DateTime.Now;//打印时间
            model.RL_Is = 0;//状态  0未录入 1未签收 2 已签收 
            model.Source = 0;//记录来源
            model.IsSend = 0;//未发送 发货通知
            model.IsDis = 0;//启用
            NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(sjId);
            if (Cusmodel != null)
            {
                if (kdgs == "zt"||kdgs=="db")
                {
                    if (string.IsNullOrEmpty(Cusmodel.Tel))
                        return "2";//联系方式不为空
                    if (string.IsNullOrEmpty(Cusmodel.qyqxname))
                        return "3";//区域不完整
                    if (kdgs == "db") model.isdy = 1;//未打印
                }
                if (Cusmodel.dycs == null)
                {
                    Cusmodel.dycs = 0;
                }
                Cusmodel.dycs = Cusmodel.dycs + 1;
                _INACustomerinfoDao.NUpdate(Cusmodel);
            }
            string orderId = _IEP_jlinfoDao.InsertID(model);
            if (!string.IsNullOrEmpty(orderId))
            {
                Session["epId"] = orderId;
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("保持返回订单号" + Session["epId"].ToString());
                json = "1";
            }
            else
            {
                json = "0";
            }
            return json;
        }

        //德邦快递 -打印记录列表中的打印
        public ActionResult dblistPrint(string Id)
        {
            EP_jlinfoView model = new EP_jlinfoView();
             model = _IEP_jlinfoDao.NGetModelById(Id);
            return View(model);
        }

        //德邦快递同步
        [HttpPost]
        public JsonResult DBorderts(string payType,string transportType,string deliveryType,string cargoName,string totalWeight,string insuranceValue)
        {
            try
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("已经进来");
                if (Session["epId"].ToString() != "" && Session["epId"] != null)
                {
                    var Id = Session["epId"].ToString();
                    EP_jlinfoView model = _IEP_jlinfoDao.NGetModelById(Id);
                    if (model.Istscg != 0)
                    { //订单未推送云端
                        model.cargoName = cargoName;
                        model.payType = payType;
                        model.transportType = transportType;
                        model.deliveryType = deliveryType;
                        model.totalWeight = totalWeight;
                        model.insuranceValue = insuranceValue;
                        _IEP_jlinfoDao.NUpdate(model);
                    }
                    else {
                        return Json(new { result = "false", msg = "订单已经推送无需下单" });//订单已经推送无需下单
                    }
                    string res = DBEpressinterHelper.Main(model);
                    log4net.LogManager.GetLogger("ApplicationInfoLog").Error("返回的结构" + res);
                    JObject jo = (JObject)JsonConvert.DeserializeObject(res);
                    string Istscg = jo["result"].ToString();//调用结果
                    string reason = jo["reason"].ToString();//
                    if (Istscg == "true")
                    {
                        model.Source = 3;
                        model.Istscg = 0;
                        model.reason = reason;
                        model.Kd_no = jo["mailNo"].ToString();
                        model.logisticID = jo["logisticID"].ToString();
                        model.uniquerRequestNumber = jo["uniquerRequestNumber"].ToString();
                        model.arrivedOutFieldName = jo["arrivedOrgSimpleName"].ToString();
                        _IEP_jlinfoDao.NUpdate(model);
                        return Json(new { result = "true", msg = "下单成功，请立即打印" });
                    }
                    else
                    {
                        model.Istscg = 1;
                        model.reason = reason;
                        _IEP_jlinfoDao.NUpdate(model);
                        return Json(new { result = "false", msg = "下单失败，"+ reason });
                    }
                    
                }
                else {
                    return Json(new { result = "false", msg = "关闭页面，重新下单打印"}); ; ;//重新打开打印页面
                }
            }
            catch(Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("已经进来=" + e);
                return Json(new { result = "false", msg = "下单失败，" + e }); ;
            }
        }
        //德邦快递订单撤销
        [HttpPost]
        public JsonResult DBrevokeorders(string Id) {
            EP_jlinfoView model = _IEP_jlinfoDao.NGetModelById(Id);
            if (model != null)
            {
                string res = DBEpressinterHelper.revokeMain(model);
                JObject jo = (JObject)JsonConvert.DeserializeObject(res);
                string Istscg = jo["result"].ToString();//调用结果
                string reason = jo["reason"].ToString();//
                if (Istscg == "true")
                {
                    model.Istscg = 2;
                    model.IsDis = 1;
                    _IEP_jlinfoDao.NUpdate(model);
                    return Json(new { result = "true", msg = "撤销成功" });
                }
                else
                {
                    return Json(new { result = "false", msg = reason });
                }
            }
            else {
                return Json(new { result = "false", msg = "订单不存在" });
            }
          
        }
        //中通订单推送
        public string ZTorderts()
        {
            try
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("已经进来");

                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("订单号1" + Session["epId"].ToString());
                if (Session["epId"].ToString() != "" && Session["epId"] != null)
                {
                    var Id = Session["epId"].ToString();
                    log4net.LogManager.GetLogger("ApplicationInfoLog").Error("订单号1" + Session["epId"].ToString());
                    EP_jlinfoView model = _IEP_jlinfoDao.NGetModelById(Id);
                    string res = ZTOHelper.Main(model);
                    //List<tsfhresmodel> amodel = getObjectByJson<tsfhresmodel>(res);
                    JObject jo = (JObject)JsonConvert.DeserializeObject(res);
                    //tsfhresmodel amodel = new tsfhresmodel();
                    //amodel.result = jo["result"].ToString();
                    //amodel.message = jo["message"].ToString();
                    //amodel.statusCode = jo["statusCode"].ToString();
                    string t=jo["statusCode"].ToString();
                    if (t == "A200")
                    {
                        model.Source = 2;
                        model.Istscg = 0;
                        _IEP_jlinfoDao.NUpdate(model);
                        return "A200";//推送成功
                    }
                    if (t == "A210")
                    {
                        model.Source = 2;
                        model.Istscg = 1;
                        _IEP_jlinfoDao.NUpdate(model);
                        return "A210";//发货手机和电话号码都是空
                    }
                    if (t == "A220")
                    {
                        model.Source = 2;
                        model.Istscg = 1;
                        _IEP_jlinfoDao.NUpdate(model);
                        return "A220";//没有找到shopKey对应的店铺
                    }
                    if (t == "A230")
                    {
                        model.Source = 2;
                        model.Istscg = 1;
                        _IEP_jlinfoDao.NUpdate(model);
                        return "A230";//该订单已经打印发货,不能修改
                    }
                    if (t == "A240")
                    {
                        model.Source = 2;
                        model.Istscg = 1;
                        _IEP_jlinfoDao.NUpdate(model);
                        return "A240";//订单入库异常
                    }
                    if (t == "A250")
                    {
                        model.Source = 2;
                        model.Istscg = 1;
                        _IEP_jlinfoDao.NUpdate(model);
                        return "A250";//存在必填字段为空
                    }
                    if (t == "A260")
                    {
                        model.Source = 2;
                        model.Istscg = 1;
                        _IEP_jlinfoDao.NUpdate(model);
                        return "A260";//收件手机和电话号码都是空
                    }
                    return "1";

                }
                else
                {
                    return "0";//重新打开打印页面
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("已经进来="+e);
                return "0";
            }
        }



        private static List<T> getObjectByJson<T>(string jsonString)
        {
            // 实例化DataContractJsonSerializer对象，需要待序列化的对象类型  
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
            //把Json传入内存流中保存  
            //jsonString = jsonString;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            // 使用ReadObject方法反序列化成对象  
            object ob = serializer.ReadObject(stream);
            List<T> ls = (List<T>)ob;
            return ls;
        }

        #region //推送返回的实名
        public class tsfhresmodel
        {
            public virtual string result { get; set; }

            public virtual string message {get;set;}

            public virtual string status { get; set; }

            public virtual string statusCode { get; set; }
        }
        #endregion

        #region //出入打印记录（平台答应电商订单数据）
        [HttpPost]
        public string UInsEPModel(string sjId, string kdgs, string addId, string UId)
        {
            EP_jlinfoView model = new EP_jlinfoView();
            string json;
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            model.JjId = Suser.Id;//寄货人
            model.SjId = sjId;//收件人
            if (kdgs == "sfx")
            {
                model.Kdgs = "sf";
            }
            else
            {
                model.Kdgs = kdgs;//快递公司
            }
            model.Jjdatetime = DateTime.Now;//打印时间
            model.RL_Is = 0;//状态  0未录入 1未签收 2 已签收 
            model.Source = 0;//记录来源
            model.IsSend = 0;//未发送 发货通知
            model.Source = 1;
            model.SjaddId = addId;
            model.DDId =Convert.ToInt32(UId);
            model.IsDis = 0;
            NACustomerinfoView Cusmodel = _INACustomerinfoDao.NGetModelById(sjId);
            NA_AddresseeInfoView addremodel = _INA_AddresseeInfoDao.NGetModelById(addId);
            if (Cusmodel != null)
            {
              
                    if (kdgs == "zt" || kdgs == "db")
                    {
                    if (addremodel != null)
                    {
                        if (string.IsNullOrEmpty(addremodel.Tel))
                            return "2";//联系方式不为空
                        if (string.IsNullOrEmpty(addremodel.qyt))
                            return "3";//区域不完整
                    }
                    else { 
                    }
 
                    }
                if (Cusmodel.dycs == null)
                {
                    Cusmodel.dycs = 0;
                }
                Cusmodel.dycs = Cusmodel.dycs + 1;
                _INACustomerinfoDao.NUpdate(Cusmodel);
            }
            string orderId = _IEP_jlinfoDao.InsertID(model);
            if (!string.IsNullOrEmpty(orderId))
            {
                Session["epId"] = orderId;
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("保持返回订单号" + Session["epId"].ToString());
                json = "1";
            }
            else
            {
                json = "0";
            }
            return json;
        }
        #endregion

        #region //数据列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<EP_jlinfoView> listmodel = new PagerInfo<EP_jlinfoView>();
            if (Session[SessionHelper.SSerch] != null)
            {
                S_EPJLPara ssearch = Session[SessionHelper.SSerch] as S_EPJLPara;
                ViewData["Name"] = ssearch.Name;
                ViewData["lxrname"] = ssearch.lxrname;
                ViewData["rlis"] = ssearch.rlis;
                ViewData["stratrldate"] = ssearch.stratrldate;
                ViewData["endrldate"] = ssearch.endrldate;
                ViewData["dystratrldate"] = ssearch.dystratrldate;
                ViewData["dyendrldate"] = ssearch.dyendrldate;
                ViewData["kdgs"] = ssearch.kdgs;
                ViewData["k3_ddno"] = ssearch.K3DDNO;
                listmodel = GetListPager(pageIndex, ssearch.Name,ssearch.lxrname, ssearch.rlis, ssearch.stratrldate, ssearch.endrldate, ssearch.dystratrldate, ssearch.dyendrldate,ssearch.kdgs,ssearch.K3DDNO);
            }
            else
            {
                listmodel = GetListPager(pageIndex, null,null, null, null, null, null, null,null,null);
            }
            return View(listmodel);
        }
        #endregion

        //记录查询条件model
        public class S_EPJLPara 
        {
            public virtual string Name
            {
                get;
                set;
            }

            /// <summary>
            /// 联系人
            /// </summary>
            public virtual string lxrname
            {
                get;
                set;
            }

            /// <summary>
            /// 记录状态
            /// </summary>
            public virtual string rlis
            {
                get;
                set;
            }

            /// <summary>
            /// 寄件时间开始时间
            /// </summary>
            public virtual string stratrldate
            {
                get;
                set;
            }
            /// <summary>
            /// 寄件结束时间
            /// </summary>
            public virtual string endrldate
            {
                get;
                set;
            }

            /// <summary>
            /// 打印开始时间
            /// </summary>
            public virtual string dystratrldate
            {
                get;
                set;
            }

            /// <summary>
            /// 打印结束时间
            /// </summary>
            public virtual string dyendrldate
            {
                get;
                set;
            }
            /// <summary>
            /// 快递公司
            /// </summary>
            public virtual string kdgs
            {
                get;
                set;
            }

            public virtual string K3DDNO
            {
                get;
                set;
            }

        }

        #region //查询
        public JsonResult SearchList()
        {
            Session[SessionHelper.SSerch] = null;
            Session.Remove(SessionHelper.SSerch);
            S_EPJLPara view = new S_EPJLPara();
            view.rlis = Request.Form["txtrlis"];
            view.Name = Request.Form["txtSearch_Name"];
            //string rlis = Request.Form["txtrlis"];
           view.stratrldate = Request.Form["stratrldate"];
           view.endrldate = Request.Form["endrldate"];
            view.dystratrldate = Request.Form["dystratrldate"];
            view.dyendrldate = Request.Form["dyendrldate"];
            view.kdgs=Request.Form["kdgs"];
            view.lxrname = Request.Form["lxrname"];
            view.K3DDNO = Request.Form["k3_ddno"];
            //Session["type"] = rlis;
            Session[SessionHelper.SSerch] = view;
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<EP_jlinfoView> listmodel = GetListPager(pageIndex, view.Name,view.lxrname, view.rlis, view.stratrldate, view.endrldate, view.dystratrldate, view.dyendrldate,view.kdgs,view.K3DDNO);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取分页数据
        private PagerInfo<EP_jlinfoView> GetListPager(int? pageIndex, string Name,string lxrname, string RLis,string startrldate,string endrkdate, string dyStratrldate, string dyEndrldate,string kdgs,string K3DDNO)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IEP_jlinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IEP_jlinfoDao.SetPagerPageSize(10);
            PagerInfo<EP_jlinfoView> listmodel = _IEP_jlinfoDao.GetCinfoList(Name,lxrname, RLis,startrldate,endrkdate,dyStratrldate,dyEndrldate,kdgs,K3DDNO, Suser);
            return listmodel;
        }
        #endregion

        #region //跳转到编辑页面
        /// <summary>
        /// 跳转到编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPage(string id,int? Pageindex )
        {
            EP_jlinfoView sys = new EP_jlinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IEP_jlinfoDao.NGetModelById(id);
            ViewData["pageindex"] = Pageindex;
            return View("Edit", sys);
        }
        #endregion

        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(EP_jlinfoView model, FrameController from)
        {
            string IsNSend = Request.Form["radiobutton"];//0 发送  1 不发送
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                model.Kd_no = model.Kd_no;//快递单号
                model.DHyjdatetime = model.DHyjdatetime;//预计到货时间
                model.Beizhu = model.Beizhu;//备注
                model.RL_datetime = model.RL_datetime;//录单时间
                model.DHno = model.DHno;//到货数量
                model.k3_ddno = model.k3_ddno;//k3单号
                _IEP_jlinfoDao.NUpdate(model);
                if (model.DHsjdatetime != null && Convert.ToString(model.DHsjdatetime) != "")
                {
                    model.DHsjdatetime = model.DHsjdatetime;//实际到货时间
                    model.RL_Is = 2;//0 未录单 1 未收货 2 已签收
                 
                    if (IsNSend == "0")//发送
                    {
                        if (model.IsSend != 1)
                        {
                            string i = MassManager.FMb_DeliveryNotice(model.SjId, model.Id);//发货通知推送
                        }
                        model.IsSend = 1;//已经发送
                    }
                }
                else
                {
                    model.RL_Is = 1;//0 未录单 1 未收货 2 已签收
                    model.RL_datetime = model.RL_datetime;//录单时间
                    if (IsNSend == "0")//发送
                    {
                        if (model.IsSend != 1)
                        {
                            string i = MassManager.FMb_DeliveryNotice(model.SjId, model.Id);//发货通知推送
                        }
                        model.IsSend = 1;//已经发送
                    }
                    if (model.DDId.ToString() != "" && model.DDId.ToString()!="null")
                    {
                        TBdskddh(model.Kd_no, model.DDId.ToString());
                    }
                }
                Session["Jjdatetime"] =Convert.ToString(model.RL_datetime);
                flay = _IEP_jlinfoDao.NUpdate(model);
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

        public void TBdskddh(string kdno,string ddid )
        {
            try
            {
                kdno = "*" + kdno;
                Newasia.XYOffer model = new Newasia.XYOffer();
                model.UpdateOrderNumber(kdno, ddid);
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog：").Error("快递单同步电商异常："+e);
            }
        }

        //确定签收
        [HttpPost]
        public JsonResult EditQs(EP_jlinfoView model)
        {
            EP_jlinfoView Newmodel = new EP_jlinfoView();
            SessionUser user = Session[SessionHelper.User] as SessionUser;
            Newmodel = _IEP_jlinfoDao.NGetModelById(model.Id);
            bool flay = false;
            Newmodel.RL_Is = 2;//签收
            Newmodel.DHsjdatetime = model.DHsjdatetime;//签收时间
            Newmodel.QRsjName = model.QRsjName;//签收人
            flay = _IEP_jlinfoDao.NUpdate(Newmodel);
            if (flay)
                return Json(new { result = "success" });
            else
                return Json(new { result = "error" });
        }

        [HttpPost]
        public string trackwl(string typeCom,string nu) 
        {
            string ApiKey = "3120c079cdf22fb0";//快递100网站申请的APIKey
            string url;
            string str;
            url = "http://www.kuaidi100.com/applyurl?key=" + ApiKey + "&com=" + typeCom + "&nu=" + nu;
            str = new System.Net.WebClient().DownloadString(url);
            return str;
        }

        //阿里快递查询接口
        public string aliEpckwl(string kdno, string kdgs,string tel)
        {
            string Gsdm = kdgsdm(kdgs);
            //string json = ExpressinterfaceHelper.Main(kdno, Gsdm);
            string json = ExpressinterfaceHelper.NewMain(kdno, Gsdm, tel);
           return json;
        }

        //返回对应的快递公司的代码
        public string kdgsdm(string kdgs)
        {
            if (kdgs == "sh")//盛辉
            {
                return "SHENGHUI";
            }
            if (kdgs == "sf")//顺丰快递
            {
                return "SFEXPRESS";
            }
            if (kdgs == "db")//德邦快递
            {
                return "DEPPON";
            }
            if (kdgs == "zt")//中通快递
            {
                return "ZTO";
            }
            if (kdgs == "tdhy")//天地华宇
            {
                return "HOAU";
            }
            if(kdgs=="ycky")//远程快运
            {
                return "YCGWL";
            }
            if (kdgs == "JJ")//佳吉
            {
                return "JIAJI";
            }
            if (kdgs == "ztky")//中通快运
            {
                return "ZTO56";
            }
            if (kdgs == "an") //安能物流
            {
                return "ANE";
            }
            return "auto";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeCom">快递公司代号</param>
        /// <param name="nu">单号</param>
        /// <returns></returns>
        
        public ViewResult wlview(string typeCom, string nu, string Id, int? Pageindex,string tel) 
        {
            ViewData["typeCom"] = typeCom;
            ViewData["nu"] = nu;
            ViewData["pageindex"] = Pageindex;
            if (tel != null)
            {
                ViewData["tel"] = tel.Substring(tel.Length - 4, 4);
            }
            else
            {
                ViewData["tel"] = null;
            }
            EP_jlinfoView sys = new EP_jlinfoView();
            if (!string.IsNullOrEmpty(Id))
                sys = _IEP_jlinfoDao.NGetModelById(Id);
            return View("wlview", sys);
        }

        /// <summary>
        /// 到货情况 打印页面
        /// </summary>
        /// <returns></returns>
        public ViewResult Pdhview(string id) 
        {
            ViewData["DHid"] = id;//到货打印
            return View();
        }

        /// <summary>
        /// 根据ID 查找打印记录信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetdyjljsonbyId(string Id) 
        {
            string json = JsonConvert.SerializeObject(_IEP_jlinfoDao.GetlistIdEp_jl(Id));
            return json;
        }

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
                List<EP_jlinfoView> sys = _IEP_jlinfoDao.NGetList_id(id) as List<EP_jlinfoView>;
                if (null != sys)
                {
                    if (_IEP_jlinfoDao.NDelete(sys))
                        return RedirectToAction("List");
                    else
                        return RedirectToAction("List");
                }
            }
            catch
            {
                return Json(new { result = "error" }, "text/html");
            }
            return View("../NACustomerinfo/List");
        }
        #endregion

        #region //各种时间的 获取模块
        /// <summary>
        /// 获取当前的服务器的时间前一天json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Getdatenewjson()
        {
            if (Session["Jjdatetime"] == null)
            {
                string flay = DateTime.Now.AddDays(-1).ToShortDateString();
                Session["Jjdatetime"] = flay;
                return "{\"status\":\"" + Session["Jjdatetime"] + "\"}";
            }
            else
            {
                return "{\"status\":\"" + Session["Jjdatetime"] + "\"}";
            }
        }


        //获取当亲服务器的时间
        public string GetNewdatetimejson()
        {
            DateTime Time = new DateTime();
            Time = DateTime.Now;
            string flay = Time.ToShortDateString();
            return flay;
        }

        //根据时间和天数计算当天时间
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="DayNumber"></param>
        /// <returns></returns>
        public string GetYCdatetime(DateTime datetime, int DayNumber)
        {
            DateTime Time = new DateTime(datetime.Year, datetime.Month, datetime.Day);
            string flay = Time.AddDays(DayNumber).ToShortDateString();
            return flay;
        } 
        #endregion

        //新的 录单页面及功能
        public ActionResult EditPageUI(string id, int? Pageindex) 
        {
            EP_jlinfoView sys = new EP_jlinfoView();
            if (!string.IsNullOrEmpty(id))
                sys = _IEP_jlinfoDao.NGetModelById(id);
            ViewData["pageindex"] = Pageindex;
            return View("EditPageUI", sys);
        }

        //检测显示该客户是否已经绑定微信公众号
        public string JcIsBingbyCusId(string Id)
        {
            IList<WX_OpenIDView> list = _IWX_OpenIDDao.GetCount_byP_Id(Id,"0");
            if (list != null && list.Count != 0)
            {
                return "{\"status\":\"已绑定微信\"}";//已经绑定
            }
            else
            {
                return "{\"status\":\"未绑定微信\"}";//未绑定
            }
        }

        //导出打印记录数据
        public string DCEP_JL(string txtSearch_Name, string txtrlis, string stratrldate, string endrldate, string dystratrldate, string dyendrldate, string kdgs, string K3DDNO)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            string Json = null;
            Json = JsonConvert.SerializeObject(_IEP_jlinfoDao.GetEP_JLINFExportJson(txtSearch_Name, txtrlis, stratrldate, endrldate, dystratrldate, dyendrldate, kdgs, K3DDNO,Suser));
            return Json;
        }

        #region //打印记录导出数据

        public FileResult DCEP_JLDATA(string txtSearch_Name, string txtrlis, string stratrldate, string endrldate, string dystratrldate, string dyendrldate, string kdgs, string K3DDNO)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IList<EP_jlinfoView> list = _IEP_jlinfoDao.GetEP_JLINFExportJson(txtSearch_Name, txtrlis, stratrldate, endrldate, dystratrldate, dyendrldate, kdgs,K3DDNO, Suser);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("快递公司");
            row1.CreateCell(1).SetCellValue("快递单号");
            row1.CreateCell(2).SetCellValue("发货时间");
            row1.CreateCell(3).SetCellValue("收件人");
            row1.CreateCell(4).SetCellValue("收件单位");
            row1.CreateCell(5).SetCellValue("K3单号");
            if (list != null)//数据不为空
            {
                for (int i = 0; i < list.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(Getkdgsname(list[i].Kdgs));
                    rowtemp.CreateCell(1).SetCellValue(list[i].Kd_no);
                    rowtemp.CreateCell(2).SetCellValue(list[i].RL_datetime.ToString());
                    rowtemp.CreateCell(3).SetCellValue(list[i].QRsjName);
                    rowtemp.CreateCell(4).SetCellValue(GetPTsjgsnamebyId(list[i].SjId));
                    rowtemp.CreateCell(5).SetCellValue(list[i].k3_ddno);
                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "快递单打印数据.xls");
        }


        //平台打印的收件公司名称
        public string GetPTsjgsnamebyId(string Id)
        {
            try
            {
                NACustomerinfoView pmodel = _INACustomerinfoDao.GetcusInfobyId(Id);
                if (pmodel != null)
                {
                    return pmodel.Name;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        //电商平台打印收件公司名称
        public string GetaddresseeInfobyCustId(string Id)
        {
            try
            {
                NA_AddresseeInfoView model = _INA_AddresseeInfoDao.NGetModelById(Id);
                if (model != null)
                {
                    return model.ACompany;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
            
        } 

        //返回快递公司名称
        public string Getkdgsname(string val)
        {
            var kdnam="";
            if (val != "")
            {
                if (val == "sh")
                {
                    kdnam = "盛辉物流";
                }
                else if (val == "sf")
                {
                    kdnam = "顺丰快递";
                }
                else if (val == "db")
                {
                    kdnam = "德邦快递";
                }
                else if (val == "zt")
                {
                    kdnam = "中通快递";
                }
                else if (val == "tdhy")
                {
                    kdnam = "天地华宇";
                }
                else if (val == "JJ")
                {
                    kdnam = "佳吉物流";
                }
                else if (val == "ycky")
                {
                    kdnam = "远成快运";
                }
                else if (val == "ztky")
                {
                    kdnam = "中通快运";
                }
            }
            return kdnam;
        }
        #endregion

        #region //按月打印 每天打印一页
        //
        public ActionResult EPJLbymonView(string starttime, string endtime)
        {
            //ViewData["starttime"] = starttime;
            //ViewData["endtime"] = endtime;
            int dtsum = DateDiff(Convert.ToDateTime(starttime),Convert.ToDateTime(endtime));//天数
            //年月
            string Ym = Convert.ToDateTime(starttime).ToString("yyyy-MM");
            ViewData["daysum"] = dtsum;
            ViewData["Ym"] = Ym;
            return View();
        }


        //根据时间查询打印记录
        public string GetEPjlbytime(string starttime)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            string json;
            json = JsonConvert.SerializeObject(_IEP_jlinfoDao.GetEP_JLinfobyjjtime(starttime, starttime, Suser));
            return json;
        }

        /// <summary>
        /// 计算俩个时间之间的天数
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        private static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
        #endregion



    }
}
