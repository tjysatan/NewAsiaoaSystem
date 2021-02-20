using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IIQC_llNoticeordinfoDao:IBaseDao<IQC_llNoticeordinfoView>
    {
        #region //根据K3 来料通知单单号查询该单数据
        /// <summary>
        /// 根据K3 来料通知单单号查询该单数据
        /// </summary>
        /// <param name="ddno">订单单号</param>
        /// <returns></returns>
        IQC_llNoticeordinfoView GetllNoticeinfobyddno(string ddno); 
        #endregion

        #region //根据K3 自增Id查询排序查询最大的一条数据
        /// <summary>
        /// 根据K3 自增Id查询排序查询最大的一条数据
        /// </summary>
        /// <returns></returns>
        IQC_llNoticeordinfoView GetllNoticeinfoorderbyddId(); 
        #endregion

        #region //保存后返回ID
        string InsertID(IQC_llNoticeordinfoView modelView); 
        #endregion
    }
}
