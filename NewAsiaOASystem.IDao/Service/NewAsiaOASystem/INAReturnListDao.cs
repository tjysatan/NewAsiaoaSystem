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
       PagerInfo<NAReturnListView> GetCinfoList(string Name, string Szt, string type, string R_pId, string fthbianhao, string CPname, string Isjsbpz, SessionUser user);

        
        #region //查询反退货订单的数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Szt"></param>
        /// <param name="type"></param>
        /// <param name="R_pId"></param>
        /// <param name="fthbianhao"></param>
        /// <param name="CPname"></param>
        /// <param name="Isjsbpz"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        IList<NAReturnListView> Getdatelist(string Name, string Szt, string type, string R_pId, string fthbianhao, string CPname, string Isjsbpz, SessionUser user); 
        #endregion

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



        #region //查询当年或者当月的订单数量
        /// <summary>
        /// 查询当年和当月的订单数量
        /// </summary>
        /// <param name="datatype">YY 查询当年的 MM查询当月的</param>
        /// <returns></returns>
        int Get_OrderNumber_YORM(string datatype);
         #endregion
    }
}
