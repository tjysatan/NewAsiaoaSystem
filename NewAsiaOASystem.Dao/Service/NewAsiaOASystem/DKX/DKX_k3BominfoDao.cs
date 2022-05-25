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
    public class DKX_k3BominfoDao:ServiceConversion<DKX_k3BominfoView,DKX_k3Bominfo>,IDKX_k3BominfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_k3Bominfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_k3BominfoView GetView(DKX_k3Bominfo data)
        {
            DKX_k3BominfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_k3BominfoView model)
        {
            DKX_k3Bominfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_k3BominfoView model)
        {
            DKX_k3Bominfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_k3BominfoView model)
        {
            DKX_k3Bominfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_k3BominfoView> model)
        {
            IList<DKX_k3Bominfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_k3BominfoView> NGetList()
        {
            List<DKX_k3Bominfo> listdata = base.GetList() as List<DKX_k3Bominfo>;
            IList<DKX_k3BominfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_k3BominfoView> NGetList_id(string id)
        {
            List<DKX_k3Bominfo> list = base.GetList_id(id) as List<DKX_k3Bominfo>;
            IList<DKX_k3BominfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_k3Bominfo> NGetListID(string id)
        {
            IList<DKX_k3Bominfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_k3BominfoView NGetModelById(string id)
        {
            DKX_k3BominfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据订单Id和K3的bom表的编码查询料单
        /// <summary>
        /// 根据订单Id和K3的bom表的编码查询料单
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="bomno">bom编号</param>
        /// <returns></returns>
        public IList<DKX_k3BominfoView> GetliaodanlistbyIdandbomno(string Id, string bomno)
        {
           
            string Hqlstr = string.Format("from DKX_k3Bominfo where dd_Id='{0}' and FnumberBom='{1}' order by FNumber desc ", Id, bomno);
            List<DKX_k3Bominfo> list = HibernateTemplate.Find<DKX_k3Bominfo>(Hqlstr) as List<DKX_k3Bominfo>;
            IList<DKX_k3BominfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion

        #region //根据订单(方案库产品)Id 查询料单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">订单Id 或者产品Id</param>
        /// <returns></returns>
        public IList<DKX_k3BominfoView> GetliaodanlistbyId(string Id)
        {
            string Hqlstr = string.Format("from DKX_k3Bominfo where dd_Id='{0}' order by FNumber desc ", Id);
            List<DKX_k3Bominfo> list = HibernateTemplate.Find<DKX_k3Bominfo>(Hqlstr) as List<DKX_k3Bominfo>;
            IList<DKX_k3BominfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion
    }
}
