using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INA_XSinfoDao:IBaseDao<NA_XSinfoView>
    {
        #region //销售订单的分页数据
        PagerInfo<NA_XSinfoView> GetXSinfoList(string Name, string Issm, string sm_zt, SessionUser user);
        #endregion

        #region //保存后返回ID
        string InsertID(NA_XSinfoView modelView); 
        #endregion

         #region //检测订单是否存在重复的
        bool jccfbySc_Id(string Sc_Id); 
        #endregion

        #region //根据订单编号查找订单数据
        NA_XSinfoView GetxsinfobyOrderCode(string ordercode); 
        #endregion

        #region //查找最新一条的销售订单
        NA_XSinfoView GetxsinfoNewdate(); 
        #endregion

        #region //根据SORT 电商平台订单ID 查找订单信息
        NA_XSinfoView GetxsInfobySort(string Sort); 
	    #endregion
    }
}
