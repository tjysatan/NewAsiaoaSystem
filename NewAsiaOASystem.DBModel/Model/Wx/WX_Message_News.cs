 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 图文库
    /// </summary>
   public class WX_Message_News
    {
       /// <summary>
       /// ID
       /// </summary>
       public virtual string N_Id
       {
           get;
           set;
       }
       /// <summary>
       /// 
       /// </summary>
       public virtual string A_Id
       {
           get;
           set;
       }

       /// <summary>
       /// 图文消息标题
       /// </summary>
  
       public virtual string Title
       {
           get;
           set;
       }

       /// <summary>
       /// 图文消息描述
       /// </summary>
       public virtual string Description
       {
           get;
           set;
       }

       /// <summary>
       /// 图片链接
       /// </summary>
       public virtual string PicUrl
       {
           get;
           set;
       }

       /// <summary>
       /// 点击图文消息跳转链接
       /// </summary>
       public virtual string Url
       {
           get;
           set;
       }

       /// <summary>
       /// 文章详情
       /// </summary>
       public virtual string MnContent
       {
           get;
           set;
       }

       /// <summary>
       /// 图文类型  0 群发  1 绩效 NULL 自定义回复
       /// </summary>
       public virtual Int32 MType
       {
           get;
           set;
       }

       public virtual WX_Message wx_Message
       {
           get;
           set;
       }
       /// <summary>
       /// 排序 越小越靠前
       /// </summary>
       public virtual int? sort
       {
           get;
           set;
       }
       
    }
}
