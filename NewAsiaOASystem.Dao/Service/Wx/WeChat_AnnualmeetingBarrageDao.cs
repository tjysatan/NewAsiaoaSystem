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
    public class WeChat_AnnualmeetingBarrageDao:ServiceConversion<WeChat_AnnualmeetingBarrageView,WeChat_AnnualmeetingBarrage>,IWeChat_AnnualmeetingBarrageDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WeChat_AnnualmeetingBarrage).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WeChat_AnnualmeetingBarrageView GetView(WeChat_AnnualmeetingBarrage data)
        {
            WeChat_AnnualmeetingBarrageView view = ConvertToDTO(data);
            return view;
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WeChat_AnnualmeetingBarrageView model)
        {
            WeChat_AnnualmeetingBarrage listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WeChat_AnnualmeetingBarrageView model)
        {
            WeChat_AnnualmeetingBarrage listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WeChat_AnnualmeetingBarrageView model)
        {
            WeChat_AnnualmeetingBarrage listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WeChat_AnnualmeetingBarrageView> model)
        {
            IList<WeChat_AnnualmeetingBarrage> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WeChat_AnnualmeetingBarrageView> NGetList()
        {
            List<WeChat_AnnualmeetingBarrage> listdata = base.GetList() as List<WeChat_AnnualmeetingBarrage>;
            IList<WeChat_AnnualmeetingBarrageView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WeChat_AnnualmeetingBarrageView> NGetList_id(string id)
        {
            List<WeChat_AnnualmeetingBarrage> list = base.GetList_id(id) as List<WeChat_AnnualmeetingBarrage>;
            IList<WeChat_AnnualmeetingBarrageView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WeChat_AnnualmeetingBarrage> NGetListID(string id)
        {
            IList<WeChat_AnnualmeetingBarrage> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChat_AnnualmeetingBarrageView NGetModelById(string id)
        {
            WeChat_AnnualmeetingBarrageView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        //查询未上过墙的10条数据
        public IList<WeChat_AnnualmeetingBarrageView> listinfobyhd()
        {
            string temHql;
            temHql = string.Format(" from WeChat_AnnualmeetingBarrage where Issq='0' order by C_time asc");
            IQuery query = Session.CreateQuery(temHql);
            query.SetFirstResult(0);
            query.SetMaxResults(11);
            List<WeChat_AnnualmeetingBarrage> list = query.List<WeChat_AnnualmeetingBarrage>() as List<WeChat_AnnualmeetingBarrage>;
            IList<WeChat_AnnualmeetingBarrageView> listmodel = GetViewlist(list);
            return listmodel;
        }
    }
}
