using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using NHibernate;
using Newtonsoft.Json;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
    public  class PP_StaffinfoDao:ServiceConversion<PP_StaffinfoView,PP_Staffinfo>,IPP_StaffinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_Staffinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_StaffinfoView GetView(PP_Staffinfo data)
        {
            PP_StaffinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_StaffinfoView model)
        {
            PP_Staffinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_StaffinfoView model)
        {
            PP_Staffinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_StaffinfoView model)
        {
            PP_Staffinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_StaffinfoView> model)
        {
            IList<PP_Staffinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_StaffinfoView> NGetList()
        {
            List<PP_Staffinfo> listdata = base.GetList() as List<PP_Staffinfo>;
            IList<PP_StaffinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_StaffinfoView> NGetList_id(string id)
        {
            List<PP_Staffinfo> list = base.GetList_id(id) as List<PP_Staffinfo>;
            IList<PP_StaffinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_Staffinfo> NGetListID(string id)
        {
            IList<PP_Staffinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_StaffinfoView NGetModelById(string id)
        {
            PP_StaffinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 查询员工信息按照部门排序
        /// </summary>
        /// <returns></returns>
        public IList<PP_StaffinfoView> Getallyuangonginfo()
        {
            string sql = string.Format("from PP_Staffinfo  order by Sat_teamId");
            List<PP_Staffinfo> list = HibernateTemplate.Find<PP_Staffinfo>(sql) as List<PP_Staffinfo>;
            IList<PP_StaffinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        #region //个人（员工）信息管理列表

        /// <summary>
        /// 个人（员工）信息管理列表
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="tel">电话</param>
        /// <param name="bmname">部门</param>
        /// <param name="user">当前登陆的帐号</param>
        /// <returns></returns>
        public PagerInfo<PP_StaffinfoView> Getstaffinfopage(string name, string tel, string bmname, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(name))
               TempHql.AppendFormat(" and u.Sat_Name like '%{0}%'", name);
            if (!string.IsNullOrEmpty(tel))
                TempHql.AppendFormat(" and u.Sat_Tel like '%{0}%'",tel);
            if (!string.IsNullOrEmpty(bmname))
                TempHql.AppendFormat(" and u.Sat_teamId='{0}'",bmname);
            if (user.RoleType != "0")
            {
                TempHql.AppendFormat("and u.Sat_teamId  in (select Id from PP_Teaminfo where Team_glyuser='{0}')", user.Id);
            }
            //TempHql.AppendFormat(" order by C_time desc");
            PagerInfo<PP_StaffinfoView> list = Search();
            return list;
        }
        #endregion

        #region //查询该团队管理人员信息
        /// <summary>
        /// 查询该团队管理人员信息
        /// </summary>
        /// <param name="guanliyuanId">当前登录的Id</param>
        /// <returns></returns>
        public IList<PP_StaffinfoView> GetguanliyuanInfo(string guanliyuanId)
        {
            string hqlstr = string.Format("from PP_Staffinfo where type='1' and Sat_teamId in (select Id from PP_Teaminfo where Team_glyuser='{0}')",guanliyuanId);
            List<PP_Staffinfo> list = HibernateTemplate.Find<PP_Staffinfo>(hqlstr) as List<PP_Staffinfo>;
            IList<PP_StaffinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //查询员工数据 管理员查询全部员工信息团队管理员查询团队的员工
        /// <summary>
        /// 查询员工数据 管理员查询全部员工信息团队管理员查询团队的员工
        /// </summary>
        /// <param name="user">当前登录的帐号</param>
        /// <returns></returns>
        public IList<PP_StaffinfoView> GetyuangongbySuer(SessionUser user)
        {
            string hqlstr = "";
            if (user.RoleType != "0")
            {
                hqlstr = string.Format("from PP_Staffinfo where Sat_teamId in (select Id from PP_Teaminfo where Team_glyuser='{0}')", user.Id);
            }
            else
            {
                hqlstr = string.Format("from PP_Staffinfo");
            }
            List<PP_Staffinfo> list = HibernateTemplate.Find<PP_Staffinfo>(hqlstr) as List<PP_Staffinfo>;
            IList<PP_StaffinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

    }
}
