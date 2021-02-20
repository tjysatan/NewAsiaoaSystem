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
    public class XSFXdqinfoDao:ServiceConversion<XSFXdqinfoView,XSFXdqinfo>,IXSFXdqinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(XSFXdqinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override XSFXdqinfoView GetView(XSFXdqinfo data)
        {
            XSFXdqinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(XSFXdqinfoView model)
        {
            XSFXdqinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(XSFXdqinfoView model)
        {
            XSFXdqinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(XSFXdqinfoView model)
        {
            XSFXdqinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<XSFXdqinfoView> model)
        {
            IList<XSFXdqinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<XSFXdqinfoView> NGetList()
        {
            List<XSFXdqinfo> listdata = base.GetList() as List<XSFXdqinfo>;
            IList<XSFXdqinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<XSFXdqinfoView> NGetList_id(string id)
        {
            List<XSFXdqinfo> list = base.GetList_id(id) as List<XSFXdqinfo>;
            IList<XSFXdqinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<XSFXdqinfo> NGetListID(string id)
        {
            IList<XSFXdqinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XSFXdqinfoView NGetModelById(string id)
        {
            XSFXdqinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }




        public PagerInfo<XSFXdqinfoView> GetxsfxList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.dqname ='{0}'", Name);
            //if (!string.IsNullOrEmpty(RL_is))
            //    TempHql.AppendFormat(" and u.Cg_Isdh ='{0}' ", RL_is);
            //if (!string.IsNullOrEmpty(Stratrldate) && !string.IsNullOrEmpty(Endrldate))
            //    TempHql.AppendFormat("and Cg_xdtime>='{0}' and Cg_xdtime<='{1}'", Stratrldate, Endrldate);
            //if (user.RoleType != "0")
            //{
            //    TempHql.AppendFormat("and u.JjId in ('{0}')", user.Id);
            //}
            TempHql.AppendFormat(" order by C_datetime desc");
            PagerInfo<XSFXdqinfoView> list = Search();
            return list;
        }

    }
}
