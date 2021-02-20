using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
   public  class PP_TeaminfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 团队编号
        /// </summary>
        public virtual string Team_No { get; set; }

        /// <summary>
        /// 团队名称
        /// </summary>
        public virtual string Team_Name { get; set; }

        /// <summary>
        /// 团队责任人
        /// </summary>
        public virtual string Team_zrname { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Team_zrTel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Team_Beizhu { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 团队管理帐号
        /// </summary>
        public virtual string Team_glyuser { get; set; }
    }
}
