﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 免疫点
    /// </summary>
    public class DisImmuneCenterView
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
        /// 免疫点名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        ///  父级编号
        /// </summary>
        public virtual string ParentId
        {
            get;
            set;
        }
        /// <summary>
        /// 排列次序
        /// </summary>
        public virtual int? Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 免疫点地址
        /// </summary>
        public virtual string DisAddress
        {
            get;
            set;
        }

        /// <summary>
        ///免疫点联系电话
        /// </summary>
        public virtual string DisPhone
        {
            get;
            set;
        }

        /// <summary>
        ///免疫点联系人
        /// </summary>
        public virtual string DisPerson
        {
            get;
            set;
        }

        /// <summary>
        /// Description  描述
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }
        /// <summary>
        /// CreatePerson 创建人
        /// </summary>
        public virtual string CreatePerson
        {
            get;
            set;
        }
        /// <summary>
        /// CreateTime 创建时间
        /// </summary>
        public virtual DateTime? CreateTime
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
        /// UpdateTime  更新时间
        /// </summary>
        public virtual DateTime? UpdateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int? Status
        {
            get;
            set;
        }

        /// <summary>
        /// 免疫点地址
        /// </summary>
        public virtual string D_Address
        {
            get;
            set;
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public virtual string D_Lat
        {
            get;
            set;
        }

        /// <summary>
        /// 经度
        /// </summary>
        public virtual string D_lon
        {
            get;
            set;
        }

    

    }
}
