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
    public class IQC_JyffinfoDao:ServiceConversion<IQC_JyffinfoView,IQC_Jyffinfo>,IIQC_JyffinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(IQC_Jyffinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IQC_JyffinfoView GetView(IQC_Jyffinfo data)
        {
            IQC_JyffinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(IQC_JyffinfoView model)
        {
            IQC_Jyffinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(IQC_JyffinfoView model)
        {
            IQC_Jyffinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IQC_JyffinfoView model)
        {
            IQC_Jyffinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<IQC_JyffinfoView> model)
        {
            IList<IQC_Jyffinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<IQC_JyffinfoView> NGetList()
        {
            List<IQC_Jyffinfo> listdata = base.GetList() as List<IQC_Jyffinfo>;
            IList<IQC_JyffinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<IQC_JyffinfoView> NGetList_id(string id)
        {
            List<IQC_Jyffinfo> list = base.GetList_id(id) as List<IQC_Jyffinfo>;
            IList<IQC_JyffinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<IQC_Jyffinfo> NGetListID(string id)
        {
            IList<IQC_Jyffinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQC_JyffinfoView NGetModelById(string id)
        {
            IQC_JyffinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //根据检验单SOP 和检验项目查询检验方法
        /// <summary>
        /// 根据检验单SOP 和检验项目查询检验方法
        /// </summary>
        /// <param name="sopid">作业流程单Id</param>
        /// <param name="xmtype">检验的项目</param>
        /// <returns></returns>
        public IQC_JyffinfoView GetjyffmodebysopIdandxmtype(string sopid, string xmtype)
        {
            string hqlstr = string.Format("from IQC_Jyffinfo where sopId='{0}' and Jyxmtype='{1}' order by C_time", sopid, xmtype);
            List<IQC_Jyffinfo> list = HibernateTemplate.Find<IQC_Jyffinfo>(hqlstr) as List<IQC_Jyffinfo>;
            IList<IQC_JyffinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
    }
}
