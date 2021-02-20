using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NHibernate;
using Spring.Data;
using Spring.Data.NHibernate.Generic;
using Spring.Data.NHibernate.Generic.Support;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace NewAsiaOASystem.Dao
{
    public abstract class DBService<T> : HibernateDaoSupport where T : class
    {

        public ISession GetSession()
        {
            return Session;
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool insert(T model)
        {
            try
            {
                HibernateTemplate.Save(model);
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return true;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            try
            {
                HibernateTemplate.Merge(model);//此处不能使用Update否则多表更新时报错
               // HibernateTemplate.Update(model);
                return true;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            try
            {
                HibernateTemplate.Delete(model);
                return true;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return false;
            }
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(IList<T> model)
        {
            try
            {
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        HibernateTemplate.Delete(item);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return false;
            }
        }

        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        public IList<T> GetList()
        {
          string tempHql = " from " + typeof(T).ToString() + " as u ";
            try
            {
                IList<T> list = Session.CreateQuery(tempHql).List<T>();
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }

        /// <summary>
        /// 排序查询
        /// </summary>
        /// <param name="temHql">hsql语句</param>
        /// <returns></returns>
        public IList<T> GetList_orderby(string orderby)
        {
            string tempHql = " from " + typeof(T).ToString() + " as u  order by " + orderby;
            try
            {
                IList<T> list = Session.CreateQuery(tempHql).List<T>();
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }

        /// <summary>
        /// 根据HQL条件查询
        /// </summary>
        /// <param name="temHql">HQL条件</param>
        /// <returns></returns>
        public IList<T> GetList_HQL(string HQL)
        {
            string tempHql = " from " + typeof(T).ToString() + " as u  " + HQL;
            try
            {
                IList<T> list = Session.CreateQuery(tempHql).List<T>();
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }

        /// <summary>
        /// 获取记录总条数
        /// </summary>
        /// <param name="SqlStr">查询条件</param>
        /// <returns></returns>
        public int PageCount(string SqlStr)
        {
            try
            {
                if (string.IsNullOrEmpty(SqlStr))
                {
                    SqlStr = "1=1";
                }
                T t = default(T);
                t = Activator.CreateInstance<T>();
                IQuery query = Session.CreateQuery(string.Format("select count(ID) from {0} where 1=1 and {1} ", t.GetType().Name, SqlStr));
                int i = Convert.ToInt32(query.UniqueResult());
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return i;
            }

            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return 0;
            }
        }

        /// <summary>
        /// 根据多个ID  查询多个记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<T> GetList_id(string id)
        {
            try
            {
                T t = default(T);
                t = Activator.CreateInstance<T>();
                string HQLSTR = string.Format(" from {0} where Id in ({1})", t.GetType().Name, id);
                return HibernateTemplate.Find<T>(HQLSTR);
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }

        public T GetModelById(string keyId)
        {
            try
            {
                return Session.Get<T>(keyId);
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// 根据sql查询返回表集合
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            SqlConnection conn = null;
            string connStr = System.Web.Configuration.WebConfigurationManager.AppSettings["connStr"];
            using (conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
                catch (Exception e)
                {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
                }
            }
        }

     
    }
}
