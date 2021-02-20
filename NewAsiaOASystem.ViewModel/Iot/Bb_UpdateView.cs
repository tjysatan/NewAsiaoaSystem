using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.ViewModel
{
    //授权代码表
    public class Bb_UpdateView
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
        /// apk版本号
        /// </summary>
        public virtual string Bb_NO
        {
            get;
            set;
        }

        /// <summary>
        /// APK名称
        /// </summary>
        public virtual string ApkName
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual string P_Id
        {
            get;
            set;
        }

        /// <summary>
        /// Apk类型（支持安卓，苹果，wp上传）
        /// </summary>
        public virtual string ApkType
        {
            get;
            set;
        }

        /// <summary>
        /// 上传路径
        /// </summary>
        public virtual string Bb_Url
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? Bb_Time
        {
            get;
            set;
        }
    }
}