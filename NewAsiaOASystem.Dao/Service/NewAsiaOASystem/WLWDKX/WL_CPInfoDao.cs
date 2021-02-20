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
    public class WL_CPInfoDao:ServiceConversion<WL_CPInfoView,WL_CPInfo>,IWL_CPInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WL_CPInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WL_CPInfoView GetView(WL_CPInfo data)
        {
            WL_CPInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WL_CPInfoView model)
        {
            WL_CPInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WL_CPInfoView model)
        {
            WL_CPInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WL_CPInfoView model)
        {
            WL_CPInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WL_CPInfoView> model)
        {
            IList<WL_CPInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WL_CPInfoView> NGetList()
        {
            List<WL_CPInfo> listdata = base.GetList() as List<WL_CPInfo>;
            IList<WL_CPInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WL_CPInfoView> NGetList_id(string id)
        {
            List<WL_CPInfo> list = base.GetList_id(id) as List<WL_CPInfo>;
            IList<WL_CPInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WL_CPInfo> NGetListID(string id)
        {
            IList<WL_CPInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WL_CPInfoView NGetModelById(string id)
        {
            WL_CPInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region // 区域管理 分页列表数据 +GetQyinfoList（）
        /// <summary>
        /// 区域管理 分页列表数据
        /// </summary>
        /// <param name="Name">区域名称</param>
        /// <param name="user">当前登录用户</param>
        /// <returns></returns>
        public PagerInfo<WL_CPInfoView> GetWLcpinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Qyname like '%{0}%'", Name);
            TempHql.AppendFormat(" and u.States='{0}'", "0");
            TempHql.AppendFormat("order by Sort desc");
            PagerInfo<WL_CPInfoView> list = Search();
            return list;
        }
        #endregion
    }
}
