using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    //角色表
    public class SysRole
    {

        /// <summary>
        /// Id   角色编号
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }
        /// <summary>
        /// Name  角色名称
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 该角色所拥有的菜单列表
        /// </summary>
        public virtual IList<SysMenu> SysMenu { get; set; }

        /// <summary>
        /// 授权代码
        /// </summary>
        public virtual IList<SysAuthorize> SysAuth { get; set; }

        /// <summary>
        /// 功能权限
        /// </summary>
        public virtual IList<SysFunction> SysFun { get; set; }

        /// <summary>
        /// 角色对应用户
        /// </summary>
        public virtual IList<SysPerson> SysPerson { get; set; }

        /// <summary>
        /// 角色对应部门
        /// </summary>
        public virtual IList<SysDepartment> SysDept { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Description
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
        /// CreateTime  创建时间
        /// </summary>
        public virtual DateTime? CreateTime
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

        /// <summary>
        /// 父级ID
        /// </summary>
        public virtual string Pid { get; set; }

        /// <summary>
        /// 角色类型（0表示超级管理员，1表示普通管理员，2表示普通用户）
        /// 超级管理员可以查看所有数据，普通管理员只能查看本单位或部门数据，普通用户只能查看自己数据
        /// </summary>
        public virtual int RoleType { get; set; }
    }
}