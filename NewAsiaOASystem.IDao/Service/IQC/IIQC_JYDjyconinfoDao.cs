using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IIQC_JYDjyconinfoDao:IBaseDao<IQC_JYDjyconinfoView>
    {
        #region //根据检验单Id 查询测试项目
        /// <summary>
        /// 根据检验单Id 查询测试项目
        /// </summary>
        /// <param name="jydId">检验单测试单Id</param>
        /// <returns></returns>
        IList<IQC_JYDjyconinfoView> GetJYDjyconinfobyjydId(string jydId); 
        #endregion

        #region //根据检验Id 和项目内容 查询检验测试项目
        /// <summary>
        /// 根据检验Id 和项目内容 查询检验测试项目
        /// </summary>
        /// <param name="jydId">检验单Id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        IList<IQC_JYDjyconinfoView> GetJYDjyconinfobyjydIdandtype(string jydId, string type); 
        #endregion

        #region //根据检验单Id 查找未处理的检验测试项目
        /// <summary>
        /// 根据检验单Id 查找未处理的检验测试项目
        /// </summary>
        /// <param name="JYDId">检验单Id </param>
        /// <returns></returns>
        IList<IQC_JYDjyconinfoView> GetJYDjyconinfobyJYDIdwcl(string JYDId); 
        #endregion
    }
}
