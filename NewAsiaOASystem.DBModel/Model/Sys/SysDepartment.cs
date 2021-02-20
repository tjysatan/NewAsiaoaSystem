using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.DBModel
{
    //SysDepartment  机构表
    public class SysDepartment
    {

        /// <summary>
        /// Id  机构编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Name  机构名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// ParentId 父级编号
        /// </summary>
        public virtual string ParentId
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
        public virtual int? State
        {
            get;
            set;
        }

        /// <summary>
        /// 部门对应成员
        /// </summary>
        public virtual IList<SysPerson> sysperson
        {
            get;
            set;
        }
    }
}