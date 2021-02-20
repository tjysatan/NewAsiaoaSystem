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
    /// 不良原因
    /// </summary>
    public class NQ_BlinfoDao:ServiceConversion<NQ_BlinfoView,NQ_Blinfo>,INQ_BlinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_Blinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_BlinfoView GetView(NQ_Blinfo data)
        {
            NQ_BlinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_BlinfoView model)
        {
            NQ_Blinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_BlinfoView model)
        {
            NQ_Blinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_BlinfoView model)
        {
            NQ_Blinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_BlinfoView> model)
        {
            IList<NQ_Blinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_BlinfoView> NGetList()
        {
            List<NQ_Blinfo> listdata = base.GetList() as List<NQ_Blinfo>;
            IList<NQ_BlinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_BlinfoView> NGetList_id(string id)
        {
            List<NQ_Blinfo> list = base.GetList_id(id) as List<NQ_Blinfo>;
            IList<NQ_BlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_Blinfo> NGetListID(string id)
        {
            IList<NQ_Blinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_BlinfoView NGetModelById(string id)
        {
            NQ_BlinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        public PagerInfo<NQ_BlinfoView> GetCinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
            TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
            PagerInfo<NQ_BlinfoView> list = Search();
            return list;

        }

        #region //查询全部的父级原因信息
        public IList<NQ_BlinfoView> GetlistisPar()
        {
            List<NQ_Blinfo> list = HibernateTemplate.Find<NQ_Blinfo>("from NQ_Blinfo where ParentId='0' and Status='1'") as List<NQ_Blinfo>;
            IList<NQ_BlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //父级ID 查询返退货原因信息
        public IList<NQ_BlinfoView> Getlistreason_byPid(string PID)
        {
            List<NQ_Blinfo> list = HibernateTemplate.Find<NQ_Blinfo>("from NQ_Blinfo where ParentId='" + PID + "' and Status='1'") as List<NQ_Blinfo>;
            IList<NQ_BlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //通过Id查询不良原因
        public IList<NQ_BlinfoView> Getlistreason_byId(string Id)
        {
            List<NQ_Blinfo> list = HibernateTemplate.Find<NQ_Blinfo>("from NQ_Blinfo where Id='" + Id + "'") as List<NQ_Blinfo>;
            IList<NQ_BlinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
