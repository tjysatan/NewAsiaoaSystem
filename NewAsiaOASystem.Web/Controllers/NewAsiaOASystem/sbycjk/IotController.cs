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
using Newtonsoft.Json.Linq;

namespace NewAsiaOASystem.Web.Controllers
{
    public class IotController : Controller
    {
        //
        // GET: /Iot/

        IWx_configinfoDao _IWx_configinfoDao = ContextRegistry.GetContext().GetObject("Wx_configinfoDao") as IWx_configinfoDao;

        IWx_openIdinfoDao _IWx_openIdinfoDao = ContextRegistry.GetContext().GetObject("Wx_openIdinfoDao") as IWx_openIdinfoDao;
        IYCAccountbindingInfoDao _IYCAccountbindingInfoDao = ContextRegistry.GetContext().GetObject("YCAccountbindingInfoDao") as IYCAccountbindingInfoDao;
        IYCACCandSIDInfoDao _IYCACCandSIDInfoDao = ContextRegistry.GetContext().GetObject("YCACCandSIDInfoDao") as IYCACCandSIDInfoDao;
        IYCnoticeinfoDao _IYCnoticeinfoDao = ContextRegistry.GetContext().GetObject("YCnoticeinfoDao") as IYCnoticeinfoDao;


        #region //主页面
        public ActionResult Index(string code)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("code" + code);
            Wx_configinfoView model = _IWx_configinfoDao.Getweixinpayconfig();
            if (model != null)
            {
                NewConfig config = new NewConfig();
                Session[SessionHelper.Wpayconfig] = null;
                Session.Remove(SessionHelper.Wpayconfig);
                config.APPID = model.APPID;
                config.MCHID = model.MCHID;
                config.KEY = model.KEYvalue;
                config.APPSECRET = model.APPSECRET;
                config.NOTIFY_URL = model.NOTIFY_URL;
                Session[SessionHelper.Wpayconfig] = config;
            }
            JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("obj:" + obj+",obj2:"+obj.Count);
            if (obj.Count == 2)
            {
                NewConfig config = Session[SessionHelper.Wpayconfig] as NewConfig;
                //构造网页授权获取code的URL
                string host = Request.Url.Host;
                string path = Request.Path;
                string redirect_uri = HttpUtility.UrlEncode("http://" + host + path);
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("地址" + redirect_uri);
                WxPayData data = new WxPayData();
                data.SetValue("appid", config.APPID);
                data.SetValue("redirect_uri", redirect_uri);
                data.SetValue("response_type", "code");
                data.SetValue("scope", "snsapi_userinfo");
                data.SetValue("state", "STATE" + "#wechat_redirect");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("最终地址" + data.ToUrl());
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
                return Redirect(url);
            }
            else
            {
                NewConfig config = Session[SessionHelper.Wpayconfig] as NewConfig;
                Wx_openIdinfoView modelopenId = _IWx_openIdinfoDao.Getnewcusinfobyiccid(obj["openid"].ToString(),"");
                if (modelopenId == null)//不存在-插入
                {
                    JObject obj1 = (JObject)GetYHiNFOinface(obj["access_token"].ToString(), obj["openid"].ToString());
                    InsertOPenid(obj1, "");
                }
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("openid:" + obj["openid"].ToString());
                Session["Openid"] = obj["openid"].ToString();
                Wx_openIdinfoView openIdmodel = _IWx_openIdinfoDao.GetwxcusinfobyopenId(Session["Openid"].ToString());
                ViewData["TXURL"] = openIdmodel.headimgurl;
                ViewData["nickname"] = openIdmodel.nickname;
                ViewData["bdsum"] = _IYCAccountbindingInfoDao.bdzhcountbyopenId(obj["openid"].ToString()).ToString();
                ViewData["fssum"] = _IYCnoticeinfoDao.GetTodayFXsum(obj["openid"].ToString()).ToString();
                return View();
            }

        }

        #region //显示绑定数量和当天通知数量
        public string GetbdsumandtzsumbyopenId(string openId)
        {
            fhmodel model = new fhmodel();
            string json = "";
            model.bdsum = _IYCAccountbindingInfoDao.bdzhcountbyopenId(openId).ToString();
            model.TodayTZsum = _IYCnoticeinfoDao.GetTodayFXsum(openId).ToString();
            json = JsonConvert.SerializeObject(model);
            return json;
        }

        //返回绑定数量和当天通知条数的实体
        public class fhmodel
        {
            /// <summary>
            /// 绑定数量
            /// </summary>
            public virtual string bdsum { get; set; }

            /// <summary>
            /// 当天通知的
            /// </summary>
            public virtual string TodayTZsum { get; set; }
        }
        #endregion
        #endregion

        #region //帐号绑定页面
        public ActionResult bindingView()
        {
            if (Session["Openid"] != null)
            {
                ViewData["openid"] = Session["Openid"].ToString();
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        //帐号绑定提交ajax
        public string bingdingEide(string username, string pwd, string openId)
        {
            YCAccountbindingInfoView model = new YCAccountbindingInfoView();
            model = _IYCAccountbindingInfoDao.GetAccbindingbyuserandopenId(username, openId);
            if (model == null)
            {
                //检测帐号密码的正确性
                string strres = JCZHinfece(username, pwd);
                if (strres == "0")//帐号密码通过
                {
                    YCAccountbindingInfoView newmodel = new YCAccountbindingInfoView();
                    newmodel.openId = openId;
                    newmodel.Username = username;
                    newmodel.Pwd = pwd;
                    newmodel.Isbd = 0;
                    newmodel.C_time = DateTime.Now;
                    if (_IYCAccountbindingInfoDao.Ninsert(newmodel))
                    {
                        YCAccountbindingInfoView ccmodel = new YCAccountbindingInfoView();
                        ccmodel = _IYCAccountbindingInfoDao.GetAccbindingbyuserandopenId(username, openId);
                        if (ccmodel != null)
                        {
                            string jsonstr = Getjkdinfoinfece(ccmodel.Username);
                            InsertzhandSID(ccmodel.Id, jsonstr);
                        }
                        return "2";//绑定成功
                    }
                    else
                    {
                        return "3";//绑定失败
                    }
                }
                else
                {
                    return "1";//帐号或密码有误
                }
            }
            else
            {
                return "0";//该微信和该帐号已经绑定
            }
        }
        #endregion

        public object GetYHiNFOinface(string access_token, string openid)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("access_token：" + access_token + "openid:" + openid);
            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            string json = HttpUtility11.GetData(url);
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("信息：" + json);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            return obj;
        }

        public object Getaccess_tokenbycodeinface(string code)
        {
            //string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx44f57e0f1268190b&secret=bcbfe205a0e5fad5a4ab7f2ebb90656d&code={0}&grant_type=authorization_code ", code);
            //string json = HttpUtility11.GetData(url);
            //JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            //return obj;
            NewConfig config = Session[SessionHelper.Wpayconfig] as NewConfig;
            string appid = config.APPID;
            string secret = config.APPSECRET;
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("code2" + code);
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("APPID" + config.APPID);
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code ", appid, secret, code);
            string json = HttpUtility11.GetData(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            return obj;
        }

        //插入微信访问的客户信息
        public void InsertOPenid(object obj, string dlname)
        {
            try
            {
                JObject NEWobj = (JObject)obj;
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("cuowu1:" + obj);
                Wx_openIdinfoView model = new Wx_openIdinfoView();
                model.openid = NEWobj["openid"].ToString();//OPENId
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("cuowu2:" + NEWobj["openid"].ToString());
                model.nickname = NEWobj["nickname"].ToString();//昵称
                model.sex = Convert.ToInt32(NEWobj["sex"].ToString());//性别
                model.city = NEWobj["city"].ToString();//城市
                model.province = NEWobj["province"].ToString();//省份
                model.country = NEWobj["country"].ToString();//国家
                model.headimgurl = NEWobj["headimgurl"].ToString();//头像地址
                model.C_time = DateTime.Now;
                model.Dlname = dlname;
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("cuowu2:" + NEWobj["openid"].ToString());
                _IWx_openIdinfoDao.Ninsert(model);

            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("=========================插入openid 错误===================================");
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("11:" + e);
            }
        }



        #region //绑定帐号查看页面
        public ActionResult ACClistView()
        {
            if (Session["Openid"] != null)
            {
                ViewData["openid"] = Session["Openid"].ToString();
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        #region //openId绑定的帐号数据json
        public string GetbdzhDATAJson(string openId)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IYCAccountbindingInfoDao.GetacclistinfobyopenId(openId));
            return json;
        }
        #endregion
        #endregion

        #region //该微信绑定的帐号下面的监控点管理
        /// <summary>
        /// 监控点页面
        /// </summary>
        /// <param name="zhId"></param>
        /// <returns></returns>
        public ActionResult JKDlistView(string zhId)
        {
            if (Session["Openid"] != null)
            {
                ViewData["openid"] = Session["Openid"].ToString();
                ViewData["zhId"] = zhId;
                return View();
            }
            else
            {
                return View("Index");
            }
            //ViewData["zhId"] = zhId;
            //return View();
        }

        //查询监控点数据
        public string GetjkdDATAajax(string zhId, string type)
        {
            try
            {
                string json = "";
                json = JsonConvert.SerializeObject(_IYCACCandSIDInfoDao.GetSIDlistIsfsbyzhId(zhId, type));
                return json;
            }
            catch
            {
                return "";
            }
        }

        //监控点取消/接受-通知
        public string JKXQXORJSTZEide(string Id, string type)
        {
            try
            {
                YCACCandSIDInfoView model = _IYCACCandSIDInfoDao.NGetModelById(Id);
                if (type == "0")
                {
                    model.Isfs = 0;
                }
                else
                {
                    model.Isfs = 1;
                }
                model.Cz_time = DateTime.Now;
                if (_IYCACCandSIDInfoDao.NUpdate(model))
                    return "2";//操作成功
                else
                    return "1";//操作失败
            }
            catch
            {
                return "0";//操作异常
            }
        }

        //刷新监控点监控点
        public string shuaxinjkdEide(string nameId)
        {
            try
            {
                //查询绑定帐号
                YCAccountbindingInfoView zhmodel = _IYCAccountbindingInfoDao.GetbdzhinfobyId(nameId);
                if (zhmodel != null)
                {
                    //查询监控点
                    List<YCACCandSIDInfoView> jkdmodelliet = _IYCACCandSIDInfoDao.GetALLSIDlistbyzhId(nameId) as List<YCACCandSIDInfoView>;
                    if (jkdmodelliet != null)
                    {
                        _IYCACCandSIDInfoDao.NDelete(jkdmodelliet);
                    }
                    string jsonstr = Getjkdinfoinfece(zhmodel.Username);
                    InsertzhandSID(zhmodel.Id, jsonstr);
                    return "2";
                }
                else
                {
                    return "1";//帐号不存在
                }

            }
            catch
            {
                return "0";//操作异常
            }
        }
        #endregion

        #region //检测帐号密码是否正确的接口
        public string JCZHinfece(string username, string pwd)
        {
            string str = string.Empty;
            str += "http://www.sbycjk.net/";
            str += "userconfirm?username=" + username;
            str += "&password=" + pwd;
            string result = null;
            result = HttpUtility11.GetData(str);
            int index = result.IndexOf("true");
            if (index > -1)
                return "0";
            else
                return "1";
        }
        #endregion

        #region //插入帐号对应的SID信息
        public void InsertzhandSID(string opId, string jsonString)
        {
            try
            {
                YCACCandSIDInfoView model = new YCACCandSIDInfoView();
                JObject jsonlist = JObject.Parse(jsonString);
                JToken obstr = jsonlist["monitorList"];
                foreach (var arry in obstr)
                {
                    string monitor_id = arry["monitor_id"].ToString();
                    string SID = arry["sid"].ToString();
                    string monitor_name = arry["monitor_name"].ToString();
                    if (_IYCACCandSIDInfoDao.GetSIDbynameIdandsid(opId, SID) == null)
                    {
                        model = new YCACCandSIDInfoView();
                        model.nameId = opId;
                        model.jkdname = monitor_name;
                        model.SID = SID;
                        model.C_time = DateTime.Now;
                        model.SIDId = Convert.ToInt32(monitor_id);
                        model.Isfs = 0;
                        _IYCACCandSIDInfoDao.Ninsert(model);
                    }
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("转换插入异常:" + e);
            }
        }
        #endregion

        #region //根据帐号GET监控点信息
        public string Getjkdinfoinfece(string username)
        {
            try
            {
                string str = string.Empty;
                str += "http://www.sbycjk.net/";
                str += "getwechatusermonitorlist?user_name=" + username;
                string result = null;
                result = HttpUtility11.GetData(str);
                // getObjectByJson(result);
                return result;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("监控点接口异常:" + e);
                return null;
            }
        }
        #endregion

        #region json转换model
        private static void getObjectByJson(string jsonString)
        {
            JObject jsonlist = JObject.Parse(jsonString);
            JToken obstr = jsonlist["monitorList"];
            foreach (var arry in obstr)
            {
                string y = arry["sid"].ToString();
            }
        }
        #endregion

        #region //告警/通知的消息列表
        public ActionResult NoticelistView()
        {
            if (Session["Openid"] != null)
            {
                ViewData["openid"] = Session["Openid"].ToString();
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        //查询近5天内的告警数据
        public string GetwutianNoticejson(string openId)
        {
            try
            {
                string json = "";
                json = JsonConvert.SerializeObject(_IYCnoticeinfoDao.GetwutianFxnoticeinfo(openId));
                return json;
               
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //对外接口（告警通知发送）
        public string YCTemplatemessageInterface(string SID, string constr, string type)
        {
            resmodel model = new resmodel();
            string json = "";
            try
            {
                //检查SID是否存在
                IList<YCACCandSIDInfoView> SIDmodellist = _IYCACCandSIDInfoDao.GetSIDBDZHlistbySID(SID);
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("是否存在:");
                if (SIDmodellist != null)
                {
                    foreach (var item in SIDmodellist)
                    {
                        YCACCandSIDInfoView SIDmodel = item;
                        //判定是否需要发送
                        if (SIDmodel.Isfs == 0)//需要发送
                        {
                            //查找帐号
                            YCAccountbindingInfoView zhmodel = _IYCAccountbindingInfoDao.GetbdzhinfobyId(SIDmodel.nameId);
                            if (zhmodel != null)
                            {
                                string I = MassManager.YCNotice_MB(zhmodel.openId, zhmodel.Username, constr, SIDmodel.jkdname, SIDmodel.SID, type);

                            }
                            model.code = "0";
                            model.result = "调用成功";
                        }
                        else
                        {
                            model.code = "0";
                            model.result = "调用成功";
                        }
                    }
                }
                else
                {
                    model.code = "3";
                    model.result = "没有绑定微信公众号";
                }
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("接口异常:" + e);
                model.code = "2";
                model.result = "接口异常";
            }
            json = JsonConvert.SerializeObject(model);
            return json;
        }

        public class resmodel
        {
            /// <summary>
            /// 状态 0 发送成功 1发送失败 2 接口异常 3 没有绑定微信公众号
            /// </summary>
            public virtual string code { get; set; }

            /// <summary>
            /// 状态说明
            /// </summary>
            public virtual string result { get; set; }
        }
        #endregion

    }
}
