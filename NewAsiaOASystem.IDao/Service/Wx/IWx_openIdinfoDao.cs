using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWx_openIdinfoDao:IBaseDao<Wx_openIdinfoView>
    {
        /// <summary>
        /// 通过openId和代理名称查询微信客户信息
        /// </summary>
        /// <param name="openid">openId</param>
        /// <param name="dlname">代理名称</param>
        /// <returns></returns>
        Wx_openIdinfoView Getnewcusinfobyiccid(string openid, string dlname);

        /// <summary>
        /// 通过openId查找微信客户信息
        /// </summary>
        /// <param name="openid">openId</param>
        /// <returns></returns>
        Wx_openIdinfoView GetwxcusinfobyopenId(string openid);
    }
}
