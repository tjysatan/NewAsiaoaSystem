using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 问卷调查 基础设置表
    /// </summary>  
   public   class Vote_ConfigView
    {
        /// <summary>
        /// c_id 编号
        /// </summary>
        public virtual string C_Id
        {
            get;
            set;
        }

        /// <summary>
        ///  RestrictIP 是否限制ID  0为限制  1未不限制
        /// </summary>
        public virtual int? RestrictIP
        {
            get;
            set;
        }

        /// <summary>
        /// AllowView 是否允许投票者查看统计结果
        /// </summary>
        public virtual int AllowView
        {
            get;
            set;
        }

        /// <summary>
        /// refuseTime 同一个IP 在多长时间内 不允许重复提交
        /// </summary>
        public virtual int? refuseTime
        {
            get;
            set;
        }
    }
}
