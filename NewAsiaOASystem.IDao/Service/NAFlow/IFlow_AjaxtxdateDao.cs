using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IFlow_AjaxtxdateDao:IBaseDao<Flow_AjaxtxdateView>
    {
        /// <summary>
        /// 查询需要提醒的通知数据
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="tzdtype"></param>
        /// <returns></returns>
        IList<Flow_AjaxtxdateView> GetWTZajaxtxdate(string Type, string tzdtype);
    }
}
