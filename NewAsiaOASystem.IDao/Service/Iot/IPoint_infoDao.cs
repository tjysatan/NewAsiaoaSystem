using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
   public interface  IPoint_infoDao:IBaseDao<Point_infoView>
    {
        #region //获取巡查人员当前最新位置信息的接口
        string GetNewPoint(); 
        #endregion

        #region//根据ID 查询巡查人员的当前最新位置信息的接口
        string GetNewPointby_Id(string Id); 
        #endregion

       #region //获取全部巡查路线的信息 +GetPointlinedata()
        string GetPointlinedata();
	   #endregion

        #region //根据P_ID查找巡查人员在一段时间内的巡查信息转换成json接口 +GetlinejsonbyP_Id(string P_Id); 
        string GetlinejsonbyP_Id(string P_Id); 
        #endregion
    }
}
