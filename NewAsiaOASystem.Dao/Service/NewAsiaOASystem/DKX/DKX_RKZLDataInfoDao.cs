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
    public class DKX_RKZLDataInfoDao:ServiceConversion<DKX_RKZLDataInfoView,DKX_RKZLDataInfo>,IDKX_RKZLDataInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_RKZLDataInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_RKZLDataInfoView GetView(DKX_RKZLDataInfo data)
        {
            DKX_RKZLDataInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_RKZLDataInfoView model)
        {
            DKX_RKZLDataInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_RKZLDataInfoView model)
        {
            DKX_RKZLDataInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_RKZLDataInfoView model)
        {
            DKX_RKZLDataInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_RKZLDataInfoView> model)
        {
            IList<DKX_RKZLDataInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_RKZLDataInfoView> NGetList()
        {
            List<DKX_RKZLDataInfo> listdata = base.GetList() as List<DKX_RKZLDataInfo>;
            IList<DKX_RKZLDataInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_RKZLDataInfoView> NGetList_id(string id)
        {
            List<DKX_RKZLDataInfo> list = base.GetList_id(id) as List<DKX_RKZLDataInfo>;
            IList<DKX_RKZLDataInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_RKZLDataInfo> NGetListID(string id)
        {
            IList<DKX_RKZLDataInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_RKZLDataInfoView NGetModelById(string id)
        {
            DKX_RKZLDataInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据产品Id查询入库资料库
        /// <summary>
        /// 根据产品Id查询入库资料库
        /// </summary>
        /// <param name="cpId">产品Id</param>
        /// <returns></returns>
        public IList<DKX_RKZLDataInfoView> GetDKXCPZLdatalist(string cpId)
        {
            string Hqlstr = string.Format(" from DKX_RKZLDataInfo where cpId='{0}' and Start='0' ", cpId);
            List<DKX_RKZLDataInfo> list = HibernateTemplate.Find<DKX_RKZLDataInfo>(Hqlstr) as List<DKX_RKZLDataInfo>;
            IList<DKX_RKZLDataInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //通过产品Id 和类型查询资料的list 数据
        /// <summary>
        /// 通过产品Id 和类型查询资料的list 数据
        /// </summary>
        /// <param name="Id">产品Id</param>
        /// <param name="type">资料类型</param>
        /// <returns></returns>
        public IList<DKX_RKZLDataInfoView> GetRKzljsonbyId(string Id, string type)
        {
            string Hqlstr = string.Format(" from DKX_RKZLDataInfo where cpId='{0}' and Zl_type='{1}' and Start='0'", Id, type);
            List<DKX_RKZLDataInfo> list = HibernateTemplate.Find<DKX_RKZLDataInfo>(Hqlstr) as List<DKX_RKZLDataInfo>;
            IList<DKX_RKZLDataInfoView> listmodel = GetViewlist(list);
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
        public DKX_RKZLDataInfoView GetzldatamodelbyIdandtype(string Id, string type)
        {
            string Hqlstr = string.Format(" from DKX_RKZLDataInfo where cpId='{0}' and Zl_type='{1}' and Start='0'", Id, type);
            List<DKX_RKZLDataInfo> list = HibernateTemplate.Find<DKX_RKZLDataInfo>(Hqlstr) as List<DKX_RKZLDataInfo>;
            IList<DKX_RKZLDataInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion
    }
}
