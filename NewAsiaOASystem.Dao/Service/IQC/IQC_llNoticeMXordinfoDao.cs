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
    public class IQC_llNoticeMXordinfoDao:ServiceConversion<IQC_llNoticeMXordinfoView,IQC_llNoticeMXordinfo>,IIQC_llNoticeMXordinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(IQC_llNoticeMXordinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IQC_llNoticeMXordinfoView GetView(IQC_llNoticeMXordinfo data)
        {
            IQC_llNoticeMXordinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(IQC_llNoticeMXordinfoView model)
        {
            IQC_llNoticeMXordinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(IQC_llNoticeMXordinfoView model)
        {
            IQC_llNoticeMXordinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IQC_llNoticeMXordinfoView model)
        {
            IQC_llNoticeMXordinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<IQC_llNoticeMXordinfoView> model)
        {
            IList<IQC_llNoticeMXordinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<IQC_llNoticeMXordinfoView> NGetList()
        {
            List<IQC_llNoticeMXordinfo> listdata = base.GetList() as List<IQC_llNoticeMXordinfo>;
            IList<IQC_llNoticeMXordinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<IQC_llNoticeMXordinfoView> NGetList_id(string id)
        {
            List<IQC_llNoticeMXordinfo> list = base.GetList_id(id) as List<IQC_llNoticeMXordinfo>;
            IList<IQC_llNoticeMXordinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<IQC_llNoticeMXordinfo> NGetListID(string id)
        {
            IList<IQC_llNoticeMXordinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQC_llNoticeMXordinfoView NGetModelById(string id)
        {
            IQC_llNoticeMXordinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //分页列表
        /// <summary>
        /// 同步的数据分页数据
        /// </summary>
        /// <param name="DDNO">来料检验通知单单号</param>
        /// <param name="YJQWL">元器件物联代码</param>
        /// <param name="YQJname">元器件名称</param>
        /// <param name="gyswl">供应商的物联代码</param>
        /// <param name="gysname">供应商名称</param>
        /// <returns></returns>
        public PagerInfo<IQC_llNoticeMXordinfoView> GetllNoticelistPager(string DDNO,string YJQWL,string YQJname,string gyswl,string gysname,string Isscjyd)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DDNO))
                TempHql.AppendFormat(" and DDNO='{0}'",DDNO);
            if (!string.IsNullOrEmpty(YJQWL))
                TempHql.AppendFormat(" and Yqjwldno='{0}'", YJQWL);
            if (!string.IsNullOrEmpty(YQJname))
                TempHql.AppendFormat(" and Yqjname like '%{0}%'", YQJname);
            if (!string.IsNullOrEmpty(gyswl))
                TempHql.AppendFormat(" and gyswlno='{0}'", YJQWL);
            if (!string.IsNullOrEmpty(gysname))
                TempHql.AppendFormat(" and gysname like '%{0}%'", gysname);
            if (!string.IsNullOrEmpty(Isscjyd))
                TempHql.AppendFormat(" and Isjy='{0}'",Isscjyd);
            TempHql.AppendFormat(" order by lytype desc, DDNO desc,C_time asc");
            PagerInfo<IQC_llNoticeMXordinfoView> list = Search();
            return list;
        }
        #endregion

        #region //通过送检单号和元器件物料代码查询明细信息
        /// <summary>
        /// 通过送检单号和元器件物料代码查询明细信息
        /// </summary>
        /// <param name="ddno">送检单号</param>
        /// <param name="wldm">物料代码</param>
        /// <returns></returns>
        public IQC_llNoticeMXordinfoView GetllnotceMXbysjdhandwldm(string ddno, string wldm)
        {
            string hqlstr = string.Format("from IQC_llNoticeMXordinfo where DDNO='{0}'and Yqjwldno='{1}' ", ddno,wldm);
            List<IQC_llNoticeMXordinfo> list = HibernateTemplate.Find<IQC_llNoticeMXordinfo>(hqlstr) as List<IQC_llNoticeMXordinfo>;
            IList<IQC_llNoticeMXordinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion

        #region //通过送检单号查询明细
        /// <summary>
        /// 通过送检单号查询明细
        /// </summary>
        /// <param name="ddno">送检单号</param>
        /// <returns></returns>
        public IList<IQC_llNoticeMXordinfoView> Getsjdlistmodelbyddno(string ddno)
        {
            string hqlstr = string.Format("from IQC_llNoticeMXordinfo where DDNO='{0}'",ddno);
            List<IQC_llNoticeMXordinfo> list = HibernateTemplate.Find<IQC_llNoticeMXordinfo>(hqlstr) as List<IQC_llNoticeMXordinfo>;
            IList<IQC_llNoticeMXordinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //查询普实单号字段最近的数据
        public IQC_llNoticeMXordinfoView GetllNoticeMXMaxDDNO()
        {
            string hqlstr = string.Format("from IQC_llNoticeMXordinfo where DDNO=(SELECT MAX(DDNO) FROM IQC_llNoticeMXordinfo where lytype='1')");
            List<IQC_llNoticeMXordinfo> list = Session.CreateQuery(hqlstr).List<IQC_llNoticeMXordinfo>() as List<IQC_llNoticeMXordinfo>;
            IList<IQC_llNoticeMXordinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion
    }
}
