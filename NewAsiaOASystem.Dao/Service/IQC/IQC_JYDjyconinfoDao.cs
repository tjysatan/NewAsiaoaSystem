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
    public class IQC_JYDjyconinfoDao:ServiceConversion<IQC_JYDjyconinfoView,IQC_JYDjyconinfo>,IIQC_JYDjyconinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(IQC_JYDjyconinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IQC_JYDjyconinfoView GetView(IQC_JYDjyconinfo data)
        {
            IQC_JYDjyconinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(IQC_JYDjyconinfoView model)
        {
            IQC_JYDjyconinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(IQC_JYDjyconinfoView model)
        {
            IQC_JYDjyconinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IQC_JYDjyconinfoView model)
        {
            IQC_JYDjyconinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<IQC_JYDjyconinfoView> model)
        {
            IList<IQC_JYDjyconinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<IQC_JYDjyconinfoView> NGetList()
        {
            List<IQC_JYDjyconinfo> listdata = base.GetList() as List<IQC_JYDjyconinfo>;
            IList<IQC_JYDjyconinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<IQC_JYDjyconinfoView> NGetList_id(string id)
        {
            List<IQC_JYDjyconinfo> list = base.GetList_id(id) as List<IQC_JYDjyconinfo>;
            IList<IQC_JYDjyconinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<IQC_JYDjyconinfo> NGetListID(string id)
        {
            IList<IQC_JYDjyconinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQC_JYDjyconinfoView NGetModelById(string id)
        {
            IQC_JYDjyconinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据检验单Id 查询测试项目
        /// <summary>
        /// 根据检验单Id 查询测试项目
        /// </summary>
        /// <param name="jydId">检验单测试单Id</param>
        /// <returns></returns>
        public IList<IQC_JYDjyconinfoView> GetJYDjyconinfobyjydId(string jydId)
        {
            string hqlstr = string.Format("from IQC_JYDjyconinfo where jydId='{0}' order by C_time ", jydId);
            List<IQC_JYDjyconinfo> list = HibernateTemplate.Find<IQC_JYDjyconinfo>(hqlstr) as List<IQC_JYDjyconinfo>;
            IList<IQC_JYDjyconinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据检验Id 和项目内容 查询检验测试项目
        /// <summary>
        /// 根据检验Id 和项目内容 查询检验测试项目
        /// </summary>
        /// <param name="jydId">检验单Id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IList<IQC_JYDjyconinfoView> GetJYDjyconinfobyjydIdandtype(string jydId, string type)
        {
            string hqlstr = string.Format("from IQC_JYDjyconinfo where jydId='{0}' and type='{1}' order by C_time ", jydId, type);
            List<IQC_JYDjyconinfo> list = HibernateTemplate.Find<IQC_JYDjyconinfo>(hqlstr) as List<IQC_JYDjyconinfo>;
            IList<IQC_JYDjyconinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion


        #region //根据检验单Id 查找未处理的检验测试项目
        /// <summary>
        /// 根据检验单Id 查找未处理的检验测试项目
        /// </summary>
        /// <param name="JYDId">检验单Id </param>
        /// <returns></returns>
        public IList<IQC_JYDjyconinfoView> GetJYDjyconinfobyJYDIdwcl(string JYDId)
        {
            string hqlstr = string.Format("from IQC_JYDjyconinfo where jydId='{0}' and Ispd='0' ", JYDId);
            List<IQC_JYDjyconinfo> list = HibernateTemplate.Find<IQC_JYDjyconinfo>(hqlstr) as List<IQC_JYDjyconinfo>;
            IList<IQC_JYDjyconinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

    }
}
