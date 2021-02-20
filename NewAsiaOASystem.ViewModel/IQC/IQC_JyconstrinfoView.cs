using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 检验内容实体
    /// </summary>
    public class IQC_JyconstrinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 检验名称
        /// </summary>
        public virtual string Jyname { get; set; }

        /// <summary>
        /// 检验项目 0 电气性能 1尺寸 2外观 3可靠性 4其它
        /// </summary>
        public virtual int? Jyxmname { get; set; }

        /// <summary>
        /// 检验设备
        /// </summary>
        public virtual string Jyyqname { get; set; }

        /// <summary>
        /// 缺点等级
        /// </summary>
        public virtual string QDDJ { get; set; }

        /// <summary>
        /// 送检作业标准单Id
        /// </summary>
        public virtual string SopId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 状态 0 正常 1 删除
        /// </summary>
        public virtual int State { get; set; }

        /// <summary>
        /// 是否免检 0 不免检 1 免检
        /// </summary>
        public virtual int? Ismj { get; set; }
    }
}
