using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWeChat_forwardInfoDao:IBaseDao<WeChat_forwardInfoView>
    {
        //检测是否有重复的openid
        bool Jcopenid(string openid);

        PagerInfo<WeChat_forwardInfoView> GetWXHDcusList();

        #region //直接点击量和间接统计量
        /// <summary>
        /// 直接统计量
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        int GetzjtjcunotbyOpenId(string OpenId);

        /// <summary>
        /// 间接统计量
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        int GetjjtjcunotbyOpenId(string OpenId); 
        #endregion

          //根据OPENID 查找微信好友的信息
        WeChat_forwardInfoView Getwxinfobyopenid(string openid);
    }
}
