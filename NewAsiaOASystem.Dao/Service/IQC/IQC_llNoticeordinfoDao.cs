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
    public class IQC_llNoticeordinfoDao:ServiceConversion<IQC_llNoticeordinfoView,IQC_llNoticeordinfo>,IIQC_llNoticeordinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(IQC_llNoticeordinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IQC_llNoticeordinfoView GetView(IQC_llNoticeordinfo data)
        {
            IQC_llNoticeordinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(IQC_llNoticeordinfoView model)
        {
            IQC_llNoticeordinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(IQC_llNoticeordinfoView model)
        {
            IQC_llNoticeordinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IQC_llNoticeordinfoView model)
        {
            IQC_llNoticeordinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<IQC_llNoticeordinfoView> model)
        {
            IList<IQC_llNoticeordinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<IQC_llNoticeordinfoView> NGetList()
        {
            List<IQC_llNoticeordinfo> listdata = base.GetList() as List<IQC_llNoticeordinfo>;
            IList<IQC_llNoticeordinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<IQC_llNoticeordinfoView> NGetList_id(string id)
        {
            List<IQC_llNoticeordinfo> list = base.GetList_id(id) as List<IQC_llNoticeordinfo>;
            IList<IQC_llNoticeordinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<IQC_llNoticeordinfo> NGetListID(string id)
        {
            IList<IQC_llNoticeordinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQC_llNoticeordinfoView NGetModelById(string id)
        {
            IQC_llNoticeordinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据K3 来料通知单单号查询该单数据
        /// <summary>
        /// 根据K3 来料通知单单号查询该单数据
        /// </summary>
        /// <param name="ddno">订单单号</param>
        /// <returns></returns>
        public IQC_llNoticeordinfoView GetllNoticeinfobyddno(string ddno)
        {
            string hqlstr = string.Format("from IQC_llNoticeordinfo where ddno='{0}' ", ddno);
            List<IQC_llNoticeordinfo> list = HibernateTemplate.Find<IQC_llNoticeordinfo>(hqlstr) as List<IQC_llNoticeordinfo>;
            IList<IQC_llNoticeordinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion

        #region //根据K3 自增Id查询排序查询最大的一条数据
        /// <summary>
        /// 根据K3 自增Id查询排序查询最大的一条数据
        /// </summary>
        /// <returns></returns>
        public IQC_llNoticeordinfoView GetllNoticeinfoorderbyddId()
        {
            string hqlstr = string.Format("from IQC_llNoticeordinfo order by ddId desc ");
            List<IQC_llNoticeordinfo> list = HibernateTemplate.Find<IQC_llNoticeordinfo>(hqlstr) as List<IQC_llNoticeordinfo>;
            IList<IQC_llNoticeordinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion


        #region //保存后返回ID
        public string InsertID(IQC_llNoticeordinfoView modelView)
        {
            IQC_llNoticeordinfo model = GetData(modelView);
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

    }
}
