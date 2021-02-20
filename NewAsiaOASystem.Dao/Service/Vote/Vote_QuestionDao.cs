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
    public class Vote_QuestionDao : ServiceConversion<Vote_QuestionView, Vote_Question>, IVote_QuestionDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Vote_QuestionView GetView(Vote_Question data)
        {
            Vote_QuestionView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Vote_QuestionView model)
        {
            Vote_Question listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Vote_QuestionView model)
        {
            Vote_Question listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Vote_QuestionView model)
        {
            Vote_Question listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Vote_QuestionView> model)
        {
            IList<Vote_Question> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Vote_QuestionView> NGetList()
        {
            List<Vote_Question> listdata = base.GetList() as List<Vote_Question>;
            IList<Vote_QuestionView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Vote_QuestionView> NGetList_id(string id)
        {
            List<Vote_Question> list = base.GetList_id(id) as List<Vote_Question>;
            IList<Vote_QuestionView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Vote_Question> NGetListID(string id)
        {
            IList<Vote_Question> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vote_QuestionView NGetModelById(string id)
        {
            Vote_QuestionView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //根据标题T_Id 查找该标题下所有的选项
        public IList<Vote_QuestionView> Vote_QGetListby_Tid(string id)
        {
            id = "'" + id + "'";
            string tempHql = string.Format(" from  Vote_Question  where  T_Id  in ({0})  order by Q_Order asc", id);
            try
            {
                List<Vote_Question> list = Session.CreateQuery(tempHql).List<Vote_Question>() as List<Vote_Question>;
                IList<Vote_QuestionView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion


        #region 根据多个主题ID 查找对应的V选项
        public IList<Vote_QuestionView> VoteQGetVlistby_Msid(string sid)
        {
            string tempHql = string.Format(" from  Vote_Question  where  S_Id  in ({0})", sid);
            try
            {
                List<Vote_Question> list = Session.CreateQuery(tempHql).List<Vote_Question>() as List<Vote_Question>;
                IList<Vote_QuestionView> listmodel = GetViewlist(list);
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
