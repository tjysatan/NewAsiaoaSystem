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
    public class WX_SendCDao : ServiceConversion<WX_SendCView, WX_SendC>, IWX_SendCDao
    {
        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WX_SendCView GetView(WX_SendC data)
        {
            WX_SendCView view = ConvertToDTO(data);

            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_SendCView model)
        {
            WX_SendC listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_SendCView model)
        {
            WX_SendC listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_SendCView model)
        {
            WX_SendC listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_SendCView> model)
        {
            IList<WX_SendC> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_SendCView> NGetList()
        {
            List<WX_SendC> listdata = base.GetList() as List<WX_SendC>;
            IList<WX_SendCView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_SendCView> NGetList_id(string id)
        {
            List<WX_SendC> list = base.GetList_id(id) as List<WX_SendC>;
            IList<WX_SendCView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_SendC> NGetListID(string id)
        {
            IList<WX_SendC> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_SendCView NGetModelById(string id)
        {
            WX_SendCView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 统计当月群发消息的次数
        /// </summary>
        /// <returns></returns>
        public int GetSM(int type)
        {
            string tempHql = string.Format(" from  WX_SendC  where  S_Month = '{0}' and S_Year='{1}' and S_Type='{2}'",Convert.ToString(DateTime.Now.Month), Convert.ToString(DateTime.Now.Year),type);
            try
            {
                List<WX_SendC> list = Session.CreateQuery(tempHql).List<WX_SendC>() as List<WX_SendC>;
               // IList<WX_OpenIDView> listmodel = GetViewlist(list);

                if (list != null && list.Count != 0)
                {
                    return list.Count;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return 0;
            }
        }
    }
}
