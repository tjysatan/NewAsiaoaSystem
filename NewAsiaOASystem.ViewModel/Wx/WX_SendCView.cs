using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
  
    /// <summary>
    /// 统计群发 次数
    /// </summary>
    public class WX_SendCView
    {
        /// <summary>
        /// ID
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 发送月份
        /// </summary>
        public virtual string S_Month
        {
            get;
            set;
        }

        /// <summary>
        /// 发送的年份
        /// </summary>
        public virtual string S_Year
        {
            get;
            set;
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public virtual DateTime S_Time
        {
            get;
            set;
        }

        /// <summary>
        /// 统计的种类  0  群发，1绩效推送 ，2未免疫儿童数量 各个免疫点推送
        /// </summary>
        public virtual Int32 S_Type
        {
            get;
            set;
        }
    }
}
