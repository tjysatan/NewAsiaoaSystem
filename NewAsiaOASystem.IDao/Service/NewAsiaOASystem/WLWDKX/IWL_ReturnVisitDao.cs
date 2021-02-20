using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_ReturnVisitDao:IBaseDao<_20150510WL_ReturnVisitView>
    {
        /// <summary>
        /// 查找回访记录
        /// </summary>
        /// <param name="Sid"></param>
        /// <param name="Rvtype"></param>
        /// <returns></returns>
        _20150510WL_ReturnVisitView GetModelbySidrvtype(string Sid, string Rvtype);
    }
}
