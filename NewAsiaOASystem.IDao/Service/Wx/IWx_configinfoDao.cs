using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWx_configinfoDao:IBaseDao<Wx_configinfoView>
    {
        //查询平台微信支付配置信息
        Wx_configinfoView Getweixinpayconfig();

        
        #region //查询蓝河微信的配置信息
        /// <summary>
        /// 查询蓝河微信的配置信息
        /// </summary>
        /// <returns></returns>
        Wx_configinfoView Getlanheweixinpayconfig(); 
        #endregion
    }
}
