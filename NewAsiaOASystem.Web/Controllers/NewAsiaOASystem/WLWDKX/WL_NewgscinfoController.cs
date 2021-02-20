using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Data.OleDb;
using System.Data;
using System.Runtime.Serialization.Json;

namespace NewAsiaOASystem.Web.Controllers
{
    public class WL_NewgscinfoController : Controller
    {
        //
        // GET: /WL_Newgscinfo/
        IWL_XFOrderinfoDao _IWL_XForderinfoDao = ContextRegistry.GetContext().GetObject("WL_XFOrderinfoDao") as IWL_XFOrderinfoDao;
        IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
        IWL_NewgscinfoDao _IWL_NewgscinfoDao = ContextRegistry.GetContext().GetObject("WL_NewgscinfoDao") as IWL_NewgscinfoDao;


        public ActionResult Index()
        {
            return View();
        }

        #region //同步工程商对应sid的数据
        //同步数据的操作页面
        public ActionResult TBgcssidView()
        {
            return View();
        }

        //同步数据
        public string TBNewgcsinfodata()
        {
            WL_NewgscinfoView model = _IWL_NewgscinfoDao.GetNewnewgcsinfo();
            int t = 0;
            if (model != null)
            {
                if (model.Ids != null)
                {
                    t = Convert.ToInt32(model.Ids);
                }
            }
            string url;
            url = "http://www.sbycjk.net/getsidsofshipment/getProjectUserByids/" + t;
            string result = HttpUtility11.GetData(url);
            List<Jsongcssidclass> Newgcsmodel = getObjectByJson<Jsongcssidclass>(result);
            foreach (var a in Newgcsmodel)
            {
                WL_NewgscinfoView newgscmodel = new WL_NewgscinfoView();
                newgscmodel = _IWL_NewgscinfoDao.GetnewgcsinfobyIds(a.ids);
                if (newgscmodel != null)
                {
                    newgscmodel.user_name = a.user_name;//工程商名称
                    newgscmodel.mobile = a.mobile;//联系方式
                    newgscmodel.Ids = Convert.ToInt32(a.ids);//远程Id
                    newgscmodel.sid = a.sid;//sid
                    newgscmodel.huoqutime = DateTime.Now;//同步数据时间
                    _IWL_NewgscinfoDao.NUpdate(newgscmodel);
                }
                else
                {
                    newgscmodel = new WL_NewgscinfoView();
                    newgscmodel.user_name = a.user_name;//工程商名称
                    newgscmodel.mobile = a.mobile;//联系方式
                    newgscmodel.Ids = Convert.ToInt32(a.ids);//远程Id
                    newgscmodel.sid = a.sid;//sid
                    newgscmodel.huoqutime = DateTime.Now;//同步数据时间
                    _IWL_NewgscinfoDao.Ninsert(newgscmodel);
                }
            }
            if (Newgcsmodel != null)
            {
                return Newgcsmodel.Count().ToString();
            }
            else
            {
                return "0";
            }
        }

        //返回json数据 转换方法
        private static List<T> getObjectByJson<T>(string jsonString)
        {
            // 实例化DataContractJsonSerializer对象，需要待序列化的对象类型  
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<T>));
            //把Json传入内存流中保存  
            //jsonString = jsonString;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            // 使用ReadObject方法反序列化成对象  
            object ob = serializer.ReadObject(stream);
            List<T> ls = (List<T>)ob;
            return ls;
        }

        //json转换实体
        public class Jsongcssidclass
        {
            /// <summary>
            /// 工程商名称
            /// </summary>
            public virtual string user_name { get; set; }

            /// <summary>
            /// 电话
            /// </summary>
            public virtual string mobile { get; set; }

            /// <summary>
            /// 远程IDS
            /// </summary>
            public virtual string ids { get; set; }

            /// <summary>
            /// 工程商公司名称
            /// </summary>
            public virtual string company { get; set; }

            /// <summary>
            /// sid
            /// </summary>
            public virtual string sid { get; set; }
        }
        #endregion



    }
}
