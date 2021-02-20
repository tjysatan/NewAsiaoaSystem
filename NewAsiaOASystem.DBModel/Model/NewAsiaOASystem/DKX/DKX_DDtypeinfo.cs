using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 电控箱类型 物联网  小系统 大系统
    /// </summary>
    public class DKX_DDtypeinfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 0 小系统 1大系统 2 物联网
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }

        /// <summary>
        /// 是否启用 0 启用 1禁用
        /// </summary>
        public virtual int Start { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? Up_time { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string Up_name { get; set; }

        //是否需要审核 0 无需审核  1 需要审核
        public virtual int? Issh { get; set; }


    }
}
