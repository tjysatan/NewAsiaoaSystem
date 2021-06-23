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
    public class IQC_SopInfoDao:ServiceConversion<IQC_SopInfoView,IQC_SopInfo>,IIQC_SopInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(IQC_SopInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IQC_SopInfoView GetView(IQC_SopInfo data)
        {
            IQC_SopInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(IQC_SopInfoView model)
        {
            IQC_SopInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(IQC_SopInfoView model)
        {
            IQC_SopInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IQC_SopInfoView model)
        {
            IQC_SopInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<IQC_SopInfoView> model)
        {
            IList<IQC_SopInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<IQC_SopInfoView> NGetList()
        {
            List<IQC_SopInfo> listdata = base.GetList() as List<IQC_SopInfo>;
            IList<IQC_SopInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<IQC_SopInfoView> NGetList_id(string id)
        {
            List<IQC_SopInfo> list = base.GetList_id(id) as List<IQC_SopInfo>;
            IList<IQC_SopInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<IQC_SopInfo> NGetListID(string id)
        {
            IList<IQC_SopInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQC_SopInfoView NGetModelById(string id)
        {
            IQC_SopInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //public PagerInfo<IQC_SopInfoView> GetCinfoList(string Name, SessionUser user)
        //{
        //    TempList = new List<string>();
        //    TempHql = new StringBuilder();
        //    if (!string.IsNullOrEmpty(Name))
        //        TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
        //    TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
        //    PagerInfo<CG_aqkcView> list = Search();
        //    return list;
        //}

        #region //来料检验标准的分页数据
        //来料检验标准的分页数据
        /// <summary>
        /// 来料检验标准的分页数据
        /// </summary>
        /// <param name="yqjname">元器件名称</param>
        /// <param name="yqjxh">元器件型号</param>
        /// <param name="yqjwldm">元器件物料代码</param>
        /// <param name="shzt">审核状态</param>
        /// <param name="fxdatetime">发行时间</param>
        /// <param name="Iszf">是否作废</param>
        /// <returns></returns>
        public PagerInfo<IQC_SopInfoView> GetIQC_Soppagelist(string yqjname, string yqjxh, string yqjwldm, string shzt, string fxdatetime, string Iszf)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(yqjname))
                TempHql.AppendFormat(" and u.YqjId in (select Id from NQ_YJinfo where Y_Name like '%{0}%')", yqjname);
            if (!string.IsNullOrEmpty(yqjxh))
                TempHql.AppendFormat(" and u.YqjId in (select Id from NQ_YJinfo where Y_Ggxh like '%{0}%')", yqjxh);
            if (!string.IsNullOrEmpty(yqjwldm))
                TempHql.AppendFormat(" and u.YqjId in (select Id from NQ_YJinfo where Yqjdm='{0}')", yqjwldm);
            if (!string.IsNullOrEmpty(shzt))
                TempHql.AppendFormat(" and u.Issh='{0}'", shzt);
            if (!string.IsNullOrEmpty(fxdatetime))
                TempHql.AppendFormat(" and u.Sopfaxing='{0}'", fxdatetime);
            if (!string.IsNullOrEmpty(Iszf))
                TempHql.AppendFormat(" and u.Iszf='{0}'", Iszf);
            TempHql.AppendFormat(" order by Yqjdm desc");
            PagerInfo<IQC_SopInfoView> list = Search();
            return list;
        } 
        #endregion

        #region //保存后返回ID
        public string InsertID(IQC_SopInfoView modelView)
        {
            IQC_SopInfo model = GetData(modelView);
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

        #region //通过元器件Id 查找是否有正常存在的检验作业单
        /// <summary>
        /// 通过元器件Id 查找是否有正常存在的检验作业单
        /// </summary>
        /// <param name="yjId">元器件Id</param>
        /// <returns></returns>
        public IQC_SopInfoView GetsopmodelbyyjId(string yjId)
        {
            string hqlstr = string.Format("from IQC_SopInfo where YqjId='{0}' and Iszf='0'",yjId);
            List<IQC_SopInfo> list = HibernateTemplate.Find<IQC_SopInfo>(hqlstr) as List<IQC_SopInfo>;
            IList<IQC_SopInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //通过元器件物料代理查询是否正常存在检验作业单
        /// <summary>
        /// 通过元器件物料代理查询是否正常存在检验作业单
        /// </summary>
        /// <param name="wlno">元器件物料代码</param>
        /// <returns></returns>
        public IQC_SopInfoView Getsopinfobyyqjwlno(string wlno)
        {
            string hqlstr = string.Format("from IQC_SopInfo where Yqjdm='{0}' and Iszf='0'", wlno);
            List<IQC_SopInfo> list = HibernateTemplate.Find<IQC_SopInfo>(hqlstr) as List<IQC_SopInfo>;
            IList<IQC_SopInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //查咋全部检验标准的数据
        /// <summary>
        /// 查询全部正常的检验标准的数据
        /// </summary>
        /// <returns></returns>
        public IList<IQC_SopInfoView> GetAllIQC_Soppagelist()
        {
            string hqlstr = string.Format("from IQC_SopInfo where  Iszf='0'");
            List<IQC_SopInfo> list = HibernateTemplate.Find<IQC_SopInfo>(hqlstr) as List<IQC_SopInfo>;
            IList<IQC_SopInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion



    }
}
