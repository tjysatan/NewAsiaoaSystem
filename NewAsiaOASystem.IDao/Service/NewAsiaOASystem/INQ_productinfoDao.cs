using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_productinfoDao:IBaseDao<NQ_productinfoView>
    {
        PagerInfo<NQ_productinfoView> GetCinfoList(string Name, string P_xh, string bianhao, SessionUser user);

        #region //根据产品名称查找产品信息
        NQ_productinfoView GetProinfobyname(string Pname); 
        #endregion

        
        #region  //保存后返回ID
        string InsertID(NQ_productinfoView modelView); 
        #endregion

        #region //根据产品 检测该订单是否需要扫描
        bool JcProisSm(string sku); 
        #endregion

        List<Object> GetTHwxfxgroupcpId(string starttime, string enedtime);

        #region //查询状态为空的数据
        /// <summary>
        /// 查询状态为空的数据
        /// </summary>
        /// <returns></returns>
        IList<NQ_productinfoView> Getproductbystaitisnull(); 
        #endregion


        #region //根据物料号查询产品数据
        /// <summary>
        /// 根据物料号查询产品数据
        /// </summary>
        /// <param name="fnumber">物料号</param>
        /// <returns></returns>
        NQ_productinfoView Getproductbyfnumber(string fnumber); 
        #endregion
    }
}
