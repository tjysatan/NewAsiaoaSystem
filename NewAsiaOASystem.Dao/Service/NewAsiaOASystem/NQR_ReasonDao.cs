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
    /// <summary>
    /// 返退原因业务处理
    /// tjy_satan
    /// </summary>
    public class NQR_ReasonDao:ServiceConversion<NQR_ReasonView,NQR_Reason>,INQR_ReasonDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQR_Reason).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQR_ReasonView GetView(NQR_Reason data)
        {
            NQR_ReasonView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQR_ReasonView model)
        {
            NQR_Reason listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQR_ReasonView model)
        {
            NQR_Reason listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQR_ReasonView model)
        {
            NQR_Reason listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQR_ReasonView> model)
        {
            IList<NQR_Reason> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQR_ReasonView> NGetList()
        {
            List<NQR_Reason> listdata = base.GetList() as List<NQR_Reason>;
            IList<NQR_ReasonView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQR_ReasonView> NGetList_id(string id)
        {
            List<NQR_Reason> list = base.GetList_id(id) as List<NQR_Reason>;
            IList<NQR_ReasonView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQR_Reason> NGetListID(string id)
        {
            IList<NQR_Reason> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQR_ReasonView NGetModelById(string id)
        {
            NQR_ReasonView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        public PagerInfo<NQR_ReasonView> GetCinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);

            TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
            PagerInfo<NQR_ReasonView> list = Search();
            return list;

        }

        #region //查询全部的父级原因信息
        public IList<NQR_ReasonView> GetlistisPar()
        {
            List<NQR_Reason> list = HibernateTemplate.Find<NQR_Reason>("from NQR_Reason where ParentId='0'") as List<NQR_Reason>;
            IList<NQR_ReasonView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //父级ID 查询返退货原因信息
        public IList<NQR_ReasonView> Getlistreason_byPid(string PID)
        {
            List<NQR_Reason> list = HibernateTemplate.Find<NQR_Reason>("from NQR_Reason where ParentId='"+PID+"'") as List<NQR_Reason>;
            IList<NQR_ReasonView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

    }
}
