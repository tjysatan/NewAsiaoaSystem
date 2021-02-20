using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 返退 产品信息
    /// </summary>
    public class NQ_productinfo
    {
       /// <summary>
       /// 编号
       /// </summary>
       public virtual string Id
       {
           get;
           set;
       }
       /// <summary>
       /// 产品种类
       /// </summary>
       public virtual int? p_type
       {
           get;
           set;
       }
       /// <summary>
       ///  产品名称
       /// </summary>
       public virtual string Pname
       {
           get;
           set;
       }
         
       /// <summary>
       /// 产品编号
       /// </summary>
       public virtual string P_bianhao
       {
           get;
           set;
       }

        /// <summary>
        /// 单位
        /// </summary>
       public virtual string dw
       {
           get;
           set;
       }

        /// <summary>
        /// 产品 型号
        /// </summary>
       public virtual string P_xh
       {
           get;
           set;
       }
        /// <summary>
        /// 安全库存
        /// </summary>
       public virtual decimal P_aqkc
       {
           get;
           set;
       }

        /// <summary>
        /// 当前库存
        /// </summary>
       public virtual decimal P_dqkc
       {
           get; 
           set;
       }

       /// <summary>
       /// 创建人
       /// </summary>
       public virtual string CreatePerson
       {
           get;
           set;
       }

       /// <summary>
       /// 创建时间
       /// </summary>
       public virtual DateTime? CreateTime
       {
           get;
           set;
       }

       /// <summary>
       /// 更新人
       /// </summary>
       public virtual string UpdatePerson
       {
           get;
           set;
       }

       /// <summary>
       /// 更新时间
       /// </summary>
       public virtual DateTime? UpdateTime
       {
           get;
           set;
       }

        /// <summary>
        /// 扫码发货
        /// </summary>
       public virtual int? SMFH
       {
           get;
           set;
       }

        /// <summary>
        /// 是否启用 0 启用 1禁用
        /// </summary>
       public virtual int? stait
       {
           get;
           set;
       }

       /// <summary>
       /// 仓库类型 0 电器原料 1 电子原料 2 辅料 3温控器 4 电控箱 5温控器（半成品） 6软件 7其他项目
       /// </summary>
       public virtual int? Nptype
       {
           get;
           set;
       }
       
    }
}
