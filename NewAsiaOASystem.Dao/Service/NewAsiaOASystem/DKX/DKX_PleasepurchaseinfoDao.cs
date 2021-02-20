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
    public class DKX_PleasepurchaseinfoDao:ServiceConversion<DKX_PleasepurchaseinfoView,DKX_Pleasepurchaseinfo>,IDKX_PleasepurchaseinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_Pleasepurchaseinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_PleasepurchaseinfoView GetView(DKX_Pleasepurchaseinfo data)
        {
            DKX_PleasepurchaseinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_PleasepurchaseinfoView model)
        {
            DKX_Pleasepurchaseinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_PleasepurchaseinfoView model)
        {
            DKX_Pleasepurchaseinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_PleasepurchaseinfoView model)
        {
            DKX_Pleasepurchaseinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_PleasepurchaseinfoView> model)
        {
            IList<DKX_Pleasepurchaseinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_PleasepurchaseinfoView> NGetList()
        {
            List<DKX_Pleasepurchaseinfo> listdata = base.GetList() as List<DKX_Pleasepurchaseinfo>;
            IList<DKX_PleasepurchaseinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_PleasepurchaseinfoView> NGetList_id(string id)
        {
            List<DKX_Pleasepurchaseinfo> list = base.GetList_id(id) as List<DKX_Pleasepurchaseinfo>;
            IList<DKX_PleasepurchaseinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_Pleasepurchaseinfo> NGetListID(string id)
        {
            IList<DKX_Pleasepurchaseinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_PleasepurchaseinfoView NGetModelById(string id)
        {
            DKX_PleasepurchaseinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //根据元器件的编码查询各个状态下的采购单
        /// <summary>
        /// 根据元器件的编码查询各个状态下的采购单
        /// </summary>
        /// <param name="wlbm">物料编码</param>
        /// <param name="type">请购单状态 0 未采购 1 采购中 2 完成</param>
        /// <returns></returns>
        public DKX_PleasepurchaseinfoView GetYQJQgdanDATAbyyqjbm(string wlbm, string type)
        {
            string Hqlstr = string.Format(" from DKX_Pleasepurchaseinfo where Yqj_bianhao='{0}' and Type='{1}'",wlbm,type);
            List<DKX_Pleasepurchaseinfo> list = HibernateTemplate.Find<DKX_Pleasepurchaseinfo>(Hqlstr) as List<DKX_Pleasepurchaseinfo>;
            IList<DKX_PleasepurchaseinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion


        #region //采购单分页列表（生产）
        /// <summary>
        /// 采购单分页列表（生产）
        /// </summary>
        /// <param name="yqjbm">元器件编码</param>
        /// <param name="yqjnam">元器件名称</param>
        /// <param name="yqjxh">元器件型号</param>
        /// <param name="type">采购单当前的状态 0 未采购 1采购中 2 完成</param>
        /// <param name="starttime">下单时间</param>
        /// <param name="endtime"></param>
        /// <param name="user">当前登录的帐号</param>
        /// <returns></returns>
        public PagerInfo<DKX_PleasepurchaseinfoView> GetDKX_SCcgdpagelist(string yqjbm, string yqjnam, string yqjxh, string type, string starttime,
            string endtime, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(yqjbm))
                TempHql.AppendFormat(" and Yqj_bianhao like '%{0}%'", yqjbm);
            if (!string.IsNullOrEmpty(yqjnam))
                TempHql.AppendFormat(" and Yqj_Name like '%{0}%'",yqjnam);
            if (!string.IsNullOrEmpty(yqjxh))
                TempHql.AppendFormat(" and Yqj_Namexh like '%{0}%'",yqjxh);
            if (!string.IsNullOrEmpty(type))
                TempHql.AppendFormat(" and Type='{0}'",type);
            if(!string.IsNullOrEmpty(starttime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            TempHql.AppendFormat("order by u.C_time desc");
            PagerInfo<DKX_PleasepurchaseinfoView> list = Search();
            return list;
        }
        #endregion

        
    }
}
