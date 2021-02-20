using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_DDCLyqNoteInfoDao:IBaseDao<DKX_DDCLyqNoteInfoView>
    {
        #region //查询今天是否有该类型的提醒数据
        /// <summary>
        /// 查询今天是否有该类型的提醒数据
        /// </summary>
        /// <param name="type">提醒类型</param>
        /// <param name="fstype">发送时段 0 上午 1 下午</param>
        /// <returns></returns>
        bool JCFSshujubytypeandfstypw(string type, string fstype); 
        #endregion


        #region //逾期处理提醒通知的数据分页列表
        /// <summary>
        /// 逾期处理提醒通知的数据分页列表
        /// </summary>
        /// <param name="DD_Bianhao">订单号</param>
        /// <param name="khname">客户名称</param>
        /// <param name="yqtype">逾期类型</param>
        /// <param name="jsname">通知人</param>
        /// <param name="tztimetype">发送时间断 0 上网 1下午</param>
        /// <param name="starttime">通知时间</param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        PagerInfo<DKX_DDCLyqNoteInfoView> GetddclyqNotedataPagerlist(string DD_Bianhao, string khname, string yqtype, string jsname, string tztimetype, string starttime, string endtime); 
        #endregion

        #region //根据订单Id查询逾期通知数据
        /// <summary>
        /// 根据订单Id查询逾期通知数据
        /// </summary>
        /// <param name="DDid">订单Id</param>
        /// <returns></returns>
        IList<DKX_DDCLyqNoteInfoView> GetyqtzinfobyddId(string DDid); 
        #endregion
    }
}
