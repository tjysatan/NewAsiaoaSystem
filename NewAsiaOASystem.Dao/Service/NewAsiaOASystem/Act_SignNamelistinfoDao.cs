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
    public class Act_SignNamelistinfoDao:ServiceConversion<Act_SignNamelistinfoView,Act_SignNamelistinfo>,IAct_SignNamelistinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Act_SignNamelistinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Act_SignNamelistinfoView GetView(Act_SignNamelistinfo data)
        {
            Act_SignNamelistinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Act_SignNamelistinfoView model)
        {
            Act_SignNamelistinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Act_SignNamelistinfoView model)
        {
            Act_SignNamelistinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Act_SignNamelistinfoView model)
        {
            Act_SignNamelistinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Act_SignNamelistinfoView> model)
        {
            IList<Act_SignNamelistinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Act_SignNamelistinfoView> NGetList()
        {
            List<Act_SignNamelistinfo> listdata = base.GetList() as List<Act_SignNamelistinfo>;
            IList<Act_SignNamelistinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Act_SignNamelistinfoView> NGetList_id(string id)
        {
            List<Act_SignNamelistinfo> list = base.GetList_id(id) as List<Act_SignNamelistinfo>;
            IList<Act_SignNamelistinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }



        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Act_SignNamelistinfo> NGetListID(string id)
        {
            IList<Act_SignNamelistinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Act_SignNamelistinfoView NGetModelById(string id)
        {
            Act_SignNamelistinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        //根据公司名称查询
        /// <summary>
        /// 根据公司名称查询
        /// </summary>
        /// <param name="company">公司名称</param>
        /// <returns></returns>
        public Act_SignNamelistinfoView Gethuiyimingdanmodelbycompany(string company)
        {
            string strHql;
            strHql = string.Format("from Act_SignNamelistinfo where Company='{0}'",company);
            List<Act_SignNamelistinfo> list = HibernateTemplate.Find<Act_SignNamelistinfo>(strHql) as List<Act_SignNamelistinfo>;
            IList<Act_SignNamelistinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }

        //根据与会者查询
        /// <summary>
        /// 根据与会者查询会与人名单
        /// </summary>
        /// <param name="name">与会者</param>
        /// <returns></returns>
        public Act_SignNamelistinfoView GethuiyimingdanbyName(string name)
        {
            string strHql;
            strHql = string.Format("from Act_SignNamelistinfo where Name='{0}'", name);
            List<Act_SignNamelistinfo> list = HibernateTemplate.Find<Act_SignNamelistinfo>(strHql) as List<Act_SignNamelistinfo>;
            IList<Act_SignNamelistinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }

        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="dm">编码</param>
        /// <returns></returns>
        public Act_SignNamelistinfoView Gethuiyimingdanbydm(string dm)
        {
            string strHql;
            strHql = string.Format("from Act_SignNamelistinfo where Dm='{0}'", dm);
            List<Act_SignNamelistinfo> list = HibernateTemplate.Find<Act_SignNamelistinfo>(strHql) as List<Act_SignNamelistinfo>;
            IList<Act_SignNamelistinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }

        //根据签到时间排序查询出已经签到没有在头部显示过的名单
        public Act_SignNamelistinfoView Getyijingqiandaomeiyoupiaoguo()
        {
            string strHql;
            strHql = string.Format("from Act_SignNamelistinfo where Issq='{0}' and Ispg='{1}' order by Qddatetime asc", "1", "0");
            List<Act_SignNamelistinfo> list = HibernateTemplate.Find<Act_SignNamelistinfo>(strHql) as List<Act_SignNamelistinfo>;
            IList<Act_SignNamelistinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }

        //根据签到时间的排序查处已经签到已经飘过的名单
        public IList<Act_SignNamelistinfoView> Getyijingqiandaoyijingpiaoguo()
        {
            string strHql;
            strHql = string.Format("from Act_SignNamelistinfo where Issq='{0}' and Ispg='{1}' order by Qddatetime asc", "1", "1");
            List<Act_SignNamelistinfo> list = HibernateTemplate.Find<Act_SignNamelistinfo>(strHql) as List<Act_SignNamelistinfo>;
            IList<Act_SignNamelistinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

        //根据采购单查找采购明细
        //public IList<CG_DetailinfoView> Getcgmxlist(string id)
        //{
        //    List<CG_Detailinfo> list = HibernateTemplate.Find<CG_Detailinfo>("from CG_Detailinfo where cgId='" + id + "'") as List<CG_Detailinfo>;
        //    IList<CG_DetailinfoView> listmodel = GetViewlist(list);
        //    return listmodel;
        //}
    }
}
