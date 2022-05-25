using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface Ilanhe_QzCusinfoDao : IBaseDao<lanhe_QzCusinfoView>
    { 
        #region //根据电话号码查询是否存在该手机号码的记录
        /// <summary>
        /// 存在记录 返回false 不存在记录返回 true
        /// </summary>
        /// <param name="tel">联系电话</param>
        /// <returns></returns>
        bool checktelIscf(string tel); 
        #endregion
    }
}
