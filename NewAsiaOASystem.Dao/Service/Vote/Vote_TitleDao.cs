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
    public class Vote_TitleDao : ServiceConversion<Vote_TitleView, Vote_Title>,IVote_TitleDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Vote_TitleView GetView(Vote_Title data)
        {
            Vote_TitleView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Vote_TitleView model)
        {
            Vote_Title listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Vote_TitleView model)
        {
            Vote_Title listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Vote_TitleView model)
        {
            Vote_Title listmodel = GetData(model);
            return base.Delete(listmodel);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Vote_TitleView> model)
        {
            IList<Vote_Title> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }



        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Vote_TitleView> NGetList()
        {
            List<Vote_Title> listdata = base.GetList() as List<Vote_Title>;
            IList<Vote_TitleView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Vote_TitleView> NGetList_id(string id)
        {
            List<Vote_Title> list = base.GetList_id(id) as List<Vote_Title>;
            IList<Vote_TitleView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Vote_Title> NGetListID(string id)
        {
            IList<Vote_Title> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vote_TitleView NGetModelById(string id)
        {
            Vote_TitleView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region 根据主题ID 查找对应的标题
        public IList<Vote_TitleView> GetVotetitleby_sid(string sid)
        {
            string tempHql = string.Format(" from  Vote_Title  where  S_Id  = '{0}'", sid);
            try
            {
                // List<WX_Message> list = Session.CreateQuery(tempHql) as List<WX_Message>;
                List<Vote_Title> list = Session.CreateQuery(tempHql).List<Vote_Title>() as List<Vote_Title>;
                IList<Vote_TitleView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion


        #region //保存后返回ID
        public string InsertID(Vote_TitleView modelView)
        {
            Vote_Title model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
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



        #region //没有转换的删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool VotedataDelete(Vote_Title model)
        {
            return base.Delete(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool VotedataDelete(List<Vote_Title> model)
        {
            return base.NDelete(model);
        } 
        #endregion


    
        #region 根据主题ID 查找对应的标题
        public IList<Vote_Title> VoteTGetlistby_sid(string sid)
        {
            string tempHql = string.Format(" from  Vote_Title  where  S_Id  = '{0}'", sid);
            try
            {
                List<Vote_Title> list = Session.CreateQuery(tempHql).List<Vote_Title>() as List<Vote_Title>;
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region 根据多个主题ID 查找对应的标题
        public IList<Vote_Title> VoteTGetlistby_Msid(string sid)
        {
            string tempHql = string.Format(" from  Vote_Title  where  S_Id  in ({0})", sid);
            try
            {
                List<Vote_Title> list = Session.CreateQuery(tempHql).List<Vote_Title>() as List<Vote_Title>;
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region 根据多个主题ID 查找对应的V标题
        public IList<Vote_TitleView> VoteTGetVlistby_Msid(string sid)
        {
            string tempHql = string.Format(" from  Vote_Title  where  S_Id  in ({0})", sid);
            try
            {
                List<Vote_Title> list = Session.CreateQuery(tempHql).List<Vote_Title>() as List<Vote_Title>;
                IList<Vote_TitleView> listmodel = GetViewlist(list);
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
