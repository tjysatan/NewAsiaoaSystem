using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IEP_jlinfoDao:IBaseDao<EP_jlinfoView>
    {
        PagerInfo<EP_jlinfoView> GetCinfoList(string Name, string lxrname, string RL_is, string Stratrldate, string Endrldate, string dyStratrldate, string dyEndrldate, string kdgs, string K3DDNO, SessionUser user);
        #region //根据多个Id查找多条打印记录根据答应寄件时间排序
        IList<EP_jlinfoView> GetlistIdEp_jl(string Id); 
        #endregion

        #region //根据DD_Id查询打印记录
        EP_jlinfoView GetEP_jlmodelbydd_Id(string dd_Id); 
        #endregion

         //导出数据查询
        IList<EP_jlinfoView> GetEP_JLINFExportJson(string Name, string RL_is, string Stratrldate, string Endrldate, string dyStratrldate, string dyEndrldate, string kdgs,string K3DDNO, SessionUser user);

                /// <summary>
        /// 根据寄件时间查询打印记录
        /// </summary>
        /// <param name="starttime">寄件时间</param>
        /// <param name="endtime">寄件时间</param>
        /// <param name="user">当前登录的角色</param>
        /// <returns></returns>
        IList<EP_jlinfoView> GetEP_JLinfobyjjtime(string starttime, string endtime, SessionUser user);

        
        #region //保存后返回ID
        string InsertID(EP_jlinfoView modelView); 
        #endregion
    }
}
