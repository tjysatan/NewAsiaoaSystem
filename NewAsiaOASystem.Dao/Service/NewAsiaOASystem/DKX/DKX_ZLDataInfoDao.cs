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
    public class DKX_ZLDataInfoDao:ServiceConversion<DKX_ZLDataInfoView,DKX_ZLDataInfo>,IDKX_ZLDataInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_ZLDataInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_ZLDataInfoView GetView(DKX_ZLDataInfo data)
        {
            DKX_ZLDataInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_ZLDataInfoView model)
        {
            DKX_ZLDataInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_ZLDataInfoView model)
        {
            DKX_ZLDataInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_ZLDataInfoView model)
        {
            DKX_ZLDataInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_ZLDataInfoView> model)
        {
            IList<DKX_ZLDataInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_ZLDataInfoView> NGetList()
        {
            List<DKX_ZLDataInfo> listdata = base.GetList() as List<DKX_ZLDataInfo>;
            IList<DKX_ZLDataInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_ZLDataInfoView> NGetList_id(string id)
        {
            List<DKX_ZLDataInfo> list = base.GetList_id(id) as List<DKX_ZLDataInfo>;
            IList<DKX_ZLDataInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_ZLDataInfo> NGetListID(string id)
        {
            IList<DKX_ZLDataInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_ZLDataInfoView NGetModelById(string id)
        {
            DKX_ZLDataInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据订单Id,和资料类型查询资料数据list
        /// <summary>
        /// 根据订单Id,和资料类型查询资料数据list
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="type">资料类型 0 需求 1 料单 2 料单 3 照片 4 调式单</param>
        /// <returns></returns>
        public IList<DKX_ZLDataInfoView> GetzljsonbyId(string Id, string type)
        {
            string Hqlstr = string.Format(" from DKX_ZLDataInfo where dd_Id='{0}' and Zl_type='{1}' and Start='0'", Id, type);
            List<DKX_ZLDataInfo> list = HibernateTemplate.Find<DKX_ZLDataInfo>(Hqlstr) as List<DKX_ZLDataInfo>;
            IList<DKX_ZLDataInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据订单Id,和资料类型查询管理资料数据
        /// <summary>
        /// 根据订单Id,和资料类型查询管理资料数据model关联的资料
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DKX_ZLDataInfoView GetzldatamodelbyIdandtype(string Id, string type)
        {
            string Hqlstr = string.Format(" from DKX_ZLDataInfo where dd_Id='{0}' and Zl_type='{1}' and Start='0'", Id, type);
            List<DKX_ZLDataInfo> list = HibernateTemplate.Find<DKX_ZLDataInfo>(Hqlstr) as List<DKX_ZLDataInfo>;
            IList<DKX_ZLDataInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //根据订单的Id查询全部的资料数据
        /// <summary>
        /// 根据订单的Id查询全部的资料数据
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <returns></returns>
        public IList<DKX_ZLDataInfoView> GetAllzldatabyId(string Id)
        {
            string Hqlstr = string.Format(" from DKX_ZLDataInfo where dd_Id='{0}' and  Start='0'",Id);
            List<DKX_ZLDataInfo> list = HibernateTemplate.Find<DKX_ZLDataInfo>(Hqlstr) as List<DKX_ZLDataInfo>;
            IList<DKX_ZLDataInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion


    }
}
