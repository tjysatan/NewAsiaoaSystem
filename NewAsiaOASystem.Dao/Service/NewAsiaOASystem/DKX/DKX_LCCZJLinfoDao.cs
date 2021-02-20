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
    public class DKX_LCCZJLinfoDao:ServiceConversion<DKX_LCCZJLinfoView,DKX_LCCZJLinfo>,IDKX_LCCZJLinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_LCCZJLinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_LCCZJLinfoView GetView(DKX_LCCZJLinfo data)
        {
            DKX_LCCZJLinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_LCCZJLinfoView model)
        {
            DKX_LCCZJLinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_LCCZJLinfoView model)
        {
            DKX_LCCZJLinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_LCCZJLinfoView model)
        {
            DKX_LCCZJLinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_LCCZJLinfoView> model)
        {
            IList<DKX_LCCZJLinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_LCCZJLinfoView> NGetList()
        {
            List<DKX_LCCZJLinfo> listdata = base.GetList() as List<DKX_LCCZJLinfo>;
            IList<DKX_LCCZJLinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_LCCZJLinfoView> NGetList_id(string id)
        {
            List<DKX_LCCZJLinfo> list = base.GetList_id(id) as List<DKX_LCCZJLinfo>;
            IList<DKX_LCCZJLinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_LCCZJLinfo> NGetListID(string id)
        {
            IList<DKX_LCCZJLinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_LCCZJLinfoView NGetModelById(string id)
        {
            DKX_LCCZJLinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查找记录根据订单Id
        /// <summary>
        /// 查找记录根据订单Id
        /// </summary>
        /// <param name="oId">订单Id</param>
        /// <returns></returns>
        public IList<DKX_LCCZJLinfoView> GetlcczjldatabyoId(string oId)
        {
            string Hqlstr = string.Format("from DKX_LCCZJLinfo where dd_Id='{0}' order by c_time desc", oId);
            List<DKX_LCCZJLinfo> list = HibernateTemplate.Find<DKX_LCCZJLinfo>(Hqlstr) as List<DKX_LCCZJLinfo>;
            IList<DKX_LCCZJLinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion

        #region //根据条件查找操作记录列表
        /// <summary>
        /// 根据条件查找操作记录列表
        /// </summary>
        /// <param name="constr">操作内容</param>
        /// <param name="ddid">订单Id</param>
        /// <param name="gcsid">工程师Id</param>
        /// <param name="ddbianhao">订单编号</param>
        /// <param name="c_timestart">创建时间</param>
        /// <param name="c_timeend"></param>
        /// <returns></returns>
        public IList<DKX_LCCZJLinfoView> Getlcczjldatabycondition(string ddid, string gcsid, string ddbianhao, string c_timestart, string c_timeend,string CBRId)
        {
             DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            TempList = new List<string>();
            TempHql = new StringBuilder();
            TempHql.AppendFormat(" from DKX_LCCZJLinfo where ");
            TempHql.AppendFormat("  caozuotype='0'");
            if (!string.IsNullOrEmpty(ddid))
                TempHql.AppendFormat(" and dd_Id = '{0}'", ddid);
            if (!string.IsNullOrEmpty(gcsid))
                TempHql.AppendFormat(" and gcs_Id='{0}'", gcsid);
            if (!string.IsNullOrEmpty(ddbianhao))
                TempHql.AppendFormat(" and dd_bianhao  like '%{0}%'", ddbianhao);
            if (!string.IsNullOrEmpty(CBRId))
                TempHql.AppendFormat(" and CBRId='{0}'", CBRId);
            if (!string.IsNullOrEmpty(c_timestart) && !string.IsNullOrEmpty(c_timeend))
                TempHql.AppendFormat("and c_time>='{0}' and c_time<='{1}'", c_timestart + " 00:00:00", c_timeend + " 23:59:59");
            else
                TempHql.AppendFormat("and c_time>='{0}' and c_time<='{1}'", ksdate + " 00:00:00", jsdate + " 23:59:59");
          
            List<DKX_LCCZJLinfo> list = HibernateTemplate.Find<DKX_LCCZJLinfo>(TempHql.ToString()) as List<DKX_LCCZJLinfo>;
            IList<DKX_LCCZJLinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;

        }
        #endregion

        #region //查询所有工程师ID为null的生产退单
        public IList<DKX_LCCZJLinfoView> Getzllistdata()
        {
            string Hqlstr = string.Format("from DKX_LCCZJLinfo where caozuo like '%生产返退%' and gcs_Id is NULL");
            List<DKX_LCCZJLinfo> list = HibernateTemplate.Find<DKX_LCCZJLinfo>(Hqlstr) as List<DKX_LCCZJLinfo>;
            IList<DKX_LCCZJLinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;

        }
        #endregion
    }
}
