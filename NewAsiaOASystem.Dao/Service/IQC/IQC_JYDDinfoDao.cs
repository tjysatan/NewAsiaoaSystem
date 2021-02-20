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
    public class IQC_JYDDinfoDao:ServiceConversion<IQC_JYDDinfoView,IQC_JYDDinfo>,IIQC_JYDDinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(IQC_JYDDinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IQC_JYDDinfoView GetView(IQC_JYDDinfo data)
        {
            IQC_JYDDinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(IQC_JYDDinfoView model)
        {
            IQC_JYDDinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(IQC_JYDDinfoView model)
        {
            IQC_JYDDinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IQC_JYDDinfoView model)
        {
            IQC_JYDDinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<IQC_JYDDinfoView> model)
        {
            IList<IQC_JYDDinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<IQC_JYDDinfoView> NGetList()
        {
            List<IQC_JYDDinfo> listdata = base.GetList() as List<IQC_JYDDinfo>;
            IList<IQC_JYDDinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<IQC_JYDDinfoView> NGetList_id(string id)
        {
            List<IQC_JYDDinfo> list = base.GetList_id(id) as List<IQC_JYDDinfo>;
            IList<IQC_JYDDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<IQC_JYDDinfo> NGetListID(string id)
        {
            IList<IQC_JYDDinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQC_JYDDinfoView NGetModelById(string id)
        {
            IQC_JYDDinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }



        #region //通过通知单明细Id 查询检验单
        /// <summary>
        /// 通过通知单明细Id 查询检验单
        /// </summary>
        /// <param name="Id">通知单明细Id</param>
        /// <returns></returns>
        public IQC_JYDDinfoView GetJYDinfobymxId(string Id)
        {
            string hqlstr = string.Format("from IQC_JYDDinfo where MxId='{0}' and IsDis='0'", Id);
            List<IQC_JYDDinfo> list = HibernateTemplate.Find<IQC_JYDDinfo>(hqlstr) as List<IQC_JYDDinfo>;
            IList<IQC_JYDDinfoView> listmodel = GetViewlist(list);
            if(listmodel!=null)
                return listmodel[0];
            return null;
        }
        #endregion

        #region //检验测试单分页数据
        /// <summary>
        /// 检验测试单分页数据
        /// </summary>
        /// <param name="JYDDno">检验单单号</param>
        /// <param name="gysname">供应商</param>
        /// <param name="yqjname">元器件</param>
        /// <param name="yqjxh">型号</param>
        /// <param name="clzt">处理状态</param>
        /// <param name="cljg">处理结果</param>
        /// <returns></returns>
        public PagerInfo<IQC_JYDDinfoView> GetJYDDlistPager(string JYDDno, string gysname, string yqjname, string yqjxh, string clzt, string cljg
            ,string shstrattime,string shendtime)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(JYDDno))
                TempHql.AppendFormat(" and Jyddno='{0}'", JYDDno);
            if (!string.IsNullOrEmpty(gysname))
                TempHql.AppendFormat(" and Gysname like '%{0}%'", gysname);
            if (!string.IsNullOrEmpty(yqjname))
                TempHql.AppendFormat(" and Yqjname like '%{0}%'", yqjname);
            if (!string.IsNullOrEmpty(yqjxh))
                TempHql.AppendFormat(" and Yqjxh like '%{0}%'", yqjxh);
            if (!string.IsNullOrEmpty(clzt))
                TempHql.AppendFormat(" and Jydzt='{0}'", clzt);
            if (!string.IsNullOrEmpty(cljg))
                TempHql.AppendFormat(" and Jydjg='{0}'",cljg);
            if (!string.IsNullOrEmpty(shstrattime))
                TempHql.AppendFormat(" and u.C_time>='{0}' and C_time<='{1}'", shstrattime + " 00:00:00", shendtime + " 23:59:59");
            TempHql.AppendFormat(" and u.IsDis='0'");
            TempHql.AppendFormat(" order by Jydzt asc, Jyddno desc, C_time desc");
            PagerInfo<IQC_JYDDinfoView> list = Search();
            return list;
        }

        #endregion

        #region //返回当日检验单的数量
        /// <summary>
        /// 返回当日检验单的数量
        /// </summary>
        /// <returns></returns>
        public int GetJYDcount()
        {
            try
            {
                string temHql = string.Format(" from IQC_JYDDinfo where DateDiff(dd,C_time,getdate())=0 ");
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region //按照审核时间查询审核完成 的检验测试单数据
        /// <summary>
        /// 按照审核时间查询审核完成 的检验测试单数据
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<IQC_JYDDinfoView> GetSHTGDATAbyshtime(string starttime, string endtime)
        {
            if (string.IsNullOrEmpty(starttime))
            {
                starttime = DateTime.Now.ToString("yyyy-MM-dd");
                endtime = DateTime.Now.ToString("yyyy-MM-dd");
            }
            string hqlstr = string.Format("from IQC_JYDDinfo where Jydzt='3' and shdatime>='{0}' and shdatime<='{1}' order by Jyddno asc", starttime + " 00:00:00", endtime + " 23:59:59");
            List<IQC_JYDDinfo> list = HibernateTemplate.Find<IQC_JYDDinfo>(hqlstr) as List<IQC_JYDDinfo>;
            IList<IQC_JYDDinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            return null;
        }
        #endregion
      
    }
}
