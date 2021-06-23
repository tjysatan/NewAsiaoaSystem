using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IIQC_SopInfoDao:IBaseDao<IQC_SopInfoView>
    {
                /// <summary>
        /// 来料检验标准的分页数据
        /// </summary>
        /// <param name="yqjname">元器件名称</param>
        /// <param name="yqjxh">元器件型号</param>
        /// <param name="yqjwldm">元器件物料代码</param>
        /// <param name="shzt">审核状态</param>
        /// <param name="fxdatetime">发行时间</param>
        /// <param name="Iszf">是否作废</param>
        /// <returns></returns>
        PagerInfo<IQC_SopInfoView> GetIQC_Soppagelist(string yqjname, string yqjxh, string yqjwldm, string shzt, string fxdatetime, string Iszf);

        #region //保存后返回ID
        string InsertID(IQC_SopInfoView modelView); 
        #endregion

        /// <summary>
        /// 通过元器件Id 查找是否有正常存在的检验作业单
        /// </summary>
        /// <param name="yjId">元器件Id</param>
        /// <returns></returns>
        IQC_SopInfoView GetsopmodelbyyjId(string yjId);


        #region //通过元器件物料代理查询是否正常存在检验作业单
        /// <summary>
        /// 通过元器件物料代理查询是否正常存在检验作业单
        /// </summary>
        /// <param name="wlno">元器件物料代码</param>
        /// <returns></returns>
        IQC_SopInfoView Getsopinfobyyqjwlno(string wlno);
        #endregion


        
        #region //查咋全部检验标准的数据
        /// <summary>
        /// 查询全部正常的检验标准的数据
        /// </summary>
        /// <returns></returns>
        IList<IQC_SopInfoView> GetAllIQC_Soppagelist(); 
        #endregion
    }
}
