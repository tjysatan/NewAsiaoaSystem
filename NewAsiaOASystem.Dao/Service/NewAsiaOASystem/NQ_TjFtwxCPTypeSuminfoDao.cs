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
    public class NQ_TjFtwxCPTypeSuminfoDao:ServiceConversion<NQ_TjFtwxCPTypeSuminfoView,NQ_TjFtwxCPTypeSuminfo>,INQ_TjFtwxCPTypeSuminfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_TjFtwxCPTypeSuminfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_TjFtwxCPTypeSuminfoView GetView(NQ_TjFtwxCPTypeSuminfo data)
        {
            NQ_TjFtwxCPTypeSuminfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_TjFtwxCPTypeSuminfoView model)
        {
            NQ_TjFtwxCPTypeSuminfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_TjFtwxCPTypeSuminfoView model)
        {
            NQ_TjFtwxCPTypeSuminfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_TjFtwxCPTypeSuminfoView model)
        {
            NQ_TjFtwxCPTypeSuminfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_TjFtwxCPTypeSuminfoView> model)
        {
            IList<NQ_TjFtwxCPTypeSuminfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_TjFtwxCPTypeSuminfoView> NGetList()
        {
            List<NQ_TjFtwxCPTypeSuminfo> listdata = base.GetList() as List<NQ_TjFtwxCPTypeSuminfo>;
            IList<NQ_TjFtwxCPTypeSuminfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_TjFtwxCPTypeSuminfoView> NGetList_id(string id)
        {
            List<NQ_TjFtwxCPTypeSuminfo> list = base.GetList_id(id) as List<NQ_TjFtwxCPTypeSuminfo>;
            IList<NQ_TjFtwxCPTypeSuminfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_TjFtwxCPTypeSuminfo> NGetListID(string id)
        {
            IList<NQ_TjFtwxCPTypeSuminfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_TjFtwxCPTypeSuminfoView NGetModelById(string id)
        {
            NQ_TjFtwxCPTypeSuminfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //根据产品Id检查是否存在改产品
        public bool JccpIscz(string cpId)
        {
            IList<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>("from NQ_TjFtwxCPTypeSuminfo where Cp_Id='" + cpId + "'");
            return list.Count > 0 ? true : false;
        }

        /// <summary>
        /// 根据产品Id和时间检测是否该产品已经存在
        /// </summary>
        /// <param name="cpId">产品Id</param>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <returns></returns>
        public bool JccpIsczbycpIDandYM(string cpId, string Year, string Mon)
        {
            string Hqlstr = string.Format(" from NQ_TjFtwxCPTypeSuminfo where Cp_Id='{0}' and FT_Year='{1}' and FT_Mone='{2}' and Tj_Type='0'",cpId,Year,Mon);
            List<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>(Hqlstr) as List<NQ_TjFtwxCPTypeSuminfo>;
            return list.Count > 0 ? true : false;
        }

        /// <summary>
        /// 根据元器件Id和时间检测是否元器件是否已经存在
        /// </summary>
        /// <param name="YQJId"></param>
        /// <param name="Year"></param>
        /// <param name="Mon"></param>
        /// <returns></returns>
        public bool JcYQJIsczbyyqjIdandYM(string YQJId, string Year, string Mon)
        {
            string Hqlstr = string.Format(" from NQ_TjFtwxCPTypeSuminfo where Cp_Id='{0}' and  FT_Year='{1}' and FT_Mone='{2}' and Tj_Type='2'",YQJId,Year,Mon);
            List<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>(Hqlstr) as List<NQ_TjFtwxCPTypeSuminfo>;
            return list.Count > 0 ? true : false;
        }

        /// <summary>
        /// 根据不良原因和时间检测是否存在
        /// </summary>
        /// <param name="blyyId"></param>
        /// <param name="Year"></param>
        /// <param name="Mon"></param>
        /// <returns></returns>
        public bool JcblyyIsczbyblyyIdandYM(string blyyId, string Year, string Mon)
        {
            string Hqlstr = string.Format(" from NQ_TjFtwxCPTypeSuminfo where Cp_Id='{0}' and  FT_Year='{1}' and FT_Mone='{2}' and Tj_Type='1' ",blyyId,Year,Mon);
            List<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>(Hqlstr) as List<NQ_TjFtwxCPTypeSuminfo>;
            return list.Count > 0 ? true : false;
        }

        //根据统计类型年份月份查找统计数据
        /// <summary>
        /// 根据统计类型年份月份查找统计数据
        /// </summary>
        /// <param name="tjtype">统计类型</param>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <returns></returns>
        public IList<NQ_TjFtwxCPTypeSuminfoView> GetTjftinfobytjtypeandYM(string tjtype, string Year, string Mon)
        {
            string Hqlstr = string.Format(" from NQ_TjFtwxCPTypeSuminfo where Tj_Type='{0}' and FT_Year='{1}' and FT_Mone='{2}'",tjtype,Year,Mon);
            List<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>(Hqlstr) as List<NQ_TjFtwxCPTypeSuminfo>;
            IList<NQ_TjFtwxCPTypeSuminfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        //
        /// <summary>
        /// 根据数量多少排序（正序倒序查询）
        /// </summary>
        /// <param name="type">0 从大到小 1从小到大</param>
        /// <returns></returns>
        public IList<NQ_TjFtwxCPTypeSuminfoView> GetALLInfoorderby(string type)
        {
            string hqlstr;
            if(type=="0")
            {
                hqlstr = string.Format("from NQ_TjFtwxCPTypeSuminfo  order by FT_SUM desc");
            }
           else
            {
                hqlstr = string.Format("from NQ_TjFtwxCPTypeSuminfo  order by FT_SUM asc");
            }
            List<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>(hqlstr) as List<NQ_TjFtwxCPTypeSuminfo>;
            IList<NQ_TjFtwxCPTypeSuminfoView> listmodel = GetViewlist(list);
            return listmodel;
               
        }

        //查询返退产品的统计
        /// <summary>
        /// 查询返退产品的统计
        /// </summary>
        /// <param name="tjtype">统计种类</param>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <param name="pxtype">排序</param>
        /// <returns></returns>
        public IList<NQ_TjFtwxCPTypeSuminfoView> GetTjinfobytypeandYm(string tjtype, string Year, string Mon, string pxtype)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(tjtype))
                TempHql.AppendFormat(" and Tj_Type='{0}'",tjtype);
            if (!string.IsNullOrEmpty(Year))
                TempHql.AppendFormat(" and FT_Year='{0}'",Year);
            if (!string.IsNullOrEmpty(Mon))
                TempHql.AppendFormat(" and FT_Mone='{0}'",Mon);
            if (pxtype == "0")
                TempHql.AppendFormat(" order by FT_SUM desc");
            if (pxtype == "1")
                TempHql.AppendFormat(" order by FT_SUM asc");
            string HQLstr = string.Format(" from NQ_TjFtwxCPTypeSuminfo where 1=1 {0}", TempHql);
           // List<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>(HQLstr) as List<NQ_TjFtwxCPTypeSuminfo>;
            List<NQ_TjFtwxCPTypeSuminfo> list = HibernateTemplate.Find<NQ_TjFtwxCPTypeSuminfo>(HQLstr) as List<NQ_TjFtwxCPTypeSuminfo>;
           // List<NQ_TjFtwxCPTypeSuminfo> list = Session.CreateQuery(HQLstr).List<NQ_TjFtwxCPTypeSuminfo>() as List<NQ_TjFtwxCPTypeSuminfo>;
            IList<NQ_TjFtwxCPTypeSuminfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 前10条最多的返退产品
        /// </summary>
        /// <returns></returns>
        //public IList<NQ_TjFtwxCPTypeSuminfoView> Getftcptop10info()
        //{
        //    string temHql;
        //    temHql = string.Format(" from NQ_TjFtwxCPTypeSuminfo  order by FT_SUM desc ");
        //    IQuery query = Session.CreateQuery(temHql);
        //    query.SetFirstResult(0);
        //    query.SetMaxResults(10);
        //    List<NQ_TjFtwxCPTypeSuminfo> list = query.List<NQ_TjFtwxCPTypeSuminfo>() as List<NQ_TjFtwxCPTypeSuminfo>;
        //    IList<NQ_TjFtwxCPTypeSuminfoView> listmodel = GetViewlist(list);
        //    if (listmodel != null)
        //        return listmodel;
        //    else
        //        return null;
        //}
    }
}
