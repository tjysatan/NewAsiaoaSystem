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
    public class NQ_CHdetailinfoDao : ServiceConversion<NQ_CHdetailinfoView,NQ_CHdetailinfo>,INQ_CHdetailinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_CHdetailinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_CHdetailinfoView GetView(NQ_CHdetailinfo data)
        {
            NQ_CHdetailinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_CHdetailinfoView model)
        {
            NQ_CHdetailinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_CHdetailinfoView model)
        {
            NQ_CHdetailinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_CHdetailinfoView model)
        {
            NQ_CHdetailinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_CHdetailinfoView> model)
        {
            IList<NQ_CHdetailinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_CHdetailinfoView> NGetList()
        {
            List<NQ_CHdetailinfo> listdata = base.GetList() as List<NQ_CHdetailinfo>;
            IList<NQ_CHdetailinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_CHdetailinfoView> NGetList_id(string id)
        {
            List<NQ_CHdetailinfo> list = base.GetList_id(id) as List<NQ_CHdetailinfo>;
            IList<NQ_CHdetailinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_CHdetailinfo> NGetListID(string id)
        {
            IList<NQ_CHdetailinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_CHdetailinfoView NGetModelById(string id)
        {
            NQ_CHdetailinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

  
        //#region //查询全部的客户信息
        //public IList<NQ_CHdetailinfoView> GetlistCust()
        //{
        //    List<NQ_CHdetailinfoView> list = HibernateTemplate.Find<NACustomerinfo>("from NACustomerinfo order by Sort asc ") as List<NACustomerinfo>;
        //    IList<NACustomerinfoView> listmodel = GetViewlist(list);
        //    return listmodel;
        //}
        //#endregion

        #region //根据返退货流程Id 查询出货产品明细
        public IList<NQ_CHdetailinfoView> Getchinfoby_rid(string R_Id)
        {
            string sql = string.Format("from NQ_CHdetailinfo where R_Id='{0}' order by P_Id", R_Id);
            List<NQ_CHdetailinfo> list = HibernateTemplate.Find<NQ_CHdetailinfo>(sql) as List<NQ_CHdetailinfo>;
            IList<NQ_CHdetailinfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //根据返退货r_Id和产品Id和处理方式 检测是否已经存在4
        /// <summary>
        /// 检测出货单中是否存在相同的处理方式的相同产品 如果存在就 返回false 如果不存在就返回true
        /// </summary>
        /// <param name="r_Id">返退货单子Id</param>
        /// <param name="cp_Id">产品Id</param>
        /// <param name="clfs">处理方式</param>
        /// <returns></returns>
        public bool JxCHcpbyridandcpidandclfs(string r_Id, string cp_Id, string clfs)
        {
            string Hqlstr = string.Format("from NQ_CHdetailinfo where R_Id='{0}' and P_Id='{1}' and clfs='{2}'",r_Id,cp_Id,clfs);
            List<NQ_CHdetailinfo> list = HibernateTemplate.Find<NQ_CHdetailinfo>(Hqlstr) as List<NQ_CHdetailinfo>;
           // IList<NQ_CHdetailinfoView> listmodel = GetViewlist(list);
           // return listmodel;
            return list.Count > 0 ? false : true;
        }
        #endregion

        #region //根据返退货Id产品Id处理方式 查找退货单产品信息
        /// <summary>
        /// 根据返退货Id产品Id处理方式 查找退货单产品信息
        /// </summary>
        /// <param name="r_Id">返退Id</param>
        /// <param name="cp_Id">产品Id</param>
        /// <param name="clfs">处理方式</param>
        /// <returns></returns>
        public NQ_CHdetailinfoView GetCHdetailinfobyr_IdcpIdclfs(string r_Id, string cp_Id, string clfs)
        {
            string Hqlstr = string.Format("from NQ_CHdetailinfo where R_Id='{0}' and P_Id='{1}' and clfs='{2}'", r_Id, cp_Id, clfs);
            List<NQ_CHdetailinfo> list = HibernateTemplate.Find<NQ_CHdetailinfo>(Hqlstr) as List<NQ_CHdetailinfo>;
            IList<NQ_CHdetailinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        } 
        #endregion
    }
}
