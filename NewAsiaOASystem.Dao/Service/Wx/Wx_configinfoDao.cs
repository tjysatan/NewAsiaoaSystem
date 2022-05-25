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
    public class  Wx_configinfoDao:ServiceConversion<Wx_configinfoView,Wx_configinfo>,IWx_configinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Wx_configinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Wx_configinfoView GetView(Wx_configinfo data)
        {
            Wx_configinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Wx_configinfoView model)
        {
            Wx_configinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Wx_configinfoView model)
        {
            Wx_configinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Wx_configinfoView model)
        {
            Wx_configinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Wx_configinfoView> model)
        {
            IList<Wx_configinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Wx_configinfoView> NGetList()
        {
            List<Wx_configinfo> listdata = base.GetList() as List<Wx_configinfo>;
            IList<Wx_configinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Wx_configinfoView> NGetList_id(string id)
        {
            List<Wx_configinfo> list = base.GetList_id(id) as List<Wx_configinfo>;
            IList<Wx_configinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Wx_configinfo> NGetListID(string id)
        {
            IList<Wx_configinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Wx_configinfoView NGetModelById(string id)
        {
            Wx_configinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查询平台微信支付配置信息
        //查询平台微信支付配置信息
        public Wx_configinfoView Getweixinpayconfig()
        {
            string HQLstr = string.Format("from Wx_configinfo where Type='0'");
            List<Wx_configinfo> list = HibernateTemplate.Find<Wx_configinfo>(HQLstr) as List<Wx_configinfo>;
            IList<Wx_configinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion

        #region //查询蓝河微信的配置信息
        /// <summary>
        /// 查询蓝河微信的配置信息
        /// </summary>
        /// <returns></returns>
        public Wx_configinfoView Getlanheweixinpayconfig()
        {
            string HQLstr = string.Format("from Wx_configinfo where Type='1'");
            List<Wx_configinfo> list = HibernateTemplate.Find<Wx_configinfo>(HQLstr) as List<Wx_configinfo>;
            IList<Wx_configinfoView> listmodel = GetViewlist(list);
            if (listmodel == null)
                return null;
            return listmodel[0];
        }
        #endregion
    }
}
