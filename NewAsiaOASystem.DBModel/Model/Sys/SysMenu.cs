using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace NewAsiaOASystem.DBModel
{
    //SysMenu 菜单表
    public class SysMenu
    {

        /// <summary>
        /// Id  菜单编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Name  菜单名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// ParentId  父级编号
        /// </summary>
        public virtual string PId
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
        /// Description  描述
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }
        /// <summary>
        /// Url   连接地址
        /// </summary>
        public virtual string Url
        {
            get;
            set;
        }

        /// <summary>
        ///菜单所具有的按钮 
        /// </summary>
        //public virtual IList<SysButton> Sysbutton
        //{
        //    get;
        //    set;
        //}
            
        /// <summary>
        /// CreatePerson  创建人
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
        /// 状态(0禁用,1启用)
        /// </summary>
        public virtual int Status { get; set; }


        /// <summary>
        /// Ico 图标
        /// </summary>
        public virtual string Ico { get; set; }
    }
}