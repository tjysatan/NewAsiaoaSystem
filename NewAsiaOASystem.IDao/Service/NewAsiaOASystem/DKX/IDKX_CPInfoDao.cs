using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_CPInfoDao:IBaseDao<DKX_CPInfoView>
    {
        #region //根据产品名称 功率值 和功率单位查询是否存在相同的产品
        /// <summary>
        /// 根据产品名称 功率值 和功率单位查询是否存在相同的产品
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="Power">功率值</param>
        /// <param name="DW">单位</param>
        /// <returns></returns>
        DKX_CPInfoView Getcpdatabynameandpowanddw(string name, string Power, string DW, string cpgnjs); 
        #endregion

        #region //保存后返回ID
        string InsertID(DKX_CPInfoView modelView); 
        #endregion


        #region //产品分页列表，通过入库时间排序
        /// <summary>
        /// 产品分页列表，通过入库时间排序
        /// </summary>
        /// <param name="cpname">产品型号（名称）</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="Type">类型 0小系统 1 大系统 2 物联网 </param>
        /// <param name="ft">是否分体 0 一体 1 分体</param>
        /// <param name="Gcs_name">工程师名称</param>
        /// <returns></returns>
        PagerInfo<DKX_CPInfoView> GetDKXcppagelist(string cpname, string gl, string dw, string Type, string ft, string Gcs_name,string gnjs);
        #endregion

        #region //电控箱方案库全部数据
        /// <summary>
        /// 电控箱方案库全部数据
        /// </summary>
        /// <param name="cpname">产品型号（名称）</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="Type">类型 0小系统 1 大系统 2 物联网 </param>
        /// <param name="ft">是否分体 0 一体 1 分体</param>
        /// <param name="Gcs_name">工程师名称</param>
        /// <param name="Iscgdz">是否常规定制的 0 是 1 不是 空是全部</param>
        /// <returns></returns>
        PagerInfo<DKX_CPInfoView> GetALLDKXcppagelist(string wlno, string cpname, string gl, string dw, string Type, string ft, string Gcs_name,string gnjs, string IsDis, string Iscgdz);
        #endregion
 
        #region //202203 产品分页列表，通过入库时间排序
        /// <summary>
        /// 202203 产品分页列表，通过入库时间排序
        /// </summary>
        /// <param name="cpwlno">物料代码</param>
        /// <param name="cpname">产品型号（名称）</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="Type">类型 0小系统 1 大系统 2 物联网 </param>
        /// <param name="ft">是否分体 0 一体 1 分体</param>
        /// <param name="Gcs_name">工程师名称</param>
        /// <returns></returns>
        PagerInfo<DKX_CPInfoView> New_GetDKXcppagelist(string cpwlno, string cpname, string gl, string dw, string Type, string ft, string Gcs_name, string gnjs); 
        #endregion

        #region //通过控制器的查找产品
        /// <summary>
        /// 通过控制器的查找产品
        /// </summary>
        /// <param name="str">参数 控制器型号或者控制器物料代码</param>
        /// <param name="type">类型 0 型号 1物料代码</param>
        /// <returns></returns>
        IList<DKX_CPInfoView> Getcpinfobyxhorwldm(string str, string type);
        #endregion
        

        #region //查询同一个大类中的产品数据
        /// <summary>
        /// 查询同一个大类的产品数据
        /// </summary>
        /// <param name="sanduanno">查询同一个大类的产品数据</param>
        /// <returns></returns>
        int Getdaleichanpincount(string sanduanno);
        #endregion

        #region //通过工程师查询全部的电控箱方案库数据
        /// <summary>
        /// 通过工程师查询全部的电控箱方案库数据
        /// </summary>
        /// <param name="Gcs_name">工程师</param>
        /// <returns></returns>
        IList<DKX_CPInfoView> GetalldkxcpbygcsId(string Gcs_name);
        #endregion

        
        #region //20220331 根据物料号查询方案库产品信息
        /// <summary>
        /// 20220331 根据物料号查询方案库产品信息
        /// </summary>
        /// <param name="wlno"></param>
        /// <returns></returns>
        DKX_CPInfoView Getcpdatebyps_wlno(string wlno);
        #endregion

       
        #region //查询正常状态的下含有物料号的数据
        /// <summary>
        /// 查询正常状态的下含有头物料号的数据
        /// </summary>
        /// <returns></returns>
        IList<DKX_CPInfoView> GetalldkxcpdataScheme_order(); 
        #endregion


    }
}
