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
    public class IQC_JyconstrinfoDao:ServiceConversion<IQC_JyconstrinfoView,IQC_Jyconstrinfo>,IIQC_JyconstrinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(IQC_Jyconstrinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IQC_JyconstrinfoView GetView(IQC_Jyconstrinfo data)
        {
            IQC_JyconstrinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(IQC_JyconstrinfoView model)
        {
            IQC_Jyconstrinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(IQC_JyconstrinfoView model)
        {
            IQC_Jyconstrinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IQC_JyconstrinfoView model)
        {
            IQC_Jyconstrinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<IQC_JyconstrinfoView> model)
        {
            IList<IQC_Jyconstrinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<IQC_JyconstrinfoView> NGetList()
        {
            List<IQC_Jyconstrinfo> listdata = base.GetList() as List<IQC_Jyconstrinfo>;
            IList<IQC_JyconstrinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<IQC_JyconstrinfoView> NGetList_id(string id)
        {
            List<IQC_Jyconstrinfo> list = base.GetList_id(id) as List<IQC_Jyconstrinfo>;
            IList<IQC_JyconstrinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<IQC_Jyconstrinfo> NGetListID(string id)
        {
            IList<IQC_Jyconstrinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQC_JyconstrinfoView NGetModelById(string id)
        {
            IQC_JyconstrinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据检验单Id 和检验项目 查找检验内容
        /// <summary>
        /// 根据检验单Id 和检验项目 查找检验内容
        /// </summary>
        /// <param name="sopId">作业单Id</param>
        /// <param name="xmtype">检验项目 0电气性能 1 尺寸 2外观 3可靠性 4其他</param>
        /// <returns></returns>
        public IList<IQC_JyconstrinfoView> GetjyconbysopIdxmtype(string sopId, string xmtype)
        {
            string hqlstr = string.Format("from IQC_Jyconstrinfo where SopId='{0}' and Jyxmname='{1}' and State='0' order by C_time", sopId, xmtype);
            List<IQC_Jyconstrinfo> list = HibernateTemplate.Find<IQC_Jyconstrinfo>(hqlstr) as List<IQC_Jyconstrinfo>;
            IList<IQC_JyconstrinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //根据检验单Id 查询检验内容
        /// <summary>
        /// 根据检验单Id 查询检验内容
        /// </summary>
        /// <param name="sopId">检验标准Id</param>
        /// <returns></returns>
        public IList<IQC_JyconstrinfoView> GetjyconbysopId(string sopId)
        {
            string hqlstr = string.Format("from IQC_Jyconstrinfo where SopId='{0}' and State='0' order by C_time", sopId);
            List<IQC_Jyconstrinfo> list = HibernateTemplate.Find<IQC_Jyconstrinfo>(hqlstr) as List<IQC_Jyconstrinfo>;
            IList<IQC_JyconstrinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
