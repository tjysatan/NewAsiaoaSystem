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
    public class activityController : Controller
    {
        //
        // GET: /activity/

        IWeChat_forwardInfoDao _IWeChat_forwardInfoDao = ContextRegistry.GetContext().GetObject("WeChat_forwardInfoDao") as IWeChat_forwardInfoDao;
        IWeChat_OrderInfoDao _IWeChat_OrderInfoDao = ContextRegistry.GetContext().GetObject("WeChat_OrderInfoDao") as IWeChat_OrderInfoDao;
        IWechat_TXinfoDao _IWechat_TXinfoDao = ContextRegistry.GetContext().GetObject("Wechat_TXinfoDao") as IWechat_TXinfoDao;
        IWeChat_signuppersonInfoDao _IWeChat_signuppersonInfoDao = ContextRegistry.GetContext().GetObject("WeChat_signuppersonInfoDao") as IWeChat_signuppersonInfoDao;
        IWeChat_SlyderpersonInfoDao _IWeChat_SlyderpersonInfoDao = ContextRegistry.GetContext().GetObject("WeChat_SlyderpersonInfoDao") as IWeChat_SlyderpersonInfoDao;

        //年会活动
        IWeChat_AnnualmeetingSignDao _IWeChat_AnnualmeetingSignDao = ContextRegistry.GetContext().GetObject("WeChat_AnnualmeetingSignDao") as IWeChat_AnnualmeetingSignDao;
        IWeChat_AnnualmeetingBarrageDao _IWeChat_AnnualmeetingBarrageDao = ContextRegistry.GetContext().GetObject("WeChat_AnnualmeetingBarrageDao") as IWeChat_AnnualmeetingBarrageDao;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult activityView(string AID, string BID, string code)
        {
            try
            {
                JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
                if (obj.Count == 2)
                {
                    ViewData["APOpenid"] = AID;
                    ViewData["BPOpenid"] = "true";
                }
                else
                {
                    JObject obj1 = (JObject)GetYHiNFOinface(obj["access_token"].ToString(), obj["openid"].ToString());
                    ViewData["Openid"] = obj["openid"].ToString();
                    ViewData["APOpenid"] = AID;
                    InsertOPenid(obj1, AID);
                }
                //ViewData["Openid"] = "ol6V6t-M-QUOkjLqQ8_wgSZ9jYMs";
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================12322232322===================================");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("11:" + e);
            }
            return View();
        }

        public object Getaccess_tokenbycodeinface(string code)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx44f57e0f1268190b&secret=bcbfe205a0e5fad5a4ab7f2ebb90656d&code={0}&grant_type=authorization_code ", code);
            string json = HttpUtility11.GetData(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            return obj;
        }

        public object GetYHiNFOinface(string access_token, string openid)
        {
            //access_token = "0HrM7H8eHJyw03m8xv0UkCFrdz4AGGEc9eZCe_0odEyRnv_MnfgjhVnubOxJHpM2excWR2nkVlrUxgPrhT03l7GF9uREMqzyqayVsckwLng";
            //openid = "ol6V6t-M-QUOkjLqQ8_wgSZ9jYMs";
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            string json = HttpUtility11.GetData(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            return obj;
        }

        public void InsertOPenid(object obj, string Pid)
        {
            try
            {
                JObject NEWobj = (JObject)obj;
                if (_IWeChat_forwardInfoDao.Jcopenid(NEWobj["openid"].ToString()) == false)
                {
                    WeChat_forwardInfoView model = new WeChat_forwardInfoView();
                    model.openid = NEWobj["openid"].ToString();//OPENId
                    model.nickname = NEWobj["nickname"].ToString();//昵称
                    model.sex =Convert.ToInt32(NEWobj["sex"].ToString());//性别
                    model.city = NEWobj["city"].ToString();//城市
                    model.province = NEWobj["province"].ToString();//省份
                    model.country = NEWobj["country"].ToString();//国家
                    model.headimgurl = NEWobj["headimgurl"].ToString();//头像地址
                    model.P_openid = Pid;
                    model.C_time = DateTime.Now;
                    _IWeChat_forwardInfoDao.Ninsert(model);
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================插入openid 错误===================================");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("11:" + e);
            }
        }

        //点击数据列表

        #region //列表以及查询页面

        #region //分页列表页面
        public ActionResult List(int? pageIndex)
        {
            PagerInfo<WeChat_forwardInfoView> listmodel = GetListPager(pageIndex);
            return View(listmodel);
        }
        #endregion

        //条件查询
        #region //条件查询
        public JsonResult SearchList()
        {
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<WeChat_forwardInfoView> listmodel = GetListPager(pageIndex);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //分页数据
        private PagerInfo<WeChat_forwardInfoView> GetListPager(int? pageIndex)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IWeChat_forwardInfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IWeChat_forwardInfoDao.SetPagerPageSize(11);
            PagerInfo<WeChat_forwardInfoView> listmodel = _IWeChat_forwardInfoDao.GetWXHDcusList();
            return listmodel;
        }
        #endregion
        #endregion

        //直接点击量或者间接点击量
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OPenid"></param>
        /// <param name="type">0 直接点击量  1间接点击量</param>
        /// <returns></returns>
        public string AjaxGetzjtjcount(string OPenid,string type)
        {
            int count=0;
            if (type == "0") {
                count = _IWeChat_forwardInfoDao.GetzjtjcunotbyOpenId(OPenid);
            }
            if (type == "1")
            {
                count = _IWeChat_forwardInfoDao.GetjjtjcunotbyOpenId(OPenid);
            }
            return count.ToString();
        }


        //商品选择页面
        public ActionResult OrderView(string openid) 
        {
            ViewData["openid"] = openid;
            return View();
        }

        //订单提交
        public string SubmitWxOrder(string Openid,string cpname,string sl,string fangan,string CPJG,string TXJG)
        {
            //bool flay = false;
            WeChat_OrderInfoView model = new WeChat_OrderInfoView();
            model.OpenId = Openid;
            model.Cpname = cpname;
            model.cpJiage =Convert.ToDecimal(CPJG);
            model.txjg = Convert.ToDecimal(TXJG);
            model.Jiage = Convert.ToInt32(CPJG) + Convert.ToInt32(TXJG);
            model.C_time = DateTime.Now;
            model.Type = 0;
            model.Fangan = Convert.ToInt32(fangan);
            model.Sl = Convert.ToInt32(sl);
            string Id = _IWeChat_OrderInfoDao.InsertID(model);
            Session["o_Id"] = Id;
            if (Id!=null)
            {
                return Id;//成功
            }
            else
            {
                return "1";//失败
            }
        }

        /// <summary>
        /// 订单详情页面及支付页面
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public ActionResult OrderInfoPay(string Id, string OpenId) 
        {
            return View();
        }

        //支付成功页面
        public ActionResult PaySuccView(string Id)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================支付成功返回===================================");
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error(Id);
            WeChat_OrderInfoView model = new WeChat_OrderInfoView();
            model= _IWeChat_OrderInfoDao.NGetModelById(Id);
            model.Type = 1;
            model.Zf_time = DateTime.Now;
            _IWeChat_OrderInfoDao.NUpdate(model);
            return View();
        }

        //ajax地址修改
        public string UpdateADDR(string Id, string addr, string Tel)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================地址修改===================================");
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error(Id+","+addr+","+Tel);
            WeChat_OrderInfoView model = new WeChat_OrderInfoView();
            model = _IWeChat_OrderInfoDao.NGetModelById(Id);
            model.addr = addr;
            model.Tel = Tel;
            _IWeChat_OrderInfoDao.NUpdate(model);
            return "1";//修改成功
        }

        //微信收益（直接收益）
        public string ajaxchengjiaoshul(string type, string openId)
        {
            int n=_IWeChat_OrderInfoDao.Getchengjiaoshuli(type,openId);
            return n.ToString();
        }

        //最新好友成交记录
        public string ajaxcjjl(string OpenID)
        {
            string json;
            json =JsonConvert.SerializeObject(_IWeChat_OrderInfoDao.Getcjjltop(OpenID));
            return json;
        }

        //查询微信信息
        public string ajaxwxinfobyopenid(string openid)
        {
            string json;
            json = JsonConvert.SerializeObject(_IWeChat_forwardInfoDao.Getwxinfobyopenid(openid));
            return json;
        }

        //已提现多少钱
        public string ajawtxinfoopenid(string openid)
        {
            string json;
            json = JsonConvert.SerializeObject(_IWechat_TXinfoDao.GetTxinfobyopenid(openid));
            return json;
        }

        //提现申请
        public string InsertTxsq(string Jine, string openid)
        {
            if (_IWechat_TXinfoDao.Jcwclsq(openid) == true)
            {
                return "0";//检测到有未出来的提现申请
            }
            else
            {
                Jine = Jine.Substring(0, Jine.Length - 1);
                Wechat_TXinfoView model = new Wechat_TXinfoView();
                model.Jine = Convert.ToInt32(Jine);//
                model.OpenId = openid;
                model.QR_type = 0;
                model.Tx_time = DateTime.Now;
                _IWechat_TXinfoDao.Ninsert(model);
                return "1";//申请成功
            }

        }


        #region //现场抽奖活动
          //signup 构造微信访问链接 
        public ActionResult Signup(string code)
        {

            JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
            if (obj.Count == 2)
            {
                return Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx44f57e0f1268190b&redirect_uri=http://wx.chinanewasia.com/activity/Signup&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect");
            }
            else
            {
                JObject obj1 = (JObject)GetYHiNFOinface(obj["access_token"].ToString(), obj["openid"].ToString());
                ViewData["Openid"] = obj["openid"].ToString();
                string cjzt = InsertSignupinfo(obj1);
                ViewData["czzt"] = cjzt;
                return View();
            }
        }

        public ActionResult testView(string Code)
        {
            try
            {
                JObject obj = (JObject)Getaccess_tokenbycodeinface(Code);
                if (obj.Count == 2)
                {
                    return Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx44f57e0f1268190b&redirect_uri=http://wx.chinanewasia.com/activity/testView&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect");
                }
                else
                {
                    JObject obj1 = (JObject)GetYHiNFOinface(obj["access_token"].ToString(), obj["openid"].ToString());
                    ViewData["Openid"] = obj["openid"].ToString();
                    string cjzt = InsertSignupinfo(obj1);
                    ViewData["czzt"] = cjzt;
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }


        //插入微信信息
        public string  InsertSignupinfo(object obj)
        {
            try
            {
                JObject NEWobj = (JObject)obj;
                WeChat_signuppersonInfoView model =_IWeChat_signuppersonInfoDao.Getwxinfobyopenid(NEWobj["openid"].ToString());
                if (model==null)
                {
                    model = new WeChat_signuppersonInfoView();
                    model.openid = NEWobj["openid"].ToString();//OPENId
                    model.nickname = NEWobj["nickname"].ToString();//昵称
                    model.sex = Convert.ToInt32(NEWobj["sex"].ToString());//性别
                    model.city = NEWobj["city"].ToString();//城市
                    model.province = NEWobj["province"].ToString();//省份
                    model.country = NEWobj["country"].ToString();//国家
                    model.headimgurl = NEWobj["headimgurl"].ToString();//头像地址
                    model.C_time = DateTime.Now;
                    model.Iscj = 0;
                    _IWeChat_signuppersonInfoDao.Ninsert(model);
                    return "0";//未参加过
                }
                else
                {
                    if (model.Iscj == 1)
                    {
                        return "1";
                    }
                    else
                    {
                        return "2";
                    }
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================插入openid 错误===================================");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("11:" + e);
            }
            return "1";
        }

        //确定参加信息
        public string Insertsignup(string openid, string name, string tel)
        {
            WeChat_signuppersonInfoView model =_IWeChat_signuppersonInfoDao.Getwxinfobyopenid(openid);
            if (model != null)
            {   
                model.Gsname = name;
                model.Tel = tel;
                model.Iscj = 1;
                if (_IWeChat_signuppersonInfoDao.NUpdate(model))
                {
                    return "1";//成功
                }
                else
                {
                    return "0";//失败
                }
            }
            else
            {
                return "0";
            }
        }

        //照片墙
        public ActionResult Photowall()
        {
            //当前参加的数量
            int COUNT = _IWeChat_signuppersonInfoDao.Getsignupcount();
            ViewData["signupcount"] = COUNT;
            return View();
        }

        //获取全部参加人的信息
        public string Signupjson()
        {
            string json;
            json =JsonConvert.SerializeObject(_IWeChat_signuppersonInfoDao.NGetList());
            return json;

        }

        //抽奖页面
        public ActionResult CJview()
        {
            string imgurl="";
            string nc="";
            IList<WeChat_signuppersonInfoView> modellist = _IWeChat_signuppersonInfoDao.NGetList();
            if (modellist != null)
            {
                for (int i = 0,j=modellist.Count; i < j; i++)
                {
                    imgurl = modellist[i].headimgurl + "," + imgurl;
                    nc = modellist[i].nickname + "&" + modellist[i].Gsname + "&"+modellist[i].Tel + "," + nc;
                }
            }
            imgurl = imgurl.Substring(0, imgurl.Length - 1);
            nc = nc.Substring(0, nc.Length - 1);
            ViewData["imgurl"] = imgurl;
            ViewData["nc"] = nc;
            return View();
        }

        #endregion

        #region //微信线上大转盘抽奖

        //抽奖页面
        public ActionResult LuckdrawView(string code)
        {
            //JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
            //if (obj.Count == 2)
            //{
            //    log4net.LogManager.GetLogger("ApplicationInfoLog").Error("obj：" + obj.Count);
            //    return Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx44f57e0f1268190b&redirect_uri=http://wx.chinanewasia.com/activity/LuckdrawView&response_type=code&scope=snsapi_base&state=STATE&connect_redirect=1#wechat_redirect");

            //}
            //else
            //{
            //    string openid = obj["openid"].ToString();
            //    WeChat_SlyderpersonInfoView model = new WeChat_SlyderpersonInfoView();
            //    model = _IWeChat_SlyderpersonInfoDao.Getwxinfobyopenid(openid);
            //    if (model == null)
            //    {
            //        Insartdzpinfo(openid);
            //        model = new WeChat_SlyderpersonInfoView();
            //        model = _IWeChat_SlyderpersonInfoDao.Getwxinfobyopenid(openid);
            //    }
            //    return View(model);
            //}
            string openid = "ol6V6t-M-QUOkjLqQ8_wgSZ9jYMs";
            WeChat_SlyderpersonInfoView model = new WeChat_SlyderpersonInfoView();
            model = _IWeChat_SlyderpersonInfoDao.Getwxinfobyopenid(openid);
            if (model == null)
            {
                Insartdzpinfo(openid);
                model = new WeChat_SlyderpersonInfoView();
                model = _IWeChat_SlyderpersonInfoDao.Getwxinfobyopenid(openid);
            }
            return View(model);
            
        }

        //插入参加人的openid
        public void Insartdzpinfo(string opendId)
        {
            WeChat_SlyderpersonInfoView model = new WeChat_SlyderpersonInfoView();
            model.openid = opendId;
            model.IsWin = 0;
            model.Winname = null;
            model.C_time = DateTime.Now;
            _IWeChat_SlyderpersonInfoDao.Ninsert(model);
        }

        /// <summary>
        /// 中奖信息插入
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="zjname"></param>
        /// <returns></returns>
        public string ZJupdate(string Id, string zjname)
        {
            WeChat_SlyderpersonInfoView model = new WeChat_SlyderpersonInfoView();
            string sjstr = Ranstr();
            model=_IWeChat_SlyderpersonInfoDao.NGetModelById(Id);
            model.IsWin = 1;
            model.Winname = zjname;
            model.Winstr = sjstr;//中奖窜号
            if (_IWeChat_SlyderpersonInfoDao.NUpdate(model))
            {
                return sjstr;
            }
            else
            {
                return "1";
            }
        }


        //随机生产领奖号码
        public string Ranstr()
        {
            string str;
            Random ran = new Random();
            int RandKey = ran.Next(1000, 9999);
            str =RandKey.ToString();
            return str;

        }

        //获取全部中奖信息json
        public string AjaxALLzjinfojson()
        {
            string json;
            json = _IWeChat_SlyderpersonInfoDao.GetZJjson();
            return json;
        }

        /// <summary>
        /// 领奖页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AwardView()
        {
            return View();
        }

        public string AwardEdit(string zjstr)
        {
            WeChat_SlyderpersonInfoView model = _IWeChat_SlyderpersonInfoDao.Getinfobyzjstr(zjstr);
            string json;
            if (model != null)
            {
                if (model.IsWin == 1) {
                    model.IsWin = 2;
                    _IWeChat_SlyderpersonInfoDao.NUpdate(model);
                    model.Id = "0";//成功领奖 

                }
                else if (model.IsWin == 0)
                {
                    model.Id = "1";//未中奖

                }
                else
                {
                    model.Id = "2";//已经领奖
                }
                json = JsonConvert.SerializeObject(model);
            }
            else
            {
                json = JsonConvert.SerializeObject(model);
            }
            return json;
        }
        #endregion

        #region //年会签到上墙和留言弹幕
        public ActionResult An_ClientView(string code)
        {
            JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
            if (obj.Count == 2)
            {
                return Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx44f57e0f1268190b&redirect_uri=http://wx.chinanewasia.com/activity/An_ClientView&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect");
            }
            else
            {
                JObject obj1 = (JObject)GetYHiNFOinface(obj["access_token"].ToString(), obj["openid"].ToString());
                ViewData["Openid"] = obj1["openid"].ToString();
                ViewData["nickname"] = obj1["nickname"].ToString();
                ViewData["headimgurl"] = obj1["headimgurl"].ToString();
                InsertAn_signinfo(obj1);
                return View();
            }
        }


        //插入签到的人
        public void InsertAn_signinfo(object obj)
        {
            try 
            {
                JObject NEWobj = (JObject)obj;
                WeChat_AnnualmeetingSignView model = _IWeChat_AnnualmeetingSignDao.Getwxinfobyopenid(NEWobj["openid"].ToString());
                if (model == null)
                {
                    model = new WeChat_AnnualmeetingSignView();
                    model.openid = NEWobj["openid"].ToString();//OPENId
                    model.nickname = NEWobj["nickname"].ToString();//昵称
                    model.sex = Convert.ToInt32(NEWobj["sex"].ToString());//性别
                    model.city = NEWobj["city"].ToString();//城市
                    model.province = NEWobj["province"].ToString();//省份
                    model.country = NEWobj["country"].ToString();//国家
                    model.headimgurl = NEWobj["headimgurl"].ToString();//头像地址
                    model.C_time = DateTime.Now;
                    _IWeChat_AnnualmeetingSignDao.Ninsert(model);
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================插入openid 错误===================================");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("11:" + e);
            }
        }

        //提交插入留言信息
        public string InsertAn_BarrageEide(string conter, string Id)
        {
            WeChat_AnnualmeetingBarrageView model = new WeChat_AnnualmeetingBarrageView();
            WeChat_AnnualmeetingSignView modelnew = _IWeChat_AnnualmeetingSignDao.Getwxinfobyopenid(Id);
            model.C_time = DateTime.Now;
            model.Conter = conter;
            model.headimgurl = modelnew.headimgurl;
            model.OpenId = modelnew.openid;
            model.nickname = modelnew.nickname;
            model.Issq = 0;
            if( _IWeChat_AnnualmeetingBarrageDao.Ninsert(model))
            {
                return "0";
            }else
            {
                return "1";
            }
        }


        //签到墙以及显示弹幕的页面
        public ActionResult AnView()
        {
            int Rcount = _IWeChat_AnnualmeetingSignDao.Getsignupcount();
            ViewData["Rcount"] = Rcount;
            return View();
        }

        //获取签到人数

        //获取全部参加人的信息
        public string ANsignjson()
        {
            string json;
            json = JsonConvert.SerializeObject(_IWeChat_AnnualmeetingSignDao.NGetList());
            return json;

        }

        //获取留言
        public string ANlYJson()
        {
            string json;
            IList<WeChat_AnnualmeetingBarrageView> listmodel = _IWeChat_AnnualmeetingBarrageDao.listinfobyhd();
            json = JsonConvert.SerializeObject(listmodel);
            if (listmodel != null)
            {
                for (int i = 0,j=listmodel.Count; i < j; i++)
                {
                    listmodel[i].Issq = 1;
                    _IWeChat_AnnualmeetingBarrageDao.NUpdate(listmodel[i]);
                }
            }
            return json;
        }
        #endregion

    }
}
