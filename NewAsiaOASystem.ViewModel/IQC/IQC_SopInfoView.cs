using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 来料建议作业标准实体
    /// </summary>
    public class IQC_SopInfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 文件编号
        /// </summary>
        public virtual string Sopbianhao { get; set; }

        /// <summary>
        /// 文件版本
        /// </summary>
        public virtual string Sopbanben { get; set; }

        /// <summary>
        /// 物质分类
        /// </summary>
        public virtual string Sopwztype { get; set; }

        /// <summary>
        /// 发行时间 默认是当前时间 可以修改
        /// </summary>
        public virtual DateTime? Sopfaxing { get; set; }

        /// <summary>
        /// 编制
        /// </summary>
        public virtual string Sopbianzhi { get; set; }

        /// <summary>
        /// 审核
        /// </summary>
        public virtual string Sopshname { get; set; }

        /// <summary>
        /// 是否审核通过 -1 未提交  0 待审核 1 审核通过 2审核未通过
        /// </summary>
        public virtual int? Issh { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public virtual DateTime? Shdatetime { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public virtual string Soppzname { get; set; }

        /// <summary>
        /// 是否批准 0未批准 1已批准
        /// </summary>
        public virtual int? Ispz { get; set; }

        /// <summary>
        /// 批准时间
        /// </summary>
        public virtual DateTime? Pzdatetime { get; set; }

        /// <summary>
        /// 元器件Id
        /// </summary>
        public virtual string YqjId { get; set; }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public virtual string Yqjname { get; set; }

        /// <summary>
        /// 元器件型号
        /// </summary>
        public virtual string Yqjxh { get; set; }

        /// <summary>
        /// 物料代码
        /// </summary>
        public virtual string Yqjdm { get; set; }

        /// <summary>
        /// 是否作废 0 正常 1 作废
        /// </summary>
        public virtual int? Iszf { get; set; }

        /// <summary>
        /// 作废时间
        /// </summary>
        public virtual DateTime? Zfdatetime { get; set; }

        /// <summary>
        /// 技术规格书 图片地址
        /// </summary>
        public virtual string Jsggsimgurl { get; set; }

        /// <summary>
        /// 资料名称1
        /// </summary>
        public virtual string zlname1 { get; set; }

        /// <summary>
        /// 技术规格书 地址2
        /// </summary>
        public virtual string Jsggsimgurl2 { get; set; }

        /// <summary>
        /// 资料名称2
        /// </summary>
        public virtual string zlname2 { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_datetime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string Updatename { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? Updatetime { get; set; }

        /// <summary>
        /// 是否完成 0 未完成 1 已完成
        /// </summary>
        public virtual int? Iswc { get; set; }
    }
}
