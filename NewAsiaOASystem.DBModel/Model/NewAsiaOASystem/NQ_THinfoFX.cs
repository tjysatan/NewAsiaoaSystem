using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 反推品 分析记录
    /// </summary>
    public class NQ_THinfoFX
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 是否未填
        /// </summary>
        public virtual int Iscl { get; set; }

        /// <summary>
        /// 返退流程ID
        /// </summary>
        public virtual string R_Id { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public virtual string P_Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string Cpname { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public virtual string Cpmode { get; set; }

        /// <summary>
        /// 产品物料编码
        /// </summary>
        public virtual string Cpwlno { get; set; }

        /// <summary>
        /// 生存批号
        /// </summary>
        public virtual string TH_Ph { get; set; }

        /// <summary>
        /// 生产时间
        /// </summary>
        public virtual DateTime? TH_SCdate { get; set; }

          /// <summary>
        /// 不良现象
        /// </summary>
        public virtual string TH_BLXXX { get; set; }
      
        /// <summary>
        /// 不良原因1
        /// </summary>
        public virtual string TH_BLXX { get; set; }

        /// <summary>
        /// 不良原因2
        /// </summary>
        public virtual string TH_BLYY { get; set; }

        /// <summary>
        /// 元器件名称
        /// </summary>
        public virtual string TH_YQJname { get; set; }

        /// <summary>
        /// 元器件品牌
        /// </summary>
        public virtual string TH_YQJpp { get; set; }

        /// <summary>
        /// 受损图片1
        /// </summary>
        public virtual string TH_imgurl1 { get; set; }

        /// <summary>
        /// 受损图片2
        /// </summary>
        public virtual string TH_imgurl2 { get; set; }

        /// <summary>
        /// 受损图片3
        /// </summary>
        public virtual string TH_imgurl3 { get; set; }

        /// <summary>
        /// 在保时间  18个月  18个月至3年  3年外 
        /// </summary>
        public virtual int? TH_zbshj { get; set; }

        /// <summary>
        /// 备注1
        /// </summary>
        public virtual string TH_bz { get; set; }

        /// <summary>
        /// 元器件价格
        /// </summary>
        public virtual decimal? TH_yqjjg { get; set; }

        /// <summary>
        /// 人工费
        /// </summary>
        public virtual decimal? TH_RGF { get; set; }

        /// <summary>
        /// 包材费
        /// </summary>
        public virtual decimal? TH_BCF { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public virtual decimal? TH_XJ { get; set; }

        /// <summary>
        /// 均摊运费
        /// </summary>
        public virtual decimal? TH_YF { get; set; }

        /// <summary>
        /// 均摊寄回运费
        /// </summary>
        public virtual decimal? TH_JHYF { get; set; }

        /// <summary>
        /// 备注2
        /// </summary>
        public virtual string TH_bz2 { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? C_time { get; set; }

        /// <summary>
        /// 维修登记时间
        /// </summary>
        public virtual DateTime? wx_time { get; set; }

        /// <summary>
        /// 是否定责 0未定责 1已定责  2旧数据
        /// </summary>
        public virtual int? Isdz { get; set; }

        /// <summary>
        /// 责任部门
        /// </summary>
        public virtual string zrbm { get; set; }

        /// <summary>
        /// 最终定责时间
        /// </summary>
        public virtual DateTime? dzdatetime { get; set; }

        /// <summary>
        /// 定责人
        /// </summary>
        public virtual string dz_cusId { get; set; }

        /// <summary>
        /// 客户描述的不良现象
        /// </summary>
        public virtual string Cust_Baddescribe { get; set; }

        /// <summary>
        /// 技术分析人
        /// </summary>
        public virtual string Analyst { get; set; }
    }
}
