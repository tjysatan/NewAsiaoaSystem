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
    public class PP_TeaminfoDao:ServiceConversion<PP_TeaminfoView,PP_Teaminfo>,IPP_TeaminfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_Teaminfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_TeaminfoView GetView(PP_Teaminfo data)
        {
            PP_TeaminfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_TeaminfoView model)
        {
            PP_Teaminfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_TeaminfoView model)
        {
            PP_Teaminfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_TeaminfoView model)
        {
            PP_Teaminfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_TeaminfoView> model)
        {
            IList<PP_Teaminfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_TeaminfoView> NGetList()
        {
            List<PP_Teaminfo> listdata = base.GetList() as List<PP_Teaminfo>;
            IList<PP_TeaminfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_TeaminfoView> NGetList_id(string id)
        {
            List<PP_Teaminfo> list = base.GetList_id(id) as List<PP_Teaminfo>;
            IList<PP_TeaminfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_Teaminfo> NGetListID(string id)
        {
            IList<PP_Teaminfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_TeaminfoView NGetModelById(string id)
        {
            PP_TeaminfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        /// <summary>
        /// 团队信息分析数据
        /// </summary>
        /// <param name="Name">团队名称</param>
        /// <param name="zrname">责任人</param>
        /// <param name="tel">电信</param>
        /// <returns></returns>
        public PagerInfo<PP_TeaminfoView> GetTeamList(string Name, string zrname, string tel)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Team_Name like '%{0}%'", Name);
            if (!string.IsNullOrEmpty(zrname))
                TempHql.AppendFormat(" and u.Team_zrname like '%{0}%'", zrname);
            if (!string.IsNullOrEmpty(tel))
                TempHql.AppendFormat(" and u.Team_zrTel like '%{0}%'", tel);
            TempHql.AppendFormat("order by C_time desc");
            PagerInfo<PP_TeaminfoView> list = Search();
            return list;
        }

        #region //根据团队Id 查找团队名称
        /// <summary>
        /// 根据团队Id 查找团队名称
        /// </summary>
        /// <param name="Id">团队Id</param>
        /// <returns></returns>
        public PP_TeaminfoView Getpp_teambyId(string Id)
        {
            string sql = string.Format("from PP_Teaminfo where Id='{0}'",Id);
            List<PP_Teaminfo> list = HibernateTemplate.Find<PP_Teaminfo>(sql) as List<PP_Teaminfo>;
            IList<PP_TeaminfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //根据管理员Id 查找团队信息
        /// <summary>
        /// 根据管理员Id 查找团队信息
        /// </summary>
        /// <param name="Id">当前登录用户的Id</param>
        /// <returns></returns>
        public PP_TeaminfoView Getpp_teambyguanliId(string Id)
        {
            string Hqlstr = string.Format(" from PP_Teaminfo where Team_glyuser='{0}'",Id);
            List<PP_Teaminfo> list = HibernateTemplate.Find<PP_Teaminfo>(Hqlstr) as List<PP_Teaminfo>;
            IList<PP_TeaminfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion
    }
}
