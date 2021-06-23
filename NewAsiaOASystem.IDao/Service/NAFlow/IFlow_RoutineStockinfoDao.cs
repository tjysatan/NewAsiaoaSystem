using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IFlow_RoutineStockinfoDao:IBaseDao<Flow_RoutineStockinfoView>
    {
        /// <summary>
        /// 常规产品的库存情况数据
        /// </summary>
        /// <param name="Sort"></param>
        /// <returns></returns>
        IList<Flow_RoutineStockinfoView> GetxsinfobyOrderCode(string Sort, string Category, string cpname, string wlSort);

         //通过物料代码查询常规产品的信息
        Flow_RoutineStockinfoView Getmodelinfobyp_bianhao(string p_bianhao);

          /// <summary>
        /// 根据 是否被锁定的插好 产品的库存信息
        /// </summary>
        /// <param name="Isscing"></param>
        /// <returns></returns>
        IList<Flow_RoutineStockinfoView> GetCpkcInfo(string Isscing);


        #region //常规电控箱
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sort">告警数量排序 0 倒序 1正序</param>
        /// <param name="Category">0 成品 1半成品</param>
        /// <param name="cpname">产品名称</param>
        /// <param name="wlSort">物料代码排序</param>
        /// <returns></returns>
        IList<Flow_RoutineStockinfoView> DKGetcgDATAinfo(string Sort, string Category, string cpname, string wlSort); 


        /// <summary>
        /// 常规电控箱不在生产的中的数据
        /// </summary>
        /// <param name="Isscing">是否锁定 0 未锁定 1锁定</param>
        /// <returns></returns>
        IList<Flow_RoutineStockinfoView> DKXGetCpkcInfo(string Isscing);
        #endregion




        #region //常规电控箱的分页数据
        /// <summary>
        /// 常规电控温控的分页数据
        /// </summary>
        /// <param name="cpname">产品名称</param>
        /// <param name="wlno">物料号</param>
        /// <param name="type">类型 0 温控 1电控</param>
        /// <returns></returns>
        PagerInfo<Flow_RoutineStockinfoView> Getcgdiankongpagerlist(string cpname, string wlno, string type, string category); 
        #endregion
    }
}
