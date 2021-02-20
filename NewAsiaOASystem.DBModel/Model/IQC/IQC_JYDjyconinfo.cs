using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 检验单的检验内容
    /// </summary>
    public class IQC_JYDjyconinfo
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 检验单Id
        /// </summary>
        public virtual string jydId { get; set; }

        /// <summary>
        /// 检验项目 0 电气性能 1尺寸 2外观 3可靠性 4其它
        /// </summary>
        public virtual int? type { get; set; }

        /// <summary>
        /// 检验内容
        /// </summary>
        public virtual string conff { get; set; }

        /// <summary>
        /// 检验工具
        /// </summary>
        public virtual string jygj { get; set; }

        /// <summary>
        /// 测试数据
        /// </summary>
        public virtual string csdataconstr { get; set; }

        /// <summary>
        /// 测试数据1
        /// </summary>
        public virtual string csdatastr1 { get; set; }

        /// <summary>
        /// 测试数据2
        /// </summary>
        public virtual string csdatastr2 { get; set; }

        /// <summary>
        /// 测试数据3
        /// </summary>
        public virtual string csdatastr3 { get; set; }

        /// <summary>
        /// 测试数据4
        /// </summary>
        public virtual string csdatastr4 { get; set; }

        /// <summary>
        /// 测试数据5
        /// </summary>
        public virtual string csdatastr5 { get; set; }

        /// <summary>
        /// 是否判定 0 未  1 OK  2 NG
        /// </summary>
        public virtual int? Ispd { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string beizhu { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public virtual DateTime? txtime { get; set; }

        /// <summary>
        /// 测试人
        /// </summary>
        public virtual string txname { get; set; }

        /// <summary>
        /// 测试时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 缺点等级
        /// </summary>
        public virtual string QDDJ { get; set; }

        /// <summary>
        /// 是否免检  0 不免检  1免检
        /// </summary>
        public virtual int? Ismj { get; set; }
    }
}
