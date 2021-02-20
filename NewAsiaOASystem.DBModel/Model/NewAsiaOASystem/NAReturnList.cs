using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 返退货单 实体类
    /// tjy_satan
    /// </summary>
    public class NAReturnList
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
        /// 型号/规格
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual int? R_Number
        {
            get;
            set;
        }

        /// <summary>
        /// 客户Id
        /// </summary>
        public virtual string C_Id
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 仓库管理员
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
        /// 修改人
        /// </summary>
        public virtual string UpdatePerson
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 状态 （0 禁用 1 启用）
        /// </summary>
        public virtual int? Status
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int? Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 返退产品ID
        /// </summary>
        public virtual string R_pId
        {
            get;
            set;
        }

        /// <summary>
        /// 返退日期
        /// </summary>
        public virtual DateTime? R_FTdate
        {
            get;
            set;
        }

        /// <summary>
        /// 返退货原因1
        /// </summary>
        public virtual string R_FTyyp
        {
            get;
            set;
        }

        /// <summary>
        /// 返退货原因
        /// </summary>
        public virtual string R_FTyy
        {
            get;
            set;
        }

        /// <summary>
        /// 返退货其他原因说明
        /// </summary>
        public virtual string R_FTyysm
        {
            get;
            set;
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public virtual string R_CLjg
        {
            get;
            set;
        }

        /// <summary>
        /// 处理结果费用（翻新入库减帐）
        /// </summary>
        public virtual Decimal? R_CLFY
        {
            get;
            set;
        }

        /// <summary>
        /// 要求处理结果
        /// </summary>
        public virtual string R_CLQTSM
        {
            get;
            set;
        }

        /// <summary>
        /// 运费
        /// </summary>
        public virtual Decimal? R_Yf
        {
            get;
            set;
        }

        /// <summary>
        /// 寄回运费
        /// </summary>
        public virtual decimal? R_Jhyf
        {
            get;set;
        }

        /// <summary>
        /// 客户索赔
        /// </summary>
        public virtual string R_sp
        {
            get;
            set;
        }

        /// <summary>
        /// 补充要求
        /// </summary>
        public virtual string Remark1
        {
            get;
            set;
        }

        /// <summary>
        /// 客服专员
        /// </summary>
        public virtual string Kfzy
        {
            get;
            set;
        }

        /// <summary>
        /// 客服主管
        /// </summary>
        public virtual string Kfzg
        {
            get;
            set;
        }

        /// <summary>
        /// 客服专员处理时间
        /// </summary>
        public virtual DateTime? ClDate
        {
            get;
            set;
        }


        /// <summary>
        /// 客服主管确认时间
        /// </summary>
        public virtual DateTime? Kfzgqrdate
        {
            get;
            set;
        }

        /// <summary>
        /// 返退类型   0 维修   1 翻新
        /// </summary>
        public virtual int? FTtype
        {
            get;
            set;
        }

        /// <summary>
        /// 是否保修期内
        /// </summary>
        public virtual int? R_isbxqn
        {
            get;
            set;
        }

        /// <summary>
        /// 翻新记录简述
        /// </summary>
        public virtual string R_Fxjlcon
        {
            get;
            set;
        }

        /// <summary>
        /// 品质判定 
        /// 0 保修期外 不予入库  1 客户使用不当，不予入库  2 翻新入库 可减帐 3 其他情况说明 
        /// </summary>
        public virtual int? R_Pzpd
        {
            get;
            set;
        }


        /// <summary>
        /// 其他情况说明
        /// </summary>
        public virtual string R_qtqksm
        {
            get;
            set;
        }

        /// <summary>
        /// 元器件 费用
        /// </summary>
        public virtual Decimal? R_YQJ
        {
            get;
            set;
        }

        /// <summary>
        /// 人工费
        /// </summary>
        public virtual Decimal? R_RGF
        {
            get;
            set;
        }

        /// <summary>
        /// 包材费
        /// </summary>
        public virtual Decimal? R_BCF
        {
            get;
            set;
        }

        /// <summary>
        /// 小计
        /// </summary>
        public virtual Decimal? R_XJ
        {
            get;
            set;
        }

        /// <summary>
        /// 车间主管
        /// </summary>
        public virtual string R_CJFZR
        {
            get;
            set;
        }

        /// <summary>
        /// 车间处理时间
        /// </summary>
        public virtual DateTime? R_cjcldate
        {
            get;
            set;
        }

        /// <summary>
        /// 责任判定
        /// </summary>
        public virtual string R_bbzrpd
        {
            get;
            set;
        }

        /// <summary>
        /// 责任部门
        /// </summary>
        public virtual string R_bbzrbm
        {
            get;
            set;
        }

        /// <summary>
        /// 品保部管理员
        /// </summary>
        public virtual string R_bbgly
        {
            get;
            set;
        }

        /// <summary>
        /// 品保部责任判定时间
        /// </summary>
        public virtual DateTime? R_bbcldate
        {
            get;
            set;
        }

        /// <summary>
        /// 营销部 处理意见
        /// </summary>
        public virtual string R_xybclyj
        {
            get;
            set;
        }

        /// <summary>
        /// 区域经理
        /// </summary>
        public virtual string R_qyjl
        {
            get;
            set;
        }

        /// <summary>
        /// 营销经理
        /// </summary>
        public virtual string R_yxzj
        {
            get;
            set;
        }
        /// <summary>
        /// 营销部已经处理时间
        /// </summary>
        public virtual DateTime? R_yxcldate
        {
            get;
            set;
        }

        /// <summary>
        /// 审核意见
        /// </summary>
        public virtual string R_JLSHYJ
        {
            get;
            set;
        }

        /// <summary>
        /// 总经理/管理者代表
        /// </summary>
        public virtual string R_ZJL
        {
            get;
            set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public virtual DateTime? R_SHDATE
        {
            get;
            set;
        }

        /// <summary>
        /// 返退货单子状态 0 未处理 1 待确定 2 待维修   3 待定责 4 待处理 5 待审核 6 已完成 （7 维修未完成 含有配件的）
        /// </summary>
        public virtual int? L_type
        {
            get;
            set;
        }


        /// <summary>
        /// 品保签字
        /// </summary>
        public virtual string PbId
        {
            get;
            set;
        }

        /// <summary>
        /// 签字时间
        /// </summary>
        public virtual DateTime? Pb_DateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 出货不同意原因
        /// </summary>
        public virtual string R_ThYY { get; set; }

        /// <summary>
        /// 出货单 状态 0 待开单  1 待品保确认 2 待出货  3已经出货
        /// </summary>
        public virtual int NQ_Type
        {
            get;
            set;
        }

        /// <summary>
        /// 品保是否同意出货0 同意 1 不同意
        /// </summary>
        public virtual int? R_isty
        {
            get;
            set;
        }

        /// <summary>
        /// 仓库管理员  出货人
        /// </summary>
        public virtual string ckqr
        {
            get;
            set;
        }

        /// <summary>
        /// 出货时间
        /// </summary>
        public virtual DateTime? ckchdatetime
        {
            get;
            set;
        }

        /// <summary>
        /// 出货单 备注
        /// </summary>
        public virtual string beizhu
        {
            get;
            set;
        }

        /// <summary>
        /// 返退货编号
        /// </summary>
        public virtual string fthbianhao
        {
            get;
            set;
        }

    } 
}
