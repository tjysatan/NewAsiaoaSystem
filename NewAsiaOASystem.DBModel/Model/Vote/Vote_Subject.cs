using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 问卷调查 主题 表
    /// </summary>
  public  class Vote_Subject
    {
        /// <summary>
        /// Id 编码
        /// </summary>
       public virtual string S_Id
        {
            get;
            set;
        }

      /// <summary>
       /// S_title 主题的标题
      /// </summary>
       public virtual string S_title
       {
           get;
           set;
       }

      /// <summary>
       /// S_conten  主题的描述
      /// </summary>
       public virtual string S_conten
       {
           get;
           set;
       }

      /// <summary>
       /// S_QX 主题的到期期限
      /// </summary>
       public virtual DateTime? S_QX
       {
           get;
           set;
       }


      /// <summary>
       /// S_Type 主题问卷 正对的对象向：0 全部微信用户，1绑定的微信用户
      /// </summary>
       public virtual int? S_Type
       {
           get;
           set;
       }
      /// <summary>
      /// 创建时间
      /// </summary>
       public virtual DateTime S_time
       {
           get;
           set;
       }

      /// <summary>
      /// 创建人
      /// </summary>
       public virtual string S_person
       {
           get;
           set;
       }

       public virtual IList<Vote_Title> voteTitle
       {
           get;
           set;
       }
    }
}
