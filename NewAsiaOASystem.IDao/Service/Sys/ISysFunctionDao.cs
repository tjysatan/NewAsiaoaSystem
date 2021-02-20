using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace  NewAsiaOASystem.IDao
{
    public interface ISysFunctionDao : IBaseDao<SysFunctionView>
    {
        string NGetListJson();
        IList<SysFunction> NGetListModel(string id);
        bool CheckAction(string action);
        /// <summary>
        /// 根据用户ID 获取用户角色对应的 功能菜单
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        List<string> GetRole_Fun(string Id);
    }
}
