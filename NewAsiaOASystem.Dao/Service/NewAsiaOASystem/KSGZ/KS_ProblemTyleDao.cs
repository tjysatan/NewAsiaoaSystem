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
    public class KS_ProblemTyleDao:ServiceConversion<KS_ProblemTyleView,KS_ProblemTyle>,IKS_ProblemTyleDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(KS_ProblemTyle).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override KS_ProblemTyleView GetView(KS_ProblemTyle data)
        {
            KS_ProblemTyleView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(KS_ProblemTyleView model)
        {
            KS_ProblemTyle listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(KS_ProblemTyleView model)
        {
            KS_ProblemTyle listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(KS_ProblemTyleView model)
        {
            KS_ProblemTyle listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<KS_ProblemTyleView> model)
        {
            IList<KS_ProblemTyle> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<KS_ProblemTyleView> NGetList()
        {
            List<KS_ProblemTyle> listdata = base.GetList() as List<KS_ProblemTyle>;
            IList<KS_ProblemTyleView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<KS_ProblemTyleView> NGetList_id(string id)
        {
            List<KS_ProblemTyle> list = base.GetList_id(id) as List<KS_ProblemTyle>;
            IList<KS_ProblemTyleView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<KS_ProblemTyle> NGetListID(string id)
        {
            IList<KS_ProblemTyle> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KS_ProblemTyleView NGetModelById(string id)
        {
            KS_ProblemTyleView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //客诉问题分类的分页数据
        /// <summary>
        /// 客诉问题分类的分页数据
        /// </summary>
        /// <param name="WTname">问题分类名称</param>
        /// <param name="start"></param>
        /// <returns></returns>
        public PagerInfo<KS_ProblemTyleView> KSGetProblemTylePagerlist(string WTname, string start)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(WTname))
                TempHql.AppendFormat(" and u.WTName like '%{0}%'", WTname);
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and Start='{0}'", start);
            else
                TempHql.AppendFormat(" and Start='0'");
            TempHql.AppendFormat("order by u.Sort asc");
            PagerInfo<KS_ProblemTyleView> list = Search();
            return list;
        }
        #endregion
    }
}
