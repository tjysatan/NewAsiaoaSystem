using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 返退货原因实体类
    /// tjy_satan
    /// </summary>
    public class NQR_Reason
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }



        /// <summary>
        /// 原因
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 父级Id
        /// </summary>
        public virtual string ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark
        {
            get;
            set;
        }
        
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string CreatePerson
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string UpdatePerson
        {
            get;
            set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 状态 0 禁用 1 启用
        /// </summary>
        public virtual int? Status
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? Sort
        {
            get;
            set;
        }
    }
}
