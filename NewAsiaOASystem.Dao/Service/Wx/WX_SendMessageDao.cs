using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
    public class WX_SendMessageDao:ServiceConversion<WX_SendMessageView,WX_SendMessage>,IWX_SendMessageDao
    {
      

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_SendMessageView model)
        {
            WX_SendMessage listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_SendMessageView model)
        {
            WX_SendMessage listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_SendMessageView model)
        {
            WX_SendMessage listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_SendMessageView> model)
        {
            IList<WX_SendMessage> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_SendMessageView> NGetList()
        {
            List<WX_SendMessage> listdata = base.GetList() as List<WX_SendMessage>;
            IList<WX_SendMessageView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_SendMessageView> NGetList_id(string id)
        {
            List<WX_SendMessage> list = base.GetList_id(id) as List<WX_SendMessage>;
            IList<WX_SendMessageView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_SendMessage> NGetListID(string id)
        {
            IList<WX_SendMessage> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_SendMessageView NGetModelById(string id)
        {
            WX_SendMessageView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public string ResponseTextMessage(WX_ReceiveMessageView model, string contnet)
        {
            StringBuilder builder = new StringBuilder();
            WX_SendMessageView message = new WX_SendMessageView
            {
                ToUserName = model.FromUserName,
                FromUserName = model.ToUserName,
                CreateTime = Tools.GetUnixTimeForDateTimeNow(DateTime.Now.ToString()),
                MsgType = model.MsgType,
             //   Content = contnet
                R_Content=contnet
            };
            builder.Append("<xml>");
            builder.Append("<ToUserName><![CDATA[" + message.ToUserName + "]]></ToUserName>");
            builder.Append("<FromUserName><![CDATA[" + message.FromUserName + "]]></FromUserName>");
            builder.Append("<CreateTime>" + message.CreateTime + "</CreateTime>");
            builder.Append("<MsgType><![CDATA[" + message.MsgType + "]]></MsgType>");
            builder.Append("<Content><![CDATA[" + message.R_Content + "]]></Content>");
            builder.Append("</xml>");
            message.S_XMLData = builder.ToString();
            this.Ninsert(message);
            return builder.ToString();
        }



        public string ResponseNewsMessage(WX_Message AutoMaticMessage, WX_ReceiveMessageView model)
        {
            StringBuilder builder = new StringBuilder();
            WX_SendMessageView message = new WX_SendMessageView
            {
                ToUserName = model.FromUserName,
                FromUserName = model.ToUserName,
                CreateTime = Tools.GetUnixTimeForDateTimeNow(DateTime.Now.ToString()),
                MsgType = AutoMaticMessage.MsgType
            };
            builder.Append("<xml>");
            builder.Append("<ToUserName><![CDATA[" + message.ToUserName + "]]></ToUserName>");
            builder.Append("<FromUserName><![CDATA[" + message.FromUserName + "]]></FromUserName>");
            builder.Append("<CreateTime>" + message.CreateTime + "</CreateTime>");
            builder.Append("<MsgType><![CDATA[" + AutoMaticMessage.MsgType + "]]></MsgType>");
            if (AutoMaticMessage.MsgType == "news")
            {
                int num = 0;
                StringBuilder builder2 = new StringBuilder();
                if (AutoMaticMessage.wx_Message_News.Count != 0)
                {
                    builder2.Append("<Articles>");
                    foreach (WX_Message_News news in AutoMaticMessage.wx_Message_News)
                    {
                        if ((news.Title != null) && !(news.Title == ""))
                        {
                            num++;
                            builder2.Append("<item>");
                            builder2.Append("<Title><![CDATA[" + news.Title + "]]></Title>");
                            if (news.Description != "")
                            {
                                builder2.Append("<Description><![CDATA[" + news.Description + "]]></Description>");
                            }
                            builder2.Append("<PicUrl><![CDATA[" + news.PicUrl + "]]></PicUrl>");
                            builder2.Append("<Url><![CDATA[" + news.Url + "]]></Url>");
                            builder2.Append("</item>");
                        }
                    }
                    builder2.Append("</Articles>");
                }
                builder.Append("<ArticleCount><![CDATA[" + num + "]]></ArticleCount>");
                builder.Append(builder2.ToString());
            }
            if (AutoMaticMessage.MsgType == "text")
            {
                builder.Append("<Content><![CDATA[" + AutoMaticMessage.A_Content + "]]></Content>");
            }
            builder.Append("</xml>");
            message.S_XMLData = builder.ToString();
            //new WX_SendMessageBLL().InsertWX_SendMessage(message);
            Ninsert(message);
            return builder.ToString();
        }
    }
}
