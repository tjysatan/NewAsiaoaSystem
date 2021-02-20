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
   public class Flow_ProductionNoticeinfoDao:ServiceConversion<Flow_ProductionNoticeinfoView,Flow_ProductionNoticeinfo>,IFlow_ProductionNoticeinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(Flow_ProductionNoticeinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override Flow_ProductionNoticeinfoView GetView(Flow_ProductionNoticeinfo data)
        {
            Flow_ProductionNoticeinfoView view = ConvertToDTO(data);
            return view;
        }


 

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(Flow_ProductionNoticeinfoView model)
        {
            Flow_ProductionNoticeinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(Flow_ProductionNoticeinfoView model)
        {
            Flow_ProductionNoticeinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(Flow_ProductionNoticeinfoView model)
        {
            Flow_ProductionNoticeinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<Flow_ProductionNoticeinfoView> model)
        {
            IList<Flow_ProductionNoticeinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<Flow_ProductionNoticeinfoView> NGetList()
        {
            List<Flow_ProductionNoticeinfo> listdata = base.GetList() as List<Flow_ProductionNoticeinfo>;
            IList<Flow_ProductionNoticeinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Flow_ProductionNoticeinfoView> NGetList_id(string id)
        {
            List<Flow_ProductionNoticeinfo> list = base.GetList_id(id) as List<Flow_ProductionNoticeinfo>;
            IList<Flow_ProductionNoticeinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<Flow_ProductionNoticeinfo> NGetListID(string id)
        {
            IList<Flow_ProductionNoticeinfo> list = base.GetList_id(id);
            return list;
        }


        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow_ProductionNoticeinfoView NGetModelById(string id)
        {
            Flow_ProductionNoticeinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        //public PagerInfo<NQ_chuhuoinfonView> GetCinfoList(string Name, SessionUser user)
        //{
        //    TempList = new List<string>();
        //    TempHql = new StringBuilder();
        //    if (!string.IsNullOrEmpty(Name))
        //        TempHql.AppendFormat("and Pname like '%{0}%' ", Name);

        //    TempHql.AppendFormat("order by CreateTime desc");
        //    PagerInfo<NQ_chuhuoinfonView> list = Search();
        //    return list;
        //}

    }
}
