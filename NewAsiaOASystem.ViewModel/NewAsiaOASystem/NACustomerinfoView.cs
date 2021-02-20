using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    ///Name: 客户信息 实体类view
    ///author：tjy_satan
    /// </summary>

    public  class NACustomerinfoView
    {
        /// <summary>
        /// 客户信息编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public virtual string LxrName
        {
            get;
            set;
        }

        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string Tel
        {
            get;
            set;
        }

        /// <summary>
        /// 客户地址
        /// </summary>
        public virtual string Adress
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
        /// 状态 0 启用 1 禁用
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
        /// 区域Id
        /// </summary>
        public virtual string qyId { get; set; }


        /// <summary>
        /// 区域Id(地级市)
        /// </summary>
        public virtual string qyCId { get; set; }

        /// <summary>
        /// 客户代码
        /// </summary>
        public virtual string KH_NO { get; set; }

        /// <summary>
        /// 是否是经销商（物联网电控箱） 0 否 1是
        /// </summary>
        public virtual int isjxs { get; set; }

        /// <summary>
        /// 电商平台 用户uid
        /// </summary>
        public virtual string DsUid { get; set; }

        /// <summary>
        /// 是否电商同步过来的 0 不是 1 是
        /// </summary>
        public virtual int? Isds { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public virtual decimal? dycs { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public virtual string qyname { get; set; }

        /// <summary>
        /// 地级市名称
        /// </summary>
        public virtual string qycname { get; set; }

        /// <summary>
        /// 区县名称
        /// </summary>
        public virtual string qyqxname { get; set; }
    }
}
