using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWeChat_AnnualmeetingSignDao:IBaseDao<WeChat_AnnualmeetingSignView>
    {
         //检测是否有重复的openid
        bool Jcopenid(string openid);

        //根据OPENID 查找微信好友的信息
        WeChat_AnnualmeetingSignView Getwxinfobyopenid(string openid);

        //查找报名数量
        int Getsignupcount();
    }
}
