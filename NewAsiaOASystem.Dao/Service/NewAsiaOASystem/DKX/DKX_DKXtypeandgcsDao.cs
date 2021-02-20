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
    public class DKX_DKXtypeandgcsDao:ServiceConversion<DKX_DKXtypeandgcsView, DKX_DKXtypeandgcs>, IDKX_DKXtypeandgcsDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_DKXtypeandgcs).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_DKXtypeandgcsView GetView(DKX_DKXtypeandgcs data)
        {
            DKX_DKXtypeandgcsView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_DKXtypeandgcsView model)
        {
            DKX_DKXtypeandgcs listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_DKXtypeandgcsView model)
        {
            DKX_DKXtypeandgcs listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_DKXtypeandgcsView model)
        {
            DKX_DKXtypeandgcs listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_DKXtypeandgcsView> model)
        {
            IList<DKX_DKXtypeandgcs> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_DKXtypeandgcsView> NGetList()
        {
            List<DKX_DKXtypeandgcs> listdata = base.GetList() as List<DKX_DKXtypeandgcs>;
            IList<DKX_DKXtypeandgcsView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_DKXtypeandgcsView> NGetList_id(string id)
        {
            List<DKX_DKXtypeandgcs> list = base.GetList_id(id) as List<DKX_DKXtypeandgcs>;
            IList<DKX_DKXtypeandgcsView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_DKXtypeandgcs> NGetListID(string id)
        {
            IList<DKX_DKXtypeandgcs> list = base.GetList_id(id);
            return list;
        }


        //根据工程师的Id 查找对应的关系
        /// <summary>
        /// 根据工程师的Id 查找对应的关系
        /// </summary>
        /// <param name="gcsId">工程师Id</param>
        /// <returns></returns>
        public List<DKX_DKXtypeandgcs> GetdkxtypelistbygcsId(string gcsId)
        {
            string hqlstr = string.Format("from DKX_DKXtypeandgcs where gcsId='{0}'",gcsId);
            try
            {
                List<DKX_DKXtypeandgcs> list = Session.CreateQuery(hqlstr).List<DKX_DKXtypeandgcs>() as List<DKX_DKXtypeandgcs>;
                //IList<SysPersonView> listmodel = GetViewlist(list);
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }

        /// <summary>
        /// 根据工程师的Id 查找对应的关系
        /// </summary>
        /// <param name="gcsId"></param>
        /// <returns></returns>
        public IList<DKX_DKXtypeandgcsView> GetdkxtypeandgcslistViewbygcsId(string gcsId)
        {
            string hqlstr = string.Format("from DKX_DKXtypeandgcs where gcsId='{0}'", gcsId);
            try
            {
                List<DKX_DKXtypeandgcs> list = Session.CreateQuery(hqlstr).List<DKX_DKXtypeandgcs>() as List<DKX_DKXtypeandgcs>;
                IList<DKX_DKXtypeandgcsView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_DKXtypeandgcsView NGetModelById(string id)
        {
            DKX_DKXtypeandgcsView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }
    }
}
