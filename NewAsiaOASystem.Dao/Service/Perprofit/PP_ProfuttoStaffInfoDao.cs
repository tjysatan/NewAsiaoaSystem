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
    public class PP_ProfuttoStaffInfoDao:ServiceConversion<PP_ProfuttoStaffInfoView,PP_ProfuttoStaffInfo>,IPP_ProfuttoStaffInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_ProfuttoStaffInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_ProfuttoStaffInfoView GetView(PP_ProfuttoStaffInfo data)
        {
            PP_ProfuttoStaffInfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_ProfuttoStaffInfoView model)
        {
            PP_ProfuttoStaffInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_ProfuttoStaffInfoView model)
        {
            PP_ProfuttoStaffInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_ProfuttoStaffInfoView model)
        {
            PP_ProfuttoStaffInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_ProfuttoStaffInfoView> model)
        {
            IList<PP_ProfuttoStaffInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffInfoView> NGetList()
        {
            List<PP_ProfuttoStaffInfo> listdata = base.GetList() as List<PP_ProfuttoStaffInfo>;
            IList<PP_ProfuttoStaffInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffInfoView> NGetList_id(string id)
        {
            List<PP_ProfuttoStaffInfo> list = base.GetList_id(id) as List<PP_ProfuttoStaffInfo>;
            IList<PP_ProfuttoStaffInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_ProfuttoStaffInfo> NGetListID(string id)
        {
            IList<PP_ProfuttoStaffInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_ProfuttoStaffInfoView NGetModelById(string id)
        {
            PP_ProfuttoStaffInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过个人（员工）Id查找对应的收入项的关系记录
        /// <summary>
        /// 通过个人（员工）Id查找对应的收入项的关系记录
        /// </summary>
        /// <param name="staffId">Id</param>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffInfoView> Getptoslistbystaffid(string staffId)
        {
            string Hqlstr = string.Format(" from PP_ProfuttoStaffInfo where StaffId='{0}' and type='0'",staffId);
            List<PP_ProfuttoStaffInfo> list = HibernateTemplate.Find<PP_ProfuttoStaffInfo>(Hqlstr) as List<PP_ProfuttoStaffInfo>;
            IList<PP_ProfuttoStaffInfoView> modellist = GetViewlist(list);
            return modellist;
        }
        #endregion

        #region //通过个人（员工）Id查找对应的支出项的管理记录

        /// <summary>
        /// 通过个人（员工）Id查找对应的支出项的管理记录
        /// </summary>
        /// <param name="staffId">个人（员工）Id</param>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffInfoView> Getptoszhichulistbystaffid(string staffId)
        {
            string Hqlstr = string.Format(" from PP_ProfuttoStaffInfo where StaffId='{0}' and type='1'", staffId);
            List<PP_ProfuttoStaffInfo> list = HibernateTemplate.Find<PP_ProfuttoStaffInfo>(Hqlstr) as List<PP_ProfuttoStaffInfo>;
            IList<PP_ProfuttoStaffInfoView> modellist = GetViewlist(list);
            return modellist;
        }

        #endregion
    }
}
