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
    public class WX_M_newDao : ServiceConversion<WX_M_newView, WX_M_new>, IWX_M_newDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WX_M_newView GetView(WX_M_new data)
        {
            WX_M_newView view = ConvertToDTO(data);

            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_M_newView model)
        {
            WX_M_new listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_M_newView model)
        {
            WX_M_new listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_M_newView model)
        {
            WX_M_new listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_M_newView> model)
        {
            IList<WX_M_new> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_M_newView> NGetList()
        {
            List<WX_M_new> listdata = base.GetList() as List<WX_M_new>;
            IList<WX_M_newView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_M_newView> NGetList_id(string id)
        {
            List<WX_M_new> list = base.GetList_id(id) as List<WX_M_new>;
            IList<WX_M_newView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_M_new> NGetListID(string id)
        {
            IList<WX_M_new> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_M_newView NGetModelById(string id)
        {
            WX_M_newView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }
    }
}
