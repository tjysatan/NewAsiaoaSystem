using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
   public class CG_DetailinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }


        /// <summary>
        /// 供应商Id
        /// </summary>
        public virtual string GysId { get; set; }

        /// <summary>
        /// 采购单Id
        /// </summary>
        public virtual string cgId { get; set; }

        /// <summary>
        /// 元器件ID
        /// </summary>
        public virtual string YqjId { get; set; }

        //采购shul
        /// <summary>
        ///系统建议 采购数量
        /// </summary>
        public virtual decimal Cgsl { get; set; }

        /// <summary>
        /// 实际采购数量
        /// </summary>
        public virtual decimal sjcgsl { get; set; }

        /// <summary>
        /// 三月平均用量
        /// </summary>
        public virtual decimal sypjsl { get; set; }

        /// <summary>
        /// 实际到货数量
        /// </summary>
        public virtual decimal Dhsl { get; set; }
    }
}
