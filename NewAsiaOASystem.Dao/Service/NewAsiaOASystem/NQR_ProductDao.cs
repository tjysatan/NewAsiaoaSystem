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
    public class NQR_ProductDao:ServiceConversion<NQR_ProductView,NQR_Product>,INQR_ProductDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQR_Product).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQR_ProductView GetView(NQR_Product data)
        {
            NQR_ProductView view = ConvertToDTO(data);

            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQR_ProductView model)
        {
            NQR_Product listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQR_ProductView model)
        {
            NQR_Product listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQR_ProductView model)
        {
            NQR_Product listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQR_ProductView> model)
        {
            IList<NQR_Product> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQR_ProductView> NGetList()
        {
            List<NQR_Product> listdata = base.GetList() as List<NQR_Product>;
            IList<NQR_ProductView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQR_ProductView> NGetList_id(string id)
        {
            List<NQR_Product> list = base.GetList_id(id) as List<NQR_Product>;
            IList<NQR_ProductView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQR_Product> NGetListID(string id)
        {
            IList<NQR_Product> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQR_ProductView NGetModelById(string id)
        {
            NQR_ProductView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public PagerInfo<NQR_ProductView> GetCinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
            TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
            PagerInfo<NQR_ProductView> list = Search();
            return list;

        }
    }
}
