using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NA_QyinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public virtual string Qyname { get; set; }

        /// <summary>
        /// 上级区域 Id
        /// </summary>
        public virtual string Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual string Sort { get; set; }

        /// <summary>
        /// 状态 0 启用 1禁用
        /// </summary>
        public virtual string States { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Beizhu { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime C_time { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string C_name { get; set; }

        /// <summary>
        /// 区域类型 0 省（直辖市） 1 地级市 2 县（区）
        /// </summary>
        public virtual int? Qy_type { get; set; }
    }
}
