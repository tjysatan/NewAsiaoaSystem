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
    public class Js_xz_yfcostDao: ServiceConversion<Js_xz_yfcostView, Js_xz_yfcost>, IJs_xz_yfcostDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Js_xz_yfcost).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Js_xz_yfcostView model)
        {
            Js_xz_yfcost listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Js_xz_yfcostView model)
        {
            Js_xz_yfcost listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Js_xz_yfcostView model)
        {
            Js_xz_yfcost listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Js_xz_yfcostView> model)
        {
            IList<Js_xz_yfcost> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Js_xz_yfcostView> NGetList()
        {
            List<Js_xz_yfcost> listdata = base.GetList() as List<Js_xz_yfcost>;
            IList<Js_xz_yfcostView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<Js_xz_yfcost> listdata = base.GetList() as List<Js_xz_yfcost>;
            IList<Js_xz_yfcostView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Js_xz_yfcostView> NGetList_id(string id)
        {
            List<Js_xz_yfcost> list = base.GetList_id(id) as List<Js_xz_yfcost>;
            IList<Js_xz_yfcostView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Js_xz_yfcost> NGetListModel(string id)
        {
            IList<Js_xz_yfcost> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Js_xz_yfcostView NGetModelById(string id)
        {
            Js_xz_yfcostView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过日期和小组名称查询小组数据研发成本数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Js_xz_name">小组名称</param>
        /// <param name="Fy_data">费用日期</param>
        /// <returns></returns>
        public Js_xz_yfcostView GetJx_xzcostbyxznameanddate(string Js_xz_name, string Fy_data)
        {
            string HQLstr = string.Format(" from Js_xz_yfcost where Js_xz_name='{0}' and Fy_data='{1}'", Js_xz_name, Fy_data);
            List<Js_xz_yfcost> list = HibernateTemplate.Find<Js_xz_yfcost>(HQLstr) as List<Js_xz_yfcost>;
            IList<Js_xz_yfcostView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
           
        }
        #endregion
    }
}
