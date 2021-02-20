using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 调查问卷的 标题表
    /// </summary>
   public  class Vote_TitleView
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
        /// T_Name 标题名称
        /// </summary>
        public virtual string T_Name
        {
            get;
            set;
        }

        /// <summary>
        /// S_Id 主题Id
        /// </summary>
        public virtual string S_Id
        {
            get;
            set;
        }

        /// <summary>
        /// T_type 是否是多选  0是单选  1是多选
        /// </summary>
        public virtual int T_type
        {
            get;
            set;
        }

        /// <summary>
        /// T_Acount 该标题的总投票人数
        /// </summary>
        public virtual int? T_Acount
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime T_time
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string T_Person
        {
            get;
            set;
        }
    }
}
