using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPP_ProfuttoStaffTDInfoDao:IBaseDao<PP_ProfuttoStaffTDInfoView>
    {
        /// <summary>
        /// 根据员工查询与之有关系的团体收入项目信息
        /// </summary>
        /// <param name="Id">个人Id</param>
        /// <returns></returns>
        IList<PP_ProfuttoStaffTDInfoView> GetProfuttostafIdTDshouruinfobystafId(string Id);

        /// <summary>
        /// 根据员工查询与之有关系的团体支出项目信息
        /// </summary>
        /// <param name="Id">个人Id</param>
        /// <returns></returns>
        IList<PP_ProfuttoStaffTDInfoView> GetProfuttpstafIdTDzhichubystafId(string Id);

        /// <summary>
        /// //通过个人Id和团体项目Id查找关系信息
        /// </summary>
        /// <param name="stafId">个人Id</param>
        /// <param name="ProfutId">项目Id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        PP_ProfuttoStaffTDInfoView GetstafIdtoProfitinfobystafId(string stafId, string ProfutId, string type);

        /// <summary>
        /// 通过团体项目Id查找团体和个人的关系数据
        /// </summary>
        /// <param name="profuId">项目Id</param>
        /// <returns></returns>
        IList<PP_ProfuttoStaffTDInfoView> GetprofIdtostafIdinfobyprofuId(string profuId);
    }
}
