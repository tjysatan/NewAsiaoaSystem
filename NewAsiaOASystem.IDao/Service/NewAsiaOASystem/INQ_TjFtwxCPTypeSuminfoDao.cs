using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_TjFtwxCPTypeSuminfoDao:IBaseDao<NQ_TjFtwxCPTypeSuminfoView>
    {
        //根据产品Id检查是否存在改产品
        bool JccpIscz(string cpId);

          /// <summary>
        /// 根据数量多少排序（正序倒序查询）
        /// </summary>
        /// <param name="type">0 从大到小 1从小到大</param>
        /// <returns></returns>
        IList<NQ_TjFtwxCPTypeSuminfoView> GetALLInfoorderby(string type);

            /// <summary>
        /// 根据产品Id和时间检测是否该产品已经存在
        /// </summary>
        /// <param name="cpId">产品Id</param>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <returns></returns>
        bool JccpIsczbycpIDandYM(string cpId, string Year, string Mon);


        /// <summary>
        /// 根据元器件Id和时间检测是否元器件是否已经存在
        /// </summary>
        /// <param name="YQJId"></param>
        /// <param name="Year"></param>
        /// <param name="Mon"></param>
        /// <returns></returns>
        bool JcYQJIsczbyyqjIdandYM(string YQJId, string Year, string Mon);

        
        /// <summary>
        /// 根据统计类型年份月份查找统计数据
        /// </summary>
        /// <param name="tjtype">统计类型</param>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <returns></returns>
        IList<NQ_TjFtwxCPTypeSuminfoView> GetTjftinfobytjtypeandYM(string tjtype, string Year, string Mon);

          /// <summary>
        /// 根据不良原因和时间检测是否存在
        /// </summary>
        /// <param name="blyyId"></param>
        /// <param name="Year"></param>
        /// <param name="Mon"></param>
        /// <returns></returns>
        bool JcblyyIsczbyblyyIdandYM(string blyyId, string Year, string Mon);

        /// <summary>
        /// 查询返退产品的统计
        /// </summary>
        /// <param name="tjtype">统计种类</param>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <param name="pxtype">排序</param>
        /// <returns></returns>
        IList<NQ_TjFtwxCPTypeSuminfoView> GetTjinfobytypeandYm(string tjtype, string Year, string Mon, string pxtype);
    }
}
