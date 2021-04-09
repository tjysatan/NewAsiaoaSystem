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
    public class DKX_GCSinfoDao:ServiceConversion<DKX_GCSinfoView,DKX_GCSinfo>,IDKX_GCSinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_GCSinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_GCSinfoView GetView(DKX_GCSinfo data)
        {
            DKX_GCSinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_GCSinfoView model)
        {
            DKX_GCSinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_GCSinfoView model)
        {
            DKX_GCSinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_GCSinfoView model)
        {
            DKX_GCSinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_GCSinfoView> model)
        {
            IList<DKX_GCSinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_GCSinfoView> NGetList()
        {
            List<DKX_GCSinfo> listdata = base.GetList() as List<DKX_GCSinfo>;
            IList<DKX_GCSinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_GCSinfoView> NGetList_id(string id)
        {
            List<DKX_GCSinfo> list = base.GetList_id(id) as List<DKX_GCSinfo>;
            IList<DKX_GCSinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_GCSinfo> NGetListID(string id)
        {
            IList<DKX_GCSinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_GCSinfoView NGetModelById(string id)
        {
            DKX_GCSinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //电气工程师数据分页列表
        /// <summary>
        /// 电气工程师数据分页列表
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="zhname">管理的帐号</param>
        /// <param name="tel">电话</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        public PagerInfo<DKX_GCSinfoView> GetGCSlistpage(string Name, string zhname, string tel, string start)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and Name like '%{0}%'", Name);
            if (!string.IsNullOrEmpty(tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'",tel);
            if (!string.IsNullOrEmpty(zhname))
                TempHql.AppendFormat(" and ZH_Id in (select Id from SysPerson where Name like '%{0}%')",zhname);
            if(!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and start='{0}'", start);
            else
                TempHql.AppendFormat(" and start='0'");
            TempHql.AppendFormat("order by u.Sort asc");
            PagerInfo<DKX_GCSinfoView> list = Search();
            return list;
        }
        #endregion

        #region //保存后返回ID
        public string InsertID(DKX_GCSinfoView modelView)
        {
            DKX_GCSinfo model = GetData(modelView);
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

        #region //根据类型的Id 查找对应的工程师数据
        /// <summary>
        /// 根据类型的Id 查找对应的工程师数据
        /// </summary>
        /// <param name="dkxtypeId">电控箱类型的Id</param>
        /// <returns></returns>
        public IList<DKX_GCSinfoView> GetgcsinfobydkxtypeId(string dkxtypeId)
        {
            string Hqlstr = string.Format(" from DKX_GCSinfo where Id in(select gcsId from DKX_DKXtypeandgcs where DkxtypeId='{0}') and Start='0'", dkxtypeId);
            List<DKX_GCSinfo> list = HibernateTemplate.Find<DKX_GCSinfo>(Hqlstr) as List<DKX_GCSinfo>;
            IList<DKX_GCSinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //通过账号Id查询工程师数据
        /// <summary>
        /// 通过账号Id查询工程师数据
        /// </summary>
        /// <param name="ZHId">关联账号的Id</param>
        /// <returns></returns>
        public DKX_GCSinfoView Getdkx_gscmodelbyuserId(string ZHId)
        {
            string Hqlstr = string.Format(" from DKX_GCSinfo where ZH_Id='{0}'", ZHId);
            List<DKX_GCSinfo> list = HibernateTemplate.Find<DKX_GCSinfo>(Hqlstr) as List<DKX_GCSinfo>;
            IList<DKX_GCSinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //查找全部工程师的数据
        public IList<DKX_GCSinfoView> GetALLgcsDATA()
        {
            string Hqlstr = string.Format(" from DKX_GCSinfo where  Start='0' order by Sort asc");
            List<DKX_GCSinfo> list = HibernateTemplate.Find<DKX_GCSinfo>(Hqlstr) as List<DKX_GCSinfo>;
            IList<DKX_GCSinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
