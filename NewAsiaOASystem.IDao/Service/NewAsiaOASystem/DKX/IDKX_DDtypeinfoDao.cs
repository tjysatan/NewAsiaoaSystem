using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_DDtypeinfoDao:IBaseDao<DKX_DDtypeinfoView>
    {
        #region //电控箱种类管理分页列表数据            
        /// <summary>
        /// 电控箱种类管理分页列表数据
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="type">是否禁用</param>
        /// <returns></returns>
        PagerInfo<DKX_DDtypeinfoView> Getdkxtypelistpage(string Name, string start);
	    #endregion


                /// <summary>
        /// 设置电控箱类型下拉框值(编辑页面时)
        /// </summary>
        /// <param name="gcsId">需要选中的Value值</param>
        /// <returns></returns>
        string DKXtypeAlbumDropdown(string gcsId);


        #region //查询电控箱类型的全部数据
        /// <summary>
        /// 按序查找全部电控箱类型的数据
        /// </summary>
        /// <returns></returns>
        IList<DKX_DDtypeinfoView> GetALLdkxtypejson(); 
        #endregion

        #region //查找全部启用的电控箱类型的全部数据
        /// <summary>
        /// 按序查找全部电控箱类型的数据
        /// </summary>
        /// <returns></returns>
        IList<DKX_DDtypeinfoView> GetALLQYdkxtypejson(); 
        #endregion

        #region //通过类型的编号查找数据
        DKX_DDtypeinfoView Getdkxtypebytype(string type); 
        #endregion
    }
}
