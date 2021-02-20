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
   public class Wechat_TXinfoDao:ServiceConversion<Wechat_TXinfoView,Wechat_TXinfo>,IWechat_TXinfoDao
   {
       //重写sql语句
       private StringBuilder TempHql = null;
       private List<string> TempList = null;
       public override String GetSearchHql()
       {
           return string.Format(" from {0} u where 1=1 {1}", typeof(Wechat_TXinfo).Name, TempHql.ToString());
       }

       /// <summary>
       /// DATA 转换成 TDO  
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
       public override Wechat_TXinfoView GetView(Wechat_TXinfo data)
       {
           Wechat_TXinfoView view = ConvertToDTO(data);
           return view;
       }


       /// <summary>
       /// 插入数据
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool Ninsert(Wechat_TXinfoView model)
       {
           Wechat_TXinfo listmodel = GetData(model);
           return base.insert(listmodel);
       }

       /// <summary>
       /// 更新
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool NUpdate(Wechat_TXinfoView model)
       {
           Wechat_TXinfo listmodel = GetData(model);
           return base.Update(listmodel);
       }
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool NDelete(Wechat_TXinfoView model)
       {
           Wechat_TXinfo listmodel = GetData(model);
           return base.Delete(listmodel);
       }
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool NDelete(List<Wechat_TXinfoView> model)
       {
           IList<Wechat_TXinfo> listmodel = GetDatalist(model);
           return base.NDelete(listmodel);
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <returns></returns>
       public IList<Wechat_TXinfoView> NGetList()
       {
           List<Wechat_TXinfo> listdata = base.GetList() as List<Wechat_TXinfo>;
           IList<Wechat_TXinfoView> listmodel = GetViewlist(listdata);
           return listmodel;
       }


       /// <summary>
       /// 根据多个ID  查询多条数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public IList<Wechat_TXinfoView> NGetList_id(string id)
       {
           List<Wechat_TXinfo> list = base.GetList_id(id) as List<Wechat_TXinfo>;
           IList<Wechat_TXinfoView> listmodel = GetViewlist(list);
           return listmodel;
       }


       /// <summary>
       /// 根据多个ID  查询多条数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns>返回list数据</returns>
       public IList<Wechat_TXinfo> NGetListID(string id)
       {
           IList<Wechat_TXinfo> list = base.GetList_id(id);
           return list;
       }

       /// <summary>
       /// 根据ID 查询一条记录的
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public Wechat_TXinfoView NGetModelById(string id)
       {
           Wechat_TXinfoView listmodel = GetView(base.GetModelById(id));
           return listmodel;
       }


       #region //提现记录根据openID 查询
       public IList<Wechat_TXinfoView> GetTxinfobyopenid(string openid)
       {
           string temHql = string.Format("from Wechat_TXinfo where OpenId='{0}' and QR_type='1'", openid);
           List<Wechat_TXinfo> list = HibernateTemplate.Find<Wechat_TXinfo>(temHql) as List<Wechat_TXinfo>;
           IList<Wechat_TXinfoView> listmodel = GetViewlist(list);
           return listmodel;
       } 
       #endregion

       //检测是否 存在未处理的 提现申请
       public bool Jcwclsq(string openid)
       {
           string temHql = string.Format("from Wechat_TXinfo where OpenId='{0}' and QR_type='0'", openid);
           List<Wechat_TXinfo> list = HibernateTemplate.Find<Wechat_TXinfo>(temHql) as List<Wechat_TXinfo>;
           return list.Count > 0 ? true : false;
       }


       //#region //保存后返回ID
       //public string InsertID(Wechat_TXinfoView modelView)
       //{
       //    Wechat_TXinfo model = GetData(modelView);
       //    try
       //    {
       //        HibernateTemplate.SaveOrUpdate(model);
       //        // Session.Save(model);
       //        string Id = model.Id;
       //        log4net.LogManager.GetLogger("ApplicationInfoLog");
       //        return Id;
       //    }
       //    catch (Exception e)
       //    {
       //        log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
       //        return null;
       //    }
       //}
       //#endregion
   }
}
