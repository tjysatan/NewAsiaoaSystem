using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_GCSinfoDao:IBaseDao<DKX_GCSinfoView>
    {
        #region //电气工程师数据分页列表
        /// <summary>
        /// 电气工程师数据分页列表
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="zhname">管理的帐号</param>
        /// <param name="tel">电话</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        PagerInfo<DKX_GCSinfoView> GetGCSlistpage(string Name, string zhname, string tel, string start); 
        #endregion

        #region //保存后返回ID
        string InsertID(DKX_GCSinfoView modelView); 
        #endregion

       
        #region //查找全部工程师的数据
        IList<DKX_GCSinfoView> GetALLgcsDATA(); 
        #endregion

        #region //根据类型的Id 查找对应的工程师数据
        /// <summary>
        /// 根据类型的Id 查找对应的工程师数据
        /// </summary>
        /// <param name="dkxtypeId">电控箱类型的Id</param>
        /// <returns></returns>
        IList<DKX_GCSinfoView> GetgcsinfobydkxtypeId(string dkxtypeId);
        #endregion

        
        #region //通过账号Id查询工程师数据
        /// <summary>
        /// 通过账号Id查询工程师数据
        /// </summary>
        /// <param name="ZHId">关联账号的Id</param>
        /// <returns></returns>
        DKX_GCSinfoView Getdkx_gscmodelbyuserId(string ZHId); 
        #endregion
    }
}
