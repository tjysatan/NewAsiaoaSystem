using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPP_ProfuttoStaffInfoDao:IBaseDao<PP_ProfuttoStaffInfoView>
    {
        /// <summary>
        /// 通过个人（员工）Id查找对应的收入项的关系记录
        /// </summary>
        /// <param name="staffId">Id</param>
        /// <returns></returns>
       IList<PP_ProfuttoStaffInfoView> Getptoslistbystaffid(string staffId);

        /// <summary>
        /// 通过个人（员工）Id查找对应的支出项的管理记录
        /// </summary>
        /// <param name="staffId">个人（员工）Id</param>
        /// <returns></returns>
       IList<PP_ProfuttoStaffInfoView> Getptoszhichulistbystaffid(string staffId);
    }
}
