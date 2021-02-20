using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 发送消息表
    /// </summary>
   public  class WX_SendMessageView
    {
 
       /// <summary>
       /// id
       /// </summary>
       public virtual string S_Id
       {
           get;
           set;
       }
       /// <summary>
       /// 发送方帐号（一个OpenID）
       /// </summary>
       public virtual string ToUserName
       {
           get;
           set;
       }

       /// <summary>
       /// 开发者微信号
       /// </summary>
       public virtual string FromUserName
       {
           get;
           set;
       }

       /// <summary>
       /// 消息创建时间（发送时使用整型）
       /// </summary>
       public virtual string CreateTime
       {
           get;
           set;
       }

       /// <summary>
       /// text/语音:voice/image/视频:video/地理位置:location
       /// </summary>
       public virtual string MsgType
       {
           get;
           set;
       }

       /// <summary>
       /// 文本消息内容
       /// </summary>
       public virtual string R_Content
       {
           get;
           set;
       }

       /// <summary>
       /// 消息id
       /// </summary>
       public virtual string MsgId
       {
           get;
           set;
       }

       /// <summary>
       /// 通过上传多媒体文件，得到的id
       /// </summary>
       public virtual string MediaId
       {
           get;
           set;
       }

       /// <summary>
       /// 音乐链接
       /// </summary>
       public virtual string MusicURL
       {
           get;
           set;
       }

       /// <summary>
       /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
       /// </summary>
       public virtual string HQMusicUrl
       {
           get;
           set;
       }

       /// <summary>
       /// 视频消息的标题/音乐标题/图文消息标题
       /// </summary>
       public virtual string Title
       {
           get;
           set;
       }

       /// <summary>
       /// 视频消息的描述/音乐描述/图文消息描述
       /// </summary>
       public virtual string Description
       {
           get;
           set;
       }

       /// <summary>
       /// 缩略图的媒体id，通过上传多媒体文件，得到的id
       /// </summary>
       public virtual string ThumbMediaId
       {
           get;
           set;
       }

       /// <summary>
       /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
       /// </summary>
       public virtual string Articles
       {
           get;
           set;
       }

       /// <summary>
       /// 图文消息个数，限制为10条以内
       /// </summary>
       public virtual int ArticleCount
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
       /// 图片链接
       /// </summary>
       public virtual string PicUrl
       {
           get;
           set;
       }

       /// <summary>
       /// 
       /// </summary>
       public virtual string S_CreateTime
       {
           get;
           set;
       }

       /// <summary>
       /// 发送的xml数据
       /// </summary>
       public virtual string S_XMLData
       {
           get;
           set;
       }
    }
}
