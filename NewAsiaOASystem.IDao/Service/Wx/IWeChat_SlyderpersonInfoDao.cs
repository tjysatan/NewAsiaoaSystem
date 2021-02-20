using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWeChat_SlyderpersonInfoDao:IBaseDao<WeChat_SlyderpersonInfoView>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        //检测是否有重复的openid
        bool Jcopenid(string openid);

        //根据OPENID 查找微信好友的信息
        WeChat_SlyderpersonInfoView Getwxinfobyopenid(string openid);

        #region //获取中奖名单json(领奖和未领奖的)
        string GetZJjson(); 
        #endregion


        #region //列表
        PagerInfo<WeChat_SlyderpersonInfoView> GetCinfoList(string zjstr, string wintype);
        #endregion

        #region //根据stid 查找中奖信息
        WeChat_SlyderpersonInfoView Getinfobyzjstr(string Winstr); 
        #endregion
    }
}
