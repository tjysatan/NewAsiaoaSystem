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
    public class Wx_openIdinfoDao:ServiceConversion<Wx_openIdinfoView,Wx_openIdinfo>,IWx_openIdinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Wx_openIdinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Wx_openIdinfoView model)
        {
            Wx_openIdinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Wx_openIdinfoView model)
        {
            Wx_openIdinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Wx_openIdinfoView model)
        {
            Wx_openIdinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Wx_openIdinfoView> model)
        {
            IList<Wx_openIdinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Wx_openIdinfoView> NGetList()
        {
            List<Wx_openIdinfo> listdata = base.GetList() as List<Wx_openIdinfo>;
            IList<Wx_openIdinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<Wx_openIdinfo> listdata = base.GetList() as List<Wx_openIdinfo>;
            IList<Wx_openIdinfoView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Wx_openIdinfoView> NGetList_id(string id)
        {
            List<Wx_openIdinfo> list = base.GetList_id(id) as List<Wx_openIdinfo>;
            IList<Wx_openIdinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Wx_openIdinfo> NGetListModel(string id)
        {
            IList<Wx_openIdinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Wx_openIdinfoView NGetModelById(string id)
        {
            Wx_openIdinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //通过openId和代理名称查询微信客户信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="openid">openId</param>
        /// <param name="dlname">代理名称</param>
        /// <returns></returns>
        public Wx_openIdinfoView Getnewcusinfobyiccid(string openid, string dlname)
        {
            string HQLstr = string.Format("from Wx_openIdinfo where openid='{0}' and Dlname='{1}'", openid, dlname);
            List<Wx_openIdinfo> list = HibernateTemplate.Find<Wx_openIdinfo>(HQLstr) as List<Wx_openIdinfo>;
            IList<Wx_openIdinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion

        #region //通过openId查找微信客户信息
        /// <summary>
        /// 通过openId查找微信客户信息
        /// </summary>
        /// <param name="openid">openId</param>
        /// <returns></returns>
        public Wx_openIdinfoView GetwxcusinfobyopenId(string openid)
        {
            string HQLstr = string.Format("from Wx_openIdinfo where openid='{0}'", openid);
            List<Wx_openIdinfo> list = HibernateTemplate.Find<Wx_openIdinfo>(HQLstr) as List<Wx_openIdinfo>;
            IList<Wx_openIdinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion

    }
}
