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
    public class DKX_CPInfoDao:ServiceConversion<DKX_CPInfoView,DKX_CPInfo>,IDKX_CPInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_CPInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_CPInfoView GetView(DKX_CPInfo data)
        {
            DKX_CPInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_CPInfoView model)
        {
            DKX_CPInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_CPInfoView model)
        {
            DKX_CPInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_CPInfoView model)
        {
            DKX_CPInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_CPInfoView> model)
        {
            IList<DKX_CPInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_CPInfoView> NGetList()
        {
            List<DKX_CPInfo> listdata = base.GetList() as List<DKX_CPInfo>;
            IList<DKX_CPInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_CPInfoView> NGetList_id(string id)
        {
            List<DKX_CPInfo> list = base.GetList_id(id) as List<DKX_CPInfo>;
            IList<DKX_CPInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_CPInfo> NGetListID(string id)
        {
            IList<DKX_CPInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_CPInfoView NGetModelById(string id)
        {
            DKX_CPInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据产品名称 功率值 和功率单位查询是否存在相同的产品
        /// <summary>
        /// 根据产品名称 功率值 和功率单位查询是否存在相同的产品
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="Power">功率值</param>
        /// <param name="DW">单位</param>
        /// <returns></returns>
        public DKX_CPInfoView Getcpdatabynameandpowanddw(string name, string Power, string DW, string cpgnjs)
        {
            string Hqlstr = string.Format(" from DKX_CPInfo where Cpname='{0}' and Power='{1}' and DW='{2}' and cpgnjs='{3}' and IsDis!='4'", name, Power, DW,cpgnjs);
            List<DKX_CPInfo> list = HibernateTemplate.Find<DKX_CPInfo>(Hqlstr) as List<DKX_CPInfo>;
            IList<DKX_CPInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //保存后返回ID
        public string InsertID(DKX_CPInfoView modelView)
        {
            DKX_CPInfo model = GetData(modelView);
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

        #region //产品分页列表，通过入库时间排序
        /// <summary>
        /// 产品分页列表，通过入库时间排序
        /// </summary>
        /// <param name="cpname">产品型号（名称）</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="Type">类型 0小系统 1 大系统 2 物联网 </param>
        /// <param name="ft">是否分体 0 一体 1 分体</param>
        /// <param name="Gcs_name">工程师名称</param>
        /// <returns></returns>
        public PagerInfo<DKX_CPInfoView> GetDKXcppagelist(string cpname, string gl, string dw, string Type, string ft,string Gcs_name,string gnjs)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(cpname))
                TempHql.AppendFormat(" and u.Cpname like '%{0}%'",cpname);
            if (!string.IsNullOrEmpty(gl))
                TempHql.AppendFormat(" and u.Power='{0}'",gl);
            if (!string.IsNullOrEmpty(dw))
                TempHql.AppendFormat(" and u.DW='{0}'",dw);
            if (!string.IsNullOrEmpty(Type))
                TempHql.AppendFormat(" and u.Type='{0}'", Type);
            if (!string.IsNullOrEmpty(ft))
                TempHql.AppendFormat(" and u.Isft='{0}'", ft);
            if (!string.IsNullOrEmpty(Gcs_name))
                TempHql.AppendFormat(" and u.Gcs_name='{0}'", Gcs_name);
            if (!string.IsNullOrEmpty(gnjs))
                TempHql.AppendFormat(" and u.cpgnjs like'%{0}%'", gnjs);
            TempHql.AppendFormat(" and u.IsDis='0'");
            TempHql.AppendFormat("order by u.RK_time desc");
            PagerInfo<DKX_CPInfoView> list = Search();
            return list;
        }
        #endregion

        #region //电控箱方案库全部数据
        /// <summary>
        /// 电控箱方案库全部数据
        /// </summary>
        /// <param name="cpname">产品型号（名称）</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="Type">类型 0小系统 1 大系统 2 物联网 </param>
        /// <param name="ft">是否分体 0 一体 1 分体</param>
        /// <param name="Gcs_name">工程师名称</param>
        /// <returns></returns>
        public PagerInfo<DKX_CPInfoView> GetALLDKXcppagelist(string cpname, string gl, string dw, string Type, string ft, string Gcs_name,string gnjs)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(cpname))
                TempHql.AppendFormat(" and u.Cpname like '%{0}%'", cpname);
            if (!string.IsNullOrEmpty(gl))
                TempHql.AppendFormat(" and u.Power='{0}'", gl);
            if (!string.IsNullOrEmpty(dw))
                TempHql.AppendFormat(" and u.DW='{0}'", dw);
            if (!string.IsNullOrEmpty(Type))
                TempHql.AppendFormat(" and u.Type='{0}'", Type);
            if (!string.IsNullOrEmpty(ft))
                TempHql.AppendFormat(" and u.Isft='{0}'", ft);
            if (!string.IsNullOrEmpty(Gcs_name))
                TempHql.AppendFormat(" and u.Gcs_name='{0}'", Gcs_name);
            if (!string.IsNullOrEmpty(gnjs))
                TempHql.AppendFormat(" and u.cpgnjs like '%{0}%'",gnjs);
            TempHql.AppendFormat(" and u.IsDis!='4'");
            TempHql.AppendFormat("order by u.IsDis desc, u.RK_time desc");
            PagerInfo<DKX_CPInfoView> list = Search();
            return list;
        }
        #endregion

        #region //通过控制器的查找产品
        /// <summary>
        /// 通过控制器的查找产品
        /// </summary>
        /// <param name="str">参数 控制器型号或者控制器物料代码</param>
        /// <param name="type">类型 0 型号 1物料代码</param>
        /// <returns></returns>
        public IList<DKX_CPInfoView> Getcpinfobyxhorwldm(string str,string type)
        {
            string Hqlstr = "";
            if(type=="0")
                Hqlstr = string.Format("from DKX_CPInfo where Id in ( select b.cpId from  DKX_k3Bominfo a, DKX_RKZLDataInfo b where a.FItemName='{0}'  and  a.FnumberBom=b.BomNo and b.Start='0' )",str);
            else
                Hqlstr = string.Format("from DKX_CPInfo where Id in ( select b.cpId from  DKX_k3Bominfo a, DKX_RKZLDataInfo b where a.FNumber='{0}'  and  a.FnumberBom=b.BomNo and b.Start='0' )", str);
            List<DKX_CPInfo> list = HibernateTemplate.Find<DKX_CPInfo>(Hqlstr) as List<DKX_CPInfo>;
            IList<DKX_CPInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //查询同一个大类中的产品数据
        /// <summary>
        /// 查询同一个大类的产品数据
        /// </summary>
        /// <param name="sanduanno">查询同一个大类的产品数据</param>
        /// <returns></returns>
        public int Getdaleichanpincount(string sanduanno)
        {
            string Hqlstr = string.Format(" from DKX_CPInfo where Ps_sanduanno='{0}'  ", sanduanno);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", Hqlstr));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //通过工程师查询全部的电控箱方案库数据
        /// <summary>
        /// 通过工程师查询全部的电控箱方案库数据
        /// </summary>
        /// <param name="Gcs_name">工程师</param>
        /// <returns></returns>
        public IList<DKX_CPInfoView> GetalldkxcpbygcsId(string Gcs_name)
        {
            string Hqlstr = string.Format("from DKX_CPInfo where Gcs_name='{0}'",Gcs_name);
            List<DKX_CPInfo> list = HibernateTemplate.Find<DKX_CPInfo>(Hqlstr) as List<DKX_CPInfo>;
            IList<DKX_CPInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion


    }
}
