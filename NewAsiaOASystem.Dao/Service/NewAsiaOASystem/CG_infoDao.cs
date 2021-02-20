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
    public class CG_infoDao:ServiceConversion<CG_infoView,CG_info>,ICG_infoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(CG_info).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override CG_infoView GetView(CG_info data)
        {
            CG_infoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(CG_infoView model)
        {
            CG_info listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(CG_infoView model)
        {
            CG_info listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(CG_infoView model)
        {
            CG_info listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<CG_infoView> model)
        {
            IList<CG_info> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<CG_infoView> NGetList()
        {
            List<CG_info> listdata = base.GetList() as List<CG_info>;
            IList<CG_infoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<CG_infoView> NGetList_id(string id)
        {
            List<CG_info> list = base.GetList_id(id) as List<CG_info>;
            IList<CG_infoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<CG_info> NGetListID(string id)
        {
            IList<CG_info> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CG_infoView NGetModelById(string id)
        {
            CG_infoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }




        public PagerInfo<CG_infoView> GetCginfoList(string Name, string RL_is, string Stratrldate, string Endrldate, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.GysId ='{0}'", Name);
            if (!string.IsNullOrEmpty(RL_is))
                TempHql.AppendFormat(" and u.Cg_Isdh ='{0}' ", RL_is);
            if (!string.IsNullOrEmpty(Stratrldate) && !string.IsNullOrEmpty(Endrldate))
                TempHql.AppendFormat("and Cg_xdtime>='{0}' and Cg_xdtime<='{1}'", Stratrldate, Endrldate);
            //if (user.RoleType != "0")
            //{
            //    TempHql.AppendFormat("and u.JjId in ('{0}')", user.Id);
            //}
            TempHql.AppendFormat(" order by Cg_xdtime desc");
            PagerInfo<CG_infoView> list = Search();
            return list;
        }


        #region //保存后返回ID
        public string InsertID(CG_infoView modelView)
        {
            CG_info model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
                // Session.Save(model);
                string Id = model.Id;
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return Id;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


    }
}
