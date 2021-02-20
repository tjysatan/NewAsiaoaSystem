using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_PleasepurchaseinfoDao:IBaseDao<DKX_PleasepurchaseinfoView>
    {
        #region //根据元器件的编码查询各个状态下的采购单
        /// <summary>
        /// 根据元器件的编码查询各个状态下的采购单
        /// </summary>
        /// <param name="wlbm">物料编码</param>
        /// <param name="type">请购单状态 0 未采购 1 采购中 2 完成</param>
        /// <returns></returns>
        DKX_PleasepurchaseinfoView GetYQJQgdanDATAbyyqjbm(string wlbm, string type); 
        #endregion

        #region //采购单分页列表（生产）
        /// <summary>
        /// 采购单分页列表（生产）
        /// </summary>
        /// <param name="yqjbm">元器件编码</param>
        /// <param name="yqjnam">元器件名称</param>
        /// <param name="yqjxh">元器件型号</param>
        /// <param name="type">采购单当前的状态 0 未采购 1采购中 2 完成</param>
        /// <param name="starttime">下单时间</param>
        /// <param name="endtime"></param>
        /// <param name="user">当前登录的帐号</param>
        /// <returns></returns>
        PagerInfo<DKX_PleasepurchaseinfoView> GetDKX_SCcgdpagelist(string yqjbm, string yqjnam, string yqjxh, string type, string starttime,
            string endtime, SessionUser user); 
        #endregion
    }
}
