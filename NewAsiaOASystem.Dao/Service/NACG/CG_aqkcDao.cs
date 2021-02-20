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
   public class CG_aqkcDao:ServiceConversion<CG_aqkcView,CG_aqkc>,ICG_aqkcDao
   {
       //重写sql语句
       private StringBuilder TempHql = null;
       private List<string> TempList = null;
       public override String GetSearchHql()
       {
           return string.Format(" from {0} u where 1=1 {1}", typeof(CG_aqkc).Name, TempHql.ToString());
       }

       /// <summary>
       /// DATA 转换成 TDO  
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
       public override CG_aqkcView GetView(CG_aqkc data)
       {
           CG_aqkcView view = ConvertToDTO(data);
           return view;
       }

       /// <summary>
       /// 插入数据
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool Ninsert(CG_aqkcView model)
       {
           CG_aqkc listmodel = GetData(model);
           return base.insert(listmodel);
       }

       /// <summary>
       /// 更新
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool NUpdate(CG_aqkcView model)
       {
           CG_aqkc listmodel = GetData(model);
           return base.Update(listmodel);
       }
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool NDelete(CG_aqkcView model)
       {
           CG_aqkc listmodel = GetData(model);
           return base.Delete(listmodel);
       }
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool NDelete(List<CG_aqkcView> model)
       {
           IList<CG_aqkc> listmodel = GetDatalist(model);
           return base.NDelete(listmodel);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <returns></returns>
       public IList<CG_aqkcView> NGetList()
       {
           List<CG_aqkc> listdata = base.GetList() as List<CG_aqkc>;
           IList<CG_aqkcView> listmodel = GetViewlist(listdata);
           return listmodel;
       }


       /// <summary>
       /// 根据多个ID  查询多条数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public IList<CG_aqkcView> NGetList_id(string id)
       {
           List<CG_aqkc> list = base.GetList_id(id) as List<CG_aqkc>;
           IList<CG_aqkcView> listmodel = GetViewlist(list);
           return listmodel;
       }


       /// <summary>
       /// 根据多个ID  查询多条数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns>返回list数据</returns>
       public IList<CG_aqkc> NGetListID(string id)
       {
           IList<CG_aqkc> list = base.GetList_id(id);
           return list;
       }

       /// <summary>
       /// 根据ID 查询一条记录的
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public CG_aqkcView NGetModelById(string id)
       {
           CG_aqkcView listmodel = GetView(base.GetModelById(id));
           return listmodel;
       }

       public PagerInfo<CG_aqkcView> GetCinfoList(string Name, SessionUser user)
       {
           TempList = new List<string>();
           TempHql = new StringBuilder();
           if (!string.IsNullOrEmpty(Name))
               TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
           TempHql.AppendFormat(" order by u.Sort asc,CreateTime desc");
           PagerInfo<CG_aqkcView> list = Search();
           return list;

       }
   }
}
