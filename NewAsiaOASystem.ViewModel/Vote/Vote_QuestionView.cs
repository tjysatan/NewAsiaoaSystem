using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 问卷 选项表
    /// </summary>
   public class Vote_QuestionView
    {
        /// <summary>
        /// ID 编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 选项名称
        /// </summary>
        public virtual string Q_Question
        {
            get;
            set;
        }

        /// <summary>
        /// 主题Id
        /// </summary>
        public virtual string S_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 标题Id
        /// </summary>
        public virtual string T_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 投票人数
        /// </summary>
        public virtual int Q_Count
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime Q_time
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string Q_Person
        {
            get;
            set;
        }

       /// <summary>
       /// 选项的次序
       /// </summary>
        public virtual int? Q_Order
        {
            get;
            set;
        }
    }
}
