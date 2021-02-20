using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 报价的需求单实体
    /// </summary>
    public class DKX_PAY_CONTROL_INFOView
    {
        /// <summary>
        /// 唯一识别号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual string dd_Id { get; set; }

        /// <summary>
        /// 报价系统的自动编号
        /// </summary>
        public virtual int? CONTROL_INFO_ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public virtual string CONTROL_INFO_NO { get; set; }

        /// <summary>
        /// 省份Id
        /// </summary>
        public virtual decimal? AREA_ID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string CUST_NAME { get; set; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public virtual decimal? CUST_LEVEL { get; set; }

        /// <summary>
        /// 机组类型 0：活塞并联机控制柜  1：螺杆机控制柜 2:螺杆冷水控制柜  3：末端箱   4:一库一机 5:一机多库 6:一库双机  7：一库多机  8:多库多机(9:两库两机 10：三库三机  11：四机两库)   12：冷水机
        /// 1和2  对应压缩机 螺杆压缩机品牌 其他是 并联机类型
        /// </summary>
        public virtual decimal? COMPRESSOR_TYPE { get; set; }

        /// <summary>
        /// 箱体类型 0：横柜 1：竖柜 
        /// </summary>
        public virtual decimal? B_TYPE { get; set; }

        /// <summary>
        /// 螺杆压缩机品牌  0：比泽尔  1：汉钟  2：复盛 3：谷轮  4：雪梅  5：企鹅 6：丹佛斯  7：来富康  8：富士豪   并联机类型：0：活塞  1：涡旋  
        /// </summary>
        public virtual decimal? COMPRESSOR_BRAND { get; set; }

        /// <summary>
        /// 压缩机型号
        /// </summary>
        public virtual decimal? COMPRESSOR_MODEL { get; set; }

        /// <summary>
        /// 容调关系  1：三段二容调     2：四段三容调  3：四段四容调  4：二段单容调 5：无容调
        /// </summary>
        public virtual decimal? RT_TYPE { get; set; }

        /// <summary>
        /// 控制方式：0：制冷  1：制冷/化霜 2：制冷/化霜/风机    风机是否随压机启停  0：是  1：否
        /// </summary>
        public virtual decimal? CONTROL_TYPE { get; set; }

        /// <summary>
        /// 压缩机启动方式    0：分线圈启动  1：Y-△启动  2：直接启动
        /// </summary>
        public virtual decimal? COM_START_MODE { get; set; }

        /// <summary>
        /// 压缩机功率
        /// </summary>
        public virtual decimal? COMPRESSOR_POWER { get; set; }

        /// <summary>
        /// 压缩机数量
        /// </summary>
        public virtual decimal? COMPRESSOR_NUMBER { get; set; }

        /// <summary>
        /// 机组组合方式  0：双系统  1：独立系统
        /// </summary>
        public virtual decimal? UNIT_TYPE { get; set; }

        /// <summary>
        /// 压机起停方式 0：压力1：温度  2：手动  3：其他
        /// </summary>
        public virtual decimal? COMPRESSOR_ON_OFF { get; set; }

        /// <summary>
        /// 冷凝方式  0：风冷  1：水冷  2：蒸发冷 
        /// </summary>
        public virtual decimal? CONDEN_METHOD { get; set; }

        /// <summary>
        /// 冷凝器单列  0:单列   1:不单列
        /// </summary>
        public virtual decimal? CONDEN_DL { get; set; }

        /// <summary>
        /// 若水冷，水泵功率
        /// </summary>
        public virtual decimal? SP_POWER { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal? CONDEN_NUM { get; set; }

        /// <summary>
        /// 功率
        /// </summary>
        public virtual decimal? CONDEN_POWER { get; set; }

        /// <summary>
        /// 水泵数量
        /// </summary>
        public virtual decimal? PUMP_NUM { get; set; }

        /// <summary>
        /// 水泵功率
        /// </summary>
        public virtual decimal? PUMP_POWER { get; set; }

        /// <summary>
        /// 冷凝风机数量
        /// </summary>
        public virtual decimal? FAN_NUM { get; set; }

        /// <summary>
        /// 冷凝风机功率
        /// </summary>
        public virtual decimal? FAN_POWER { get; set; }

        /// <summary>
        /// 冷却搭风机数量
        /// </summary>
        public virtual decimal? COOL_FAN_NUM { get; set; }

        /// <summary>
        /// 冷却搭风机功率 
        /// </summary>
        public virtual decimal? COOL_FAN_POWER { get; set; }

        /// <summary>
        /// 冷凝起停方式 0：温度 1：压力 2：手动  3：其他
        /// </summary>
        public virtual decimal? ON_OFF { get; set; }

        /// <summary>
        /// 库房数量
        /// </summary>
        public virtual decimal? HOUSE_NUMBER { get; set; }

        /// <summary>
        /// 风机库数量
        /// </summary>
        public virtual decimal? HOUSE_FJ { get; set; }

        /// <summary>
        /// 排管库数量
        /// </summary>
        public virtual decimal? HOUSE_PG { get; set; }

        /// <summary>
        /// 蒸发方式0：冷风机  1：排管  2:风机/排管
        /// </summary>
        public virtual decimal? EVAPORATION_METHOD { get; set; }


        /// <summary>
        /// 冷风机数量
        /// </summary>
        public virtual decimal? COOLER_NUM { get; set; }

        /// <summary>
        /// 冷风机功率
        /// </summary>
        public virtual decimal? COOLER_POWER { get; set; }

        /// <summary>
        /// 冷风机分组数
        /// </summary>
        public virtual decimal? COOLER_GROUP { get; set; }

        /// <summary>
        /// 化霜方式 0：电热化霜  1：水冲霜    2：热氟化霜  3：风机化霜  4：未选择
        /// </summary>
        public virtual decimal? DEFROS_METHOD { get; set; }

        /// <summary>
        /// 化霜组数
        /// </summary>
        public virtual decimal? DEFROS_GROUP { get; set; }

        /// <summary>
        /// 化霜功率   组/个
        /// </summary>
        public virtual decimal? COOLER_DEFROS { get; set; }

        /// <summary>
        /// 热氟化霜 0:自动  1:手动  2:手自动
        /// </summary>
        public virtual decimal? IS_AUTO { get; set; }

        /// <summary>
        /// 水泵是否共用  0共用  1：不共用 
        /// </summary>
        public virtual decimal? IS_PUMP { get; set; }

        /// <summary>
        /// 库房 0：同开   1：不同开
        /// </summary>
        public virtual decimal? HOUSE_OPEN { get; set; }

        /// <summary>
        /// 交接品牌选择 0：正泰 1：施耐德  2：LG 3：默认
        /// </summary>
        public virtual decimal? ELEC_CHOICE_A { get; set; }

        /// <summary>
        /// 空开选择 0：正泰 1：施耐德  2：LG  3：默认
        /// </summary>
        public virtual decimal? ELEC_CHOICE_C { get; set; }

        /// <summary>
        /// 触摸屏  1：10寸   2：不需要  0:7寸
        /// </summary>
        public virtual decimal? TOUCH { get; set; }

        /// <summary>
        /// 油泵方式  0：单相  1：三相  2：无
        /// </summary>
        public virtual decimal? OIL_TYPE { get; set; }

        /// <summary>
        /// 油泵功率
        /// </summary>
        public virtual decimal? OIL_POWER { get; set; }

        /// <summary>
        /// 是否需要总空开  0：需要  1：不需要
        /// </summary>
        public virtual decimal? HAS_ZKK { get; set; }

        /// <summary>
        /// 是否需要高低压 0：需要   1：不需要  2：捷迈  3：艾默生
        /// </summary>
        public virtual decimal? HAS_GDY { get; set; }

        /// <summary>
        /// 油冷却方式 2:板换  0:风冷  1:水冷 3：不需要
        /// </summary>
        public virtual decimal? YLQ_TYPE { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal? YLQ_NUM { get; set; }

        /// <summary>
        /// 功率
        /// </summary>
        public virtual decimal? YLQ_POWER { get; set; }

        /// <summary>
        /// 油加热    0:需要  1：不需要
        /// </summary>
        public virtual decimal? HEATING_OIL { get; set; }

        /// <summary>
        /// 末端情况  0：做在一起   1：单独做
        /// </summary>
        public virtual decimal? IS_ALONE { get; set; }

        /// <summary>
        /// 电子油压差  0：四线制  1：六线制
        /// </summary>
        public virtual decimal? IS_YYC { get; set; }

        /// <summary>
        /// 喷液增焓    0：无   1：有 
        /// </summary>
        public virtual decimal? IS_PYZH { get; set; }

        /// <summary>
        /// 是否含 针对电控箱即有排管又有风机  1:不含 0：含
        /// </summary>
        public virtual decimal? IS_PAIGUAN { get; set; }

        /// <summary>
        /// 排管数量
        /// </summary>
        public virtual decimal? PG_NUM { get; set; }

        /// <summary>
        /// 远程监控  0:需要   1:不需要 
        /// </summary>
        public virtual decimal? IS_REMOTE { get; set; }

        /// <summary>
        /// 其他要求 0:无   1:分体型   2:冷暖型  3：新亚洲PLC  4：西门子PLC
        /// </summary>
        public virtual decimal? IS_OTHER { get; set; }

        /// <summary>
        /// 下单状态  0：未下单 1：已下单
        /// </summary>
        public virtual decimal? FLAG_ORDER { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string REMARK { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public virtual decimal? CREATE_BY { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CREATE_TIME { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public virtual decimal? UPDATE_BY { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UPDATE_TIME { get; set; }

        /// <summary>
        /// 报价  0：内部  1：电商平台
        /// </summary>
        public virtual decimal? FLAG_XD { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public virtual string ORDER_NO { get; set; }

        /// <summary>
        /// 备注2
        /// </summary>
        public virtual string REMARK2 { get; set; }
    }
}
