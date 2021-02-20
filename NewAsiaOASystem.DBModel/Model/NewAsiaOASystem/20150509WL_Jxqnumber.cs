using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 经销商 年经销量
    /// </summary>
    public  class _20150509WL_Jxqnumber
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public virtual string Year { get; set; }

        /// <summary>
        /// 经销权数量
        /// </summary>
        public virtual int Jxqnumber { get; set; }

        /// <summary>
        /// 工程商Id
        /// </summary>
        public virtual string Cus_Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }

        public virtual int States { get; set; }



    }
}
