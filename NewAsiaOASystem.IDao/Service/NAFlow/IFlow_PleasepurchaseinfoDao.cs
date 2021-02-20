using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IFlow_PleasepurchaseinfoDao:IBaseDao<Flow_PleasepurchaseinfoView>
    {
        PagerInfo<Flow_PleasepurchaseinfoView> GetCinfoList(string CPName, string P_Pianhao, string P_Issc, string starttime, string endtime,string cgy);

        //通过生产计划单的Id 和元器件WL_DM 检测是否已经下单
        bool Jcqgd(string P_Id, string wl_dm);

        //通过元器件的物料代码查询采购计划单中是否有未处理的
        bool checkqgdbywlbm(string wl_dm);

          //通过元器件的物料大妈查找采购计划中采购员没有出来的采购计划单
        Flow_PleasepurchaseinfoView Getqgdmodelbynm(string wl_dm);
    }
}
