using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IK3_wuliaoinfoDao:IBaseDao<K3_wuliaoinfoView>
    {
        #region //查询自增号最大的数据
        K3_wuliaoinfoView GetwuliaoMaxfitem(); 
        #endregion


        #region //通过物料代码查询出物料代码
        /// <summary>
        /// 通过物料代码查询出物料代码
        /// </summary>
        /// <param name="fnumber">物料代码</param>
        /// <returns></returns>
        K3_wuliaoinfoView Getwuliaobyfnumber(string fnumber); 
        #endregion

        #region //根据物料的类型查询物料数据
        /// <summary>
        /// 根据物料的类型查询物料数据
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IList<K3_wuliaoinfoView> Getwuliaobytype(string type); 
        #endregion

        #region //k3基本数据的分页数据
        /// <summary>
        /// k3基本数据的分页数据
        /// </summary>
        /// <param name="fnumber">物料代码</param>
        /// <param name="fname">物料名称</param>
        /// <param name="fmodel">物料型号</param>
        /// <param name="Type">类别</param>
        /// <returns></returns>
        PagerInfo<K3_wuliaoinfoView> GetK3_wuliaoinfoList(string fnumber, string fname, string fmodel, string Type);
        #endregion

        IList<K3_wuliaoinfoView> getSearchList(string suName, string suCon, string suTel, string suStatus);

        suStatus setSuStatus(K3_wuliaoinfoView oasu);

        suStatus fileDateStatus(NQ_BaseitemAttachmentView att);

        K3_wuliaoinfoView GetBaseitemByItemId(string ID);

        #region //根据物料代理的前俩位和中间三位查找物料数据
        /// <summary>
        /// 根据物料代理的前俩位和中间三位查找物料数据
        /// </summary>
        /// <param name="str1">物料前俩位</param>
        /// <param name="str2">物料中间三位</param>
        /// <returns></returns>
        IList<K3_wuliaoinfoView> Getwuliaobyfnumber23(string str1, string str2);
        #endregion

        
        #region //查询普实创建时间字段最近的数据
        K3_wuliaoinfoView GetwuliaoMaxOpDate(); 
        #endregion
    }
}
