using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NQ_YJinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 元器件代码
        /// </summary>
        public virtual string Y_Dm { get; set; }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public virtual string Y_Name { get; set; }

        /// <summary>
        /// 元器件规格
        /// </summary>
        public virtual string Y_Ggxh { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public virtual string G_Dm { get; set; }

        /// <summary>
        /// 元器件不含税代码
        /// </summary>
        public virtual string Y_DJ { get; set; }

        /// <summary>
        /// 安全库存数量
        /// </summary>
        public virtual int Y_aqkc { get; set; }

        /// <summary>
        /// 当前库存
        /// </summary>
        public virtual decimal Y_kc { get; set; }

        /// <summary>
        /// 本月当前用量
        /// </summary>
        public virtual decimal Y_DQYL { get; set; }

        /// <summary>
        /// 当前第几周
        /// </summary>
        public virtual int Y_DQDJZ { get; set; }

        /// <summary>
        /// 差异值  
        /// </summary>
        public virtual decimal Y_CYZ { get; set; }


        /// <summary>
        /// 是否需要采购 0 不要采购 1 需要采购
        /// </summary>
        public virtual int Y_iscg { get; set; }

        /// <summary>
        /// 需要采购的数量
        /// </summary>
        public virtual decimal Y_cgSL { get; set; }

        /// <summary>
        /// 三个月用量总和
        /// </summary>
        public virtual decimal Y_Syzyl { get; set; }

        /// <summary>
        /// 是否已经采购 0未采购 1 已经采购
        /// </summary>
        public virtual int Y_cgzt { get; set; }

        /// <summary>
        /// 检测库存时间
        /// </summary>
        public virtual DateTime? Y_jcdatetime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string CreatePerson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual string UpdatePerson { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }

    }
}
