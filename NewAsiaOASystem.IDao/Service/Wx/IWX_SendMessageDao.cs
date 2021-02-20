using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWX_SendMessageDao:IBaseDao<WX_SendMessageView>
    {
        #region //回复消息实体转化成 xml 并保存回复的消息
        string ResponseTextMessage(WX_ReceiveMessageView model, string contnet); 
        #endregion

        string ResponseNewsMessage(WX_Message AutoMaticMessage, WX_ReceiveMessageView model);
    }
}
