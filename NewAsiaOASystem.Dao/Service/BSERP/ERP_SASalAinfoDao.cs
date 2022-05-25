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
    public class ERP_SASalAinfoDao : ServiceConversion<ERP_SASalAinfoView, ERP_SASalAinfo>, IERP_SASalAinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(ERP_SASalAinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(ERP_SASalAinfoView model)
        {
            ERP_SASalAinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(ERP_SASalAinfoView model)
        {
            ERP_SASalAinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(ERP_SASalAinfoView model)
        {
            ERP_SASalAinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<ERP_SASalAinfoView> model)
        {
            IList<ERP_SASalAinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<ERP_SASalAinfoView> NGetList()
        {
            List<ERP_SASalAinfo> listdata = base.GetList() as List<ERP_SASalAinfo>;
            IList<ERP_SASalAinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<ERP_SASalAinfo> listdata = base.GetList() as List<ERP_SASalAinfo>;
            IList<ERP_SASalAinfoView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<ERP_SASalAinfoView> NGetList_id(string id)
        {
            List<ERP_SASalAinfo> list = base.GetList_id(id) as List<ERP_SASalAinfo>;
            IList<ERP_SASalAinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<ERP_SASalAinfo> NGetListModel(string id)
        {
            IList<ERP_SASalAinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ERP_SASalAinfoView NGetModelById(string id)
        {
            ERP_SASalAinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过销售日期 例如 202110 查询本地获取的erp中的销售出货数据
        /// <summary>
        /// 通过销售日期 例如 202110 查询本地获取的erp中的销售出货数据
        /// </summary>
        /// <param name="AbsID">日期 例如202110</param>
        /// <returns></returns>
        public IList<ERP_SASalAinfoView> GetalldatabyAbsID(string AbsID)
        {
            string HQLstr = string.Format(" from ERP_SASalAinfo where AbsID='{0}'", AbsID);
            List<ERP_SASalAinfo> list = HibernateTemplate.Find<ERP_SASalAinfo>(HQLstr) as List<ERP_SASalAinfo>;
            IList<ERP_SASalAinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //分页数据
        /// <summary>
        /// 销售出货的分页数据
        /// </summary>
        /// <param name="AbsID">日期</param>
        /// <param name="CrdName">客户名称</param>
        /// <param name="ItmID">物料代码</param>
        /// <param name="ItmName">物料名称</param>
        /// <param name="ItmSpec">物料型号</param>
        /// <returns></returns>
        public PagerInfo<ERP_SASalAinfoView> GetSASalAPagerInfo(string AbsID, string CrdName, string ItmID,string ItmName,string ItmSpec)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(AbsID))
                TempHql.AppendFormat(" and AbsID='{0}'", AbsID);
            if (!string.IsNullOrEmpty(CrdName))
                TempHql.AppendFormat(" and CrdName like '%{0}%'", CrdName);
            if (!string.IsNullOrEmpty(ItmID))
                TempHql.AppendFormat(" and ItmID='{0}'", ItmID);
            if (!string.IsNullOrEmpty(ItmName))
                TempHql.AppendFormat(" and ItmName like '%{0}%'", ItmName);
            if (!string.IsNullOrEmpty(ItmSpec))
                TempHql.AppendFormat(" and ItmSpec like '%{0}%'", ItmSpec);
            TempHql.AppendFormat("order by u.DocNum desc");
            PagerInfo<ERP_SASalAinfoView> list = Search();
            return list;
        }
        #endregion

        #region //责任工程师分组汇总数据
        /// <summary>
        /// 责任工程师分组汇总数据
        /// </summary>
        /// <param name="AbsID">日期</param>
        /// <returns></returns>
        public List<Object> GetsumjineGROUPBY(string AbsID)
        {
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(AbsID))
                TempHql.AppendFormat(" and AbsID='{0}'", AbsID);
            TempHql.AppendFormat(" group by Z_EmpName");
            string HQLstr = string.Format("select Z_EmpName,SUM(XSCostPrice),SUM(LineSum),SUM(Amount) from ERP_SASalAinfo where  1=1 {0}", TempHql.ToString());
            List<Object> obj = Session.CreateQuery(HQLstr).List<Object>() as List<Object>;
            return obj;
        }
        #endregion

        #region //按照账期查询非标产品（已经同步过来的数据）
        /// <summary>
        /// 按照账期查询非标产品（已经同步过来的数据）
        /// </summary>
        /// <param name="AbsID"></param>
        /// <returns></returns>
        public IList<ERP_SASalAinfoView> GetFB_DATA_BY_AbsID(string AbsID)
        {
            string HQLstr = string.Format(" from ERP_SASalAinfo where AbsID='{0}' and ItmName='电控箱_非标'", AbsID);
            List<ERP_SASalAinfo> list = HibernateTemplate.Find<ERP_SASalAinfo>(HQLstr) as List<ERP_SASalAinfo>;
            IList<ERP_SASalAinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
