using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao 
{
    /// <summary>
    /// 收件人信息表数据接口
    /// </summary>
    public interface INA_AddresseeInfoDao:IBaseDao<NA_AddresseeInfoView>
    {
 
         #region //通过 收件人和电话查找是否有重复的收件人信息
        NA_AddresseeInfoView GetaddresseeinfobyAnametel(string aname, string tel, string Address);
	     #endregion

         #region //保存后返回ID
        string InsertID(NA_AddresseeInfoView modelView); 
        #endregion

        #region //根据 客户Id 查找收件人信息
        NA_AddresseeInfoView GetAddresseeinfobycustId(string custId); 
        #endregion
    }
}
