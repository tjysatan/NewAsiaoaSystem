using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_dkxthjlinfoDao:IBaseDao<WL_dkxthjlinfoView>
    {
        #region //查询当天扫码退货的数据
        IList<WL_dkxthjlinfoView> GetsmthdataToday(); 
        #endregion


        #region //SID退货历史记录
        /// <summary>
        /// SID退货历史记录
        /// </summary>
        /// <param name="jxsname">经销商名称</param>
        /// <param name="startTHdatetime">退货时间</param>
        /// <param name="endTHdatetime"></param>
        /// <returns></returns>
        PagerInfo<WL_dkxthjlinfoView> GetWL_dkxthlistPage(string SID, string jxsname, string startTHdatetime, string endTHdatetime); 
        #endregion

        #region //根据经销商Id查询出sid退货的数量
        int jxqGetTHsidbyjxsId(string jxsId); 
        #endregion
    }
}
