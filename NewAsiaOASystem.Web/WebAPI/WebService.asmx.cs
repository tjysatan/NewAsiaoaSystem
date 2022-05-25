using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace NewAsiaOASystem.Web.WebAPI
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World2";
        }

        [WebMethod]
        public string  GetTestDataTable() {
           INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
           IList<NACustomerinfoView> lismode=_INACustomerinfoDao.NGetList();
           string json;
           json = JsonConvert.SerializeObject(lismode);
           return json;
        }

        #region //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetDkxbyUIdjson(string uid, int SetFrist, int SetMax)
        {
            IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
            IList<WL_DkxInfoView> lismode = _IWL_DkxInfoDao.GetSIDbyKhId(uid,SetFrist,SetMax);
            string json;
            json = JsonConvert.SerializeObject(lismode);
            return json;
        }

        [WebMethod]
        public int GetCountDkxbyUid(string uid)
        {
           IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
            int n = 0;
            n = _IWL_DkxInfoDao.GetcountdkxbyUId(uid);
            return n;
        }
        #endregion

        #region //电商平台快递单打印记录接口
        [WebMethod]
        public string InsEPDsModel(string DdId, string Jjname, string kdgs)
        {
            INA_XSinfoDao _INA_XSinfoDao = ContextRegistry.GetContext().GetObject("NA_XSinfoDao") as INA_XSinfoDao;
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            IEP_jlinfoDao _IEP_jlinfoDao = ContextRegistry.GetContext().GetObject("EP_jlinfoDao") as IEP_jlinfoDao;

            EP_jlinfoView model = new EP_jlinfoView();
            string json;
            //先更新电商平台订单
            Addorder();
            NA_XSinfoView XSmodel = _INA_XSinfoDao.GetxsInfobySort(DdId);
            model.SjId = XSmodel.KhId;//购货单位（个人）
            model.SjaddId = XSmodel.AddresseeId;//收货人
            model.JjId = _ISysPersonDao.GetUesrIdbyUserName(Jjname);//寄件人
           
            model.Jjdatetime = DateTime.Now;//打印时间
            model.RL_Is = 0;//状态  0未录入 1未签收 2 已签收
            if (kdgs == "天地华宇")//天地华宇 5天后到
            {
                model.Kdgs = "tdhy";//快递公司
                //model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(5).ToShortDateString());//预计到货时间
            }
            else if (kdgs == "德邦快递")//德邦快递 预计3天后到
            {
                model.Kdgs = "db";//快递公司
                //model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(3).ToShortDateString());//预计到货时间
            }
            else if (kdgs == "盛辉物流")//盛辉 预计3天
            {
                model.Kdgs = "sh";//快递公司
                //model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(3).ToShortDateString());//预计到货时间
            }
            else if (kdgs == "中通快递")//中通 和 顺风 隔天到
            {
                model.Kdgs = "zt";//快递公司
                //model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());//预计到货时间
            }
            else if (kdgs == "顺丰速运")
            {
                model.Kdgs = "sf";//快递公司
                //model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());//预计到货时间
            }
            else if (kdgs == "远成快运")
            {
                model.Kdgs = "ycky";//快递公司
            }
            else if (kdgs == "中通快运")
            {
                model.Kdgs = "ztky";
            }
            else if (kdgs == "德邦物流")//德邦快递 预计3天后到
            {
                model.Kdgs = "dbwl";
            }
            model.Source = 1;//记录来源
            model.IsSend = 0;//未发送 发货通知
            model.DDId = Convert.ToInt32(DdId);
            if (_IEP_jlinfoDao.Ninsert(model))
            {
                json = "1";
            }
            else
            {
                json = "0";
            }
            return json;
        }

        //更新电商平台订单
        #region //更新电商平台的订单信息
        public void Addorder()
        {
            INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
            INA_XSinfoDao _INA_XSinfoDao = ContextRegistry.GetContext().GetObject("NA_XSinfoDao") as INA_XSinfoDao;
            INA_XSdetailsinfoDao _INA_XSdetailsinfoDao = ContextRegistry.GetContext().GetObject("NA_XSdetailsinfoDao") as INA_XSdetailsinfoDao;
            INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
            INA_AddresseeInfoDao _INA_AddresseeInfoDao = ContextRegistry.GetContext().GetObject("NA_AddresseeInfoDao") as INA_AddresseeInfoDao;
            Newasia.XYOffer model = new Newasia.XYOffer();
            NA_XSinfoView OrdermodelNewdate = _INA_XSinfoDao.GetxsinfoNewdate();
            int t = 0;
            if (OrdermodelNewdate != null)
            {
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
                    Ordermodel.PaymentTypeName = PaymentTypeName;
                    NACustomerinfoView Custmodel = new NACustomerinfoView();//实例化 
                  
                    Custmodel = _INACustomerinfoDao.GetCustomerbyUId(userid);
                    if (Custmodel != null)//存在该客户信息
                    {
                        NA_AddresseeInfoView addmodel = new NA_AddresseeInfoView();//收件人实例化
                        addmodel = _INA_AddresseeInfoDao.GetaddresseeinfobyAnametel(ShipName, ShipCellPhone,ShipRegion+ShipAddress);//通过收件人和收件电话查找收件人信息
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
                            addmodel.Address = ShipRegion + ShipAddress;//收件地址
                            addmodel.qyo = Province;//省
                            addmodel.qye = City;//地级市
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
                        Custmodel.Isds = 1;//是电商同步过来的数据
                        Custmodel.Status = 1;//启用
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
                    Ordermodel.Sort = Convert.ToInt32(Sort);//排序
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
                        string PRiD = _INQ_productinfoDao.InsertID(SPmodel);
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
                    _INA_XSdetailsinfoDao.Ninsert(Ordermxmodel);
                }
            }
        }
        #endregion 


        #endregion

        //电商平台新产品添加
        [WebMethod]
        public void InsertProduct(string Pname, string P_bianhao)
        {
            INQ_productinfoDao _INQ_productinfoDao = ContextRegistry.GetContext().GetObject("NQ_productinfoDao") as INQ_productinfoDao;
            NQ_productinfoView model = new NQ_productinfoView();
            model = _INQ_productinfoDao.Getproductbyfnumber(P_bianhao);
            if (model == null)
            {
                model = new NQ_productinfoView();
                int index = Pname.IndexOf("物联网");
                model.Pname = Pname;//产品名称
                model.P_bianhao = P_bianhao;//K3 物料代码
                model.CreateTime = DateTime.Now;//创建时间
                if (index > -1)
                {
                    model.SMFH = 1;//需要扫码
                }
                else
                {
                    model.SMFH = 0;//不需要扫码
                }
                model.p_type = 0;//产品类型默认电控箱
                _INQ_productinfoDao.Ninsert(model);
            }
        }

        /// <summary>
        /// 给电商平台调用的，添加快递单号，回填办公平台的接口
        /// </summary>
        /// <param name="Kd_no">快递单号</param>
        /// <param name="DDId">订单单号</param>
        public void UpdateCouriernumber(string Kd_no, string DDId) 
        {
             IEP_jlinfoDao _IEP_jlinfoDao = ContextRegistry.GetContext().GetObject("EP_jlinfoDao") as IEP_jlinfoDao;
             EP_jlinfoView model = _IEP_jlinfoDao.GetEP_jlmodelbydd_Id(DDId);
             if (model != null) {
                 model.Kd_no = Kd_no;//快递单号
                 model.RL_datetime = DateTime.Now;//入单时间
                 if (model.Kdgs == "tdhy")//天地华宇 5天后到
                 {
                     model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(5).ToShortDateString());//预计到货时间
                 }
                 else if (  model.Kdgs == "db")//德邦快递 预计3天后到
                 {
                     model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(3).ToShortDateString());//预计到货时间
                 }
                 else if ( model.Kdgs == "sh")//盛辉 预计3天
                 {
                     model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(3).ToShortDateString());//预计到货时间
                 }
                 else if (model.Kdgs == "zt")//中通 和 顺风 隔天到
                 {
                     model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());//预计到货时间
                 }
                 else if ( model.Kdgs == "sf")
                 {
                     model.DHyjdatetime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());//预计到货时间
                 }
                 _IEP_jlinfoDao.NUpdate(model);
             }
        }


        #region //远程调用 通过sid查找经销商信息
        [WebMethod]
        public string GetJxsjsonbysid(string sid)
        {
            IWL_DkxInfoDao _IWL_DkxInfoDao = ContextRegistry.GetContext().GetObject("WL_DkxInfoDao") as IWL_DkxInfoDao;
            INACustomerinfoDao _INACustomerinfoDao = ContextRegistry.GetContext().GetObject("NACustomerinfoDao") as INACustomerinfoDao;
            WL_DkxInfoView dkxmodel = _IWL_DkxInfoDao.GetDkxinfobySID(sid);
            string json=null;
            if (dkxmodel != null)
            {
                if (dkxmodel.Xs_jxsId != "" || dkxmodel.Xs_jxsId != null)
                {
                    NACustomerinfoView cusmodel = _INACustomerinfoDao.GetcusInfobyId(dkxmodel.Xs_jxsId);
                    if (cusmodel != null)
                    {
                        Distributor model = new Distributor();
                        model.Name = cusmodel.Name;
                        model.Contacts = cusmodel.LxrName;
                        model.Tel = cusmodel.Tel;
                        model.address = cusmodel.Adress;
                        json = JsonConvert.SerializeObject(model);
                    }
                }
            }
            return json;
        }

        //返回经销商需要的资料
        public class Distributor
        {
            public  string Name;

            public  string Contacts;

            public  string Tel;

            public  string address;
        }
        #endregion

        #region //给报价系统使用的数据接口
        #region //ERP中及时库存数据
        [WebMethod]
        public string Get_ERP_Materiainventory(string code, string key)
        {
            try
            {
                if (key != "00BE974F-C52D-434D-AB99-4D9E3796CD81-2")
                    return null;
                string resjson = zypushsoftHelper.GetBCStkbyItmID(code);
                return resjson;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #endregion


    }
}
