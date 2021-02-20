using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 客诉问题分类管理
    /// </summary>
    public class KS_ProblemTyleView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 问题分类名称
        /// </summary>
        public virtual string WTName { get; set; }

        /// <summary>
        /// 问题分类描述
        /// </summary>
        public virtual string beizhu { get; set; }

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
        /// 是否禁用 0 启用 1禁用
        /// </summary>
        public virtual int? Start { get; set; }

        /// <summary>
        /// 排序 越小越靠前
        /// </summary>
        public virtual int? Sort { get; set; }
    }
}
