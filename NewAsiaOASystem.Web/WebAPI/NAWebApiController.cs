using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using NewAsiaOASystem.DBModel;

namespace NewAsiaOASystem.Web.WebAPI
{
    public class NAWebApiController : ApiController
    {
        INQR_ReasonDao _INQR_ReasonDao = ContextRegistry.GetContext().GetObject("NQR_ReasonDao") as INQR_ReasonDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        INAReturnListDao _INAReturnListDao = ContextRegistry.GetContext().GetObject("NAReturnListDao") as INAReturnListDao;
        INQR_ProductDao _INQR_ProductDao = ContextRegistry.GetContext().GetObject("NQR_ProductDao") as INQR_ProductDao;
        INQ_BlinfoDao _INQ_BlinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlinfoDao") as INQ_BlinfoDao;
        INQ_BlxxinfoDao _INQ_BlxxinfoDao = ContextRegistry.GetContext().GetObject("NQ_BlxxinfoDao") as INQ_BlxxinfoDao;
        //供应商
        IWL_GcsinfoDao _IWL_GcsinfoDao = ContextRegistry.GetContext().GetObject("WL_GcsinfoDao") as IWL_GcsinfoDao;
        //
        // GET: /NAWebApi/

        public string Options()
        {

            return null; // HTTP 200 response with empty body

        }

        #region //根据ID 查找返退货原因

        /// <summary>
        /// 根据ID 获取返退货原因
        /// </summary>
        /// <param name="Id">返退货原因ID</param>
        /// <returns></returns>
        [HttpPost]
        public string GetNAResasonjs([FromBody]string Id)
        {
              string json=null;
              if (Id != null)
              {
                  NQR_ReasonView pmodel = _INQR_ReasonDao.NGetModelById(Id);

                  if (pmodel != null)
                  {
                      json = JsonConvert.SerializeObject(pmodel);
                  }
                  else
                  {
                      json = null;
                  }
              }
            return json;
        } 
        #endregion


        #region //根据ID 查找客户信息

        /// <summary>
        /// 根据ID 查找客户信息
        /// </summary>
        /// <param name="Id">查找客户信息ID</param>
        /// <returns></returns>
        [HttpPost]
        public string GetNACusjs([FromBody]string Id)
        {
            NACustomerinfoView pmodel = _INACustomerinfoDao.GetcusInfobyId(Id);
            string json;
            if (pmodel != null)
            {

                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }

            return json;

        }
        #endregion

        #region //查找全部父级 返退货原因
        [HttpPost]
        public string Getpreasonjson()
        {
            IList<NQR_ReasonView> plist = _INQR_ReasonDao.GetlistisPar();
            string json;
            if (plist != null)
            {
                json = JsonConvert.SerializeObject(plist);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        #region //查找全部没有下级的不良原因
        [HttpPost]
        public string GetBLyyJson()
        {
            IList<NQ_BlinfoView> plist = _INQ_BlinfoDao.GetlistisPar();
            string json;
            if (plist != null)
            {
                json = JsonConvert.SerializeObject(plist);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion


        #region //根据父级ID 查找返退货原因信息
        [HttpPost]
        public string GetNAReasonby_pidjson([FromBody] string Id)
        {
            IList<NQR_ReasonView> pmodel = _INQR_ReasonDao.Getlistreason_byPid(Id);
            string json;
            if (pmodel != null)
            {
                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        #region //根据父级ID 查找不良原因的信息
        [HttpPost]
        public string GetblyybyPid([FromBody] string Id)
        {
            IList<NQ_BlinfoView> pmodel = _INQ_BlinfoDao.Getlistreason_byPid(Id);
            string json;
            if (pmodel != null)
            {
                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }
            return json;
        }

        [HttpPost]
        public string GetblyyfjbyId([FromBody] string Id)
        {
            IList<NQ_BlinfoView> pmodel = _INQ_BlinfoDao.Getlistreason_byId(Id);
            string json;
            if (pmodel != null)
            {
                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }
            return json;
        }
        #endregion


        #region //根据ID 查找返退货流程跟踪单数据json
        [HttpPost]
        public string GetReturnlistjson([FromBody] string Id)
        {
            NAReturnListView Modeljson = _INAReturnListDao.NGetModelById(Id);
            string json;
            if (Modeljson != null)
            {
                json = JsonConvert.SerializeObject(Modeljson);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        #region //根据ID 查处返退货产品类型
        [HttpPost]
        public string GetR_Productjson([FromBody] string Id)
        {
            NQR_ProductView Modeljson = _INQR_ProductDao.NGetModelById(Id);
            string json;
            if (Modeljson != null)
            {
                json = JsonConvert.SerializeObject(Modeljson);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        #region //查找不良现象
        [HttpPost]
        public string GetblxxJson()
        {
            IList<NQ_BlxxinfoView> list = _INQ_BlxxinfoDao.Getblxxallzydata();
            string json;
            if (list != null)
            {
                json = JsonConvert.SerializeObject(list);
            }
            else
            {
                json = null;
            }
            return json;
        } 
        #endregion

        //远程监控 供应商信息 接口
        /// <summary>
        /// 远程监控 供应商信息接口
        /// </summary>
        /// <param name="Name">工程商名称</param>
        /// <param name="Lxr">联系人</param>
        /// <param name="tel">联系电话</param>
        /// <param name="adress">地址</param>
        /// <param name="ycId">供应商Id(唯一识别号)</param>
        /// <param name="YcKey">密钥 4a2f262ac5ac77dbb339fbb1f93b5075</param>
        /// <returns></returns>
        [HttpPost]
        public string Editgys(string Name, string Lxr, string tel, string adress, string ycId, string YcKey)
        {
            if (YcKey == "4a2f262ac5ac77dbb339fbb1f93b5075")//接口验证是否 是远程监控的访问的
            {
                if (_IWL_GcsinfoDao.JccfbyGcs_no(ycId))
                {
                    WL_GcsinfoView gcsmodel = new WL_GcsinfoView();//实例化工程商
                    gcsmodel.DW_name = Name;//单位名称
                    gcsmodel.Lxr_name = Lxr;//联系人
                    gcsmodel.Lxr_tel = tel;//联系方式
                    gcsmodel.Adress = adress;//地址
                    gcsmodel.Gcs_no = ycId;//工程商编号（唯一识别号 远程监控的数据记录的ID）
                    gcsmodel.C_time = DateTime.Now;//创建时间
                    if (_IWL_GcsinfoDao.Ninsert(gcsmodel))
                    {
                        return "{\"status\":\"true\"}";//添加成功
                    }
                    else
                    {
                        return "{\"status\":\"false\"}";//添加失败
                    }
                }
                else
                {
                    return "{\"status\":\"false1\"}";//重复添加
                }
            }
            else
            {
                return "{\"status\":\"false2\"}";//验证失败
            }

        }

    }
}
