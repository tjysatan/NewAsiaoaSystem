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
    public class NQ_GysInfoDao:ServiceConversion<NQ_GysInfoView,NQ_GysInfo>,INQ_GysInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_GysInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_GysInfoView GetView(NQ_GysInfo data)
        {
            NQ_GysInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_GysInfoView model)
        {
            NQ_GysInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_GysInfoView model)
        {
            NQ_GysInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_GysInfoView model)
        {
            NQ_GysInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_GysInfoView> model)
        {
            IList<NQ_GysInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_GysInfoView> NGetList()
        {
            List<NQ_GysInfo> listdata = base.GetList() as List<NQ_GysInfo>;
            IList<NQ_GysInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_GysInfoView> NGetList_id(string id)
        {
            List<NQ_GysInfo> list = base.GetList_id(id) as List<NQ_GysInfo>;
            IList<NQ_GysInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_GysInfo> NGetListID(string id)
        {
            IList<NQ_GysInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_GysInfoView NGetModelById(string id)
        {
            NQ_GysInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public PagerInfo<NQ_GysInfoView> GetCinfoList(string Name,SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.G_Name like '%{0}%' ", Name);
         
            TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
            PagerInfo<NQ_GysInfoView> list = Search();
            return list;

        }



        #region //查询全部的客户信息
        public IList<NQ_GysInfoView> GetlistCust()
        {
            List<NQ_GysInfo> list = HibernateTemplate.Find<NQ_GysInfo>("from NQ_GysInfo order by Sort asc ") as List<NQ_GysInfo>;
            IList<NQ_GysInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //根据供应商代码查找供应商代码
        public string Getgysmodelbydm(string dm)
        {
            List<NQ_GysInfo> list = HibernateTemplate.Find<NQ_GysInfo>("from NQ_GysInfo where G_Dm='"+dm+"'") as List<NQ_GysInfo>;
            string json;
            if (list != null)
            {
                json = JsonConvert.SerializeObject(list[0]);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        #region //根据供应商代码查找供应商信息
        public NQ_GysInfoView Getmodelbydm(string dm)
        {
            List<NQ_GysInfo> list = HibernateTemplate.Find<NQ_GysInfo>("from NQ_GysInfo where G_Dm='" + dm + "'") as List<NQ_GysInfo>;
            IList<NQ_GysInfoView> listmodel = GetViewlist(list);
            return listmodel[0];

        } 
        #endregion

        #region //根据供应商名称查找供应商信息
        public NQ_GysInfoView Getmodelbygysname(string name)
        {
            List<NQ_GysInfo> list = HibernateTemplate.Find<NQ_GysInfo>("from NQ_GysInfo where G_Name like '%" + name + "%'") as List<NQ_GysInfo>;
            IList<NQ_GysInfoView> listmodel = GetViewlist(list);
            return listmodel[0];
        } 
        #endregion
    }
}
