using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 多图文ID
    /// </summary>
    public  class WX_M_newView
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
