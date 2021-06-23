using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.DBModel;
 
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using Newtonsoft.Json;
using System.Data.OleDb;
using System.Data;
using System.IO;
using NewAsiaOASystem.Web.Controllers.NewAsiaOASystem;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.Drawing;
using System.Xml;

namespace NewAsiaOASystem.Web.Controllers
{
    public class publicAPIController : Controller
    {
        //
        // GET: /publicAPI/
        IWL_GcsinfoDao _IWL_GcsinfoDao = ContextRegistry.GetContext().GetObject("WL_GcsinfoDao") as IWL_GcsinfoDao;
        INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
        INA_XSinfoDao _INA_XSinfoDao = ContextRegistry.GetContext().GetObject("NA_XSinfoDao") as INA_XSinfoDao;
        INA_XSdetailsinfoDao _INA_XSdetailsinfoDao = ContextRegistry.GetContext().GetObject("NA_XSdetailsinfoDao") as INA_XSdetailsinfoDao;
        INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
        INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;
        IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
        IFlow_ProductionNoticeinfoDao _IFlow_ProductionNoticeinfoDao = ContextRegistry.GetContext().GetObject("Flow_ProductionNoticeinfoDao") as IFlow_ProductionNoticeinfoDao;
        IWL_dkxthjlinfoDao _IWL_dkxthjlinfoDao = ContextRegistry.GetContext().GetObject("WL_dkxthjlinfoDao") as IWL_dkxthjlinfoDao;
        IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;
       // INA_QyinfoDao _INA_QyinfoDao = ContextRegistry.GetContext().GetObject("NA_QyinfoDao") as INA_QyinfoDao;
        IDKX_DDtypeinfoDao _IDKX_DDtypeinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDtypeinfoDao") as IDKX_DDtypeinfoDao;
        ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
        IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
        IDKX_GCSinfoDao _IDKX_GCSinfoDao = ContextRegistry.GetContext().GetObject("DKX_GCSinfoDao") as IDKX_GCSinfoDao;
        IDKX_PAY_CONTROL_INFODao _IDKX_PAY_CONTROL_INFODao = ContextRegistry.GetContext().GetObject("DKX_PAY_CONTROL_INFODao") as IDKX_PAY_CONTROL_INFODao;
        IDKX_CONTROL_LISTDao _IDKX_CONTROL_LISTDao = ContextRegistry.GetContext().GetObject("DKX_CONTROL_LISTDao") as IDKX_CONTROL_LISTDao;
        IDKX_CONTROL_LIST_DETAILDao _IDKX_CONTROL_LIST_DETAILDao = ContextRegistry.GetContext().GetObject("DKX_CONTROL_LIST_DETAILDao") as IDKX_CONTROL_LIST_DETAILDao;
        IDKX_ZLDataInfoDao _IDKX_ZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_ZLDataInfoDao") as IDKX_ZLDataInfoDao;
        IDKX_CPInfoDao _IDKX_CPInfoDao = ContextRegistry.GetContext().GetObject("DKX_CPInfoDao") as IDKX_CPInfoDao;
        IDKX_RKZLDataInfoDao _IDKX_RKZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_RKZLDataInfoDao") as IDKX_RKZLDataInfoDao;
        IDKX_k3BominfoDao _IDKX_k3BominfoDao = ContextRegistry.GetContext().GetObject("DKX_k3BominfoDao") as IDKX_k3BominfoDao;
        IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
        

        INA_QyinfoDao _INA_QyinfoDao = ContextRegistry.GetContext().GetObject("NA_QyinfoDao") as INA_QyinfoDao;
        IEP_jlinfoDao _IEP_jlinfoDao = ContextRegistry.GetContext().GetObject("EP_jlinfoDao") as IEP_jlinfoDao;
        IWx_FTUserbdopenIdinfoDao _IWx_FTUserbdopenIdinfoDao= ContextRegistry.GetContext().GetObject("Wx_FTUserbdopenIdinfoDao") as IWx_FTUserbdopenIdinfoDao;

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

        /// <summary>
        /// 电商平台 客户信息增加接口
        /// </summary>
        /// <param name="Name">客户名称</param>
        /// <param name="Lxr">联系人</param>
        /// <param name="tel">联系电话</param>
        /// <param name="adress">地址</param>
        /// <param name="dsId">电商客户信息Id</param>
        /// <param name="DsKey">密钥 4a2f262ac5ac77dbb339fbb1f93b5075</param>
        /// <returns></returns>
        [HttpPost]
        public string EditKH(string Name, string Lxr, string tel, string adress, string dsId, string DsKey)
        {
            if (DsKey == "4a2f262ac5ac77dbb339fbb1f93b5075")//接口验证是否 是电商平台访问的
            {
                if (_INACustomerinfoDao.Jccfkhbykhname(Name))
                {
                    NACustomerinfoView model = new NACustomerinfoView();
                    model.Name = Name;//客户名称
                    model.LxrName = Lxr;//联系人名称
                    model.Tel = tel;//联系方式
                    model.Adress = adress;//地址
                    model.Status = 1;//启用
                    if (_INACustomerinfoDao.Ninsert(model))
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

        #region //更新电商平台的订单信息
        [HttpPost]
        public string Addorder()
        {
 
             Newasia.XYOffer model = new Newasia.XYOffer();
             NA_XSinfoView OrdermodelNewdate = _INA_XSinfoDao.GetxsinfoNewdate();
             int t = 0;
             if (OrdermodelNewdate != null) {
                 t = OrdermodelNewdate.Sort;
             }
            DataTable dt = model.GetOrders(t);
            for (int a = 0, b = dt.Rows.Count; a < b; a++)
            {
                string OrderCode = dt.Rows[a]["OrderCode"].ToString();//订单编号（唯一识别号）
                string CreatedDate = dt.Rows[a]["CreatedDate"].ToString();//订单下单时间
                string userid = dt.Rows[a]["userid"].ToString();//电商客户Id
                string userName = dt.Rows[a]["userName"].ToString();//电商用户登录名称（客户登录名称）
                string company = dt.Rows[a]["company"].ToString();//电商平台用户公司名称（客户公司名称）
                string BuyerCellPhone = dt.Rows[a]["BuyerCellPhone"].ToString();//用户联系电话(客户电话)
                string ShipRegion = dt.Rows[a]["ShipRegion"].ToString();//客户所属区域
                string ShipAddress = dt.Rows[a]["ShipAddress"].ToString();//客户具体地址
                string ShipName = dt.Rows[a]["ShipName"].ToString();//收货人名称
                string ExpressCompanyName = dt.Rows[a]["ExpressCompanyName"].ToString();//公司名称
                string PaymentTypeName = dt.Rows[a]["PaymentTypeName"].ToString();//支付方式
                string SPName = dt.Rows[a]["Name"].ToString();//商品名称
                string Description = dt.Rows[a]["Description"].ToString();//商品描述
                string Quantity = dt.Rows[a]["Quantity"].ToString();//数量
                string AdjustedPrice = dt.Rows[a]["AdjustedPrice"].ToString();//产品价格
                string SKU = dt.Rows[a]["SKU"].ToString();//产品编号
                string OrderStatus = dt.Rows[a]["OrderStatus"].ToString();//订单状态
                string ShippingStatus = dt.Rows[a]["ShippingStatus"].ToString();//货运状态
                string PaymentStatus = dt.Rows[a]["PaymentStatus"].ToString();//支付状态
                string Sort = dt.Rows[a]["OrderId"].ToString();//订单
                string sku = dt.Rows[a]["SKU"].ToString();//产品物料码 （产品的唯一识别）
                string ShipCellPhone = dt.Rows[a]["ShipCellPhone"].ToString();//收件人电话
                string Haswlw = dt.Rows[a]["Haswlw"].ToString();//客户是否取得物联网电控箱的经销权
                string Province = dt.Rows[a]["Province"].ToString();//省
                string City = dt.Rows[a]["City"].ToString();//地级市
                string Area = dt.Rows[a]["Area"].ToString();//区县
                string beizhu = dt.Rows[a]["OrderRemark"].ToString();//备注
                if (_INA_XSinfoDao.jccfbySc_Id(OrderCode))
                { //检测是否存在重复的订单
                    NA_XSinfoView Ordermodel = new NA_XSinfoView();
                    Ordermodel.Xs_type = 1;//销售订单的类型 1 电商销售
                    Ordermodel.Xs_je = Convert.ToDecimal(Quantity) * Convert.ToDecimal(AdjustedPrice);//订单价格
                    Ordermodel.S_Number = Convert.ToDecimal(Quantity);//数量(出货扫码)
                    Ordermodel.CP_Number = Convert.ToDecimal(Quantity);//数量（出货数量）
                    if (PaymentTypeName == "线下付款")
                    {
                        Ordermodel.Fk_type = 1;//付款方式 线下付款
                    }
                    else
                    {
                        Ordermodel.Fk_type = 0;//付款方式 支付宝
                    }
                    NACustomerinfoView Custmodel = new NACustomerinfoView();//实例化 
                  
                    Custmodel = _INACustomerinfoDao.GetCustomerbyUId(userid);
                    if (Custmodel != null)//存在该客户信息
                    {
                        NA_AddresseeInfoView addmodel = new NA_AddresseeInfoView();//收件人实例化
                        addmodel = _INA_AddresseeInfoDao.GetaddresseeinfobyAnametel(ShipName, ShipCellPhone,ShipRegion + ShipAddress);//通过收件人和收件电话查找收件人信息
                        if (addmodel != null)
                        {//存在收件人信息 
                            Ordermodel.AddresseeId = addmodel.Id;//收件人Id
                            addmodel.qyo = Province;//省
                            addmodel.qye = City;//地级市
                            addmodel.qyt = Area;//区县
                            _INA_AddresseeInfoDao.NUpdate(addmodel); //更新收件人区域信息
                        }
                        else
                        {//不存在收件人信息
                            addmodel = new NA_AddresseeInfoView();
                            addmodel.ACompany = ShipName;//收件人公司信息（）
                            addmodel.Aname = ShipName;//收件人名称
                            addmodel.Tel = ShipCellPhone;//收件人电话
                            addmodel.Address =ShipRegion+ShipAddress;//收件地址
                            addmodel.qyo = Province;//省
                            addmodel.qye= City;//地级市
                            addmodel.qyt = Area;//区县
                            addmodel.CusId = Custmodel.Id;//客户ID
                            addmodel.C_datetime = DateTime.Now;//创建时间
                            string addId = _INA_AddresseeInfoDao.InsertID(addmodel);//保存收件人信息 返回Id
                            Ordermodel.AddresseeId = addId;//收件人Id
                        }
                        Ordermodel.KhId = Custmodel.Id;//客户ID
                        Ordermodel.Khname = Custmodel.Id;//客户Id
                    }
                    else//不存在 该客户信息
                    {
                        Custmodel = new NACustomerinfoView();
                        if (company != null && company != "")
                        {//如果公司名称为空，就保存用户登录名称
                            Custmodel.Name = company;//客户公司名称
                        }
                        else
                        {
                            Custmodel.Name = userName;//客户公司名称
                        }
                        Custmodel.DsUid = userid;//电商平台用户userid
                        Custmodel.LxrName = ShipName;//客户联系人名称
                        Custmodel.Tel = BuyerCellPhone;//客户电话
                        Custmodel.Adress = ShipRegion + ShipAddress;//客户地址
                        Custmodel.CreateTime = DateTime.Now;//创建时间
                        Custmodel.isjxs = Convert.ToInt32(Haswlw);//是否物联网电控箱的经销商 0 无 1 是
                        Custmodel.Status = 1;//启用
                        Custmodel.Isds = 1;//是电商同步过来的数据
                        string CusId = _INACustomerinfoDao.InsertID(Custmodel);//保存该客户 返回该客户ID
                        NA_AddresseeInfoView addmodel = new NA_AddresseeInfoView();//收件人实例化
                         addmodel.ACompany = ShipName;//收件人公司
                         addmodel.Aname = ShipName;//收件人名称
                         addmodel.Address = ShipRegion + ShipAddress;//收件人地址
                         addmodel.qyo = Province;//省
                         addmodel.qye = City;//地级市
                         addmodel.qyt = Area;//区县
                         addmodel.Tel = ShipCellPhone;//收件人地址
                         addmodel.C_datetime = DateTime.Now;//创建时间
                         addmodel.CusId = CusId;//客户ID
                        string addId = _INA_AddresseeInfoDao.InsertID(addmodel);//保存收件人信息 返回Id
                        Ordermodel.AddresseeId = addId;//收件人Id
                        Ordermodel.KhId = CusId;//客户ID
                        Ordermodel.Khname = CusId;//客户Id
                    }
                    Ordermodel.Ddzt = Convert.ToInt32(OrderStatus);//订单状态
                    Ordermodel.ShippingStatus = Convert.ToInt32(ShippingStatus);//货运状态
                    Ordermodel.PaymentStatus = Convert.ToInt32(PaymentStatus);//支付状态
                    Ordermodel.Sc_Id = OrderCode;//商城订单 编号（唯一识别）
                    Ordermodel.Xs_datetime = Convert.ToDateTime(CreatedDate);//下单时间
                    Ordermodel.Sort =Convert.ToInt32(Sort);//排序
                    Ordermodel.States = 0;//启用
                    Ordermodel.C_datetime = DateTime.Now;//创建时间
                    Ordermodel.SM_ZT = 0;//未扫描
                    if (_INQ_productinfoDao.JcProisSm(sku))
                        Ordermodel.Issm = 1;//需要扫码
                    else
                        Ordermodel.Issm = 0;//不需要扫描
                    string OrderId = _INA_XSinfoDao.InsertID(Ordermodel);//保存订单返回 订单ID
                    NA_XSdetailsinfoView Ordermxmodel = new NA_XSdetailsinfoView();
                    Ordermxmodel.xsId = OrderId;//销售订单Id
                    Ordermxmodel.Je = Convert.ToDecimal(Quantity) * Convert.ToDecimal(AdjustedPrice);//明细价格
                    Ordermxmodel.SL = Convert.ToInt32(Quantity);//产品数量
                    Ordermxmodel.cpbianmao = sku;
                    Ordermxmodel.beizhu = beizhu;
                    NQ_productinfoView SPmodel = new NQ_productinfoView();
                    SPmodel = _INQ_productinfoDao.GetProinfobyname(sku);//根据产品名称查询产品信息
                    if (SPmodel != null)
                    {
                        Ordermxmodel.cpId = SPmodel.Id;//产品
                    }
                    else
                    {
                        SPmodel = new NQ_productinfoView();
                        SPmodel.Pname = SPName;
                        SPmodel.P_bianhao = sku;
                        SPmodel.CreateTime = DateTime.Now;
                     string PRiD= _INQ_productinfoDao.InsertID(SPmodel);
                     Ordermxmodel.cpId = PRiD;
                    }
                    _INA_XSdetailsinfoDao.Ninsert(Ordermxmodel);
                }
                else
                { //有重复订单号
                    NA_XSinfoView Ordermodel = new NA_XSinfoView();//订单实例化
                    Ordermodel = _INA_XSinfoDao.GetxsinfobyOrderCode(OrderCode);
                    Ordermodel.Xs_je = Ordermodel.Xs_je + (Convert.ToDecimal(Quantity) * Convert.ToDecimal(AdjustedPrice));
                    Ordermodel.S_Number = Ordermodel.S_Number + Convert.ToDecimal(Quantity);//数量(出货扫码)
                    Ordermodel.CP_Number = Ordermodel.CP_Number + Convert.ToDecimal(Quantity);//数量（出货数量）
                    if (Ordermodel.Issm == 0)
                    {
                        if (_INQ_productinfoDao.JcProisSm(sku))
                        {
                            Ordermodel.Issm = 1;//需要扫码
                        }
                        else
                        {
                            Ordermodel.Issm = 0;//不需要扫描
                        }
                    }
                    //if (_INQ_productinfoDao.JcProisSm(sku))
                    //    Ordermodel.Issm = 1;//需要扫码
                    //else
                    //    Ordermodel.Issm = 0;//不需要扫描
                    _INA_XSinfoDao.NUpdate(Ordermodel);//更新订单总价
                    NA_XSdetailsinfoView Ordermxmodel = new NA_XSdetailsinfoView();
                    Ordermxmodel.xsId = Ordermodel.Id;//订单Id
                    Ordermxmodel.Je = Convert.ToDecimal(Quantity) * Convert.ToDecimal(AdjustedPrice);//明细价格
                    Ordermxmodel.SL = Convert.ToInt32(Quantity);//产品数量
                    Ordermxmodel.cpbianmao = sku;
                    Ordermxmodel.beizhu = beizhu;
                    NQ_productinfoView SPmodel = new NQ_productinfoView();
                    SPmodel = _INQ_productinfoDao.GetProinfobyname(sku);//根据产品名称查询产品信息
                    if (SPmodel != null)
                    {
                        Ordermxmodel.cpId = SPmodel.Id;//产品
                    }
                    else
                    {
                        SPmodel = new NQ_productinfoView();
                        SPmodel.Pname = SPName;
                        SPmodel.P_bianhao = sku;
                        SPmodel.CreateTime = DateTime.Now;
                        string PRiD = _INQ_productinfoDao.InsertID(SPmodel);
                        Ordermxmodel.cpId = PRiD;
                    }
                    //判断产品库存
                    //decimal PKcsum = GetKcsum("'" + sku + "'");//产品库存
                    //if (PKcsum < Convert.ToInt32(Quantity)) { //产品库存小于发货量
                    //  Flow_ProductionNoticeinfoView PNModel = new Flow_ProductionNoticeinfoView();
                    //     PNModel.Product_Id = Ordermxmodel.cpId;//产品Id
                    //     PNModel.Xsorder_Id = Ordermxmodel.xsId;//订单Id
                    //     PNModel.Number = Convert.ToInt32(Quantity);//数量
                    //     PNModel.Status = 0;//记录状态
                    //     PNModel.Type = 0;//处理状态
                    //     PNModel.Createtime = DateTime.Now;//创建时间
                    //     _IFlow_ProductionNoticeinfoDao.Ninsert(PNModel);
                    //}
                    _INA_XSdetailsinfoDao.Ninsert(Ordermxmodel);
                }
            }
            return "{\"status\":\"true\"}";//添加成功
        } 
        #endregion

        //检测产品是否存在库存并且返回库存数量
        public decimal GetKcsum(string P_bianhao)
        {
            Newasia.XYOffer Dsmodel = new Newasia.XYOffer();
            string Wldm =  P_bianhao;//物料代码
            string Key = "00BE974F-C52D-434D-AB99-4D9E3796CD81";
            DataTable dt = Dsmodel.GetKuCun(Wldm, Key);
            decimal kcsum=Convert.ToDecimal(dt.Rows[0]["count"]);//库存数量
            return kcsum;
        }

        /// <summary>
        /// 远程监控
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="gcsId"></param>
        /// <param name="YcKey"></param>
        /// <returns></returns>
        [HttpPost]
        public string EditgcsBD(string SID, string gcsId, string YcKey)
        {
            if (YcKey == "4a2f262ac5ac77dbb339fbb1f93b5075")//接口验证是否 是远程监控的访问的
            {
                WL_DkxInfoView wldkmodel = new WL_DkxInfoView();
                wldkmodel = _IWL_DkxInfoDao.GetDkxinfo(SID);
                if (wldkmodel != null)
                {//存在该电控箱的
                    if (wldkmodel.WL_zt != 2)
                    { //物联网电控箱还未上线的
                        WL_GcsinfoView wlgcsmodel = new WL_GcsinfoView();
                        wlgcsmodel = _IWL_GcsinfoDao.GetGcsinfo(gcsId);
                        if (wldkmodel != null)
                        {
                            wldkmodel.WL_zt = 2;//在线
                            wldkmodel.Xs_gcsId = wlgcsmodel.Id;
                            wldkmodel.Sx_time = DateTime.Now;//上线时间
                            wldkmodel.WL_BXStarttime = DateTime.Now;//保修时间开始
                            wldkmodel.WL_BXEndtime = Convert.ToDateTime(DateTime.Now.AddMonths(16).ToShortDateString());//保修时间结束
                            wldkmodel.Jf_endtime = Convert.ToDateTime(DateTime.Now.AddMonths(12).ToShortDateString());//下次缴费时间
                            if (_IWL_DkxInfoDao.NUpdate(wldkmodel))
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
                            return "{\"status\":\"false4\"}";//不存在该工程商
                        }
                    }
                    else
                    {
                        return "{\"status\":\"false1\"}";//重复绑定
                    }
                }
                else
                {
                    return "{\"status\":\"false3\"}";//不存在该电控箱
                }
            }
            else
            {
                return "{\"status\":\"false2\"}";//验证失败
            }
        }

        //更新电商平台 用户的Uid
        public string UpdateKHId()
        {
            Newasia.XYOffer model = new Newasia.XYOffer();
             DataTable dt = model.GetOrders(0);
             for (int a = 0, b = dt.Rows.Count; a < b; a++)
             {
                 string userid = dt.Rows[a]["userid"].ToString();//电商客户Id
                 string userName = dt.Rows[a]["userName"].ToString();//电商用户登录名称（客户登录名称）
                 string company = dt.Rows[a]["company"].ToString();//电商平台用户公司名称（客户公司名称）

                 NACustomerinfoView Custmodel = new NACustomerinfoView();//实例化 
                 //  Custmodel = _INACustomerinfoDao.GetKHinfobykhname(userName, ShipName);//查询客户信息
                 if (company != null && company != "")
                 {//如果客户公司名称不为空就用公司名称查询 否则就用登录名称查询
                     Custmodel = _INACustomerinfoDao.GetKHinfobyname(company);
                     if (Custmodel != null) {
                         Custmodel.DsUid = userid;//电商平台的用户userId 
                         _INACustomerinfoDao.NUpdate(Custmodel);
                     }
                 }
                 else
                 {
                     Custmodel = _INACustomerinfoDao.GetKHinfobyname(userName);
                     if (Custmodel != null)
                     {
                         Custmodel.DsUid = userid;//电商平台的用户userId 
                         _INACustomerinfoDao.NUpdate(Custmodel);
                     }
                 }
             }
             return "{\"status\":\"true\"}";//添加成功
        }

        //开放扫描出货页面

        #region //扫描查询订单
        public ViewResult OPenSmSelectOrder()
        {
            return View();
        }
        #endregion

        #region //扫码发货订单详情
        public ViewResult OpenSmOrderinfoView(string Id)
        {
            ViewData["SMOrderId"] = Id;//保存订单Id
            return View("OpenSmOrderinfoView");
        }
        #endregion

        #region //修改订单的扫码数量
        public ActionResult UpdateordersmsumView(string Id)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            return View(model);
        }

        //扫码数量修改提交
        public string EideUpdateordersmsun(string Id, string sum)
        {
            NA_XSinfoView model = _INA_XSinfoDao.NGetModelById(Id);
            if (model != null)
            {
                model.S_Number = Convert.ToDecimal(sum);
                bool flay = false;
                flay = _INA_XSinfoDao.NUpdate(model);
                if (flay)
                    return "0";//修改成功
                else
                    return "1";//修改失败

            }
            else
            {
                return "2";//为空
            }
        }
        #endregion

        #region //根据销售Id 查找销售订单的详情
        /// <summary>
        /// 查询销售订单 json
        /// </summary>
        /// <param name="Id">销售订单ID</param>
        /// <returns></returns>
        public string GetXsinfojson(string Id)
        {
            string json = "{\"status\":\"true\"}";
            NA_XSinfoView model = new NA_XSinfoView();
            model = _INA_XSinfoDao.NGetModelById(Id);
            if (model != null)
            {
                json = JsonConvert.SerializeObject(_INA_XSinfoDao.NGetModelById(Id));
            }
            return json;
        }
        #endregion

        #region //当前发货量
        [HttpPost]
        public string GetsmCount(string ordermxId)
        {
            int smcount = _IWL_DkxInfoDao.GetChcunotbyorderId(ordermxId);
            return "{\"status\":\"" + smcount + "\"}";//添加成功
        }
        #endregion

        #region //扫码出货 保存
        /// <summary>
        /// 扫码出货
        /// </summary>
        /// <param name="khId">客户Id</param>
        /// <param name="sid">sid</param>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SmEditch(string khid, string sid, string orderId, string zsl)
        {
            try
            {
                bool flay = false;
                SessionUser user = Session[SessionHelper.User] as SessionUser;
                WL_DkxInfoView modelnew = new WL_DkxInfoView();
                int smcount = _IWL_DkxInfoDao.GetChcunotbyorderId(orderId);
                if (smcount < Convert.ToInt32(zsl) * 2)//后期去掉
                {//检测是否下单完成
                    string sidstr = Getsid(sid);
                    if (sidstr.Length < 10)
                    {
                        return Json(new { result = "error1" });
                    }
                    else
                    {
                        modelnew = _IWL_DkxInfoDao.GetDkxinfo(sidstr);//通过sid 查找电控箱信息
                        if (modelnew != null)
                        {//检测该数据是否存在
                            if (modelnew.Is_xs == 0)
                            {
                                modelnew.Xs_jxsId = khid;//客户Id(经销商Id)
                                modelnew.Xs_datetime = DateTime.Now;//销售时间
                                modelnew.WL_zt = 1;//已销售（旧）
                                modelnew.Is_xs = 1;//已经销售
                                modelnew.OrdermxId = orderId;//订单Id
                                flay = _IWL_DkxInfoDao.NUpdate(modelnew);
                                if (flay)
                                    return Json(new { result = "success" });
                                else
                                    return Json(new { result = "error" });
                            }
                            else
                            {
                                return Json(new { result = "yxs" });//已经销售
                            }
                        }
                        else
                        {
                            return Json(new { result = "bcz" });//不存在该 电控箱
                        }
                    }
                }
                else
                {
                    return Json(new { result = "fhwc" });//发货完成
                }
            }
            catch
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //扫码 更加订单Id 改变订单扫码状态
        [HttpPost]
        public string Updateordersmzt(string orderId)
        {
            NA_XSinfoView xsmodel = new NA_XSinfoView();
            xsmodel = _INA_XSinfoDao.NGetModelById(orderId);
            xsmodel.SM_ZT = 1;//已经扫码
            if (_INA_XSinfoDao.NUpdate(xsmodel))
            {
                return "{\"status\":\"true\"}";//添加成功
            }
            else
            {
                return "{\"status\":\"fales\"}";//添加成功
            }
        }
        #endregion

        #region //截取sid
        public string Getsid(string a)
        {
            string s = a.Replace(" ", "").ToUpper();
            int i = s.IndexOf("D:") + 2;
            int j = s.IndexOf(".KEY");
            string str = s.Substring(i, j - i);
            return str;
        }
        #endregion

        #region //根据销售Id查找销售明细信息
        public string GetXsdetailjson(string Id)
        {
            string json;
            json = JsonConvert.SerializeObject(_INA_XSdetailsinfoDao.GetxsdetaillistbyxsId(Id));
            return json;
        }
        #endregion


        public void sidtest()
        {
            string ApiKey = "808000100201";
            string url;
            string str;
            url = "http://www.sbycjk.net/ws/greeting/" + ApiKey ;
            str = new System.Net.WebClient().DownloadString(url);
            List<JsonClass> model = getObjectByJson<JsonClass>(str);
        }

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

        public class JsonClass
        {
            public string sid { get; set; }
            public string time { get; set; }
        }

        public class Jsonsid
        {
            public virtual string WL_SSD { get; set; }
        }

        public void SerSID()
        {
            IList<WL_DkxInfoView> modellist = _IWL_DkxInfoDao.Getscwsxinfo();
            string str = JsonConvert.SerializeObject(modellist);
            List<Jsonsid> model = getObjectByJson<Jsonsid>(str);
            string jsonstr = JsonConvert.SerializeObject(model);
            string url;
            url = "http://www.sbycjk.net/ws/greeting/";
            string result = HttpUtility11.PostUrl(url,"sid="+jsonstr);
  
        }

        public ActionResult TZurl(string code)
        {
             log4net.LogManager.GetLogger("ApplicationInfoLog").Error("code:" + code);
             JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
             string openid = obj["openid"].ToString();
             log4net.LogManager.GetLogger("ApplicationInfoLog").Error("openid:" + openid);
             return Redirect("http://www.chinanewasia.com/mhome?code=" + openid);
        }

        public object Getaccess_tokenbycodeinface(string code)
        {
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx44f57e0f1268190b&secret=bcbfe205a0e5fad5a4ab7f2ebb90656d&code={0}&grant_type=authorization_code ", code);
            string json = HttpUtility11.GetData(url);
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("json:" + json);
            return obj;
        }

        #region //同步电控箱sid数据
        public string TBsidtest()
        {
            WL_DkxInfoView dkmodel = _IWL_DkxInfoDao.Getwldkxinfonewdate();
            int t = 0;
            if (dkmodel != null)
            {
                if (dkmodel.sidxh != null)
                {
                    t = Convert.ToInt32(dkmodel.sidxh);
                }
            }
            t = 65640;
            string url;
            url = "http://192.168.10.243:8081/newasia_remotexp/getsidsofshipment/getByid/" + t;
            string result = HttpUtility11.GetData(url);
            List<tbsid> timemodel = getObjectByJson<tbsid>(result);
            foreach (var a in timemodel)
            {
                WL_DkxInfoView dkxmodel = new WL_DkxInfoView();
                dkxmodel = _IWL_DkxInfoDao.GetDkxinfo(a.sid);
                if (dkxmodel != null)//存在 -更新
                {
                    dkxmodel.sidxh = Convert.ToInt32(a.id);
                    dkxmodel.sidc_time = Convert.ToDateTime(a.createTime);
                    _IWL_DkxInfoDao.NUpdate(dkxmodel);
                }
                else//不存在插入
                {
                    dkxmodel = new WL_DkxInfoView();
                    dkxmodel.WL_SSD = a.sid;
                    dkxmodel.sidxh = Convert.ToInt32(a.id);
                    dkxmodel.sidc_time = Convert.ToDateTime(a.createTime);
                    dkxmodel.SC_time = Convert.ToDateTime(a.createTime);
                    _IWL_DkxInfoDao.Ninsert(dkxmodel);
                }
            }
            return result;
        }

        //实体
        public class tbsid
        {
            public string id { get; set; }
            public string sid { get; set; }
            public string createTime { get; set; }
        }
        #endregion

        #region //微信测试客服消息发送
        public static string testFS(string menu)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", MenuContext.AccessToken);
            //string menu = FileUtility.Read(Menu_Data_Path);
            return HttpUtility11.SendHttpRequest(url, menu);
            //}
        }

        public void testBTN()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"touser\":\"" + "ol6V6t-M-QUOkjLqQ8_wgSZ9jYMs" + "\",");
            builder.Append("\"msgtype\":\"" + "text" + "\",");
            builder.Append("\"text\":{");
            builder.Append("\"content\":\"" +"你好测试！"+ "\"}");
            builder.Append("}");
            string str = builder.ToString();
            testFS(str);
        }
        #endregion

        #region //二维码后端生产测试
        public ActionResult testerweimaView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetORImage(string content)
        {
            string timeStr = DateTime.Now.ToFileTime().ToString();
            Bitmap bitmap = QRHelper.QRCodeEncoderUtil(content);
            string fileName = Server.MapPath("~") + "Content\\images\\QRImages\\" + timeStr + ".jpg";
            bitmap.Save(fileName);//保存位图
            string imageUrl = "/Content/images/QRImages/" + timeStr + ".jpg";//显示图片  
            return Content(imageUrl);
        }

        [HttpPost]
        public ActionResult GetORImageContent(string imageName)
        {
            string fileUrl = Server.MapPath("~") + "Content\\images\\QRImages\\" + imageName;
            Bitmap bitMap = new Bitmap(fileUrl);
            string content = QRHelper.QRCodeDecoderUtil(bitMap);
            return Content(content);
        }
        #endregion

        #region //给远程平台的接口
        #region //返回电控箱的发货时间（SID 仓库扫描时间） 和经销商的名称 联系方式

        public string Externalinterface(string SID)
        {
            if (SID != "" && SID != null)//参数不为空
            {
                WL_DkxInfoView dkxmodel = _IWL_DkxInfoDao.GetDkxinfo(SID);
                if (dkxmodel != null)
                {
                    string json;
                    fhjsonmodel model = new fhjsonmodel();
                    if (dkxmodel.WL_zt != 0)
                    {
                        //查询经销商
                        NACustomerinfoView cusmodel = _INACustomerinfoDao.NGetModelById(dkxmodel.Xs_jxsId);
                        model.SID = dkxmodel.WL_SSD;//sid
                        model.Deliverytime = dkxmodel.Xs_datetime.ToString();//扫码时间
                        model.CusName = cusmodel.Name;//客户名称
                        model.Tel = cusmodel.Tel;//联系方式
                        json = JsonConvert.SerializeObject(model);
                    }
                    else
                    {
                        model.SID = dkxmodel.WL_SSD;//sid
                        model.Deliverytime = "未售";//扫码时间
                        model.CusName = "未售";//客户名称
                        model.Tel = "未售";//联系方式
                        json = JsonConvert.SerializeObject(model);
                    }
                    return json;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        //返回数据实例
        public class fhjsonmodel
        {
            /// <summary>
            /// sid
            /// </summary>
            public virtual string SID { get; set; }

            /// <summary>
            /// 发货时间
            /// </summary>
            public virtual string Deliverytime { get; set; }

            /// <summary>
            /// 客户名称
            /// </summary>
            public virtual string CusName { get; set; }

            /// <summary>
            /// 联系电话
            /// </summary>
            public virtual string Tel { get; set; }
        }

        #endregion 

        #region //返回全部经销商的信息，省份
        public string GetAllExternalinterface()
        {
            try
            {
                IList<NACustomerinfoView> list = _INACustomerinfoDao.Getalljxqinfolist();
                string json = "";
                if (list != null)
                {
                    List<fhjsonalljxsmodel> modellist = new List<fhjsonalljxsmodel>();
                    foreach (var item in list)
                    {
                        fhjsonalljxsmodel sm = new fhjsonalljxsmodel();
                        sm.Cusname = item.Name;
                        if (item.qyId != "" && item.qyId != null)
                            sm.Qyname = getsfnamebyId(item.qyId);
                        else
                            sm.Qyname = "null";
                        sm.tel = item.Tel;
                        modellist.Add(sm);

                    }
                    json = JsonConvert.SerializeObject(modellist);
                }
                return json;
            }
            catch
            {
                return null;
            }
        }

        public string getsfnamebyId(string Id)
        {
            try
            {
                NA_QyinfoView model = _INA_QyinfoDao.NGetModelById(Id);
                if (model != null)
                    return model.Qyname;
                else
                    return "null";
            }
            catch
            {
                return "null";
            }
        }

        //返回的数据实力
        public class fhjsonalljxsmodel
        {
            /// <summary>
            /// 经销商名称
            /// </summary>
            public virtual string Cusname { get;set; }

            public virtual string Qyname { get; set; }

            public virtual string tel { get; set; }
        }
        #endregion
        #endregion

        #region //后端导出excel表格测试
        public FileResult TestExcel()
        {
            //获取list数据
           // var list = bll.NurseUserListExcel("", "ID asc");

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("ID");
            row1.CreateCell(1).SetCellValue("用户姓名");
            row1.CreateCell(2).SetCellValue("电话");
            row1.CreateCell(3).SetCellValue("注册时间");
            row1.CreateCell(4).SetCellValue("邀请人ID");
            row1.CreateCell(5).SetCellValue("邀请人名称");
            row1.CreateCell(6).SetCellValue("邀请人电话");
            row1.CreateCell(7).SetCellValue("总积分");
            row1.CreateCell(8).SetCellValue("已使用积分");
            row1.CreateCell(9).SetCellValue("可用积分");

            //将数据逐步写入sheet1各个行
            //for (int i = 0; i < list.Count; i++)
            //{
            //    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
            //    rowtemp.CreateCell(0).SetCellValue(list[i].ID);
            //    rowtemp.CreateCell(1).SetCellValue(list[i].Name);
            //    rowtemp.CreateCell(2).SetCellValue(list[i].Phone);
            //    rowtemp.CreateCell(3).SetCellValue(list[i].CreateTime.Value.ToString());
            //    rowtemp.CreateCell(4).SetCellValue(list[i].InviterID.Value);
            //    rowtemp.CreateCell(5).SetCellValue(list[i].iName);
            //    rowtemp.CreateCell(6).SetCellValue(list[i].iPhone);
            //    rowtemp.CreateCell(7).SetCellValue(list[i].IntegralSum);
            //    rowtemp.CreateCell(8).SetCellValue(list[i].IntegralSy);
            //    rowtemp.CreateCell(9).SetCellValue(list[i].IntegralKy);
            //}
            NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(1);
            rowtemp.CreateCell(0).SetCellValue("2");
            rowtemp.CreateCell(1).SetCellValue("3");
            rowtemp.CreateCell(2).SetCellValue("3");
            rowtemp.CreateCell(3).SetCellValue("4");
            rowtemp.CreateCell(4).SetCellValue("5");
            rowtemp.CreateCell(5).SetCellValue("6");
            rowtemp.CreateCell(6).SetCellValue("7");
            rowtemp.CreateCell(7).SetCellValue("8");
            rowtemp.CreateCell(8).SetCellValue("9");
            rowtemp.CreateCell(9).SetCellValue("10");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "用户信息.xls");
        }
        #endregion

        #region //续费 经销商分润对接电商平台
        public bool WL_JxsfrTBdata(int Uid, decimal jine)
        {
            Newasia.XYOffer model = new Newasia.XYOffer();
            bool str = model.WlwCharge(Uid,jine);
            return str;
        }
        #endregion

        #region //测试
        //k3web 接口
        public string TestK3()
        {
            string url;
            //url = "http://222.92.203.58:83//Baseitem.asmx/GetAllItemsBycpname/";
            //string result = HttpUtility.PostUrl(url, "name=" + "01");
            //return result;
            url = "http://222.92.203.58:83//Baseitem.asmx/getAllItemsByName?name=" + "1";
            string result = HttpUtility11.GetData(url);
            return result;
        }

        //阿里快递接口调用测试
        public string  KDJETEST()
        {
          //string json=  ExpressinterfaceHelper.Main("719847209470", "ZTO");
          return "";
        }
        #endregion

        #region //更新订单中出货数据
        public void updateorderchsj()
        {
           INA_XSinfoDao _INA_XSinfoDao = ContextRegistry.GetContext().GetObject("NA_XSinfoDao") as INA_XSinfoDao;
           INA_XSdetailsinfoDao _INA_XSdetailsinfoDao = ContextRegistry.GetContext().GetObject("NA_XSdetailsinfoDao") as INA_XSdetailsinfoDao;
           IList<NA_XSinfoView> listmodel = _INA_XSinfoDao.NGetList();
           foreach (var item in listmodel)
           {
               NA_XSinfoView model = new NA_XSinfoView();
               model = item;
               decimal sum = _INA_XSdetailsinfoDao.GetchsumbyxsId(model.Id);
               model.S_Number = sum;
               model.CP_Number = sum;
               _INA_XSinfoDao.NUpdate(model);
           }
        }
        #endregion

        #region //物联网电控箱退货扫码
        //退货扫码页面
        public ActionResult IotDkxTHView()
        {
            return View();
        }
        //扫码提交
        [HttpPost]
        public JsonResult THsmEide(string sid)
        {
            sid = Getsid(sid);//截取SID
            WL_DkxInfoView model = new WL_DkxInfoView();
            model = _IWL_DkxInfoDao.GetDkxinfo(sid);
            if (model != null)//存在
            {
                if (model.Is_xs == 1)//已经销售的,可以退货
                {
                    WL_dkxthjlinfoView thmodel = new WL_dkxthjlinfoView();
                    //保存退货记录（上次的发货的时间 订单 经销商）
                    thmodel.SID = sid;
                    thmodel.THdatetime = DateTime.Now;
                    thmodel.XsordId = model.OrdermxId;
                    thmodel.scxsdtetime = model.Xs_datetime;
                    thmodel.jltype = 0;//正常订单退货
                    _IWL_dkxthjlinfoDao.Ninsert(thmodel);//保存
                    model.Xs_datetime = null;//销售时间
                    model.Xs_jxsId = null;//经销商
                    model.Xs_gcsId = null;//工程商
                    model.Sx_time = null;//上线时间
                    model.WL_zt = 0;//状态
                    model.WL_BXStarttime = null;//保修时间开始
                    model.WL_BXEndtime = null;//保修时间结束
                    model.Jf_starttime = null;//上次缴费时间
                    model.Jf_endtime = null;//下次缴费时间
                    model.Jf_cs = 0;//缴费次数
                    model.OrdermxId = "";//销售单
                    model.Is_xs = 0;//是否销售
                    model.Is_sx = 0;//是否上线
                    bool flay = false;
                    flay = _IWL_DkxInfoDao.NUpdate(model);
                    if (flay)
                        return Json(new { result = "true" });//提交成功
                    else
                        return Json(new { result = "false" });//提交失败
                }
                else
                {
                    return Json(new { result = "wxs" });//该电控箱尚未扫描出货
                }
            }
            else
            {
                return Json(new { result = "bcz" });//不存在该电控箱
            }
        }

        //当天退货扫码数据
        public string GetTodayTHsmdata()
        {
            string json;
            json = JsonConvert.SerializeObject(_IWL_dkxthjlinfoDao.GetsmthdataToday());
            return json;
        }
        #endregion

        #region //物联网电控箱扫描初始化
        //初始化扫描 页面
        public ActionResult IotInitialization()
        {
            return View();
        }
        //扫码提交（初始化）
        [HttpPost]
        public JsonResult CSHtjEide(string sid)
        {
            sid = Getsid(sid);//截取SID
            WL_DkxInfoView model = new WL_DkxInfoView();
            model = _IWL_DkxInfoDao.GetDkxinfo(sid);
            if (model != null)//存在
            {
                if (model.Is_xs == 1)
                {
                    WL_dkxthjlinfoView thmodel = new WL_dkxthjlinfoView();
                    //保存退货记录（上次的发货的时间 订单 经销商）
                    thmodel.SID = sid;
                    thmodel.THdatetime = DateTime.Now;
                    thmodel.XsordId = model.OrdermxId;
                    thmodel.scxsdtetime = model.Xs_datetime;
                    thmodel.jltype = 1;//扫错码初始化
                    _IWL_dkxthjlinfoDao.Ninsert(thmodel);//保存
                    NA_XSinfoView xsmodel = _INA_XSinfoDao.NGetModelById(model.OrdermxId);//关联的订单信息
                    if (xsmodel != null)
                    {
                        xsmodel.SM_ZT = 0;
                        _INA_XSinfoDao.NUpdate(xsmodel);
                    }
                    model.Xs_datetime = null;//销售时间
                    model.Xs_jxsId = null;//经销商
                    model.Xs_gcsId = null;//工程商
                    model.Sx_time = null;//上线时间
                    model.WL_zt = 0;//状态
                    model.WL_BXStarttime = null;//保修时间开始
                    model.WL_BXEndtime = null;//保修时间结束
                    model.Jf_starttime = null;//上次缴费时间
                    model.Jf_endtime = null;//下次缴费时间
                    model.Jf_cs = 0;//缴费次数
                    model.OrdermxId = "";//销售单
                    model.Is_xs = 0;//是否销售
                    model.Is_sx = 0;//是否上线
                    bool flay = false;
                    flay = _IWL_DkxInfoDao.NUpdate(model);
                    if (flay)
                        return Json(new { result = "true" });//提交成功
                    else
                        return Json(new { result = "false" });//提交失败
                }
                else
                {
                    return Json(new { result = "wxs" });//该电控箱尚未扫描出货
                }
            }
            else
            {
                return Json(new { result = "bcz" });//不存在该电控箱
            }
        }
        #endregion

        #region //微信支付测试
        public ActionResult TestwpayView()
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("访问：成功！" );
            return View();
        }

        //查询支付参数
        public string GetSessionpayconfig()
        {
            NewConfig config = new NewConfig();
            Session[SessionHelper.Wpayconfig] = null;
            Session.Remove(SessionHelper.Wpayconfig);
            config.APPID = "wx44f57e0f1268190b";
            config.MCHID = "1280104601";
            config.KEY="Ivn9mvbWb0XoijTDmJ6VULQzCI2toNy5";
            config.APPSECRET = "bcbfe205a0e5fad5a4ab7f2ebb90656d";
            config.NOTIFY_URL = "http://paysdk.weixin.qq.com/example/ResultNotifyPage.aspx";
            Session[SessionHelper.Wpayconfig] = config;
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("支付参数" + config);
            return "1";
        }

        public ActionResult PayView(string code)
        {
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("支付页面" + code);
            JObject obj = (JObject)Getaccess_tokenbycodeinface(code);
            if (obj.Count == 2)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("obj：" + obj.Count);
                NewConfig config = Session[SessionHelper.Wpayconfig] as NewConfig;
                string appid = config.APPID;
                string dlurl = "http://wx.chinanewasia.com/";
                string redurl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}publicAPI/PayView&response_type=code&scope=snsapi_base&state=STATE&connect_redirect=1#wechat_redirect", appid, dlurl);
                return Redirect(redurl);

            }
            else
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error("asasas：" + obj["openid"].ToString());
                Session["OpenidNew"] = obj["openid"].ToString();
                return View();
            }
        }

        public void payzf(string total_fee)
        {
            string openid = Session["OpenidNew"].ToString();
            NewConfig config = Session[SessionHelper.Wpayconfig] as NewConfig;
            string name = "商品测试";
            string orderNO = WxPayApi.NewGenerateOutTradeNo(config.MCHID);//商户单号
            //string url = "http://wx.chinanewasia.com/example/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + total_fee + "&cpname=" + name + "&o_Id=00 &orderno=" + orderNO;
            string url = "http://wx.chinanewasia.com/example/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + total_fee + "&cpname=" + name + "&O_Id=00";
            Response.Redirect(url);
        }

        #endregion

        #region //通过UID查询sid接口数据测试
        public string Getdksinfobyusid()
        {
            IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
            IList<WL_DkxInfoView> lismode = _IWL_DkxInfoDao.GetSIDbyKhId("3394", 0, 200);
            string json;
            json = JsonConvert.SerializeObject(lismode);
            return json;
        }
        #endregion

        #region //提供沈红迪调用的接口
        //通过物料的前俩位和物料的中间三位查询物料数据
        public string Getmaterielinfobystr1andstr2(string str1, string str2)
        {
            IList<K3_wuliaoinfoView> modellist = _IK3_wuliaoinfoDao.Getwuliaobyfnumber23(str1,str2);
            string json = JsonConvert.SerializeObject(modellist);
            return json;
        }
        #endregion


        #region //电控箱下单汇总表
        #region //电控箱生产单列表 
        public ActionResult DKXhzlist(int? pageIndex)
        {
            //AlGCSdataDropdown(null);
            //PagerInfo<DKX_DDinfoView> listmodel = GetDKXDDlistpage(pageIndex, null, null, null, null, null, null, null, null, null, null, "0",null,null,null,null,null,null,null,null,null,null);
            //return View(listmodel);
            //Response.Redirect("http://www.chinanewasia.com/");
            return View();
        }
        #endregion


        #region //新电控箱生产单列表 
        public ActionResult NewDKXhzlist(int? pageIndex)
        {
            allDKXtypeDropdown(null);//电控箱类型的下来数据
            ViewBag.MyJson = getjsonalldkxtypedata();
            AlGCSdataDropdown(null);
            //PagerInfo<DKX_DDinfoView> listmodel = GetDKXDDlistpage(pageIndex, null, null, null, null, null, null, null, null, null, null, "0", null, null, null, null, null, null, null, null, null, null,null);
            return View("DKXhzlist");
            //string IP = GetIP();
            //if (IP == "222.92.203.58")
            //{
            //    AlGCSdataDropdown(null);
            //    PagerInfo<DKX_DDinfoView> listmodel = GetDKXDDlistpage(pageIndex, null, null, null, null, null, null, null, null, null, null, "0", null, null, null, null, null, null, null, null, null, null);
            //    return View("DKXhzlist", listmodel);
            //}
            //else
            //{
            //    Response.Redirect("http://www.chinanewasia.com/");
            //    return View();
            //}
        }
        #endregion

        #region //条件查询
        public JsonResult DKXddSearchList()
        {
            string DD_Bianhao = Request.Form["txt_DD_Bianhao"];//订单标号
            string BJno = Request.Form["txt_BJno"];//报价单号
            string DD_Type = Request.Form["txtDD_Type"];//订单类型
            string KHname = Request.Form["txt_KHname"];//客户名称
            string Lxname = Request.Form["txt_Lxname"];//客户联系人
            string Tel = Request.Form["txt_Tel"];//联系电话
            string Gcs_nameId = Request.Form["txtGCS"];//工程师Id
            string DD_ZT = Request.Form["txtDD_ZT"];//订单状态
            string startctime = Request.Form["txt_startctime"];//创建时间
            string endctiome = Request.Form["txt_endctiome"];//创建时间
            string wcysstartctime = Request.Form["txt_wcysstartctime"];//
            string wcysendctiome = Request.Form["txt_wcysendctiome"];
            string start = Request.Form["txt_start"];//是否启用
            string DHtype = Request.Form["txt_DHtype"];//
            string cpph = Request.Form["txtcpph"];
            string beizhu1 = Request.Form["txtbeizhu1"];
            string beizhu2 = Request.Form["txtbeizhu2"];
            string YQtype = Request.Form["txtYQtype"];
            string Isdqpb = Request.Form["Isdqpb"];
            string Isqtt = Request.Form["Isqtt"];
            string gnjs=Request.Form["gnjs"];
            int? pageIndex = 1;
            if (!string.IsNullOrEmpty(Request.Form["pageIndex"]))
                pageIndex = Convert.ToInt32(Request.Form["pageIndex"]);
            PagerInfo<DKX_DDinfoView> listmodel = GetDKXDDlistpage(pageIndex, DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, Gcs_nameId, DD_ZT, startctime, endctiome, "0",
                DHtype, cpph, beizhu1, beizhu2, YQtype, Isdqpb, Isqtt, gnjs, wcysstartctime, wcysendctiome,null);
            string PageNavigate = HtmlHelperComm.OutPutPageNavigate(listmodel.CurrentPageIndex, listmodel.PageSize, listmodel.RecordCount);
            if (listmodel != null)
                return Json(new { result = listmodel.GetPagingDataJson, PageN = PageNavigate });
            else
                return Json(new { result = "", PageN = PageNavigate });
        }
        #endregion

        #region //获取电控箱生产单列表分页数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">报价单号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1 大系统 2 物联网电控箱</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系人</param>
        /// <param name="Tel">客户电话</param>
        /// <param name="Gcs_nameId">工程师Id</param>
        /// <param name="DD_ZT">当前状态 -1 未提交 0已提交 1 待制图 2制图中 3 待生产 4 生产中 5 验收入库 6 待发货 7 完成 9缺料</param>
        /// <param name="startctime">创建时间</param>
        /// <param name="endctiome">创建时间</param>
        /// <param name="start">是否启用 0启用 1禁用</param>
        /// <returns></returns>
        private PagerInfo<DKX_DDinfoView> GetDKXDDlistpage(int? pageIndex, string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz,string gnjs,
             string wcstarttime, string wcendtime,string POWER)
        {
            
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(10);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.Getdkxhzlistpage(DD_Bianhao,BJno,DD_Type,KHname,Lxname,Tel,Gcs_nameId,DD_ZT,startctime,endctiome,start,DHtype,cpph,beizhu1,beizhu2,YQtype,Isdqpb,Isqttz,gnjs,wcstarttime,wcendtime, POWER);
            return listmodel;
        }
        #endregion

        #region //电控箱查询生产订单分页数据-20210621
        public ActionResult Getdkxorderlist_Publicservice(int? page, int limit, string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz, string gnjs,
             string wcstarttime, string wcendtime, string POWER)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            int CurrentPageIndex = Convert.ToInt32(page);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IDKX_DDinfoDao.SetPagerPageIndex(CurrentPageIndex);
            _IDKX_DDinfoDao.SetPagerPageSize(limit);
            PagerInfo<DKX_DDinfoView> listmodel = _IDKX_DDinfoDao.Getdkxhzlistpage(DD_Bianhao, BJno, DD_Type, KHname, Lxname, Tel, Gcs_nameId, DD_ZT, startctime, endctiome, start, DHtype, cpph, beizhu1, beizhu2, YQtype, Isdqpb, Isqttz, gnjs, wcstarttime, wcendtime, POWER);
            var result = new
            {
                code = 0,
                msg = "",
                count = listmodel.RecordCount,
                data = listmodel.DataList,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region //资料查看的页面
        public ActionResult PubZLCKView(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        }
        #endregion

        #region //订单资料json
        public string AjaxorderbyId(string Id)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_DDinfoDao.NGetModelById(Id));
            return json;
        }
        #endregion

        #region //需求数据json
        public string AjaxxuqiubyorderNo(string Bjno)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_PAY_CONTROL_INFODao.GetDKXxuqiubyORDER_NO(Bjno));
            return json;
        }
        #endregion

        #region //获取资料数据
        public string GetziliaojsonbyddIdandtype(string ddId, string type)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_ZLDataInfoDao.GetzljsonbyId(ddId, type));
            return json;
        }
        #endregion

        #region //订单操作记录查看
        //记录页面
        public ActionResult LCjlckView(string oId)
        {
            ViewData["Id"] = oId;
            return View();
        }

        //操作记录
        public string LCCZJLdata(string oId)
        {
            string json = "";
            json = JsonConvert.SerializeObject(_IDKX_LCCZJLinfoDao.GetlcczjldatabyoId(oId));
            return json;
        }
        #endregion

        #region //查询全部的电控箱类型的数据
        public string getjsonalldkxtypedata()
        {
            try
            {
                IList<DKX_DDtypeinfoView> allmodell = _IDKX_DDtypeinfoDao.NGetList();
                string json = JsonConvert.SerializeObject(allmodell);
                return json;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //电控箱类型下拉数据设置下拉框值(全部数据)
        public void allDKXtypeDropdown(string SelectedPID)
        {
            List<DKX_DDtypeinfoView> CustlistView = _IDKX_DDtypeinfoDao.GetALLdkxtypejson() as List<DKX_DDtypeinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "全部";
            Reasonmodel.Id = "";
            Reasonlist.Add(Reasonmodel);
            if (CustlistView != null)
            {
                foreach (var item in CustlistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Type.ToString();
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != null)
                ViewData["aDTlist"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["aDTlist"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion
        #endregion

        //根据工程师的Id查找工程师的帐号
        public string AjaxGetgcsinfobyId(string gcsId)
        {
            string json = null;
            json = JsonConvert.SerializeObject(_IDKX_GCSinfoDao.NGetModelById(gcsId));
            return json;
        }

        //全部工程师的下拉框数据
        #region //全部工程师的下拉框数据
        public void AlGCSdataDropdown(string SelectedPID)
        {
            List<DKX_GCSinfoView> GcslistView = _IDKX_GCSinfoDao.GetALLgcsDATA() as List<DKX_GCSinfoView>;
            List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            GetReasonlist Reasonmodel = new GetReasonlist();
            Reasonmodel.Name = "请选择";
            Reasonlist.Add(Reasonmodel);
            if (GcslistView != null)
            {
                foreach (var item in GcslistView)
                {
                    Reasonmodel = new GetReasonlist();
                    Reasonmodel.Id = item.Id;
                    Reasonmodel.Name = item.Name;
                    Reasonlist.Add(Reasonmodel);
                }
            }
            if (SelectedPID != null)
                ViewData["GCSDATA"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            else
                ViewData["GCSDATA"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        #region //电控箱下单的各个操作逾期时间
        /// <summary>
        /// 电控箱下单的各个操作逾期时间
        /// </summary>
        /// <param name="czdatetime">上一次操作的时间</param>
        /// <returns></returns>
        public string GetDKXXDCZYQTS(string czdatetime)
        {
            try
            {
                string Tianshu = QRHelper.Getdkxxdczts(czdatetime);
                return Tianshu;
            }
            catch
            {
                return "-";
            }
        }
        #endregion

        #region //电控箱数据导出
        public FileResult DaochuAlldd(string DD_Bianhao, string DHtype, string ddtype, string gcsname, string khname, string xdstrattime, string xdendtime, string ysstrattime, string ysendtime)
        {
            SessionUser Suser = Session[SessionHelper.User] as SessionUser;
            IList<DKX_DDinfoView> list = _IDKX_DDinfoDao.GetallzcDDlist(DD_Bianhao, DHtype,ddtype, gcsname, khname, xdstrattime, xdendtime, ysstrattime, ysendtime);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("生产单号");
            row1.CreateCell(1).SetCellValue("客户名称");
            row1.CreateCell(2).SetCellValue("类型");
            row1.CreateCell(3).SetCellValue("型号");
            row1.CreateCell(4).SetCellValue("功率");
            row1.CreateCell(5).SetCellValue("功能简述");
            row1.CreateCell(6).SetCellValue("提交人");
            row1.CreateCell(7).SetCellValue("工程师");
            row1.CreateCell(8).SetCellValue("进度");
            row1.CreateCell(9).SetCellValue("下单时间");
            row1.CreateCell(10).SetCellValue("完成生产验收");
            row1.CreateCell(11).SetCellValue("客服备注");
            row1.CreateCell(12).SetCellValue("套数");
            row1.CreateCell(13).SetCellValue("制图完成时间");
            row1.CreateCell(14).SetCellValue("其他物料库存确认时间");
            row1.CreateCell(15).SetCellValue("箱体库存确认时间");
            row1.CreateCell(16).SetCellValue("其他物料到齐时间");
            row1.CreateCell(17).SetCellValue("箱体到齐时间");
            row1.CreateCell(18).SetCellValue("承若交期");
            if (list != null)//数据不为空
            {
                for (int i = 0; i < list.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(list[i].DD_Bianhao);
                    rowtemp.CreateCell(1).SetCellValue(list[i].KHname);
                    rowtemp.CreateCell(2).SetCellValue(leixingfanhui(list[i].DD_Type.ToString(), list[i].DD_WLWtype.ToString()));
                    rowtemp.CreateCell(3).SetCellValue(list[i].DD_DHType);
                    rowtemp.CreateCell(4).SetCellValue(list[i].POWER + "/" + list[i].dw);
                    rowtemp.CreateCell(5).SetCellValue(list[i].gnjsstr);
                    rowtemp.CreateCell(6).SetCellValue(GetcusinfobyId(list[i].C_name));
                    rowtemp.CreateCell(7).SetCellValue(GetgcsbyId(list[i].Gcs_nameId));
                    rowtemp.CreateCell(8).SetCellValue(Getzt(list[i].DD_ZT.ToString(), list[i].xtIsq.ToString(), list[i].qtIsq.ToString(), list[i].Bnote2, list[i].Bnote1));
                    rowtemp.CreateCell(9).SetCellValue(list[i].C_time.ToString());
                    rowtemp.CreateCell(10).SetCellValue(list[i].Ysdatetime.ToString());
                    rowtemp.CreateCell(11).SetCellValue(list[i].REMARK.ToString());
                    rowtemp.CreateCell(12).SetCellValue(list[i].NUM.ToString());
                    rowtemp.CreateCell(13).SetCellValue(list[i].Gscwctime.ToString());
                    rowtemp.CreateCell(14).SetCellValue(list[i].qtqrtime.ToString());
                    rowtemp.CreateCell(15).SetCellValue(list[i].xtqrtime.ToString());
                    rowtemp.CreateCell(16).SetCellValue(list[i].qtdhtime.ToString());
                    rowtemp.CreateCell(17).SetCellValue(list[i].xtdhtime.ToString());
                    rowtemp.CreateCell(18).SetCellValue(list[i].YJJQtime.ToString());
                }
            }
            string DataNew = DateTime.Now.ToString("yyyyMMdd");
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", DataNew + "订单数据.xls");

        }

        //类型返回
        public string leixingfanhui(string type, string isft)
        {
            string str = "";
            DKX_DDtypeinfoView tylemodel = _IDKX_DDtypeinfoDao.Getdkxtypebytype(type);
            if (tylemodel != null)
            {
                str = tylemodel.Name;
            }
            if (type == "2")
            {
                if (isft == "0")
                {
                    str = str+"/一体";
                }
                else
                {
                    str =str+ "/分体";
                }
            }
            return str;
        }

        //查询用户
        public string GetcusinfobyId(string Id)
        {
            try
            {
                SysPersonView model = _ISysPersonDao.NGetModelById(Id);
                return model.UserName;
            }
            catch
            {
                return "-";
            }

        }
        //查询工程师
        public string GetgcsbyId(string gcsId)
        {
            try
            {
                DKX_GCSinfoView model = _IDKX_GCSinfoDao.NGetModelById(gcsId);
                return model.Name;
            }
            catch
            {
                return "-";
            }
        }

        //返回进度
        public string Getzt(string zt, string xtIsq, string qtIsq, string Bnote2, string Bnote1)
        {
            string str = "";
            if (zt == "-1")
            {
                str = "未提交";
            }
            if (zt == "0")
            {
                str = "已提交";
            }
            if (zt == "1")
            {
                str = "待制图";
            }
            if (zt == "2")
            {
                str = "制图中";
            }
            if (zt == "-2")
            {
                if (Bnote2 == "2")
                {
                    str = "待审核";
                }
                if (Bnote2 == "3")
                {
                    str = "资料异常";
                }
                if (Bnote1 == "2")
                {
                    str = "待审核";
                }
                if (Bnote1 == "3")
                {
                    str = "资料异常";
                }
                str = "待审核";
            }
            if (zt == "3")
            {
                str = "待发料";
            }
            if (zt == "4")
            {
                str = "可生产";
            }
            if (zt == "5")
            {
                string xtstr = "未确定";
                string qtstr = "未确定";
                if (xtIsq == "1")
                {
                    xtstr = "箱体缺";
                }
                if (xtIsq == "2")
                {
                    xtstr = "箱体齐";
                }
                if (qtIsq == "1")
                {
                    qtstr = "其他缺";
                }
                if (qtIsq == "2")
                {
                    qtstr = "其他齐";
                }
                str = "缺料("+xtstr+"/"+qtstr+")";
            }
            if (zt == "6")
            {
                str = "生产中";
            }
            if (zt == "7")
            {
                str = "待发货";
            }
            if (zt == "8")
            {
                str = "完成";
            }
            if (zt == "9")
            {
                str = "缺料";
            }
            if (zt == "-3")
            {
                str = "待品审";
            }
            return str;
        }

        #endregion

        #region //对外数据接口

        #region //根据SID查询经销商名称
        public string GetDistributorNamebySIDInterface(string SID)
        {
            sermodel remodel = new sermodel();
            string json = "";
            try
            {
                WL_DkxInfoView sidmodel = _IWL_DkxInfoDao.GetDkxinfo(SID);
                if (sidmodel == null)
                {
                    remodel.code = "1";//不存在
                    remodel.jxsname = "";
                    remodel.areaname="";
                }
                else
                {
                    if (sidmodel.Is_xs == 1)//已经销售
                    {
                        NACustomerinfoView cusmodel = _INACustomerinfoDao.NGetModelById(sidmodel.Xs_jxsId);
                        if (cusmodel != null)
                        {
                            if (cusmodel.qyId != "" && cusmodel.qyId != null)
                            {
                                NA_QyinfoView qymodel = _INA_QyinfoDao.NGetModelById(cusmodel.qyId);
                                if (qymodel != null)
                                {
                                    remodel.areaname = qymodel.Qyname;
                                }
                            }
                            else//没有
                            {
                               // remodel.areaname = "";
                                if (cusmodel.DsUid != "" && cusmodel.DsUid != null)
                                {
                                    sfdjsmodel sfdjsmodel = GetdsUserinfobyUId(Convert.ToInt32(cusmodel.DsUid));
                                    if (sfdjsmodel != null)
                                    {
                                        remodel.areaname = sfdjsmodel.Province;
                                        //更新用户区域信息
                                        UpdateCusqyinfobyIdandsfanddjs(cusmodel.Id, sfdjsmodel.Province, sfdjsmodel.City);
                                    }
                                }
                                remodel.areaname = "";
                            }
                            remodel.code = "0";
                            remodel.jxsname = cusmodel.Name;
                        }
                        else
                        {
                            remodel.code = "4";
                            remodel.jxsname = "";
                            remodel.areaname="";
                        }
                    }
                    else
                    {
                        remodel.code = "2";
                        remodel.jxsname = "";
                        remodel.areaname="";
                    }
                }
            }
            catch
            {
                remodel.code = "3";
                remodel.jxsname = "";
                  remodel.areaname="";
            }
            json = JsonConvert.SerializeObject(remodel);
            return json;
        }

        //更新平台的用户区域信息
        public void UpdateCusqyinfobyIdandsfanddjs(string Id, string sf, string djs)
        {
            try
            {
                NACustomerinfoView cusmodel = _INACustomerinfoDao.NGetModelById(Id);
                string sfId = _INA_QyinfoDao.GetqyinfoIdbyname(sf);
                string djsId = _INA_QyinfoDao.GetqyinfoIdbyname(djs);
                cusmodel.qyId = sfId;
                cusmodel.qyCId = djsId;
                _INACustomerinfoDao.NUpdate(cusmodel);
            }
            catch
            {
 
            }
        }
        public class sermodel
        {
            //查询结果 0 调用正常 1 SID不存在 2  未售 3 调用异常 4 经销商数据异常
            public virtual string code { get; set; }

            public virtual string jxsname { get; set; }

            public virtual string areaname { get; set; }
        }
        
        #endregion

        #region //通过经销商名称查询全部的SID
        public string GetSIDINFObyDistributorName(string Name)
        {
            List<SIDmodel> modellist = new List<SIDmodel>();
            string json = "";
            try
            {
                NACustomerinfoView cusmodel = _INACustomerinfoDao.GetcusinfobykhnameandIsds(Name);
                if (cusmodel != null)
                {
                    IList<WL_DkxInfoView> dkxmodellist = _IWL_DkxInfoDao.GetSIDlistbyjxsId(cusmodel.Id);
                    if (dkxmodellist != null)
                    {
                        foreach (var item in dkxmodellist)
                        {
                            SIDmodel sm=new SIDmodel();
                            sm.SID = item.WL_SSD;
                            modellist.Add(sm);
                        }
                        json = JsonConvert.SerializeObject(modellist);
                    }
                }
            }
            catch
            {
              
            }
            return json;
        }

        public class SIDmodel
        {
            public virtual string SID { get; set; }
        }
        #endregion
        #endregion

        #region //
        public sfdjsmodel GetdsUserinfobyUId(int Usid)
        {
           
            Newasia.XYOffer model = new Newasia.XYOffer();
            DataTable dt = model.GetUserProvinceCityByUserId(Usid);
          //  DataTable dt = model.GetUserProvinceCity();
            sfdjsmodel sfmodel = new sfdjsmodel();
            for (int a = 0, b = dt.Rows.Count; a < b; a++)
            {
                string Province = dt.Rows[a]["Province"].ToString();//省份
                string City = dt.Rows[a]["City"].ToString();//地级市
                sfmodel.Province = Province;
                sfmodel.City = City;
            }
            return sfmodel;
        }
        public class sfdjsmodel
        {
            public virtual string Province { get; set; }
            public virtual string City { get; set; }
        }
        #endregion


        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public string GetIP()
        {
            try
            {
                HttpRequest request = System.Web.HttpContext.Current.Request;
                //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
                string userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(userHostAddress))
                    userHostAddress = userHostAddress.ToString().Split(',')[0].Trim();
                //否则直接读取REMOTE_ADDR获取客户端IP地址
                else if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
                if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
                {
                    return userHostAddress;
                }

                return "127.0.0.1";
            }

            catch
            {
                return "127.0.0.1";
            }

        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }


        #region //测试快递单同步的数据
        public void kdinfotset(string Id)
        {
            EP_jlinfoView model = _IEP_jlinfoDao.NGetModelById(Id);
            ZTOHelper.Main(model);
            
        }
        #endregion

        #region //中通快递
        public void Getwaybillnumber()
        {
          
            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("已经进来");
            //接收从微信后台POST过来的数据
            //System.IO.Stream s = Request.InputStream;
            StreamReader reader = new StreamReader(Request.InputStream);
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("值1" + Request.InputStream);
            string ret = reader.ReadToEnd();
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("值2" + ret);
            String[] arr = ret.Split('&');
            String[] item1 = arr[0].Split('=');
            String[] item2 = arr[1].Split('=');
            String[] item3 = arr[2].Split('=');
            string orderCode = item2[1];

            string billNo = item3[1];
            string expressCompany = item1[1];
            log4net.LogManager.GetLogger("ApplicationInfoLog").Error("值3" + orderCode);
            EP_jlinfoView epmodel = _IEP_jlinfoDao.NGetModelById(orderCode);
            if (epmodel != null)
            {

                    epmodel.Kd_no = billNo;
                    epmodel.fhsmg = ret;
                    epmodel.RL_Is = 1;
                    epmodel.RL_Is = 1;//0 未录单 1 未收货 2 已签收
                    epmodel.RL_datetime = DateTime.Now;//录单时间
                    _IEP_jlinfoDao.NUpdate(epmodel);
            }
        } 

        public class ZTdata  {
            public virtual string expressCompany { get; set; }

            public virtual string billNo { get; set; }

            public virtual string orderCode { get; set; }
    }
        #endregion

        public string teststr()
        {
            string str = "expressCompany=%E4%B8%AD%E9%80%9A%E5%BF%AB%E9%80%92&orderCode=d37d27029e154f9eb8692f325f987147&billNo=73113773924452";
            String[] arr = str.Split('&');
            String[] item1 = arr[2].Split('=');

            return item1[1];
            //K3Helper.InsertBianhaoandengineer("DKX123211", "");
            //return "";
        }


        #region //PDA扫码页面
        #region //仓库扫码
        #region //登录页面
        /// <summary>
        /// pad 的登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PDAlogin()
        {
            return View();
        }

      
        public JsonResult PDAloginE(string userId, string passwd,string cztype)
        {
            try
            {
                //判断是否是ajax请求，如果不是的话直接返回登录页面
                if (!Request.IsAjaxRequest())
                {
                    Response.Redirect("./publicAPI/PDAlogin");
                    return Json(new { result = "error", msg = "不是ajax！" });
                }
                if (!"".Equals(userId) && !"".Equals(passwd))
                {
                    if (!"".Equals(cztype))
                    {
                        SysPerson model = _ISysPersonDao.GetModelByLogin(userId);
                        if (model != null)//存在
                        {
                            if (NAHelper.MD5Encrypt(model.Password, new UTF8Encoding()) == passwd)
                            {
                                Session[SessionHelper.PDAUser] = _ISysPersonDao.GetSessionuser(userId);
                                return Json(new { result = "success", msg = "登录成功" }); ;
                            }
                            else
                            {
                                return Json(new { result = "error", msg = "账号或密码不对！" });
                            }
                        }
                        else
                        {
                            return Json(new { result = "error", msg = "账号不存！" });
                        }
                    }
                    else
                    {
                        return Json(new { result = "error", msg = "请选择操作页面！" });
                    }
                }
                else {
                    return Json(new { result = "error", msg = "账号密码不为空！" });
                }
               
            }
            catch
            {
                return Json(new { result = "error",msg="登录异常,请重新登录！" });
            }
        }
        #endregion

        #region //仓库发料的扫码的页面
        public ActionResult PDAWarehouse_FLView()
        {
            return View();
            //SessionUser user = Session[SessionHelper.PDAUser] as SessionUser;
            //if (user != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return View("../publicAPI/PDAlogin");
            //}

        }
        #endregion

        #region //仓库发料的扫码详情页面
        public ActionResult PDAWarehouse_FLinfoView(string orderno)
        {
            //SessionUser user = Session[SessionHelper.PDAUser] as SessionUser;
            //if (user != null)
            //{
                DKX_DDinfoView model = new DKX_DDinfoView();
                model = _IDKX_DDinfoDao.GetDDmodelbyorderno(orderno);
                //ViewData["name"] = user.RName;
                //ViewData["Uname"] = user.UserName;
                return View(model);
            //}
            //else
            //{
            //    return View("../publicAPI/PDAlogin");
            //}
        }
        #endregion

        #region //物料确认提交
        /// <summary>
        /// 物料确认提交
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <param name="type">物料类型 0 箱体 1其他物料</param>
        /// <param name="zt">当前状态 0 未确认 1 缺 2 齐</param>
        /// <returns></returns>
        public string WLQRTJEide(string Id, string type, string zt)
        {
            SessionUser Suser = Session[SessionHelper.PDAUser] as SessionUser;
            DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            if (model != null)
            {
                //只有在未发料的状态下可以出来
                if (model.DD_ZT == 3)
                {
                    //箱体处理
                    if (type == "0")
                    {
                        if (zt == "1")//缺料
                        {
                            model.xtIsq = 1;//箱体缺
                            model.xtqrtime = DateTime.Now;//相同库存确认时间
                            //model.DD_ZT = 5;//订单缺料
                            model.DD_ZT = 3;//未发料状态
                            model.Flzt = 10;//缺料
                            NAHelper.Insertczjl(Id, "箱体库存-缺", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "4");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "4");
                            return "0";//提交成功
                        }
                        if (zt == "2")//料齐
                        {
                            model.xtIsq = 2;//箱体齐
                            model.xtdhtime = DateTime.Now;
                            if (model.qtIsq == 2)//如果其他料也齐 就变成 可生产
                            {
                                //model.DD_ZT = 4;//可生产
                                model.Flzt = 5;//待发料状态
                                model.wlsqtime = DateTime.Now;//物料双齐的时间
                            }
                            else//如果其他物料未确认或者缺料怎么变成 缺料
                            {
                                // model.DD_ZT = 5;//订单缺料
                            }
                            NAHelper.Insertczjl(Id, "箱体库存-齐", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "7");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "7");
                            return "0";//提交成功
                        }
                        return "1";//提交异常
                    }
                    if (type == "1")//其他物料确认
                    {
                        if (zt == "1")//缺料
                        {
                            model.qtIsq = 1;//其他物料缺
                            model.qtqrtime = DateTime.Now;//确认时间
                            model.DD_ZT = 3;//未发料状态
                            model.Flzt = 10;//缺料
                            NAHelper.Insertczjl(Id, "其他物料库存-缺", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "4");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "4");
                            return "0";//提交成功
                        }
                        if (zt == "2")
                        {
                            model.qtIsq = 2;//其他物料齐
                            model.qtdhtime = DateTime.Now;
                            if (model.xtIsq == 2)//如果相同不缺 就变成 可生产
                            {
                                //model.DD_ZT = 4;
                                model.Flzt = 5;//待发料状态
                                model.wlsqtime = DateTime.Now;//物料双齐的时间
                            }
                            //else {
                            //    model.DD_ZT = 5;
                            //}
                            NAHelper.Insertczjl(Id, "其他物料库存-齐", Suser.Id);
                            _IDKX_DDinfoDao.NUpdate(model);
                            IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                            if (list != null)
                            {
                                for (int i = 0, j = list.Count; i < j; i++)
                                {
                                    MassManager.FMB_FBDKXNotice(list[i].UserId, Id, "7");
                                }
                            }
                            //通知下单客服
                            MassManager.FMB_FBDKXNotice(model.C_name, Id, "7");
                            return "0";//提交成功
                        }
                        return "1";//提交异常
                    }
                    return "4";//处理类型参数缺少
                }
                else
                {
                    return "2";//当前状态不可以处理
                }
            }
            else
            {
                return "3";//订单为空
            }
        }
        #endregion

        #region //发料确认
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderno">订单号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FLconfirmEide(string orderno)
        {
            //SessionUser Suser = Session[SessionHelper.PDAUser] as SessionUser;
            //DKX_DDinfoView model = _IDKX_DDinfoDao.NGetModelById(Id);
            DKX_DDinfoView model = new DKX_DDinfoView();
            model = _IDKX_DDinfoDao.GetDDmodelbyorderno(orderno);
            if (model != null)
            {
                if (model.DD_ZT == 3)//只有在未发料的状态下可以确认完成发料
                {
                    if (model.xtIsq != 0 && model.qtIsq != 0)
                    {
                        if (model.Flzt == 5)
                        {//待发料的情况
                            model.Flzt = 15;//完成发料
                            NAHelper.Insertczjl(model.Id, "完成发料", null);
                        }
                        if (model.Flzt == 10)
                        {//缺料的情况下
                            model.Flzt = 20;//部分发料
                            NAHelper.Insertczjl(model.Id, "部分发料", null);
                        }
                        model.Flwxtime = DateTime.Now;//完成发料的时间-可以生产的时间
                        model.DD_ZT = 4;//可以生产
                      
                        _IDKX_DDinfoDao.NUpdate(model);
                      //  gwjHelper.synchronizationorderandzl(model.Id, model.DD_Bianhao, model.KHname, model.NUM.ToString(), model.KBomNo);//同步工位机平板的订单资料
                        IList<Wx_FTUserbdopenIdinfoView> list = _IWx_FTUserbdopenIdinfoDao.GetwxftbmopenIdbybmtype("7");
                        if (list != null)
                        {
                            for (int i = 0, j = list.Count; i < j; i++)
                            {
                                MassManager.FMB_FBDKXNotice(list[i].UserId, model.Id, "16");
                            }
                        }
                        return Json(new { resule = "success", msg = "操作成功。" });
                    }
                    else
                    {
                        return Json(new { resule = "error", msg = "尚未确认物料是否齐全,无法去发料操作。" });
                    }

                }
                else
                {
                    return Json(new { resule = "error", msg = "当前状态下无法进行该操作。" });
                }
            }
            else
            {
                return Json(new { resule = "error", msg = "该订单不存在。" });
            }
        }
        #endregion
        #region //查询当日发料完成的数量
        [HttpPost]
        public string GetTodayFLWCOordersum()
        {
            int smflcount = _IDKX_DDinfoDao.GetTodayFLWCordercunot();
            return "{\"status\":\"" + smflcount + "\"}";//添加成功
        }
        #endregion
        #endregion
        #endregion


        #region //K3接口测试

        public string K3test()
        {
            ICMOSysmodel model = new ICMOSysmodel();
            model.FCustName = "重庆长江";
            model.FNumber = "05.013.0009";
            model.FQty = 1;
            model.FDeptNumber = "001.07";
            model.FBOMNumber = "210402  冷库/0210D03：PLC做风冷一库双机，压机10.5KW,电化霜，温湿度传感器客户提供，NAK162PLC1.0";
            model.FBatchNo = "DKX20210402测试";
            model.FPlanCommitDate = DateTime.Now;
            model.FPlanFinishDate = DateTime.Now;
            string ss = K3Helper.InsertICMOSys(model);
            return ss;
        }
        #endregion
    }
}
