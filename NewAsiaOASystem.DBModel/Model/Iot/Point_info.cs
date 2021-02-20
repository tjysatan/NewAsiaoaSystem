using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    //巡查人员定位信息表
  public  class Point_info
    {
      /// <summary>
      /// 表Id
      /// </summary>
      public virtual string Id
      {
          get;
          set;
      }

      /// <summary>
      /// 纬度
      /// </summary>
      public virtual string Location_X
      {
          get;
          set;
      }

      /// <summary>
      /// 经度
      /// </summary>
      public virtual string Location_Y
      {
          get;
          set;
      }

      /// <summary>
      /// 巡查人员的Id
      /// </summary>
      public virtual string P_Id
      {
          get;
          set;
      }

      /// <summary>
      /// 客户端返回数据的时间
      /// </summary>
      public virtual DateTime P_Time
      {
          get;
          set;
      }

      /// <summary>
      /// 巡查人员的信息
      /// </summary>
      public virtual SysPerson sysperson
      {
          get;
          set;
      }


    }
}
