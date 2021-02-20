using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Runtime.CompilerServices;
using NewAsiaOASystem.IDao;
namespace NewAsiaOASystem.IDao
{
    public interface IWX_ReceiveMessageDao : IBaseDao<WX_ReceiveMessageView>
    {
      #region //把微信post过来的xml转换成我们的实体
        WX_ReceiveMessageView FromDyToEntity(object dy, string xml);
	#endregion

        WX_ReceiveMessageView FromDytoModel(WeixinMessage message);
    }
}
