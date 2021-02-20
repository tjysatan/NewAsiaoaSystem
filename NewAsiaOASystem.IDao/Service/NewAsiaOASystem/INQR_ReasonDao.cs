using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQR_ReasonDao:IBaseDao<NQR_ReasonView>
    {
        #region //分页查询 数据
        PagerInfo<NQR_ReasonView> GetCinfoList(string Name, SessionUser user); 
        #endregion

        #region //查找全部父级原因信息
        IList<NQR_ReasonView> GetlistisPar(); 
        #endregion

        #region //根据父级Id 查找返退货原因信息
        IList<NQR_ReasonView> Getlistreason_byPid(string PID); 
        #endregion
    }
}
