using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;


namespace NewAsiaOASystem.IDao
{
   public interface  INAReturnListDao:IBaseDao<NAReturnListView>
    {
       PagerInfo<NAReturnListView> GetCinfoList(string Name, string Szt, string type, string R_pId, string fthbianhao, string CPname, SessionUser user);

       #region //返退货 代开出货单
       PagerInfo<NAReturnListView> Getchinfolist(string name,string bianhao, SessionUser user);
       
       #endregion

       #region // 返退货 出货品保确认数据
       PagerInfo<NAReturnListView> Getchqrinfolist(string name, SessionUser user); 
       #endregion

       #region //返退货 仓库出货数据
       PagerInfo<NAReturnListView> Getckchinfolist(string name, SessionUser user); 
       #endregion

       #region //保存后返回 ID
       string InsertID(NAReturnListView modelView); 
       #endregion

       #region //查询当天返退货数量
       int GetPPcount(); 
       #endregion
    }
}
