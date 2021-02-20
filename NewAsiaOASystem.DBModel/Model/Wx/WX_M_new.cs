using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 多图文表
    /// </summary>
    public  class WX_M_new
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
        /// 图文ID
        /// </summary>
        public virtual string NewId
        {
            get;
            set;
        }

        /// <summary>
        /// 关键字 ID
        /// </summary>
        public virtual string MId
        {
            get;
            set;
        }
    }
}
