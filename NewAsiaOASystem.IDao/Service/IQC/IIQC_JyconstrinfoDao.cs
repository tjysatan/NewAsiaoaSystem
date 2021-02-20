using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IIQC_JyconstrinfoDao:IBaseDao<IQC_JyconstrinfoView>
    {
        /// <summary>
        /// 根据检验单Id 和检验项目 查找检验内容
        /// </summary>
        /// <param name="sopId">作业单Id</param>
        /// <param name="xmtype">检验项目 0电气性能 1 尺寸 2外观 3可靠性 4其他</param>
        /// <returns></returns>
        IList<IQC_JyconstrinfoView> GetjyconbysopIdxmtype(string sopId, string xmtype);


        #region //根据检验单Id 查询检验内容
        /// <summary>
        /// 根据检验单Id 查询检验内容
        /// </summary>
        /// <param name="sopId">检验标准Id</param>
        /// <returns></returns>
        IList<IQC_JyconstrinfoView> GetjyconbysopId(string sopId); 
        #endregion
    }
}
