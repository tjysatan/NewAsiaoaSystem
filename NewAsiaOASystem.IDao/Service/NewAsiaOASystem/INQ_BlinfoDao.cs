using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_BlinfoDao:IBaseDao<NQ_BlinfoView>
    {
        PagerInfo<NQ_BlinfoView> GetCinfoList(string Name, SessionUser user);

        IList<NQ_BlinfoView> GetlistisPar();

        IList<NQ_BlinfoView> Getlistreason_byPid(string PID);

       
        IList<NQ_BlinfoView> Getlistreason_byId(string Id);
    }
}
