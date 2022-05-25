using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;
using Newtonsoft.Json;

namespace NewAsiaOASystem.Dao
{
    public class ERP_SASalA_FBCP_BCBWKDao : ServiceConversion<ERP_SASalA_FBCP_BCBWKView, ERP_SASalA_FBCP_BCBWK>, IERP_SASalA_FBCP_BCBWKDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(ERP_SASalA_FBCP_BCBWK).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(ERP_SASalA_FBCP_BCBWKView model)
        {
            ERP_SASalA_FBCP_BCBWK listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(ERP_SASalA_FBCP_BCBWKView model)
        {
            ERP_SASalA_FBCP_BCBWK listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(ERP_SASalA_FBCP_BCBWKView model)
        {
            ERP_SASalA_FBCP_BCBWK listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<ERP_SASalA_FBCP_BCBWKView> model)
        {
            IList<ERP_SASalA_FBCP_BCBWK> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<ERP_SASalA_FBCP_BCBWKView> NGetList()
        {
            List<ERP_SASalA_FBCP_BCBWK> listdata = base.GetList() as List<ERP_SASalA_FBCP_BCBWK>;
            IList<ERP_SASalA_FBCP_BCBWKView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<ERP_SASalA_FBCP_BCBWK> listdata = base.GetList() as List<ERP_SASalA_FBCP_BCBWK>;
            IList<ERP_SASalA_FBCP_BCBWKView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<ERP_SASalA_FBCP_BCBWKView> NGetList_id(string id)
        {
            List<ERP_SASalA_FBCP_BCBWK> list = base.GetList_id(id) as List<ERP_SASalA_FBCP_BCBWK>;
            IList<ERP_SASalA_FBCP_BCBWKView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<ERP_SASalA_FBCP_BCBWK> NGetListModel(string id)
        {
            IList<ERP_SASalA_FBCP_BCBWK> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ERP_SASalA_FBCP_BCBWKView NGetModelById(string id)
        {
            ERP_SASalA_FBCP_BCBWKView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据账期查询非标产品包含的半成品温控的数据
        /// <summary>
        /// 根据账期查询非标产品包含的半成品温控的数据
        /// </summary>
        /// <param name="AbsID"></param>
        /// <returns></returns>
        public IList<ERP_SASalA_FBCP_BCBWKView> GetFB_WKDATA_by_AbsID(string AbsID)
        {
            string HQLstr = string.Format(" from ERP_SASalA_FBCP_BCBWK where AbsID='{0}'", AbsID);
            List<ERP_SASalA_FBCP_BCBWK> list = HibernateTemplate.Find<ERP_SASalA_FBCP_BCBWK>(HQLstr) as List<ERP_SASalA_FBCP_BCBWK>;
            IList<ERP_SASalA_FBCP_BCBWKView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
