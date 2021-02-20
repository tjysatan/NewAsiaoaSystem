using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IKS_ProblemTyleDao:IBaseDao<KS_ProblemTyleView>
    {
        #region //客诉问题分类的分页数据
        /// <summary>
        /// 客诉问题分类的分页数据
        /// </summary>
        /// <param name="WTname">问题分类名称</param>
        /// <param name="start"></param>
        /// <returns></returns>
        PagerInfo<KS_ProblemTyleView> KSGetProblemTylePagerlist(string WTname, string start); 
        #endregion
    }
}
