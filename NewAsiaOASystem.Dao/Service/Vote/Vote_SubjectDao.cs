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
    public class Vote_SubjectDao : ServiceConversion<Vote_SubjectView, Vote_Subject>, IVote_SubjectDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Vote_SubjectView GetView(Vote_Subject data)
        {
            Vote_SubjectView view = ConvertToDTO(data);
            if (data.S_QX != null)
            {
                if (data.S_QX > DateTime.Now)
                {
                    view.S_QX = "正常";
                }
                else
                {
                    view.S_QX = "到期";
                }
            }
            return view;
        }

        /// <summary>
        /// TDO 转换成 DATA
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>

        public override Vote_Subject GetData(Vote_SubjectView view)
        {
            try {
                Vote_Subject data = new Vote_Subject();
                data = ConvertToData(view);
                //if (view.S_QX != null)
                //{
                //    data.S_QX = Convert.ToDateTime(view.S_QX);
                //}
                return data;
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Vote_SubjectView model)
        {
            Vote_Subject listmodel = GetData(model);
            return base.insert(listmodel);
        }



        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Vote_SubjectView model)
        {
            Vote_Subject listmodel = GetData(model);
            return base.Update(listmodel);
        }
        #region //没有转换的跟新
        public bool DataUpdate(Vote_Subject model)
        {
            return base.Update(model);
        } 
        #endregion
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Vote_SubjectView model)
        {
            Vote_Subject listmodel = GetData(model);
            return base.Delete(listmodel);
        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Vote_SubjectView> model)
        {
            IList<Vote_Subject> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Vote_SubjectView> NGetList()
        {
            List<Vote_Subject> listdata = base.GetList() as List<Vote_Subject>;
            IList<Vote_SubjectView> listmodel = GetViewlist(listdata);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Vote_SubjectView> NGetList_id(string id)
        {
            List<Vote_Subject> list = base.GetList_id(id) as List<Vote_Subject>;
            IList<Vote_SubjectView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Vote_Subject> NGetListID(string id)
        {
            IList<Vote_Subject> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vote_SubjectView NGetModelById(string id)
        {
            Vote_SubjectView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查找数据
        public Vote_Subject GetdataModelbyID(string id)
        {
            return base.GetModelById(id);
        } 
        #endregion

        #region //根据多个Id 查询多条记录
        public IList<Vote_SubjectView> NGetListdata_id(string id)
        {
  
            string tempHql = string.Format(" from  Vote_Subject  where  S_Id  in ({0})", id);
            try
            {
                List<Vote_Subject> list = Session.CreateQuery(tempHql).List<Vote_Subject>() as List<Vote_Subject>;
                IList<Vote_SubjectView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //插入
        public bool DataInsert(Vote_Subject model)
        {
            return base.insert(model);
        }
        #endregion

        #region //删除
        public bool DataDelete(Vote_Subject model)
        {
            return base.Delete(model);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DataDelete(List<Vote_Subject> model)
        {
            return base.NDelete(model);
        }
        #endregion



        #region //根据多个Id 查询多条记录
        public IList<Vote_Subject> VoteGetListdata_id(string id)
        {

            string tempHql = string.Format(" from  Vote_Subject  where  S_Id  in ({0})", id);
            try
            {
                List<Vote_Subject> list = Session.CreateQuery(tempHql).List<Vote_Subject>() as List<Vote_Subject>;
                //IList<Vote_SubjectView> listmodel = GetViewlist(list);
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


        #region //根据问卷的类型查选问卷的
        public IList<Vote_SubjectView> VoteGetListdataby_type(int type)
        {
            string tempHql;
            if (type == 1)
            {
                tempHql = string.Format(" from  Vote_Subject  where  S_QX>'{0}'", DateTime.Now);
            }
            else
            {
                 tempHql = string.Format(" from  Vote_Subject  where  S_Type='{0}' and S_QX>'{1}'", type,DateTime.Now);
            }
            try
            {
                List<Vote_Subject> list = Session.CreateQuery(tempHql).List<Vote_Subject>() as List<Vote_Subject>;
                IList<Vote_SubjectView> listmodel = GetViewlist(list);
                return listmodel;
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
