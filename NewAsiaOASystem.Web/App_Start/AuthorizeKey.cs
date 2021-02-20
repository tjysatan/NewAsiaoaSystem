using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAsiaOASystem.Web.App_Start
{
    /// <summary>
    /// 定义常用功能的控制ID，方便基类控制器对用户权限的控制
    /// </summary>
    public class AuthorizeKey
    {
        
        /// <summary>
        /// 新增记录的功能控制ID
        /// </summary>
        public string InsertKey { get; set; }

        /// <summary>
        /// 更新记录的功能控制ID
        /// </summary>
        public string UpdateKey { get; set; }

        /// <summary>
        /// 删除记录的功能控制ID
        /// </summary>
        public string DeleteKey { get; set; }

        /// <summary>
        /// 查看列表的功能控制ID
        /// </summary>
        public string ListKey { get; set; }

        /// <summary>
        /// 查看明细的功能控制ID
        /// </summary>
        public string ViewKey { get; set; }
 
       
        /// <summary>
        /// 判断是否具有插入权限
        /// </summary>
        public bool CanInsert { get; set; }

        /// <summary>
        /// 判断是否具有更新权限
        /// </summary>
        public bool CanUpdate { get; set; }

        /// <summary>
        /// 判断是否具有删除权限
        /// </summary>
        public bool CanDelete { get; set; }

        /// <summary>
        /// 判断是否具有列表权限
        /// </summary>
        public bool CanList { get; set; }

        /// <summary>
        /// 判断是否具有查看权限
        /// </summary>
        public bool CanView { get; set; }



        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AuthorizeKey() { }

        /// <summary>
        /// 常用构造函数
        /// </summary>
        public AuthorizeKey(string insert, string update, string delete, string view = "")
        {
            this.InsertKey = insert;
            this.UpdateKey = update;
            this.DeleteKey = delete;
            this.ViewKey = view;
        }
    }
}