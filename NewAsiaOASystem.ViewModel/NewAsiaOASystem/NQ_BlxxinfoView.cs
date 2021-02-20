using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NQ_BlxxinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 不良现象名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 状态 0 禁用 1 启用
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime C_Data { get; set; }
    }
}
