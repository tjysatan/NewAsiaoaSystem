using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 访问投票的IP 统计
    /// </summary>
    public  class Vote_ipView
    {
        /// <summary>
        /// id编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 主题ID
        /// </summary>
        public virtual string S_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 用户IP
        /// </summary>
        public virtual string I_IP
        {
            get;
            set;
        }

        /// <summary>
        /// 写入时间
        /// </summary>
        public virtual DateTime I_time
        {
            get;
            set;
        }
    }
}
