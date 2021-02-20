using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.IDao
{
    public interface IVote_ipDao : IBaseDao<Vote_ipView>
    {
        #region //根据主题ID 和IP 检查该IP 是否重复提交
        bool JcIP(string sid, string IP); 
        #endregion


        #region //根据主题ID 和IP  查询实体
        Vote_ipView JcIPmodel(string sid, string IP); 
        #endregion
    }
}
