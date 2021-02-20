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
    public class NQ_ThdetailinfoDao : ServiceConversion<NQ_ThdetailinfoView, NQ_Thdetailinfo>,INQ_ThdetailinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_Thdetailinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_ThdetailinfoView GetView(NQ_Thdetailinfo data)
        {
            NQ_ThdetailinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_ThdetailinfoView model)
        {
            NQ_Thdetailinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_ThdetailinfoView model)
        {
            NQ_Thdetailinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_ThdetailinfoView model)
        {
            NQ_Thdetailinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_ThdetailinfoView> model)
        {
            IList<NQ_Thdetailinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_ThdetailinfoView> NGetList()
        {
            List<NQ_Thdetailinfo> listdata = base.GetList() as List<NQ_Thdetailinfo>;
            IList<NQ_ThdetailinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_ThdetailinfoView> NGetList_id(string id)
        {
            List<NQ_Thdetailinfo> list = base.GetList_id(id) as List<NQ_Thdetailinfo>;
            IList<NQ_ThdetailinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_Thdetailinfo> NGetListID(string id)
        {
            IList<NQ_Thdetailinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_ThdetailinfoView NGetModelById(string id)
        {
            NQ_ThdetailinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //根据返退货流程ID 查找退货明细
        public IList<NQ_ThdetailinfoView> Gettfinfoby_rid(string R_Id)
        {
            string sql = string.Format("from NQ_Thdetailinfo where R_Id='{0}' order by c_time asc ", R_Id);
            List<NQ_Thdetailinfo> list = HibernateTemplate.Find<NQ_Thdetailinfo>(sql) as List<NQ_Thdetailinfo>;
            IList<NQ_ThdetailinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }  
        #endregion


        
        
    }
}
