using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.ViewModel
{
    //按钮库
    public class SysButtonView
    {

        /// <summary>
        /// Id  按钮编码
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Name  按钮操作名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Sort  排序
        /// </summary>
        public virtual int? Sort
        {
            get;
            set;
        }
        /// <summary>
        /// Ico  图标
        /// </summary>
        public virtual string Ico
        {
            get;
            set;
        }
        /// <summary>
        /// Remark  备注
        /// </summary>
        public virtual string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime  创建时间
        /// </summary>
        public virtual DateTime? CreateTime
        {
            get;
            set;
        }
        /// <summary>
        /// CreatePerson  创建人
        /// </summary>
        public virtual string CreatePerson
        {
            get;
            set;
        }
        /// <summary>
        /// UpdateTime 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// UpdatePerson  更新人
        /// </summary>
        public virtual string UpdatePerson
        {
            get;
            set;
        }

        /// <summary>
        /// 状态(0禁用,1启用)
        /// </summary>
        public virtual int Status { get; set; }

        public virtual string menu { get; set; }

        public virtual string menuNew { get; set; }
    }
}