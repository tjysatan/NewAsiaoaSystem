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
  public  class XSFXkhinfoDao:ServiceConversion<XSFXkhinfoView,XSFXkhinfo>,IXSFXkhinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(XSFXkhinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override XSFXkhinfoView GetView(XSFXkhinfo data)
        {
            XSFXkhinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(XSFXkhinfoView model)
        {
            XSFXkhinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(XSFXkhinfoView model)
        {
            XSFXkhinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(XSFXkhinfoView model)
        {
            XSFXkhinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<XSFXkhinfoView> model)
        {
            IList<XSFXkhinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<XSFXkhinfoView> NGetList()
        {
            List<XSFXkhinfo> listdata = base.GetList() as List<XSFXkhinfo>;
            IList<XSFXkhinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<XSFXkhinfoView> NGetList_id(string id)
        {
            List<XSFXkhinfo> list = base.GetList_id(id) as List<XSFXkhinfo>;
            IList<XSFXkhinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<XSFXkhinfo> NGetListID(string id)
        {
            IList<XSFXkhinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XSFXkhinfoView NGetModelById(string id)
        {
            XSFXkhinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        //根据区县ID 查找客户明细
        public IList<XSFXkhinfoView> Getxsfxkhlist(string id)
        {
            List<XSFXkhinfo> list = HibernateTemplate.Find<XSFXkhinfo>("from XSFXkhinfo where QxId='" + id + "'") as List<XSFXkhinfo>;
            IList<XSFXkhinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
    }
}
