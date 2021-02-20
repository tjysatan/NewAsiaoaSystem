using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    //巡查人员定位信息表
    public  class Point_infoView
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id
        {
            get;
            set;
        }

        /// <summary>
        ///   Location_X 纬度
        /// </summary>
        public virtual string Location_X
        {
            get;
            set;
        }

        /// <summary>
        ///  Location_Y 经度
        /// </summary>
        public virtual string Location_Y
        {
            get;
            set;
        }

        /// <summary>
        ///  P_Id 巡查人员的ID
        /// </summary>
        public virtual string P_Id
        {
            get;
            set;
        }

        /// <summary>
        /// P_Time 发送时间
        /// </summary>
        public virtual DateTime P_Time
        {
            get;
            set;
        }

        /// <summary>
        /// sysperson 巡查人员的信息
        /// </summary>
        public virtual string sysperson
        {
            get;
            set;
        }
    }
}
