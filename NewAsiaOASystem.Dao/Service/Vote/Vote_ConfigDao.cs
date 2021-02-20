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
    public class Vote_ConfigDao : ServiceConversion<Vote_ConfigView, Vote_Config>, IVote_ConfigDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Vote_ConfigView GetView(Vote_Config data)
        {
            Vote_ConfigView view = ConvertToDTO(data);

            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Vote_ConfigView model)
        {
            Vote_Config listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Vote_ConfigView model)
        {
            Vote_Config listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Vote_ConfigView model)
        {
            Vote_Config listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Vote_ConfigView> model)
        {
            IList<Vote_Config> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Vote_ConfigView> NGetList()
        {
            List<Vote_Config> listdata = base.GetList() as List<Vote_Config>;
            IList<Vote_ConfigView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        #region //查找第一个
        public Vote_ConfigView NGetone()
        {
            IList<Vote_ConfigView> listmodel = NGetList();
            Vote_ConfigView model = new Vote_ConfigView();
            if (listmodel != null)
            {
                  model=listmodel[0];
            }
            else
            {
                model=null;
            }
            return model;
        } 
        #endregion

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Vote_ConfigView> NGetList_id(string id)
        {
            List<Vote_Config> list = base.GetList_id(id) as List<Vote_Config>;
            IList<Vote_ConfigView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Vote_Config> NGetListID(string id)
        {
            IList<Vote_Config> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vote_ConfigView NGetModelById(string id)
        {
            Vote_ConfigView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


    }
}
