using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;
using NewAsiaOASystem.Dao;
using NewAsiaOASystem.DBModel;

namespace NewAsiaOASystem.Web.Helper
{
    public class WxGET : IHttpHandler
    {
        public static class SiteContainer
        {
            // Fields
            //public static CallSite<Action<CallSite, Type, object>> p__Site1;
            public static CallSite<Func<CallSite, object, object>> p__Site3;
           // public static CallSite<Action<CallSite, Type, object>> p__Site3;
            public static CallSite<Func<CallSite, object, int, object>> p__Site4;
        }

        IWX_ReceiveMessageDao _IWX_ReceiveMessageDao = ContextRegistry.GetContext().GetObject("WX_ReceiveMessageDao") as IWX_ReceiveMessageDao;
        IWX_SendMessageDao _IWX_SendMessageDao = ContextRegistry.GetContext().GetObject("WX_SendMessageDao") as IWX_SendMessageDao;
        IWX_MessageDao _IWX_MessageDao = ContextRegistry.GetContext().GetObject("WX_MessageDao") as IWX_MessageDao;
        public void ProcessRequest(HttpContext context)
        {
              context.Response.Clear();
              if (context.Request.QueryString["echostr"] != null)
              {
                  context.Response.Write(context.Request.QueryString["echostr"]);
              }
              else
              {
                  byte[] buffer = new byte[context.Request.InputStream.Length];
                  context.Request.InputStream.Read(buffer, 0, buffer.Length);
                  string text = Encoding.UTF8.GetString(buffer);
                  WX_ReceiveMessageView model = new WX_ReceiveMessageView();
                  dynamic obj2 = new DynamicXml (text);
               
                  model = (WX_ReceiveMessageView)_IWX_ReceiveMessageDao.FromDyToEntity(obj2, text);
                  _IWX_ReceiveMessageDao.Ninsert(model);
              
                  string content = model.R_Content;

                  string s = "";
               //   WX_SendMessageBLL ebll2 = new WX_SendMessageBLL();
                  switch (content)
                  {
                      case "bjyx":
                      case "搞笑":
                      case "笑话":
                          {
                              string url = "http://api100.duapp.com/joke/";
                              string contnet = Tools.SubmitToUrl(url, "", "POST");
                              if (contnet != string.Empty)
                              {
                                  contnet = contnet.Replace("\"", "");
                                  model.MsgType = "text";
                                  s = _IWX_SendMessageDao.ResponseTextMessage(model, contnet);
                                  context.Response.Write(s);
                                  return;
                              }
                              break;
                          }
                  }
                  IList<WX_Message> list = _IWX_MessageDao.GetWX_MessageList(content);
                  if ((list == null) || (list.Count <= 0))
                  {
                      list = _IWX_MessageDao.GetWX_MessageMRList();
                  }
                  if ((list == null) || (list.Count <= 0))
                  {
                      s = _IWX_SendMessageDao.ResponseTextMessage(model, "你好，欢迎关注我们！");
                      context.Response.Write(s);
                  }
                  else
                  {
                      int num = 0;
                      if (list.Count > 1)
                      {
                          num = new Random(Guid.NewGuid().GetHashCode()).Next(0, list.Count);
                      }
                      WX_Message autoMaticMessage = list[num];
                      s = _IWX_SendMessageDao.ResponseNewsMessage(autoMaticMessage,model);
                      context.Response.Write(s);
                  }

              }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}