using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 检验单
    /// </summary>
    public class IQC_JYDDinfoView
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 检验单号
        /// </summary>
        public virtual string Jyddno { get; set; }

        /// <summary>
        /// 通知单的明细Id
        /// </summary>
        public virtual string MxId { get; set; }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public virtual string Yqjname { get; set; }

        /// <summary>
        /// 元器件型号
        /// </summary>
        public virtual string Yqjxh { get; set; }

        /// <summary>
        /// 元器件物料代码
        /// </summary>
        public virtual string Yqjwl { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string Gysname { get; set; }

        /// <summary>
        /// 供应商物料代码
        /// </summary>
        public virtual string Gyswl { get; set; }

        /// <summary>
        /// 来料时间
        /// </summary>
        public virtual DateTime? Lltime { get; set; }

        /// <summary>
        /// 来料数量
        /// </summary>
        public virtual decimal? Llsum { get; set; }

        /// <summary>
        /// 抽检数量
        /// </summary>
        public virtual decimal? Cjsum { get; set; }

        /// <summary>
        /// 合格数量
        /// </summary>
        public virtual decimal? Hgesum { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        public virtual decimal? BHgesum { get; set; }

        /// <summary>
        /// 检验状态 0 检测中 1待审核  2 审核未通过  3 完成
        /// </summary>
        public virtual int? Jydzt { get; set; }

        /// <summary>
        /// 检验单结果 0 合格 1退货 2 特采 3 试用   4 未判定
        /// </summary>
        public virtual int? Jydjg { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 录单时间
        /// </summary>
        public virtual DateTime? Rdtime { get; set; }

        /// <summary>
        /// 录单人
        /// </summary>
        public virtual string Rdname { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? Wctime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public virtual string Updatename { get; set; }

        /// <summary>
        /// 检验标准Id
        /// </summary>
        public virtual string JYBZId { get; set; }

        /// <summary>
        /// 是否禁用 0 启用 1 禁用
        /// </summary>
        public virtual int? IsDis { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string beizhu { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual string shname { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public virtual DateTime? shdatime { get; set; }

        /// <summary>
        /// 退回原因
        /// </summary>
        public virtual string thyycon { get; set; }

        /// <summary>
        /// 提交审核的时间
        /// </summary>
        public virtual DateTime? TJtime { get; set; }

        /// <summary>
        ///  LotNO
        /// </summary>
        public virtual string LotNO { get; set; }

        /// <summary>
        /// 不良说明 
        /// </summary>
        public virtual string blmxsm { get; set; }
    }
}
