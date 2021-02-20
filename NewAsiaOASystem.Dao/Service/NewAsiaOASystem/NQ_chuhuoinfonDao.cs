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
    public class NQ_chuhuoinfonDao : ServiceConversion<NQ_chuhuoinfonView,NQ_chuhuoinfon>,INQ_chuhuoinfonDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_chuhuoinfon).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_chuhuoinfonView GetView(NQ_chuhuoinfon data)
        {
            NQ_chuhuoinfonView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_chuhuoinfonView model)
        {
            NQ_chuhuoinfon listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_chuhuoinfonView model)
        {
            NQ_chuhuoinfon listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_chuhuoinfonView model)
        {
            NQ_chuhuoinfon listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_chuhuoinfonView> model)
        {
            IList<NQ_chuhuoinfon> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_chuhuoinfonView> NGetList()
        {
            List<NQ_chuhuoinfon> listdata = base.GetList() as List<NQ_chuhuoinfon>;
            IList<NQ_chuhuoinfonView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_chuhuoinfonView> NGetList_id(string id)
        {
            List<NQ_chuhuoinfon> list = base.GetList_id(id) as List<NQ_chuhuoinfon>;
            IList<NQ_chuhuoinfonView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_chuhuoinfon> NGetListID(string id)
        {
            IList<NQ_chuhuoinfon> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_chuhuoinfonView NGetModelById(string id)
        {
            NQ_chuhuoinfonView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public PagerInfo<NQ_chuhuoinfonView> GetCinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat("and Pname like '%{0}%' ", Name);
            TempHql.AppendFormat("order by CreateTime desc");
            PagerInfo<NQ_chuhuoinfonView> list = Search();
            return list;
        }
    }
}
