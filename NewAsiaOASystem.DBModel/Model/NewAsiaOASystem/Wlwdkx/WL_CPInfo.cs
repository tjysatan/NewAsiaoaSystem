using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 物联网电控箱信息表
    /// </summary>
   public class WL_CPInfo
    {
       /// <summary>
       /// 编号
       /// </summary>
       public virtual string Id { get; set; }

       /// <summary>
       /// 物联网电控箱名称
       /// </summary>
       public virtual string Name { get; set; }

       /// <summary>
       /// 单价
       /// </summary>
       public virtual decimal Dj { get; set; }

       /// <summary>
       /// 排序
       /// </summary>
       public virtual int Sort { get; set; }

       /// <summary>
       /// 状态 0 启用 1禁用
       /// </summary>
       public virtual int States { get; set; }

       /// <summary>
       /// 备注
       /// </summary>
       public virtual string Beizhu { get; set; }

       /// <summary>
       /// 创建时间
       /// </summary>
       public virtual DateTime C_time { get; set; }

       /// <summary>
       /// 创建人
       /// </summary>
       public virtual string C_name { get; set; }

    }
}
