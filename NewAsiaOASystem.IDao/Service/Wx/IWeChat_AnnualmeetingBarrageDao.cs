using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWeChat_AnnualmeetingBarrageDao:IBaseDao<WeChat_AnnualmeetingBarrageView>
    {
        //查询未上过墙的10条数据
        IList<WeChat_AnnualmeetingBarrageView> listinfobyhd();
    }
}
