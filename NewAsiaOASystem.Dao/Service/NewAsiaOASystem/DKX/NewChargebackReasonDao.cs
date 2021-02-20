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
    public class NewChargebackReasonDao:ServiceConversion<NewChargebackReasonView,NewChargebackReason>,INewChargebackReasonDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NewChargebackReason).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NewChargebackReasonView GetView(NewChargebackReason data)
        {
            NewChargebackReasonView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NewChargebackReasonView model)
        {
            NewChargebackReason listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NewChargebackReasonView model)
        {
            NewChargebackReason listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NewChargebackReasonView model)
        {
            NewChargebackReason listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NewChargebackReasonView> model)
        {
            IList<NewChargebackReason> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NewChargebackReasonView> NGetList()
        {
            List<NewChargebackReason> listdata = base.GetList() as List<NewChargebackReason>;
            IList<NewChargebackReasonView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NewChargebackReasonView> NGetList_id(string id)
        {
            List<NewChargebackReason> list = base.GetList_id(id) as List<NewChargebackReason>;
            IList<NewChargebackReasonView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NewChargebackReason> NGetListID(string id)
        {
            IList<NewChargebackReason> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewChargebackReasonView NGetModelById(string id)
        {
            NewChargebackReasonView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //管理订单退单原因的分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">原因名称模糊查询</param>
        /// <param name="type">类型 0 生产退单</param>
        /// <param name="IsRemarks">是否需要备注 0不需要 1需要 </param>
        /// <param name="IsDis">是否启用</param>
        /// <returns></returns>
        public PagerInfo<NewChargebackReasonView> GetChargebackReasonlistpage(string name, string type, string IsRemarks, string IsDis)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(name))
                TempHql.AppendFormat(" and Reason_name like '%{0}%'", name);
            if (!string.IsNullOrEmpty(type))
                TempHql.AppendFormat(" and Type='{0}'", type);
            if (!string.IsNullOrEmpty(IsRemarks))
                TempHql.AppendFormat(" and IsRemarks='{0}'", IsRemarks);
            if (!string.IsNullOrEmpty(IsDis))
                TempHql.AppendFormat(" and IsDis='{0}'", IsDis);
            TempHql.AppendFormat("order by  C_time desc");
            PagerInfo<NewChargebackReasonView> list = Search();
            return list;
        }
        #endregion

        #region //查询全部的生产退单原因数据
        public IList<NewChargebackReasonView> GetSCCBRData()
        {
            string Hqlstr = string.Format(" from NewChargebackReason where Type='0' and IsDis='0'");
            List<NewChargebackReason> list = HibernateTemplate.Find<NewChargebackReason>(Hqlstr) as List<NewChargebackReason>;
            IList<NewChargebackReasonView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
