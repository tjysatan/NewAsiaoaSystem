using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IIQC_llNoticeMXordinfoDao:IBaseDao<IQC_llNoticeMXordinfoView>
    {
        #region //分页列表
        /// <summary>
        /// 同步的数据分页数据
        /// </summary>
        /// <param name="DDNO">来料检验通知单单号</param>
        /// <param name="YJQWL">元器件物联代码</param>
        /// <param name="YQJname">元器件名称</param>
        /// <param name="gyswl">供应商的物联代码</param>
        /// <param name="gysname">供应商名称</param>
        /// <returns></returns>
        PagerInfo<IQC_llNoticeMXordinfoView> GetllNoticelistPager(string DDNO, string YJQWL, string YQJname, string gyswl, string gysname, string Isscjyd); 
        #endregion

         
        #region //通过送检单号和元器件物料代码查询明细信息
        /// <summary>
        /// 通过送检单号和元器件物料代码查询明细信息
        /// </summary>
        /// <param name="ddno">送检单号</param>
        /// <param name="wldm">物料代码</param>
        /// <returns></returns>
        IQC_llNoticeMXordinfoView GetllnotceMXbysjdhandwldm(string ddno, string wldm); 
        #endregion


        #region //通过送检单号查询明细
        /// <summary>
        /// 通过送检单号查询明细
        /// </summary>
        /// <param name="ddno">送检单号</param>
        /// <returns></returns>
        IList<IQC_llNoticeMXordinfoView> Getsjdlistmodelbyddno(string ddno); 
        #endregion
    }
}
