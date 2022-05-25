using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 来料通知单明细
    /// </summary>
    public class IQC_llNoticeMXordinfo
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// k3 Id
        /// </summary>
        public virtual int?  llnoticId { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public virtual string ddId { get; set; }

        /// <summary>
        /// K3 单号
        /// </summary>
        public virtual string DDNO { get; set; }

        /// <summary>
        /// 元器件 物料单号
        /// </summary>
        public virtual string Yqjwldno { get; set; }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public virtual string Yqjname { get; set; }

        /// <summary>
        /// 元器件型号
        /// </summary>
        public virtual string Yqjxh { get; set; }

        /// <summary>
        /// 来料数量
        /// </summary>
        public virtual decimal? Sum { get; set; }

        /// <summary>
        /// 应该检验数量
        /// </summary>
        public virtual decimal? Yjsum { get; set; }

        /// <summary>
        /// 实检数量
        /// </summary>
        public virtual decimal? Sjsum { get; set; }

        /// <summary>
        /// 供应商物料
        /// </summary>
        public virtual string gyswlno { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public virtual string gysname { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? Dj { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public virtual decimal? ZJ { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 是否创建检验单 0 未创建 1 已经创建
        /// </summary>
        public virtual int? Isjy { get; set; }

        /// <summary>
        /// 数据来源 0 K3  1普实
        /// </summary>
        public virtual int? lytype { get; set; }
    }
}
