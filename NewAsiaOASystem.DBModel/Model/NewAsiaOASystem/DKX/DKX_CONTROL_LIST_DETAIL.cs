using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 料单明细
    /// </summary>
    public class DKX_CONTROL_LIST_DETAIL
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public virtual string dd_Id { get; set; }

        /// <summary>
        /// RECORD ID，自動編號
        /// </summary>
        public virtual decimal? DETAIL_ID { get; set; }

        /// <summary>
        /// 电控箱NO 需求NO
        /// </summary>
        public virtual string CONTROL_INFO_NO { get; set; }

        /// <summary>
        /// 品名类型  0：箱体 1：接触器 2：断路器 3：电机保护器 4：指示灯 
        /// 5：蜂鸣器  6：急停按钮  7：触摸屏  8：接线端子 9：接线柱零线排 
        /// 10：中间继电器  11：转换开头 12：微动开头13：辅助触头 14：线槽 
        /// 15：风扇  16：常用控制器     
        /// 17：专用控制器  18：压力传感器   19：保险丝座  20：时间继电器   
        /// 21：相序保护器  22：PLC  23：温度传感器  24：开关电源    
        /// 25：通信模块 26：传感器线  27：辅助配件
        /// </summary>
        public virtual decimal? PARTS_TYPE { get; set; }

        /// <summary>
        /// 品牌  0：正泰 1：施耐德  2：LG 3：西门子 4：欣灵 5：艾默生 
        /// 6：昆仑通态  7：其他  8：新亚洲 9:亿维  10：九纯健  11：国达 
        /// 12：江阴长江  13：联捷 14：升亚 15：明伟  16:威纶通  17:捷迈
        /// </summary>
        public virtual decimal? BRAND { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public virtual string PARTS_NAME { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual decimal? P_COUNT { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? UNIT_PRICE { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public virtual decimal? PRICE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string REMARK { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public virtual string CREATE_BY { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CREATE_TIME { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public virtual string UPDATE_BY { get; set; }

        public virtual DateTime? UPDATE_TIME { get; set; }
    }
}
