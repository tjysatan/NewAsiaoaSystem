using NewAsiaOASystem.Dao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace NewAsiaOASystem.Web
{
    public class WeixinExecutor : IWeixinExecutor
    {
        IWX_ReceiveMessageDao _IWX_ReceiveMessageDao = ContextRegistry.GetContext().GetObject("WX_ReceiveMessageDao") as IWX_ReceiveMessageDao;
        IWX_SendMessageDao _IWX_SendMessageDao = ContextRegistry.GetContext().GetObject("WX_SendMessageDao") as IWX_SendMessageDao;
        IWX_MessageDao _IWX_MessageDao = ContextRegistry.GetContext().GetObject("WX_MessageDao") as IWX_MessageDao;
        IWX_Message_NewsDao _IWX_Message_NewsDao = ContextRegistry.GetContext().GetObject("WX_Message_NewsDao") as IWX_Message_NewsDao;
        IWX_OpenIDDao _IWX_OpenIDDao = ContextRegistry.GetContext().GetObject("WX_OpenIDDao") as IWX_OpenIDDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        IDisImmuneCenterDao _IDisImmuneCenterDao = ContextRegistry.GetContext().GetObject("DisImmuneCenterDao") as IDisImmuneCenterDao;
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string AppSecret = WebConfigurationManager.AppSettings["WeixinAppSecret"];
        public static readonly string ym = WebConfigurationManager.AppSettings["ym"];
        public WeixinExecutor()
        {
        }

        public string Execute(WeixinMessage message)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("进来了吗：" + message);
            var result = "";
            string openId = message.Body.FromUserName.Value;
            var myUserName = message.Body.ToUserName.Value;
            WX_ReceiveMessageView model = new WX_ReceiveMessageView();
            model = _IWX_ReceiveMessageDao.FromDytoModel(message);//把微信post过来的数据转换成我们的实体
            _IWX_ReceiveMessageDao.Ninsert(model);//保存微信发送过来的信息
            string content = model.R_Content;//关键词
            string CreateTime = Tools.GetUnixTimeForDateTimeNow(DateTime.Now.ToString());
            if (content != null)
            {
                IList<WX_Message> list = _IWX_MessageDao.GetWX_MessageList(content);//通过关键字查找回复的信息
                if ((list == null) || (list.Count <= 0))
                {
                    list = _IWX_MessageDao.GetWX_MessageMRList();//查询默认的回复
                }
                if ((list == null) || (list.Count <= 0))
                {
                    result = RepayText(openId, myUserName, CreateTime, content);
                }
                else
                {
                    if (content == "绑定")//传过来是绑定的值
                    {
                        if (_IWX_OpenIDDao.GetCount_byOpenId(openId,"0") == null)//判断是否已经绑定
                        {
                            IList<WX_Message> op_list = _IWX_MessageDao.GetWX_MessageList(content);
                            if ((op_list == null) || (op_list.Count <= 0))
                            {
                                op_list = _IWX_MessageDao.GetWX_MessageMRList();
                            }
                            if ((op_list == null) || (op_list.Count <= 0))
                            {
                                result = RepayText(openId, myUserName, CreateTime, "感谢关注新亚洲微信公众平台!");
                            }
                            else
                            {
                                int num = 0;
                                if (op_list.Count > 1)
                                {
                                    num = new Random(Guid.NewGuid().GetHashCode()).Next(0, op_list.Count);
                                }
                                WX_Message autoMessage = op_list[num];
                                if (autoMessage.MsgType == "text")
                                {
                                    result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                }
                                if (autoMessage.MsgType == "news")
                                {
                                    List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                    listmodel = listmodel.FindAll(x => x.Title != null);
                                    //修改绑定的URL
                                    WX_Message_News B_listmodel = listmodel[0]; ;
                                    B_listmodel.Url = ym+"/Binding/Index?OpenId=" + openId;
                                    _IWX_Message_NewsDao.UpdateUrl(B_listmodel);
                                    result = RepayNews(openId, myUserName, listmodel);
                                }
                            }
                        }
                        else//已经绑定
                        {
                            result = RepayText(openId, myUserName, CreateTime, "检测到您的微信已绑定电商平台，无需再次绑定！");
                        }
                    }
                    else if (content == "内部绑定")
                    {
                        if (_IWX_OpenIDDao.GetCount_byOpenId(openId, "1") == null)//判断是否已经绑定
                        {
                            IList<WX_Message> op_list = _IWX_MessageDao.GetWX_MessageList(content);
                            if ((op_list == null) || (op_list.Count <= 0))
                            {
                                op_list = _IWX_MessageDao.GetWX_MessageMRList();
                            }
                            if ((op_list == null) || (op_list.Count <= 0))
                            {
                                result = RepayText(openId, myUserName, CreateTime, "感谢关注新亚洲微信公众平台!");
                            }
                            else
                            {
                                int num = 0;
                                if (op_list.Count > 1)
                                {
                                    num = new Random(Guid.NewGuid().GetHashCode()).Next(0, op_list.Count);
                                }
                                WX_Message autoMessage = op_list[num];
                                if (autoMessage.MsgType == "text")
                                {
                                    result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                }
                                if (autoMessage.MsgType == "news")
                                {
                                    List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                    listmodel = listmodel.FindAll(x => x.Title != null);
                                    //修改绑定的URL
                                    WX_Message_News B_listmodel = listmodel[0]; ;
                                    B_listmodel.Url = ym + "/Binding/InsideuserBdView?OpenId=" + openId;
                                    _IWX_Message_NewsDao.UpdateUrl(B_listmodel);
                                    result = RepayNews(openId, myUserName, listmodel);
                                }
                            }
                        }
                        else//已经绑定
                        {
                            result = RepayText(openId, myUserName, CreateTime, "检测到您的微信已绑定平台帐号，无需再次绑定！");
                        }
                    }
                   else if(content=="解除绑定")//解除绑定
                    {
                        if (_IWX_OpenIDDao.GetCount_byOpenId(openId,"0") != null)//判断是否绑定
                        {
                            IList<WX_Message> op_list = _IWX_MessageDao.GetWX_MessageList(content);
                            if ((op_list == null) || (op_list.Count <= 0))
                            {
                                op_list = _IWX_MessageDao.GetWX_MessageMRList();
                            }
                            if ((op_list == null) || (op_list.Count <= 0))
                            {
                                result = RepayText(openId, myUserName, CreateTime, "感谢关注新亚洲微信公众平台!");
                            }
                            else
                            {
                                int num = 0;
                                if (op_list.Count > 1)
                                {
                                    num = new Random(Guid.NewGuid().GetHashCode()).Next(0, op_list.Count);
                                }
                                WX_Message autoMessage = op_list[num];

                                if (autoMessage.MsgType == "text")
                                {
                                    result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                }
                                if (autoMessage.MsgType == "news")
                                {
                                    List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                    listmodel = listmodel.FindAll(x => x.Title != null);
                                    //修改绑定的URL
                                    WX_Message_News B_listmodel = listmodel[0]; ;
                                    B_listmodel.Url =ym+ "/Binding/Remove?OpenId="+openId;
                                    _IWX_Message_NewsDao.UpdateUrl(B_listmodel);
                                    result = RepayNews(openId, myUserName, listmodel);
                                }
                            }
                        }
                        else
                        {
                            result = RepayText(openId, myUserName, CreateTime, "检测到该微信号尚未绑定系统帐号，无需解除绑定！");
                        }
                   }
                 else if(content=="未处理")
                  {
                      IList<WX_OpenIDView> modellist = _IWX_OpenIDDao.GetCount_byOpenId(openId,"0");//根据OPenId查找绑定的信息
                      if (modellist != null)//判断是否绑定
                      {
                          string personid=modellist[0].Person_Id;//绑定的用户Id
                          SysPerson pmodel = _ISysPersonDao.GetDModelbyId(personid);//通过用户ID查找用户信息
                          List<SysRole> listR = pmodel.Role.ToList();
                          string hf_content;
                          string icname = "疾控中心";//免疫点名称
                          string count = "0名";//未处理的人数
                          listR = listR.FindAll(x => x.RoleType != 0);//过滤疾控中心超级管理员的
                          if (listR.Count != 0)
                          {
                              DisImmuneCenterView icmodel = _IDisImmuneCenterDao.NGetModelById(pmodel.DisImmuneCenter);//通过免疫点ID查找免疫点信息
                              if (icmodel != null)
                              {
                                  icname = icmodel.Name;
                              }
                              hf_content = "您好，" + icname + "接种点,截至" + DateTime.Now.ToString("yyyy年MM月dd日") + ",还有" + count + "新入儿童未处理，请尽快处理！";
                          }
                          else
                          {
                              
                              hf_content = "您好，" + icname + ",截至" + DateTime.Now.ToString("yyyy年MM月dd日") + ",还有" + count + "新入儿童未处理，请尽快处理！";
                          }

                          result = RepayText(openId, myUserName, CreateTime, hf_content);
                      }
                      else
                      {
                          result = RepayText(openId, myUserName, CreateTime, "请回复关键字 “绑定”，进行帐号绑定后，再查询电箱上线收益数量！");
                      }
                  }
                  else if (content == "03") { //上线收益统计
                      if (_IWX_OpenIDDao.GetCount_byOpenId(openId,"0") != null)//判断是否绑定
                      {
                          IList<WX_Message> op_list = _IWX_MessageDao.GetWX_MessageList(content);
                          if ((op_list == null) || (op_list.Count <= 0))
                          {
                              op_list = _IWX_MessageDao.GetWX_MessageMRList();
                          }
                          if ((op_list == null) || (op_list.Count <= 0))
                          {
                              result = RepayText(openId, myUserName, CreateTime, "感谢关注疾控中心微信公众平台!");
                          }
                          else
                          {
                              int num = 0;
                              if (op_list.Count > 1)
                              {
                                  num = new Random(Guid.NewGuid().GetHashCode()).Next(0, op_list.Count);
                              }
                              WX_Message autoMessage = op_list[num];
                              if (autoMessage.MsgType == "text")
                              {
                                  result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                              }
                              if (autoMessage.MsgType == "news")
                              {
                                  List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                  listmodel = listmodel.FindAll(x => x.Title != null);
                                  //修改绑定的URL
                                  WX_Message_News B_listmodel = listmodel[0]; ;
                                  B_listmodel.Url = ym + "/Binding/SIDcheck?OpenId=" + openId;
                                  _IWX_Message_NewsDao.UpdateUrl(B_listmodel);
                                  result = RepayNews(openId, myUserName, listmodel);
                              }
                          }
                      }
                      else {
                          result = RepayText(openId, myUserName, CreateTime, "检测到该微信号尚未绑定电商平台帐号，请点击绑定帐号菜单进行帐号绑定！");
                      }
                    }
                    else
                    {
                        int num = 0;
                        if (list.Count > 1)
                        {
                            num = new Random(Guid.NewGuid().GetHashCode()).Next(0, list.Count);
                        }
                        WX_Message autoMessage = list[num];

                        if (autoMessage.MsgType == "text")
                        {
                            result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                        }
                        if (autoMessage.MsgType == "news")
                        {
                            List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                            listmodel = listmodel.FindAll(x => x.Title != null);
                            result = RepayNews(openId, myUserName, listmodel);
                        }
                    }
                }
            }
            else
            {//自定义菜单 
                switch (message.Type)
                {
                    case WeixinMessageType.Image:
                        result = RepayText(openId, myUserName, CreateTime, "你好！欢迎关注！");
                        break;
                    case WeixinMessageType.Location:
                        IList<WX_Message> L_list = _IWX_MessageDao.GetWX_MessageList("地理");
                        int n = 0;
                        if (L_list.Count > 1)
                        {
                            n = new Random(Guid.NewGuid().GetHashCode()).Next(0, L_list.Count);
                        }
                        WX_Message L_autoMessage = L_list[n];
                        List<WX_Message_News> l_listmodel = (L_autoMessage.wx_Message_News).ToList();
                        l_listmodel = l_listmodel.FindAll(x => x.Title != null);
                        //保存地图的地址URL
                        WX_Message_News L_model = l_listmodel[0];;
                        L_model.Url = "wx1.hininfo.com/GIS/index?Location_X=" + model.Location_X + "&Location_Y="+model.Location_Y;
                        _IWX_Message_NewsDao.UpdateUrl(L_model);
                        result = RepayNews(openId, myUserName, l_listmodel);
                        break;
                    case WeixinMessageType.Event:
                        string eventType = message.Body.Event.Value.ToLower();
                        log4net.LogManager.GetLogger("ApplicationInfoLog").Error("11111111tanjianyun：" + message.Body.EventKey);
                        switch (eventType)
                        {
                            case "subscribe":
                                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("首关数据：" + message.Body.EventKey);
                                content = message.Body.EventKey.Value;
                                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("首关数据3：" + content);
                                if (content =="")
                                {
                                    content = "subscribe";
                                }
                                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("首关数据3：" + content);
                                IList<WX_Message> list = _IWX_MessageDao.GetWX_MessageList(content);//通过关键字查找回复的信息
                                if ((list == null) || (list.Count <= 0))
                                {    
                                    list = _IWX_MessageDao.GetWX_MessageMRList();
                                }
                                if ((list == null) || (list.Count <= 0))
                                {
                                    result = RepayText(openId, myUserName, CreateTime, "欢迎订阅!");
                                }
                                else
                                {
                                    int num = 0;
                                    if (list.Count > 1)
                                    {
                                        num = new Random(Guid.NewGuid().GetHashCode()).Next(0, list.Count);
                                    }
                                    WX_Message autoMessage = list[num];

                                    if (autoMessage.MsgType == "text")
                                    {
                                        result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                    }
                                    if (autoMessage.MsgType == "news")
                                    {
                                        List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                        listmodel = listmodel.FindAll(x => x.Title != null);
                                        result = RepayNews(openId, myUserName, listmodel);
                                    }
                                }
                                break;
                            case "unsubscribe":
                                result = RepayText(openId, myUserName, CreateTime, "欢迎再来");
                                break;
                            case "scan":
                                //result = RepayText(openId, myUserName, CreateTime, "欢迎使用");
                                content = message.Body.EventKey.Value;
                                 IList<WX_Message> scanlist = _IWX_MessageDao.GetWX_MessageList(content);//通过关键字查找回复的信息
                                 if ((scanlist == null) || (scanlist.Count <= 0))
                                {  
                                    list = _IWX_MessageDao.GetWX_MessageMRList();
                                }
                                else if ((scanlist == null) || (scanlist.Count <= 0))
                                 {
                                     result = RepayText(openId, myUserName, CreateTime, "欢迎使用!");
                                 }
                                 else
                                 {
                                     int num = 0;
                                     if (scanlist.Count > 1)
                                     {
                                         num = new Random(Guid.NewGuid().GetHashCode()).Next(0, scanlist.Count);
                                     }
                                     WX_Message autoMessage = scanlist[num];

                                     if (autoMessage.MsgType == "text")
                                     {
                                         result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                     }
                                     if (autoMessage.MsgType == "news")
                                     {
                                         List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                         listmodel = listmodel.FindAll(x => x.Title != null);
                                         result = RepayNews(openId, myUserName, listmodel);
                                     }
                                }
                                break;
                            case "click":
                                content = message.Body.EventKey.Value;
                                if (content == "绑定")
                                {
                                    if (_IWX_OpenIDDao.GetCount_byOpenId(openId,"0") == null)//判断是否已经绑定
                                    {
                                        IList<WX_Message> op_list = _IWX_MessageDao.GetWX_MessageList(content);
                                        if ((op_list == null) || (op_list.Count <= 0))
                                        {
                                            op_list = _IWX_MessageDao.GetWX_MessageMRList();
                                        }
                                        if ((op_list == null) || (op_list.Count <= 0))
                                        {
                                            result = RepayText(openId, myUserName, CreateTime, "感谢关注新亚洲微信公众平台!");
                                        }
                                        else
                                        {
                                            int num = 0;
                                            if (op_list.Count > 1)
                                            {
                                                num = new Random(Guid.NewGuid().GetHashCode()).Next(0, op_list.Count);
                                            }
                                            WX_Message autoMessage = op_list[num];
                                            if (autoMessage.MsgType == "text")
                                            {
                                                result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                            }
                                            if (autoMessage.MsgType == "news")
                                            {
                                                List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                                listmodel = listmodel.FindAll(x => x.Title != null);
                                                //修改绑定的URL
                                                WX_Message_News B_listmodel = listmodel[0]; ;
                                                B_listmodel.Url = ym + "/Binding/Index?OpenId=" + openId;
                                                _IWX_Message_NewsDao.UpdateUrl(B_listmodel);
                                                result = RepayNews(openId, myUserName, listmodel);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        result = RepayText(openId, myUserName, CreateTime, "检测到该微信号已绑定电商平台帐号，请勿重新绑定！");
                                    }
                                }
                               else  if (content == "03")//菜单中点击 
                                {
                                    if (_IWX_OpenIDDao.GetCount_byOpenId(openId,"0") != null)//判断是否绑定
                                    {
                                        IList<WX_Message> op_list = _IWX_MessageDao.GetWX_MessageList(content);
                                        if ((op_list == null) || (op_list.Count <= 0))
                                        {
                                            op_list = _IWX_MessageDao.GetWX_MessageMRList();
                                        }
                                        if ((op_list == null) || (op_list.Count <= 0))
                                        {
                                            result = RepayText(openId, myUserName, CreateTime, "感谢关注新亚洲微信公众平台!");
                                        }
                                        else
                                        {
                                            int num = 0;
                                            if (op_list.Count > 1)
                                            {
                                                num = new Random(Guid.NewGuid().GetHashCode()).Next(0, op_list.Count);
                                            }
                                            WX_Message autoMessage = op_list[num];
                                            if (autoMessage.MsgType == "text")
                                            {
                                                result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                            }
                                            if (autoMessage.MsgType == "news")
                                            {
                                                List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                                listmodel = listmodel.FindAll(x => x.Title != null);
                                                //修改绑定的URL
                                                WX_Message_News B_listmodel = listmodel[0]; ;
                                                B_listmodel.Url = ym + "/Binding/SIDcheck?OpenId=" + openId;
                                                _IWX_Message_NewsDao.UpdateUrl(B_listmodel);
                                                result = RepayNews(openId, myUserName, listmodel);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        result = RepayText(openId, myUserName, CreateTime, "检测到该微信号尚未绑定电商平台帐号，请点击绑定帐号菜单进行帐号绑定！");
                                    }
                                }
                               else  
                                {
                                    IList<WX_Message> list2 = _IWX_MessageDao.GetWX_MessageList(content);//通过关键字查找回复的信息
                                    if ((list2 == null) || (list2.Count <= 0))
                                    {
                                        list2 = _IWX_MessageDao.GetWX_MessageMRList();
                                    }
                                    if ((list2 == null) || (list2.Count <= 0))
                                    {
                                        result = RepayText(openId, myUserName, CreateTime, "感谢关注苏州新亚微信公众平台!");
                                    }
                                    else
                                    {
                                        int num = 0;
                                        if (list2.Count > 1)
                                        {
                                            num = new Random(Guid.NewGuid().GetHashCode()).Next(0, list2.Count);
                                        }
                                        WX_Message autoMessage = list2[num];

                                        if (autoMessage.MsgType == "text")
                                        {
                                            result = RepayText(openId, myUserName, CreateTime, autoMessage.A_Content);
                                        }
                                        if (autoMessage.MsgType == "news")
                                        {
                                            List<WX_Message_News> listmodel = (autoMessage.wx_Message_News).ToList();
                                            listmodel = listmodel.FindAll(x => x.Title != null);
                                            result = RepayNews(openId, myUserName, listmodel);
                                        }
                                    }
                                }
                                 
                                break;
               
                        }
                        break;
                    default:
                        result = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                             "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                             "<CreateTime>{2}</CreateTime>" +
                                             "<MsgType><![CDATA[text]]></MsgType>" +
                                             "<Content><![CDATA[{3}]]></Content>" + "</xml>",
                                             openId, myUserName, DateTime.Now.ToBinary(), string.Format("未处理消息类型:{0}", myUserName));
                        break;
                }
            }
            return result;
        }

        private string RepayText(string toUserName, string fromUserName, string CreateTime, string content)
        {
            WX_SendMessageView sendmodel = new WX_SendMessageView();
            string xml = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                                   "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                                   "<CreateTime>{2}</CreateTime>" +
                                                   "<MsgType><![CDATA[text]]></MsgType>" +
                                                   "<Content><![CDATA[{3}]]></Content>" + "</xml>",
                                                   toUserName, fromUserName, CreateTime, content);
            sendmodel.ToUserName = toUserName;
            sendmodel.FromUserName = fromUserName;
            sendmodel.CreateTime = CreateTime;
            sendmodel.MsgType = "text";
            sendmodel.R_Content = content;
            sendmodel.S_XMLData = xml;
            _IWX_SendMessageDao.Ninsert(sendmodel);
            return xml;
        }

        private string Repayimage(string toUserName, string fromUserName, string content)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
"<FromUserName><![CDATA[{1}]]></FromUserName>" +
"<CreateTime>{2}</CreateTime>" +
"<MsgType><![CDATA[image]]></MsgType>" +
"<PicUrl><![CDATA[3]]></PicUrl>" +
"<MediaId><![CDATA[666666]]></MediaId>" +
"<MsgId>1234567890123456</MsgId>" + "</xml>", toUserName, fromUserName, DateTime.Now.ToBinary(), content);
        }
        private string RepayNews(string toUserName, string fromUserName, IList<WX_Message_News> news)
        {
            var couponesBuilder = new StringBuilder();
            couponesBuilder.Append(string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
            "<FromUserName><![CDATA[{1}]]></FromUserName>" +
            "<CreateTime>{2}</CreateTime>" +
            "<MsgType><![CDATA[news]]></MsgType>" +
            "<ArticleCount>{3}</ArticleCount><Articles>",
             toUserName, fromUserName,
             DateTime.Now.ToBinary(),
             news.Count
                ));
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("图文消息数量："+news.Count);
            foreach (var c in news)
            {
                string mnurl;
                if (c.Url == null)
                {
                     mnurl = ym+ "FirstBack/Details?Id=" + c.N_Id;
                }
                else
                {
                    mnurl = c.Url;
                }
                couponesBuilder.Append(string.Format("<item><Title><![CDATA[{0}]]></Title>" +
                    "<Description><![CDATA[{1}]]></Description>" +
                    "<PicUrl><![CDATA[{2}]]></PicUrl>" +
                    "<Url><![CDATA[{3}]]></Url>" +
                    "</item>",
                   c.Title, c.Description, ym+c.PicUrl, mnurl
                 ));
            }
            couponesBuilder.Append("</Articles></xml>");
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("图文消息图文：" + couponesBuilder.ToString());
            return couponesBuilder.ToString();
        }


        private string RepayE_gz(string toUserName, string fromUserName, string content)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
"<FromUserName><![CDATA[{1}]]></FromUserName>" +
"<CreateTime{2}</CreateTime>" +
"<MsgType><![CDATA[event]]></MsgType>" +
"<Event><![CDATA[subscribe]]></Event>" +
 "<Content><![CDATA[{4}]]></Content>" + "</xml>", toUserName, fromUserName, DateTime.Now.ToBinary(), content);
        }
    }

}