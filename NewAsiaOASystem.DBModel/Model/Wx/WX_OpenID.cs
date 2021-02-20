using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 微信用户和系统帐号绑定表
    /// </summary>
  public  class WX_OpenID
    {
        /// <summary>
        /// Id 编码
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

      /// <summary>
      /// 微信用户唯一识别号
      /// </summary>
        public virtual string OpenId
        {
            get;
            set;
        }

      /// <summary>
      /// 系统用户ID
      /// </summary>
        public virtual string Person_Id
        {
            get;
            set;
        }

      /// <summary>
      /// 插入时间
      /// </summary>
        public virtual DateTime Time
        {
            get;
            set;
        }

      /// <summary>
      /// 绑定帐号的类型 0 电商帐号  1 内部帐号
      /// </summary>
        public virtual int? Type
        {
            get;
            set;
        }

      /// <summary>
      /// 内部帐号 0 客服 1工程 2 技术  3 生产
      /// </summary>
        public virtual int? UserType
        {
            get;
            set;
        }
    }
}
