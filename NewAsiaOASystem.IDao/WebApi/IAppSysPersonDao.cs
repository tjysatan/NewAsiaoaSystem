using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IAppSysPersonDao
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        string login(string LoginId,string passwd);

        string UpdatePasswod(string LoginId, string OldPassword, string NewPassword);

        string UpdatePersonInfo(SysPersonView view);

        bool UploadHeadPortrait(string Id, string url);
    }
}
