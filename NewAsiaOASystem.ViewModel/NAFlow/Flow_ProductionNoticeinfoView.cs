using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class Flow_ProductionNoticeinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public virtual string Product_Id { get; set; }


        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Product_Name { get; set; }


        /// <summary>
        /// 产品编号（物料代码）
        /// </summary>
        public virtual string Product_Bianhao { get; set; }



        /// <summary>
        /// 销售订单ID
        /// </summary>
        public virtual string Xsorder_Id { get; set; }


        /// <summary>
        /// 数量（缺少）
        /// </summary>
        public virtual int? Number { get; set; }

        /// <summary>
        /// 通知单状态 0待处理 1生产中 2 已完成
        /// </summary>
        public virtual int? Type { get; set; }


        /// <summary>
        /// 预计完成时间
        /// </summary>
        public virtual DateTime? Yjdatetime { get; set; }

        /// <summary>
        /// 时间完成时间
        /// </summary>
        public virtual DateTime? Sjdatetime { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? Createtime { get; set; }


        /// <summary>
        /// 记录状态 0 启用 1 禁用
        /// </summary>
        public virtual int? Status { get; set; }


     
    }
}
