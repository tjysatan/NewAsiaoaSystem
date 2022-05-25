using NewAsiaOASystem.IDao;
using NewAsiaOASystem.Web.Helper;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.Dao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.Web
{
    public class WeixinController : Controller
    {
      // IWX_ReceiveMessageDao _IWX_ReceiveMessageDao = ContextRegistry.GetContext().GetObject("WX_ReceiveMessageDao") as IWX_ReceiveMessageDao;
      // IWX_SendMessageDao _IWX_SendMessageDao = ContextRegistry.GetContext().GetObject("WX_SendMessageDao") as IWX_SendMessageDao;
      // IWX_MessageDao _IWX_MessageDao = ContextRegistry.GetContext().GetObject("WX_MessageDao") as IWX_MessageDao;
        //
        // GET: /Weixin/
        public WeixinController()
        {

        }
        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url
        /// </summary>
        //[HttpGet]
        //[ActionName("Index")]
        //public ActionResult Get(string echostr)
        //{
        //    return Content(echostr);
        //}

        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {

            return Content(echostr);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post()
        {
            WeixinMessage message = null;
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                message = AcceptMessageAPI.Parse(streamReader.ReadToEnd());
            }
            var response = new WeixinExecutor().Execute(message);
            return new ContentResult
            {
                Content = response,
                ContentType = "text/xml",
                ContentEncoding = System.Text.UTF8Encoding.UTF8
            };

        }

        //[HttpPost]
        //[ActionName("Index")]
        //public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
        //{
        //    Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
        //    byte[] requestByte = new byte[requestStream.Length];
        //    requestStream.Read(requestByte, 0, (int)requestStream.Length);
        //    string requestStr = Encoding.UTF8.GetString(requestByte);

        //    return new ContentResult
        //    {
        //        Content = requestStr,
        //        ContentType = "text/xml",
        //        ContentEncoding = System.Text.UTF8Encoding.UTF8
        //    };



        //}
    }
}

   
