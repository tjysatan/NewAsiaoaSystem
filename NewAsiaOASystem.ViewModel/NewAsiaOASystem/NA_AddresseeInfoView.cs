using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
   public class NA_AddresseeInfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 客户Id(经销商Id)
        /// </summary>
        public virtual string CusId { get; set; }

        /// <summary>
        /// 收件公司
        /// </summary>
        public virtual string ACompany { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public virtual string Aname { get; set; }


        /// <summary>
        /// 收件地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public virtual string qyo { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public virtual string qye { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public virtual string qyt { get; set; }

        /// <summary>
        /// 收件电话
        /// </summary>
        public virtual string Tel { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_datetime { get; set; }
    }
}
