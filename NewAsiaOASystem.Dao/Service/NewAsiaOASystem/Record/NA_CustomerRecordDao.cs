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
    public class NA_CustomerRecordDao:ServiceConversion<NA_CustomerRecordView,NA_CustomerRecord>,INA_CustomerRecordDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_CustomerRecord).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_CustomerRecordView GetView(NA_CustomerRecord data)
        {
            NA_CustomerRecordView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_CustomerRecordView model)
        {
            NA_CustomerRecord listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_CustomerRecordView model)
        {
            NA_CustomerRecord listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_CustomerRecordView model)
        {
            NA_CustomerRecord listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_CustomerRecordView> model)
        {
            IList<NA_CustomerRecord> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_CustomerRecordView> NGetList()
        {
            List<NA_CustomerRecord> listdata = base.GetList() as List<NA_CustomerRecord>;
            IList<NA_CustomerRecordView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_CustomerRecordView> NGetList_id(string id)
        {
            List<NA_CustomerRecord> list = base.GetList_id(id) as List<NA_CustomerRecord>;
            IList<NA_CustomerRecordView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_CustomerRecord> NGetListID(string id)
        {
            IList<NA_CustomerRecord> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_CustomerRecordView NGetModelById(string id)
        {
            NA_CustomerRecordView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //分页数据
        //public PagerInfo<NA_CustomerRecordView> GetpageList(string Name, SessionUser user)
        //{
            
        //}
        #endregion
    }
}
