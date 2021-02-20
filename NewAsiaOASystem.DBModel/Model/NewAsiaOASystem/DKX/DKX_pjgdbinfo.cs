using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 拼接柜 底板数据实体
    /// </summary>
    public class DKX_pjgdbinfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 底板名称和型号
        /// </summary>
        public virtual string dbname { get; set; }

        /// <summary>
        /// 底板描数
        /// </summary>
        public virtual string db_describe { get; set; }

        /// <summary>
        /// 底板物料代码
        /// </summary>
        public virtual string dbwlno { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? c_time { get; set; }

        /// <summary>
        /// 更新是将
        /// </summary>
        public virtual DateTime? update_time { get; set; }
    }
}
