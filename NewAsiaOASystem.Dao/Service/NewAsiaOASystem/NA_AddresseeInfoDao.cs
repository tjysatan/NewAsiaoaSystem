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
    /// 收件人信息 方法
    /// </summary>
    public  class NA_AddresseeInfoDao:ServiceConversion<NA_AddresseeInfoView,NA_AddresseeInfo>,INA_AddresseeInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NA_AddresseeInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NA_AddresseeInfoView GetView(NA_AddresseeInfo data)
        {
            NA_AddresseeInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NA_AddresseeInfoView model)
        {
            NA_AddresseeInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NA_AddresseeInfoView model)
        {
            NA_AddresseeInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NA_AddresseeInfoView model)
        {
            NA_AddresseeInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NA_AddresseeInfoView> model)
        {
            IList<NA_AddresseeInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NA_AddresseeInfoView> NGetList()
        {
            List<NA_AddresseeInfo> listdata = base.GetList() as List<NA_AddresseeInfo>;
            IList<NA_AddresseeInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NA_AddresseeInfoView> NGetList_id(string id)
        {
            List<NA_AddresseeInfo> list = base.GetList_id(id) as List<NA_AddresseeInfo>;
            IList<NA_AddresseeInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }



        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NA_AddresseeInfo> NGetListID(string id)
        {
            IList<NA_AddresseeInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NA_AddresseeInfoView NGetModelById(string id)
        {
            NA_AddresseeInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }



        #region //通过 收件人和电话查找是否有重复的收件人信息
        public NA_AddresseeInfoView GetaddresseeinfobyAnametel(string aname, string tel, string Address)
        {
            string Hqlstr = string.Format(" from NA_AddresseeInfo where Aname='{0}' and Tel='{1}' and Address='{2}'",aname,tel,Address);
            List<NA_AddresseeInfo> list = HibernateTemplate.Find<NA_AddresseeInfo>(Hqlstr) as List<NA_AddresseeInfo>;
            IList<NA_AddresseeInfoView> listmodel = GetViewlist(list);
            if (listmodel != null && listmodel.Count != 0)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        } 
        #endregion


        #region //根据 客户Id 查找收件人信息
        public NA_AddresseeInfoView GetAddresseeinfobycustId(string custId)
        {
            List<NA_AddresseeInfo> list = HibernateTemplate.Find<NA_AddresseeInfo>("from NA_AddresseeInfo where CusId='" + custId + "'") as List<NA_AddresseeInfo>;
            IList<NA_AddresseeInfoView> listmodel = GetViewlist(list);
            if (listmodel != null && listmodel.Count != 0)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        } 
        #endregion


        #region //保存后返回ID
        public string InsertID(NA_AddresseeInfoView modelView)
        {
            NA_AddresseeInfo model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
                // Session.Save(model);
                string Id = model.Id;
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return Id;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion
    }
}
