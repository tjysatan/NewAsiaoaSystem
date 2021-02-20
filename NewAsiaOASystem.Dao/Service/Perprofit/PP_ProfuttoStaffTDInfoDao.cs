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
    public class PP_ProfuttoStaffTDInfoDao:ServiceConversion<PP_ProfuttoStaffTDInfoView,PP_ProfuttoStaffTDInfo>,IPP_ProfuttoStaffTDInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(PP_ProfuttoStaffTDInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override PP_ProfuttoStaffTDInfoView GetView(PP_ProfuttoStaffTDInfo data)
        {
            PP_ProfuttoStaffTDInfoView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(PP_ProfuttoStaffTDInfoView model)
        {
            PP_ProfuttoStaffTDInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(PP_ProfuttoStaffTDInfoView model)
        {
            PP_ProfuttoStaffTDInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(PP_ProfuttoStaffTDInfoView model)
        {
            PP_ProfuttoStaffTDInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<PP_ProfuttoStaffTDInfoView> model)
        {
            IList<PP_ProfuttoStaffTDInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffTDInfoView> NGetList()
        {
            List<PP_ProfuttoStaffTDInfo> listdata = base.GetList() as List<PP_ProfuttoStaffTDInfo>;
            IList<PP_ProfuttoStaffTDInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffTDInfoView> NGetList_id(string id)
        {
            List<PP_ProfuttoStaffTDInfo> list = base.GetList_id(id) as List<PP_ProfuttoStaffTDInfo>;
            IList<PP_ProfuttoStaffTDInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<PP_ProfuttoStaffTDInfo> NGetListID(string id)
        {
            IList<PP_ProfuttoStaffTDInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PP_ProfuttoStaffTDInfoView NGetModelById(string id)
        {
            PP_ProfuttoStaffTDInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据员工查询与之有关系的团体收入项目信息
        /// <summary>
        /// 根据员工查询与之有关系的团体收入项目信息
        /// </summary>
        /// <param name="Id">个人Id</param>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffTDInfoView> GetProfuttostafIdTDshouruinfobystafId(string Id)
        {
            string Hqlstr = string.Format(" from PP_ProfuttoStaffTDInfo where StaffId='{0}' and type='0'", Id);
            List<PP_ProfuttoStaffTDInfo> list = HibernateTemplate.Find<PP_ProfuttoStaffTDInfo>(Hqlstr) as List<PP_ProfuttoStaffTDInfo>;
            IList<PP_ProfuttoStaffTDInfoView> modellist = GetViewlist(list);
            return modellist;
        }
        #endregion

        #region //根据员工查询与之有关系的团体支出项目信息
        /// <summary>
        /// 根据员工查询与之有关系的团体支出项目信息
        /// </summary>
        /// <param name="Id">个人Id</param>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffTDInfoView> GetProfuttpstafIdTDzhichubystafId(string Id)
        {
            string Hqlstr = string.Format(" from PP_ProfuttoStaffTDInfo where StaffId='{0}' and type='1'", Id);
            List<PP_ProfuttoStaffTDInfo> list = HibernateTemplate.Find<PP_ProfuttoStaffTDInfo>(Hqlstr) as List<PP_ProfuttoStaffTDInfo>;
            IList<PP_ProfuttoStaffTDInfoView> modellist = GetViewlist(list);
            return modellist;
        }

        #endregion

        #region //通过个人Id和团体项目Id查找关系信息
        /// <summary>
        /// //通过个人Id和团体项目Id查找关系信息
        /// </summary>
        /// <param name="stafId">个人Id</param>
        /// <param name="ProfutId">项目Id</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public PP_ProfuttoStaffTDInfoView GetstafIdtoProfitinfobystafId(string stafId, string ProfutId, string type)
        {
            string Hqlstr = string.Format(" from PP_ProfuttoStaffTDInfo where StaffId='{0}' and ProfutId='{1}' and Type='{2}'",stafId,ProfutId,type);
            List<PP_ProfuttoStaffTDInfo> list = HibernateTemplate.Find<PP_ProfuttoStaffTDInfo>(Hqlstr) as List<PP_ProfuttoStaffTDInfo>;
            IList<PP_ProfuttoStaffTDInfoView> modellist = GetViewlist(list);
            if (modellist != null)
                return modellist[0];
            else
                return null;
        }
        #endregion

        #region //通过团体项目Id查找团体和个人的关系数据
        /// <summary>
        /// 通过团体项目Id查找团体和个人的关系数据
        /// </summary>
        /// <param name="profuId">项目Id</param>
        /// <returns></returns>
        public IList<PP_ProfuttoStaffTDInfoView> GetprofIdtostafIdinfobyprofuId(string profuId)
        {
            string Hqlstr = string.Format(" from PP_ProfuttoStaffTDInfo where ProfutId='{0}'",profuId);
            List<PP_ProfuttoStaffTDInfo> list = HibernateTemplate.Find<PP_ProfuttoStaffTDInfo>(Hqlstr) as List<PP_ProfuttoStaffTDInfo>;
            IList<PP_ProfuttoStaffTDInfoView> modellist = GetViewlist(list);
            return modellist;
        }
        #endregion

    }
}
