using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    /// <summary>
    /// 普实erp 销售出货数据
    /// </summary>
    public interface IERP_SASalAinfoDao:IBaseDao<ERP_SASalAinfoView>
    {
 
        #region //通过销售日期 例如 202110 查询本地获取的erp中的销售出货数据
        /// <summary>
        /// 通过销售日期 例如 202110 查询本地获取的erp中的销售出货数据
        /// </summary>
        /// <param name="AbsID">日期 例如202110</param>
        /// <returns></returns>
        IList<ERP_SASalAinfoView> GetalldatabyAbsID(string AbsID);
        #endregion

        
        #region //分页数据
        /// <summary>
        /// 销售出货的分页数据
        /// </summary>
        /// <param name="AbsID">日期</param>
        /// <param name="CrdName">客户名称</param>
        /// <param name="ItmID">物料代码</param>
        /// <param name="ItmName">物料名称</param>
        /// <param name="ItmSpec">物料型号</param>
        /// <returns></returns>
        PagerInfo<ERP_SASalAinfoView> GetSASalAPagerInfo(string AbsID, string CrdName, string ItmID, string ItmName, string ItmSpec);
        #endregion

        
        #region //责任工程师分组汇总数据
        /// <summary>
        /// 责任工程师分组汇总数据
        /// </summary>
        /// <param name="AbsID">日期</param>
        /// <returns></returns>
        List<Object> GetsumjineGROUPBY(string AbsID);
        #endregion

       
        #region //按照账期查询非标产品（已经同步过来的数据）
        /// <summary>
        /// 按照账期查询非标产品（已经同步过来的数据）
        /// </summary>
        /// <param name="AbsID"></param>
        /// <returns></returns>
        IList<ERP_SASalAinfoView> GetFB_DATA_BY_AbsID(string AbsID);
        #endregion

        
        #region //当月每天的ERP发货金额
        /// <summary>
        /// 当月每天的发货金额汇总
        /// </summary>
        /// <returns></returns>
        List<Object> Get_TheMonth_DailySales(); 
        #endregion
    }
}
