using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IFlow_PlanProductioninfoDao:IBaseDao<Flow_PlanProductioninfoView>
    {
         /// <summary>
        /// 生产计划单管理
        /// </summary>
        /// <param name="CPName">产品名称</param>
        /// <param name="P_Pianhao">产品编号</param>
        /// <param name="P_Issc">状态 0 生产中 1 缺料 2待生产 3完成</param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        PagerInfo<Flow_PlanProductioninfoView> GetCinfoList(string CPName, string P_Pianhao, string P_Issc, string starttime, string endtime,string p_type);

        string InsertID(Flow_PlanProductioninfoView modelView);

        /// <summary>
        /// 查询当天生产通知单的条数
        /// </summary>
        /// <returns></returns>
        int GetPPcount();

        #region //电控箱分页数据
        /// <summary>
        /// 电控箱分页数据
        /// </summary>
        /// <param name="CPName">产品名称</param>
        /// <param name="P_Pianhao">产品物料代理</param>
        /// <param name="P_Issc">状态 0 生产中 1 缺料 2 待生产 3 完成</param>
        /// <param name="starttime">下单时间</param>
        /// <param name="endtime"></param>
        /// <param name="p_type">订单类型 4 常规电控箱</param>
        /// <returns></returns>
        PagerInfo<Flow_PlanProductioninfoView> DKXGetPDATAPagerlist(string CPName, string P_Pianhao, string P_Issc, string starttime, string endtime, string p_type,string scbianhao); 
        #endregion

        #region //返回当天的常规电控箱的下单数量
        /// <summary>
        /// 返回当天的电控箱的下单数量
        /// </summary>
        /// <returns></returns>
        int GetDKXcount(); 
        #endregion

    }
}
