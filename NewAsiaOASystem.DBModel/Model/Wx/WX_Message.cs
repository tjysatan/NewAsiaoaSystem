using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 关键词及订阅（关注）回复
    /// </summary>
   public class WX_Message
    {
        /// <summary>
        /// Id 编码
        /// </summary>
        public virtual string A_id
        {
            get;
            set;
        }
       /// <summary>
       /// 关键词
       /// </summary>
        public virtual string A_KeyWord
        {
            get;
            set;
        }

       /// <summary>
        /// 回复的类型  text/image/voice/video/music/news
       /// </summary>
        public virtual string MsgType
        {
            get;
            set;
        }

       /// <summary>
       /// 回复消息的内容
       /// </summary>
        public virtual string A_Content
        {
            get;
            set;
        }

       /// <summary>
       /// 通过上传多媒体文件得到的Id
       /// </summary>
        public virtual string MediaId
        {
            get;
            set;
        }

       /// <summary>
       /// 视频消息的/音乐消息的 标题
       /// </summary>
        public virtual string Title
        {
            get;
            set;
        }

       /// <summary>
        /// 视频消息的/音乐消息的 描述
       /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

       /// <summary>
       /// 音乐地址
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
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id
       /// </summary>
        public virtual string ThumbMediaId
        {
            get;
            set;
        }

        public virtual DateTime A_CreateTime
        {
            get;
            set;
        }

        public virtual string A_CreateUser
        {
            get;
            set;
        }

        public virtual string A_IsValid
        {
            get;
            set;
        }
       /// <summary>
        /// 图文消息个数，限制为10条以内
       /// </summary>
        public virtual string ArticleCount
        {
            get;
            set;
        }

       /// <summary>
        /// 默认回复-只能有一个为真
       /// </summary>
        public virtual string IsDefaultMessage
        {
            get;
            set;
        }

       /// <summary>
       /// 对应多个图文消息
       /// </summary>
        public virtual IList<WX_Message_News> wx_Message_News
        {
            get;
            set;
        }
    }
}
