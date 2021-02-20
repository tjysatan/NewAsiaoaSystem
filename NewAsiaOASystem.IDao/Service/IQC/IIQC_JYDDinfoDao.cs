using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IIQC_JYDDinfoDao:IBaseDao<IQC_JYDDinfoView>
    {
        #region //通过通知单明细Id 查询检验单
        /// <summary>
        /// 通过通知单明细Id 查询检验单
        /// </summary>
        /// <param name="Id">通知单明细Id</param>
        /// <returns></returns>
        IQC_JYDDinfoView GetJYDinfobymxId(string Id); 
        #endregion

        #region //检验测试单分页数据
        /// <summary>
        /// 检验测试单分页数据
        /// </summary>
        /// <param name="JYDDno">检验单单号</param>
        /// <param name="gysname">供应商</param>
        /// <param name="yqjname">元器件</param>
        /// <param name="yqjxh">型号</param>
        /// <param name="clzt">处理状态</param>
        /// <param name="cljg">处理结果</param>
        /// <returns></returns>
        PagerInfo<IQC_JYDDinfoView> GetJYDDlistPager(string JYDDno, string gysname, string yqjname, string yqjxh, string clzt, string cljg, string shstrattime, string shendtime);
        #endregion

        #region //返回当日检验单的数量
        /// <summary>
        /// 返回当日检验单的数量
        /// </summary>
        /// <returns></returns>
        int GetJYDcount(); 
        #endregion

        #region //按照审核时间查询审核完成 的检验测试单数据
        /// <summary>
        /// 按照审核时间查询审核完成 的检验测试单数据
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        IList<IQC_JYDDinfoView> GetSHTGDATAbyshtime(string starttime, string endtime); 
        #endregion
    }
}
