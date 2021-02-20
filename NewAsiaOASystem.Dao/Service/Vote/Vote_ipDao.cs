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
    public class Vote_ipDao : ServiceConversion<Vote_ipView, Vote_ip>, IVote_ipDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Vote_ipView GetView(Vote_ip data)
        {
            Vote_ipView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Vote_ipView model)
        {
            Vote_ip listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Vote_ipView model)
        {
            Vote_ip listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Vote_ipView model)
        {
            Vote_ip listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Vote_ipView> model)
        {
            IList<Vote_ip> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Vote_ipView> NGetList()
        {
            List<Vote_ip> listdata = base.GetList() as List<Vote_ip>;
            IList<Vote_ipView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Vote_ipView> NGetList_id(string id)
        {
            List<Vote_ip> list = base.GetList_id(id) as List<Vote_ip>;
            IList<Vote_ipView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Vote_ip> NGetListID(string id)
        {
            IList<Vote_ip> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vote_ipView NGetModelById(string id)
        {
            Vote_ipView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据主题ID 和IP 检查该IP 是否重复提交
        public bool JcIP(string sid, string IP)
        {
            List<Vote_ip> list = HibernateTemplate.Find<Vote_ip>("from Vote_ip where S_Id='" + sid + "' and I_IP='" + IP + "'") as List<Vote_ip>;
            IList<Vote_ipView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return false;
            return listmodel.Count > 0 ? true : false;
        } 
        #endregion

        #region //根据主题ID 和IP  查询实体
        public Vote_ipView  JcIPmodel(string sid, string IP)
        {
            List<Vote_ip> list = HibernateTemplate.Find<Vote_ip>("from Vote_ip where S_Id='" + sid + "' and I_IP='" + IP + "'") as List<Vote_ip>;
            IList<Vote_ipView> listmodel = GetViewlist(list);
            if (listmodel != null && listmodel.Count > 0)
            {
                return listmodel[0];
            }
            return null;
        }
        #endregion
    }
}
