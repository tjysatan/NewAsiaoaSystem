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
   public class XSFXqxinfoDao:ServiceConversion<XSFXqxinfoView,XSFXqxinfo>,IXSFXqxinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(XSFXqxinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override XSFXqxinfoView GetView(XSFXqxinfo data)
        {
            XSFXqxinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(XSFXqxinfoView model)
        {
            XSFXqxinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(XSFXqxinfoView model)
        {
            XSFXqxinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(XSFXqxinfoView model)
        {
            XSFXqxinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<XSFXqxinfoView> model)
        {
            IList<XSFXqxinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<XSFXqxinfoView> NGetList()
        {
            List<XSFXqxinfo> listdata = base.GetList() as List<XSFXqxinfo>;
            IList<XSFXqxinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<XSFXqxinfoView> NGetList_id(string id)
        {
            List<XSFXqxinfo> list = base.GetList_id(id) as List<XSFXqxinfo>;
            IList<XSFXqxinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<XSFXqxinfo> NGetListID(string id)
        {
            IList<XSFXqxinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XSFXqxinfoView NGetModelById(string id)
        {
            XSFXqxinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        //根据大区ID 查找分区明细
        public IList<XSFXqxinfoView> Getxsfxqxlist(string id)
        {
            List<XSFXqxinfo> list = HibernateTemplate.Find<XSFXqxinfo>("from XSFXqxinfo where dqId='" + id + "'") as List<XSFXqxinfo>;
            IList<XSFXqxinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
    }
}
