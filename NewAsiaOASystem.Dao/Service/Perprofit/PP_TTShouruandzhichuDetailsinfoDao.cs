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
    public class PP_TTShouruandzhichuDetailsinfoDao:ServiceConversion<PP_TTShouruandzhichuDetailsinfoView,PP_TTShouruandzhichuDetailsinfo>,IPP_TTShouruandzhichuDetailsinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_TTShouruandzhichuDetailsinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_TTShouruandzhichuDetailsinfoView GetView(PP_TTShouruandzhichuDetailsinfo data)
        {
            PP_TTShouruandzhichuDetailsinfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_TTShouruandzhichuDetailsinfoView model)
        {
            PP_TTShouruandzhichuDetailsinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_TTShouruandzhichuDetailsinfoView model)
        {
            PP_TTShouruandzhichuDetailsinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_TTShouruandzhichuDetailsinfoView model)
        {
            PP_TTShouruandzhichuDetailsinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_TTShouruandzhichuDetailsinfoView> model)
        {
            IList<PP_TTShouruandzhichuDetailsinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_TTShouruandzhichuDetailsinfoView> NGetList()
        {
            List<PP_TTShouruandzhichuDetailsinfo> listdata = base.GetList() as List<PP_TTShouruandzhichuDetailsinfo>;
            IList<PP_TTShouruandzhichuDetailsinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_TTShouruandzhichuDetailsinfoView> NGetList_id(string id)
        {
            List<PP_TTShouruandzhichuDetailsinfo> list = base.GetList_id(id) as List<PP_TTShouruandzhichuDetailsinfo>;
            IList<PP_TTShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_TTShouruandzhichuDetailsinfo> NGetListID(string id)
        {
            IList<PP_TTShouruandzhichuDetailsinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_TTShouruandzhichuDetailsinfoView NGetModelById(string id)
        {
            PP_TTShouruandzhichuDetailsinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //根据团队ID 查找该团队当月的团体支出的明细
        #region //根据团队ID 查找该团队当月的团体支出的明细()
        /// <summary>
        /// 根据团队ID 查找该团队当月的团体支出的明细
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="type">类型 0 收入 1支出</param>
        /// <returns></returns>
        public IList<PP_TTShouruandzhichuDetailsinfoView> GetTTszMxtoMonbyteamId(string teamId, string type)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string starttime = d1.ToString("yyyyMMdd");
            string endtime = d2.ToString("yyyyMMdd");
            string Hqlstr = string.Format("from PP_TTShouruandzhichuDetailsinfo where TeamId='{0}' and type='{1}' and jl_time>='{2}' and jl_time<='{3}' and TT_type='0' order by jl_time", teamId, type, starttime, endtime);
            List<PP_TTShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_TTShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_TTShouruandzhichuDetailsinfo>;
            IList<PP_TTShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //根据团队Id项目Id完成时间查找团体项目 明细
        /// <summary>
        /// 根据团队Id项目Id完成时间查找团体项目 明细
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="profuId">项目Id</param>
        /// <param name="wctime">完成时间</param>
        /// <returns></returns>
        public PP_TTShouruandzhichuDetailsinfoView GetTTSZMXinfobyteamIdandprofuIdandwctime(string teamId, string profuId, string wctime)
        {
            string Hqlstr = string.Format("from PP_TTShouruandzhichuDetailsinfo where TeamId='{0}' and ProfutId='{1}' and jl_time='{2}' and  TT_type='0'", teamId, profuId, wctime);
            List<PP_TTShouruandzhichuDetailsinfo> list = HibernateTemplate.Find<PP_TTShouruandzhichuDetailsinfo>(Hqlstr) as List<PP_TTShouruandzhichuDetailsinfo>;
            IList<PP_TTShouruandzhichuDetailsinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //插入保存 返回Id
         public string InsertID(PP_TTShouruandzhichuDetailsinfoView modelView)
        {
            PP_TTShouruandzhichuDetailsinfo model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
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

        #region //团队（不固定分配）收入项分页数据
        /// <summary>
         /// 团队（不固定分配）收入项分页数据
        /// </summary>
        /// <param name="srname"></param>
        /// <param name="jltime"></param>
        /// <param name="Iswcfp"></param>
        /// <param name="TeamId"></param>
        /// <returns></returns>
         public PagerInfo<PP_TTShouruandzhichuDetailsinfoView> GetTTshourutwelistpage(string srname, string jltime, string Iswcfp, string TeamId)
         {
             TempList = new List<string>();
             TempHql = new StringBuilder();
             if (!string.IsNullOrEmpty(srname))
                 TempHql.AppendFormat(" and u.ProfutId  in (select Id from PP_Profitpointinfo where Rwname like '%{0}%')", srname);
             if (!string.IsNullOrEmpty(jltime))
                 TempHql.AppendFormat(" and u.jl_time='{0}'",jltime);
             if (!string.IsNullOrEmpty(Iswcfp))
                 TempHql.AppendFormat(" and u.Iswcfp='{0}'",Iswcfp);
             if (!string.IsNullOrEmpty(TeamId))
                 TempHql.AppendFormat(" and u.TeamId='{0}'",TeamId);
             TempHql.AppendFormat(" and u.TT_type='1'");
             TempHql.AppendFormat(" order by C_time asc");
             PagerInfo<PP_TTShouruandzhichuDetailsinfoView> list = Search();
             return list;
         }
        #endregion
         
    }
}
