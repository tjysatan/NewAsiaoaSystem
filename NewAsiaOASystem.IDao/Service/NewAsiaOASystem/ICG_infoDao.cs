using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;


namespace NewAsiaOASystem.IDao
{
    public interface ICG_infoDao:IBaseDao<CG_infoView>
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="RL_is"></param>
        /// <param name="Stratrldate"></param>
        /// <param name="Endrldate"></param>
        /// <param name="user"></param>
        /// <returns></returns>
         PagerInfo<CG_infoView> GetCginfoList(string Name, string RL_is, string Stratrldate, string Endrldate, SessionUser user);

         #region //保存后返回ID
         string InsertID(CG_infoView modelView); 
         #endregion
    }
}
