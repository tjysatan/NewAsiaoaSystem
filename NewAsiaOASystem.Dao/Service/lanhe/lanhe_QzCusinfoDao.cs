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
    public class lanhe_QzCusinfoDao: ServiceConversion<lanhe_QzCusinfoView,lanhe_QzCusinfo>,Ilanhe_QzCusinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(lanhe_QzCusinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(lanhe_QzCusinfoView model)
        {
            lanhe_QzCusinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(lanhe_QzCusinfoView model)
        {
            lanhe_QzCusinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(lanhe_QzCusinfoView model)
        {
            lanhe_QzCusinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<lanhe_QzCusinfoView> model)
        {
            IList<lanhe_QzCusinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<lanhe_QzCusinfoView> NGetList()
        {
            List<lanhe_QzCusinfo> listdata = base.GetList() as List<lanhe_QzCusinfo>;
            IList<lanhe_QzCusinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<lanhe_QzCusinfo> listdata = base.GetList() as List<lanhe_QzCusinfo>;
            IList<lanhe_QzCusinfoView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<lanhe_QzCusinfoView> NGetList_id(string id)
        {
            List<lanhe_QzCusinfo> list = base.GetList_id(id) as List<lanhe_QzCusinfo>;
            IList<lanhe_QzCusinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<lanhe_QzCusinfo> NGetListModel(string id)
        {
            IList<lanhe_QzCusinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public lanhe_QzCusinfoView NGetModelById(string id)
        {
            lanhe_QzCusinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据电话号码查询是否存在该手机号码的记录
        /// <summary>
        /// 存在记录 返回false 不存在记录返回 true
        /// </summary>
        /// <param name="tel">联系电话</param>
        /// <returns></returns>
        public bool checktelIscf(string tel)
        {
            string HQLstr = string.Format(" from lanhe_QzCusinfo where lxtle='{0}'", tel);
            List<lanhe_QzCusinfo> list = HibernateTemplate.Find<lanhe_QzCusinfo>(HQLstr) as List<lanhe_QzCusinfo>;
            IList<lanhe_QzCusinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return true;
            else
                return false;
        }
        #endregion
    }
}
