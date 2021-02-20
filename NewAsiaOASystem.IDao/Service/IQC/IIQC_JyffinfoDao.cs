using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IIQC_JyffinfoDao:IBaseDao<IQC_JyffinfoView>
    {
        /// <summary>
        /// 根据检验单SOP 和检验项目查询检验方法
        /// </summary>
        /// <param name="sopid">作业流程单Id</param>
        /// <param name="xmtype">检验的项目</param>
        /// <returns></returns>
        IQC_JyffinfoView GetjyffmodebysopIdandxmtype(string sopid, string xmtype);
    }
}
