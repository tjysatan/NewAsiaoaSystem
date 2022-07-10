using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// K3 物料基础数据
    /// </summary>
    public class K3_wuliaoinfo
    {
        /// <summary>
        /// 唯一识别号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 自增列
        /// </summary>
        public virtual int? fitem { get; set; }

        /// <summary>
        /// 物料代码
        /// </summary>
        public virtual string fnumber { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public virtual string fname { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public virtual string fmodel { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public virtual decimal? forderprice { get; set; }

        /// <summary>
        /// 产品单位
        /// </summary>
        public virtual string unitname { get; set; }

        /// <summary>
        /// 产品重量
        /// </summary>
        public virtual decimal? fnetweight { get; set; }

        /// <summary>
        /// 仓库类型 0 电器原料 1 电子原料 2 辅料 3温控器 4 电控箱 5温控器（半成品） 6软件 7其他项目
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public virtual DateTime? C_Time { get; set; }

        public virtual int FIsChecked { get; set; }

        /// <summary>
        /// 物料前俩位
        /// </summary>
        public virtual string str1 { get; set; }

        /// <summary>
        /// 物料中间三位
        /// </summary>
        public virtual string str2 { get; set; }

        /// <summary>
        /// 普实的物料创建时间
        /// </summary>
        public virtual DateTime? OpDate { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public virtual string WhsName { get; set; }

        /// <summary>
        /// 制造/采购
        /// </summary>
        public virtual string SourceID { get; set; }

        /// <summary>
        /// 采购物料属性  通通  定定  通定 定通 通通-战略 战略
        /// </summary>
        public virtual string Z_WLSX { get; set; }

        /// <summary>
        /// 是否停用 Y 停用 N 没停用
        /// </summary>
        public virtual string IsClose { get; set; }

        /// <summary>
        /// 最近更新时间
        /// </summary>
        public virtual DateTime? updatetime { get; set; }
    }
}
