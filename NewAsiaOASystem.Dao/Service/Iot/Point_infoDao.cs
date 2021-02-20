using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
   public  class Point_infoDao:ServiceConversion<Point_infoView,Point_info>,IPoint_infoDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
       public override Point_infoView GetView(Point_info data)
        {
            Point_infoView view = ConvertToDTO(data);
            if (data.sysperson != null)
            {
                SysPerson person = new SysPerson();
                person.Id = data.sysperson.Id;
                person.Name = data.sysperson.Name;
                person.Tel = data.sysperson.Tel;
                if (data.sysperson.UserName != null)
                {
                    person.UserName = data.sysperson.UserName;
                }
                person.Department = null;
                person.Role = null;
                view.sysperson = JsonConvert.SerializeObject(person);
            }  
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool Ninsert(Point_infoView model)
        {
            Point_info listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool NUpdate(Point_infoView model)
        {
            Point_info listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool NDelete(Point_infoView model)
        {
            Point_info listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public bool NDelete(List<Point_infoView> model)
        {
            IList<Point_info> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
       public IList<Point_infoView> NGetList()
        {
            List<Point_info> listdata = base.GetList() as List<Point_info>;
            IList<Point_infoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public IList<Point_infoView> NGetList_id(string id)
        {
            List<Point_info> list = base.GetList_id(id) as List<Point_info>;
            IList<Point_infoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
       public IList<Point_info> NGetListID(string id)
        {
            IList<Point_info> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public Point_infoView NGetModelById(string id)
        {
            Point_infoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


       #region //查找所有巡查人员的当前最先位置的数据 +GetNewPointData()
       public IList<Point_info> GetNewPointData()
       {
           string tempHql = " from  Point_info  where  P_Time in (select Max(P_Time) from Point_info group by P_Id)";
           try
           {
               IList<Point_info> list = Session.CreateQuery(tempHql).List<Point_info>();
               return list;
           }
           catch (Exception e)
           {
               log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
               return null;
           }
       } 
       #endregion

       #region //通过多个ID查找巡查人员的信息的数据  +GetNewPointDatabyId(string Id)
       public IList<Point_info> GetNewPointDatabyId(string Id)
       {
           string tempHql = string.Format(" from  Point_info  where  P_Time in (select Max(P_Time) from Point_info group by P_Id) and P_Id in ({0})", Id);
           //string.Format(" from {0} where Id in ({1})", t.GetType().Name, id)
           try
           {
               IList<Point_info> list = Session.CreateQuery(tempHql).List<Point_info>();
               return list;
           }
           catch (Exception e)
           {
               log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
               return null;
           }
       } 
       #endregion

       #region //根据P_ID查找巡查人员在一段时间内的巡查信息
       public IList<Point_info> GetlineDatabyP_Id(string P_Id)
       {
           string tempHql = " from  Point_info  where P_Id ='" + P_Id + "' order by P_Time desc";
           try
           {
               IList<Point_info> list = Session.CreateQuery(tempHql).SetFirstResult(0).SetMaxResults(50).List<Point_info>();
               return list;
           }
           catch (Exception e)
           {
               log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
               return null;
           }
       }
       
       #endregion

       #region //根据P_ID查找巡查人员在一段时间内的巡查信息转换成json +GetlinejsonbyP_Id(string P_Id); 
       public string GetlinejsonbyP_Id(string P_Id)
       {
           List<Point_info> list = GetlineDatabyP_Id(P_Id) as List<Point_info>;
           IList<Point_infoView> listview = GetViewlist(list);
           string str = JsonConvert.SerializeObject(listview);
           return str;
       } 
       #endregion


       #region //获取巡查人员当前最新位置信息+GetNewPoint()
       public string GetNewPoint()
       {
           List<Point_info> list = GetNewPointData() as List<Point_info>;
           IList<Point_infoView> listview = GetViewlist(list);
           string str = JsonConvert.SerializeObject(listview);
           return str;
       } 
       #endregion


      #region //根据ID 查询巡查人员的当前最新位置信息+GetNewPointby_Id(string Id)
       public string GetNewPointby_Id(string Id)
       {
           List<Point_info> list = GetNewPointDatabyId(Id) as List<Point_info>;
           IList<Point_infoView> listview = GetViewlist(list);
           string str = JsonConvert.SerializeObject(listview);
           return str;
       } 
       #endregion

       #region //获取全部巡查路线的信息 +GetPointlinedata()
       public string GetPointlinedata()
       {
           IList<Point_infoView> listview = NGetList();
           string str = JsonConvert.SerializeObject(listview);
           return str;
       } 
       #endregion
    }
}
