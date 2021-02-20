using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate;
using Spring.Context.Support;
using System.IO;

namespace NewAsiaOASystem.Dao
{
    public class AppPersonDao : ServiceConversion<SysPersonView, SysPerson>, IAppSysPersonDao
    {
        #region DATA 转换成 TDO
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override SysPersonView GetView(SysPerson data)
        {
            SysPersonView view = ConvertToDTO(data);
            if (data.Department != null)
            {
                List<SysDepartment> dept = data.Department.ToList();
                dept = dept.Where(x => x != null).ToList<SysDepartment>();
                view.Department = JsonConvert.SerializeObject(dept);
            }
            if (data.Role != null)
            {
                List<SysRole> role = data.Role.ToList();
                role = role.Where(x => x != null).ToList<SysRole>();
                view.Role = JsonConvert.SerializeObject(role);
            }
            return view;
        }

        #endregion

        #region 用户登录
        /// <summary>
        /// App登录
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public string login(string LoginId, string passwd)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginId) || string.IsNullOrEmpty(passwd))
                    return "{\"Id\":\"\",\"Name\":\"\",\"Dept\":[],\"Role\":[],\"ico\":\"\",\"Status\":\"false\"}";
                List<SysPerson> list = HibernateTemplate.Find<SysPerson>("from SysPerson where Name='" + LoginId + "' and Password='" + passwd + "'") as List<SysPerson>;
                if (list != null && list.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("{\"Id\":\"" + list[0].Id + "\",\"Name\":\"" + list[0].UserName + "\",\"Dept\":");//用户名
                    StringBuilder deptSB = new StringBuilder();
                    StringBuilder roleSB = new StringBuilder();
                    deptSB.Append("[");
                    roleSB.Append("[");
                    if (list[0].Department != null)
                    {
                        List<SysDepartment> dept = list[0].Department.ToList();
                        dept = dept.Where(x => x != null).ToList<SysDepartment>();

                        for (int i = 0; i < dept.Count; i++)
                        {
                            //deptSB.AppendFormat("{'Id':'{0}','Name':'{1}'},", dept[i].Id, dept[i].Name);
                            deptSB.Append("{'Id':'").Append(dept[i].Id).Append("','Name':'").Append(dept[i].Name).Append("'},");
                        }

                        if (dept.Count > 0)
                            deptSB.Remove(deptSB.Length - 1, 1);

                    }
                    if (list[0].Role != null)
                    {
                        List<SysRole> role = list[0].Role.ToList();
                        role = role.Where(x => x != null).ToList<SysRole>();
                        for (int i = 0; i < role.Count; i++)
                        {
                            roleSB.Append("{'Id':'").Append(role[i].Id).Append("','Name':'").Append(role[i].Name).Append("'},");
                            //roleSB.AppendFormat("{'Id':'{0}','Name':'{1}'},", role[i].Id, role[i].Name);
                        }

                        if (role.Count > 0)
                            roleSB.Remove(roleSB.Length - 1, 1);
                    }

                    deptSB.Append("]");
                    roleSB.Append("]");
                    sb.Append(deptSB.ToString()).Append(",\"Role\":").Append(roleSB.ToString())
                        .Append(",\"ico\":\"").Append(list[0].Url).Append("\",\"Status\":\"true\"}");
                    return sb.ToString();
                }
                return "{\"Id\":\"\",\"Name\":\"\",\"Dept\":[],\"Role\":[],\"ico\":\"\",\"Status\":\"false\"}";
            }

            catch
            {
                return "{\"Id\":\"\",\"Name\":\"\",\"Dept\":[],\"Role\":[],\"ico\":\"\",\"Status\":\"false\"}";
            }
        }
        #endregion

        #region 密码修改
        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="LoginId">账号</param>
        /// <param name="OldPassword">旧密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public string UpdatePasswod(string LoginId, string OldPassword, string NewPassword)
        {
            try
            {
                List<SysPerson> list = HibernateTemplate.Find<SysPerson>("from SysPerson where Name='" + LoginId + "' and Password='" + OldPassword + "'") as List<SysPerson>;
                if (list != null && list.Count > 0)
                {
                    SysPerson person = list[0];
                    person.Password = NewPassword;
                    base.Update(person);
                    return "{\"status\":\"true\"}";
                }

                return "{\"status\":\"false\"}";
            }

            catch
            {
                return "{\"status\":\"false\"}";
            }
        }

        #endregion

        #region 用户信息修改
        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public string UpdatePersonInfo(SysPersonView view)
        {
            try
            {
                SysPerson person = base.GetModelById(view.Id);
                person.UpdateTime = DateTime.Now;//最后修改时间
                person.UpdatePerson = person.Name;//修改人账号
                person.UserName = view.UserName;//姓名
                person.Tel = view.Tel;//电话
                if (base.Update(person))
                    return "{\"status\":\"true\"}";
                else
                    return "{\"status\":\"false\"}";
            }

            catch
            {
                return "{\"status\":\"false\"}";
            }
        }

        #endregion

        #region 头像上传
        /// <summary>
        /// 头像上传
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        public bool UploadHeadPortrait(string Id,string url)
        {
            try
            {
                SysPerson person = base.GetModelById(Id);
                person.Url = url;//修改头像路径
                return base.Update(person);
            }

            catch
            {
                return false;
            }
        }

        #endregion
    }
}
