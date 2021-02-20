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
    public  class WL_JxqnumberDao:ServiceConversion<_20150509WL_JxqnumberView,_20150509WL_Jxqnumber>,IWL_JxqnumberDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(_20150509WL_Jxqnumber).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override _20150509WL_JxqnumberView GetView(_20150509WL_Jxqnumber data)
        {
            _20150509WL_JxqnumberView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(_20150509WL_JxqnumberView model)
        {
            _20150509WL_Jxqnumber listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(_20150509WL_JxqnumberView model)
        {
            _20150509WL_Jxqnumber listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(_20150509WL_JxqnumberView model)
        {
            _20150509WL_Jxqnumber listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<_20150509WL_JxqnumberView> model)
        {
            IList<_20150509WL_Jxqnumber> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<_20150509WL_JxqnumberView> NGetList()
        {
            List<_20150509WL_Jxqnumber> listdata = base.GetList() as List<_20150509WL_Jxqnumber>;
            IList<_20150509WL_JxqnumberView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<_20150509WL_JxqnumberView> NGetList_id(string id)
        {
            List<_20150509WL_Jxqnumber> list = base.GetList_id(id) as List<_20150509WL_Jxqnumber>;
            IList<_20150509WL_JxqnumberView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<_20150509WL_Jxqnumber> NGetListID(string id)
        {
            IList<_20150509WL_Jxqnumber> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public _20150509WL_JxqnumberView NGetModelById(string id)
        {
            _20150509WL_JxqnumberView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public PagerInfo<_20150509WL_JxqnumberView> GetCinfoList(string Name,string Year,  SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Cus_Id in(select Id from NACustomerinfo where Name like '%{0}%' )", Name);
            if (!string.IsNullOrEmpty(Year))
                TempHql.AppendFormat(" and u.Year ='{0}'",Year);
            TempHql.AppendFormat("and u.States='0'  order by C_time  desc");
            PagerInfo<_20150509WL_JxqnumberView> list = Search();
            return list;
        }

        /// <summary>
        /// 检查该经销商在该年份中是否已经存在记录
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Cu_Id"></param>
        /// <returns></returns>
        public bool checkrepeat(string Year,string Cu_Id)
        {
            string tempHql;
            tempHql = string.Format("from _20150509WL_Jxqnumber where Year='{0}' and Cus_Id='{1}'",Year,Cu_Id);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}",tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            if (count >= 1)
                return false;
            else
                return true;
        }
    }
}
