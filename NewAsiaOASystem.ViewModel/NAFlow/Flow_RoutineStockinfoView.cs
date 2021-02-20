using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 常规产品和常规半成品的信息及库存信息
    /// </summary>
   public  class Flow_RoutineStockinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// K3的物料编号
        /// </summary>
        public virtual string P_Bianhao { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string P_Name { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public virtual string P_Model { get; set; }

        /// <summary>
        /// 月用量
        /// </summary>
        public virtual decimal? M_Consumption { get; set; }

        /// <summary>
        /// 周用量
        /// </summary>
        public virtual decimal? W_Consumption { get; set; }

        /// <summary>
        /// 当前的及时库存
        /// </summary>
        public virtual decimal? N_Stock { get; set; }

        /// <summary>
        /// 报警数量
        /// </summary>
        public virtual decimal? A_Sum { get; set; }


        /// <summary>
        /// 需要生产的数量
        /// </summary>
        public virtual decimal? SC_Sum { get; set; }

        /// <summary>
        /// 是否报警 0 正常 1 异常
        /// </summary>
        public virtual int? Isbj { get; set; }


        /// <summary>
        /// 产品的种类  0 成品 1半成品
        /// </summary>
        public virtual int? Category { get; set; }


        /// <summary>
        /// 是否生产中 0 正常 1生产中
        /// </summary>
        public virtual int? Isscing { get; set; }

        /// <summary>
        /// 库存的更新时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }

        /// <summary>
        /// 更新库的帐号
        /// </summary>
        public virtual string Updateperson { get; set; }

        /// <summary>
        /// 实际生产
        /// </summary>
        public virtual decimal? Sjsc_Sum { get; set; }

        /// <summary>
        /// 启用状态 0 启用 1禁用
        /// </summary>
        public virtual int? state { get; set; }

       /// <summary>
       /// 产品的类型 0 温控 1电控箱
       /// </summary>
        public virtual int? type { get; set; }
    }
}
