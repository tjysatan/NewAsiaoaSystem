using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 工程师实体
    /// </summary>
    public class DKX_GCSinfoView
    {
        public virtual string Id { get; set; }

        /// <summary>
        /// 帐号 Id
        /// </summary>
        public virtual string ZH_Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Tel { get; set; }

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

        /// <summary>
        /// 是否启用 0 启用 1禁用
        /// </summary>
        public virtual int Start { get; set; }

        /// <summary>
        /// 排序 越小越靠前
        /// </summary>
        public virtual int Sort { get; set; }
    }
}
