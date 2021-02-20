using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWeChat_OrderInfoDao:IBaseDao<WeChat_OrderInfoView>
    {
         
        string InsertID(WeChat_OrderInfoView modelView);

        int Getchengjiaoshuli(string type, string OpenId);

            //好友最新成交记录
        IList<WeChat_OrderInfoView> Getcjjltop(string OpenId);
    }
}
