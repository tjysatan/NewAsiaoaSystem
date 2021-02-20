using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 供应商信息
    /// </summary>
    public class NQ_GysInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public virtual string G_Dm { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string G_Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string G_Lxr { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string G_Dz { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string G_Tel { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string CreatePerson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string UpdatePerson { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }
    }
}
