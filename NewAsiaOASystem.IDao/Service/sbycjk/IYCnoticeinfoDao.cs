using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IYCnoticeinfoDao:IBaseDao<YCnoticeinfoView>
    {
        #region //当天发送的数量
        decimal GetTodayFXsum(string openId);
        #endregion

        #region //近五天内的告警数据
        /// <summary>
        /// 近五天内的告警数据
        /// </summary>
        /// <param name="openId">微信openId</param>
        /// <returns></returns>
        IList<YCnoticeinfoView> GetwutianFxnoticeinfo(string openId);
        #endregion
    }
}
