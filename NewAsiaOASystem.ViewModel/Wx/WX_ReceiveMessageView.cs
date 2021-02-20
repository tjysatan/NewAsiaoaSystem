using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 接受消息表
    /// </summary>
  public  class WX_ReceiveMessageView
    {
 
       /// <summary>
       /// Id
       /// </summary>
       public virtual string R_ID
       {
           get;
           set;
       }
       /// <summary>
       /// 开发者微信号
       /// </summary>
       public virtual string ToUserName
       {
           get;
           set;
       }

       /// <summary>
       /// 发送方帐号（一个OpenID）
       /// </summary>
       public virtual string FromUserName
       {
           get;
           set;
       }

       /// <summary>
       /// 消息创建时间
       /// </summary>
       public virtual DateTime CreateTime
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
       /// 图片链接
       /// </summary>
       public virtual string PicUrl
       {
           get;
           set;
       }

       /// <summary>
       /// 图片消息媒体id
       /// </summary>
       public virtual string MediaId
       {
           get;
           set;
       }

       /// <summary>
       /// 语音格式，如amr，speex等
       /// </summary>
       public virtual string Format
       {
           get;
           set;
       }

       /// <summary>
       /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据
       /// </summary>
       public virtual string ThumbMediaId
       {
           get;
           set;
       }

       /// <summary>
       /// 地理位置纬度
       /// </summary>
       public virtual string Location_X
       {
           get;
           set;
       }

       /// <summary>
       /// 地理位置精度
       /// </summary>
       public virtual string Location_Y
       {
           get;
           set;
       }

       /// <summary>
       /// 地图缩放大小
       /// </summary>
       public virtual int Scale
       {
           get;
           set;
       }

       /// <summary>
       /// 地理位置信息
       /// </summary>
       public virtual string Labe
       {
           get;
           set;
       }

       public virtual DateTime R_CreateTime
       {
           get;
           set;
       }

       /// <summary>
       /// 接收的原始xml数据.
       /// </summary>
       public virtual string R_XMLData
       {
           get;
           set;
       }

       /// <summary>
       /// 消息标题
       /// </summary>
       public virtual string Title
       {
           get;
           set;
       }

       /// <summary>
       /// 消息描述
       /// </summary>
       public virtual string Description
       {
           get;
           set;
       }

       /// <summary>
       /// 消息链接
       /// </summary>
       public virtual string Url
       {
           get;
           set;
       }
    }
}
