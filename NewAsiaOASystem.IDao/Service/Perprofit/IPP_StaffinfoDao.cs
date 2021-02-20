using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPP_StaffinfoDao:IBaseDao<PP_StaffinfoView>
    {
        IList<PP_StaffinfoView> Getallyuangonginfo();

        /// <summary>
        /// 个人（员工）信息管理列表
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="tel">电话</param>
        /// <param name="bmname">部门</param>
        /// <param name="user">当前登陆的帐号</param>
        /// <returns></returns>
        PagerInfo<PP_StaffinfoView> Getstaffinfopage(string name, string tel, string bmname, SessionUser user);

        /// <summary>
        /// 查询该团队管理人员信息
        /// </summary>
        /// <param name="guanliyuanId">当前登录的Id</param>
        /// <returns></returns>
        IList<PP_StaffinfoView> GetguanliyuanInfo(string guanliyuanId);

        /// <summary>
        /// 查询员工数据 管理员查询全部员工信息团队管理员查询团队的员工
        /// </summary>
        /// <param name="user">当前登录的帐号</param>
        /// <returns></returns>
        IList<PP_StaffinfoView> GetyuangongbySuer(SessionUser user);
    }
}
