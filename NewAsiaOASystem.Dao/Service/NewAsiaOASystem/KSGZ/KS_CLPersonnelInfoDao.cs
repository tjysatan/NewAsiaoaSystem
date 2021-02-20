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
    public class KS_CLPersonnelInfoDao:ServiceConversion<KS_CLPersonnelInfoView,KS_CLPersonnelInfo>,IKS_CLPersonnelInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(KS_CLPersonnelInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override KS_CLPersonnelInfoView GetView(KS_CLPersonnelInfo data)
        {
            KS_CLPersonnelInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(KS_CLPersonnelInfoView model)
        {
            KS_CLPersonnelInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(KS_CLPersonnelInfoView model)
        {
            KS_CLPersonnelInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(KS_CLPersonnelInfoView model)
        {
            KS_CLPersonnelInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<KS_CLPersonnelInfoView> model)
        {
            IList<KS_CLPersonnelInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<KS_CLPersonnelInfoView> NGetList()
        {
            List<KS_CLPersonnelInfo> listdata = base.GetList() as List<KS_CLPersonnelInfo>;
            IList<KS_CLPersonnelInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<KS_CLPersonnelInfoView> NGetList_id(string id)
        {
            List<KS_CLPersonnelInfo> list = base.GetList_id(id) as List<KS_CLPersonnelInfo>;
            IList<KS_CLPersonnelInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<KS_CLPersonnelInfo> NGetListID(string id)
        {
            IList<KS_CLPersonnelInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KS_CLPersonnelInfoView NGetModelById(string id)
        {
            KS_CLPersonnelInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //客诉处理人员的分页数据
        /// <summary>
        /// 客诉处理人员的分页数据
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="zhname">帐号</param>
        /// <param name="tel">电话</param>
        /// <param name="start">启用状态</param>
        /// <returns></returns>
        public PagerInfo<KS_CLPersonnelInfoView> KSGetCLRinfoPagerlist(string name, string zhname, string tel, string start)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(name))
                TempHql.AppendFormat(" and u.Name like '%{0}%'",name);
            if(!string.IsNullOrEmpty(zhname))
                TempHql.AppendFormat(" and ZH_Id in (select Id from SysPerson where Name like '%{0}%')", zhname);
            if (!string.IsNullOrEmpty(tel))
                TempHql.AppendFormat(" and u.Tel like '%{0}%'", tel);
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and Start='{0}'", start);
            else
                TempHql.AppendFormat(" and Start='0'");
            TempHql.AppendFormat("order by u.Sort asc");
            PagerInfo<KS_CLPersonnelInfoView> list = Search();
            return list;
        }
        #endregion
    }
}
