using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    /// <summary>
    /// 客诉处理人员信息 数据处理接口
    /// </summary>
    public interface IKS_CLPersonnelInfoDao:IBaseDao<KS_CLPersonnelInfoView>
    {
        #region //客诉处理人员的分页数据
        /// <summary>
        /// 客诉处理人员的分页数据
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="zhname">帐号</param>
        /// <param name="tel">电话</param>
        /// <param name="start">启用状态</param>
        /// <returns></returns>
        PagerInfo<KS_CLPersonnelInfoView> KSGetCLRinfoPagerlist(string name, string zhname, string tel, string start); 
        #endregion
    }
}
