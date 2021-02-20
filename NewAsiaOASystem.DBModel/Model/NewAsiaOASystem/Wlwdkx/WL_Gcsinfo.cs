using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 工程商信息
    /// </summary>
    public class WL_Gcsinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 工程商 唯一识别号
        /// </summary>
        public virtual string Gcs_no { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string DW_name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Lxr_name { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Lxr_tel { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Adress { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

        /// <summary>
        /// 是否启用 0 启用 1禁用
        /// </summary>
        public virtual int States { get; set; }


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
    }
}
