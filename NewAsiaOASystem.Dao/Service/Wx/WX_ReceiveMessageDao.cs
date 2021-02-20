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
using System.Runtime.CompilerServices;

namespace NewAsiaOASystem.Dao
{
    public class WX_ReceiveMessageDao : ServiceConversion<WX_ReceiveMessageView, WX_ReceiveMessage>, IWX_ReceiveMessageDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WX_ReceiveMessageView GetView(WX_ReceiveMessage data)
        {
            WX_ReceiveMessageView view = ConvertToDTO(data);

            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_ReceiveMessageView model)
        {
            WX_ReceiveMessage listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_ReceiveMessageView model)
        {
            WX_ReceiveMessage listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_ReceiveMessageView model)
        {
            WX_ReceiveMessage listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_ReceiveMessageView> model)
        {
            IList<WX_ReceiveMessage> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_ReceiveMessageView> NGetList()
        {
            List<WX_ReceiveMessage> listdata = base.GetList() as List<WX_ReceiveMessage>;
            IList<WX_ReceiveMessageView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_ReceiveMessageView> NGetList_id(string id)
        {
            List<WX_ReceiveMessage> list = base.GetList_id(id) as List<WX_ReceiveMessage>;
            IList<WX_ReceiveMessageView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_ReceiveMessage> NGetListID(string id)
        {
            IList<WX_ReceiveMessage> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_ReceiveMessageView NGetModelById(string id)
        {
            WX_ReceiveMessageView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 微信POST过来的xml数据转换为我们的实体
        /// </summary>
        /// <param name="dy"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public WX_ReceiveMessageView FromDyToEntity(object dy, string xml)
        {
            WX_ReceiveMessageView messageView = new WX_ReceiveMessageView();
            string str = (string)((dynamic)dy).ToUserName.Value;
            string str2 = (string)((dynamic)dy).FromUserName.Value;
            DateTime time = (DateTime)Tools.GetDateTimeForUnix(((dynamic)dy).CreateTime.Value);
            string str3 = (string)((dynamic)dy).MsgType.Value;
            try
            {
                messageView.MsgId = (string)((dynamic)dy).MsgId.Value;
            }
            catch
            {
            }
            try
            {
                messageView.PicUrl = (string)((dynamic)dy).PicUrl.Value;
            }
            catch
            {
            }
            try
            {
                if (str3 == "event")
                {
                    messageView.R_Content = (string)((dynamic)dy).EventKey.Value;
                }
                else
                {
                    messageView.R_Content = (string)((dynamic)dy).Content.Value;
                }
            }
            catch
            {
            }
            try
            {
                messageView.MediaId = (string)((dynamic)dy).MediaId.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Format = (string)((dynamic)dy).Format.Value;
            }
            catch
            {
            }
            try
            {
                messageView.ThumbMediaId = (string)((dynamic)dy).ThumbMediaId.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Location_X = (string)((dynamic)dy).Location_X.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Location_Y = (string)((dynamic)dy).Location_Y.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Scale = (int)((dynamic)dy).Scale.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Labe = (string)((dynamic)dy).Label.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Title = (string)((dynamic)dy).Title.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Url = (string)((dynamic)dy).Url.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Description = (string)((dynamic)dy).Description.Value;
            }
            catch
            {
            }
            messageView.ToUserName = str;
            messageView.FromUserName = str2;
            messageView.CreateTime = time;
            messageView.MsgType = str3;
            messageView.R_CreateTime = DateTime.Now;
            messageView.R_XMLData = xml;
          //  WX_ReceiveMessageView listmodel = GetView(base.GetModelById(id));
            return messageView;
        }

        /// <summary>
        /// 微信POST过来的xml数据转换为我们的实体
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public WX_ReceiveMessageView FromDytoModel(WeixinMessage message)
        {
            WX_ReceiveMessageView messageView = new WX_ReceiveMessageView();
            string str =message.Body.ToUserName.Value;
            string str2 = message.Body.FromUserName.Value;
            DateTime time =Tools.GetDateTimeForUnix(message.Body.CreateTime.Value);
            string str3 = message.Body.MsgType.Value;
            try
            {
                messageView.MsgId = message.Body.MsgId.Value;
            }
            catch
            {
            }
            try
            {
                messageView.PicUrl = message.Body.PicUrl.Value;
            }
            catch
            {
            }
            try
            {
                messageView.R_Content = message.Body.Content.Value;
            }
            catch
            {
            }
            
            try
            {
                messageView.MediaId = message.Body.MediaId.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Format = message.Body.Format.Value;
            }
            catch
            {
            }
            try
            {
                messageView.ThumbMediaId = message.Body.ThumbMediaId.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Location_X = message.Body.Location_X.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Location_Y = message.Body.Location_Y.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Scale = message.Body.Scale.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Labe = message.Body.Label.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Title = message.Body.Title.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Url = message.Body.Url.Value;
            }
            catch
            {
            }
            try
            {
                messageView.Description = message.Body.Description.Value;
            }
            catch
            {
            }
            messageView.ToUserName = str;
            messageView.FromUserName = str2;
            messageView.CreateTime = time;
            messageView.MsgType = str3;
            messageView.R_CreateTime = DateTime.Now;
           // messageView.R_XMLData = xml;
            //  WX_ReceiveMessageView listmodel = GetView(base.GetModelById(id));
            return messageView;
           
        }





    }
}
