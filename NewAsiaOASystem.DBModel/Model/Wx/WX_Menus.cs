using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 自定义菜单表
    /// </summary>
  public  class WX_Menus
    {
      /// <summary>
      /// 自定义菜单的ID
      /// </summary>
      public virtual string Id
      {
          get;
          set;
      }
      /// <summary>
      /// 自定义菜单名称
      /// </summary>
      public virtual string M_Name
      {
          get;
          set;
      }

      /// <summary>
      /// 自定义菜单点击种类
      /// </summary>
      public virtual string M_Type
      {
          get;
          set;
      }

      /// <summary>
      /// 关联关键词
      /// </summary>
      public virtual string M_Key
      {
          get;
          set;
      }

      /// <summary>
      /// 链接
      /// </summary>
      public virtual string M_Url
      {
          get;
          set;
      }

      /// <summary>
      /// 几级目录
      /// </summary>
      public virtual string M_Level
      {
          get;
          set;
      }

      /// <summary>
      /// 父级ID
      /// </summary>
      public virtual string M_ParentID
      {
          get;
          set;
      }

      /// <summary>
      /// 是否启用
      /// </summary>
      public virtual string M_IsValid
      {
          get;
          set;
      }

      /// <summary>
      /// 创建时间
      /// </summary>
      public virtual DateTime M_CreateTime
      {
          get;
          set;
      }

      /// <summary>
      /// 创建人
      /// </summary>
      public virtual string M_CreateUser
      {
          get;
          set;
      }

      /// <summary>
      /// 自定义菜单排序
      /// </summary>
      public virtual int? Sort
      {
          get;
          set;
      }
    }
}
