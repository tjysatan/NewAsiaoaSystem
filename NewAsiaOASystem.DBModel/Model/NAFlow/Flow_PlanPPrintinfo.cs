using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class Flow_PlanPPrintinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 生产计划Id
        /// </summary>
        public virtual string Plan_Id { get; set; }

        /// <summary>
        /// 生产批号
        /// </summary>
        public virtual string Scph { get; set; }

        /// <summary>
        /// 生产数量
        /// </summary>
        public virtual string Scsl { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string Cusname { get; set; }

        /// <summary>
        /// 要货时间
        /// </summary>
        public virtual DateTime? Yhdate { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string CPname { get; set; }


        /// <summary>
        /// 电源电压
        /// </summary>
        public virtual string DYDY { get; set; }

        /// <summary>
        /// 要货性质 0 库存储备 1订单生产
        /// </summary>
        public virtual int? Yhxz { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string Kfname { get; set; }

        /// <summary>
        /// 客服确认时间
        /// </summary>
        public virtual DateTime? Kfdatetime { get; set; }

        /// <summary>
        /// 配料名称
        /// </summary>
        public virtual string PLname { get; set; }

        /// <summary>
        /// 生产工艺
        /// </summary>
        public virtual string Scgy { get; set; }

        /// <summary>
        /// 技术确认
        /// </summary>
        public virtual string Jsname { get; set; }

        /// <summary>
        /// 技术确认时间
        /// </summary>
        public virtual DateTime? Jsdatetime { get; set; }

        /// <summary>
        /// 技术备注
        /// </summary>
        public virtual string JsBeizhu { get; set; }

        /// <summary>
        /// 客服备注
        /// </summary>
        public virtual string kfBeizhu { get; set; }
    }
}
