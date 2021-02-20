using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_CHdetailinfoDao:IBaseDao<NQ_CHdetailinfoView>
    {
        #region //根据返退货流程Id 查询出货产品明细
        IList<NQ_CHdetailinfoView> Getchinfoby_rid(string R_Id);
	    #endregion

             /// <summary>
        /// 检测出货单中是否存在相同的处理方式的相同产品 如果存在就 返回false 如果不存在就返回true
        /// </summary>
        /// <param name="r_Id">返退货单子Id</param>
        /// <param name="cp_Id">产品Id</param>
        /// <param name="clfs">处理方式</param>
        /// <returns></returns>
        bool JxCHcpbyridandcpidandclfs(string r_Id, string cp_Id, string clfs);

                /// <summary>
        /// 根据返退货Id产品Id处理方式 查找退货单产品信息
        /// </summary>
        /// <param name="r_Id">返退Id</param>
        /// <param name="cp_Id">产品Id</param>
        /// <param name="clfs">处理方式</param>
        /// <returns></returns>
        NQ_CHdetailinfoView GetCHdetailinfobyr_IdcpIdclfs(string r_Id, string cp_Id, string clfs);
    }
}
