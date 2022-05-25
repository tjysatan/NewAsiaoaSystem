using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.IO;

namespace NewAsiaOASystem.Web.Controllers
{
    [ApiAuthorize]
    public class MSController : ApiController
    {
        IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
        IYCAccountbindingInfoDao _IYCAccountbindingInfoDao = ContextRegistry.GetContext().GetObject("YCAccountbindingInfoDao") as IYCAccountbindingInfoDao;
        IDKX_ZLDataInfoDao _IDKX_ZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_ZLDataInfoDao") as IDKX_ZLDataInfoDao;
        //ERP数据汇总
        IERP_SASalAinfoDao _IERP_SASalAinfoDao = ContextRegistry.GetContext().GetObject("ERP_SASalAinfoDao") as IERP_SASalAinfoDao;
        // GET api/ms
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/ms/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/ms
        public void Post([FromBody] string value)
        {
        }

        // PUT api/ms/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/ms/5
        public void Delete(int id)
        {
        }

        #region //修改工序的接口
        [HttpPost]
        public  HttpResponseMessage Update_Process_statusAsync([FromBody] Process_status data)
        {
            Result res = new Result();
            if (!string.IsNullOrEmpty(data.orderno))
            {
                //查询订单
                DKX_DDinfoView ordermodel = _IDKX_DDinfoDao.GetDDmodelbyorderno(data.orderno);
                if (ordermodel != null)
                {
                    if (ordermodel.DD_ZT == 6)
                    {
                        ordermodel.sczt = data.P_status.ToString();

                        switch (data.P_status)
                        {
                            case "底板装配":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx1starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx1endtime = DateTime.Now;
                                    }
                                }
                                break;
                            case "控制线配线":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx2starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx2endtime = DateTime.Now;
                                    }
                                }
                                break;
                            case "主回路配线":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx3starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx3endtime = DateTime.Now;
                                    }
                                }
                                break;
                            case "面板装箱":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx4starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx4endtime = DateTime.Now;
                                    }
                                }
                                break;
                            case "底板装箱":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx5starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx5endtime = DateTime.Now;
                                    }
                                }
                                break;
                            case "面板接线":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx6starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx6endtime = DateTime.Now;
                                    }
                                }
                                break;
                            case "调试":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx7starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx7endtime = DateTime.Now;
                                    }
                                }
                                break;
                            case "验收":
                                {

                                    if (data.type == 0)
                                    {
                                        ordermodel.scgx8starttime = DateTime.Now;
                                    }
                                    if (data.type == 1)
                                    {
                                        ordermodel.scgx8endtime = DateTime.Now;
                                    }
                                }
                                break;

                        }
                        if (_IDKX_DDinfoDao.NUpdate(ordermodel))
                        {
                            res.code = "1";
                            res.message = "success";
                        }
                        else
                        {
                            res.code = "0";
                            res.message = "更新异常";
                        }

                    }
                    else
                    {
                        res.code = "0";
                        res.message = "订单状态不是生产中";
                    }
                }
                else
                {
                    res.code = "0";
                    res.message = "订单号不存在";
                }
            }
            else
            {
                res.code = "0";
                res.message = "订单号不为空";
            }
            res.data = "{}";
            var jsonStr = JsonConvert.SerializeObject(res);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "text/json")
            };
            return result;

        }

        public class Process_status{
            public virtual string orderno { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public virtual string P_status { get; set; }

            /// <summary>
            /// 0 开工  1 报工
            /// </summary>
            public virtual int? type { get; set; }
             
            public virtual DateTime? C_Time { get; set; }
        }

        //public enum P_status
        //{
        //    等待开始 = 0,
        //    底板装配 = 5,
        //    控制线配线 = 10,
        //    主回路配线 = 15,
        //    面板装箱 = 20,
        //    底板装箱 = 25,
        //    面板接线 = 30,
        //    调试 = 35,
        //    验收 = 40,
        //}

        #endregion

        #region //下达 
        [HttpPost]
        public HttpResponseMessage Update_order_statusAsync([FromBody] DKX_DDinfoView model)
        {
            Result res = new Result();
            if (!string.IsNullOrEmpty(model.DD_Bianhao))
            {
                //查询订单
                DKX_DDinfoView ordermodel = _IDKX_DDinfoDao.GetDDmodelbyorderno(model.DD_Bianhao);
                if (ordermodel != null)
                {
                    if (ordermodel.DD_ZT == 4)//大于未发料
                    {
                        if (model.DD_ZT == 6)
                        {
                            ordermodel.DD_ZT = 6;//生产中
                            model.Scjdtime = DateTime.Now;
                            if (_IDKX_DDinfoDao.NUpdate(ordermodel))
                            {
                                res.code = "1";
                                res.message = "success";
                            }
                            else
                            {
                                res.code = "0";
                                res.message = "更新异常";
                            }
                            NAHelper.Insertczjl(ordermodel.Id, "下达工单-生产中", "");
                        }
                        else
                        {
                            res.code = "0";
                            res.message = "订单状态参数传递异常";
                        }
                    }
                    else
                    {
                        res.code = "0";
                        res.message = "订单状态状态不允许开工";
                    }
                }
                else
                {
                    res.code = "0";
                    res.message = "订单号不存在";
                }

            }
            else
            {
                res.code = "0";
                res.message = "订单号不为空";
            }
            res.data = "{}";
            var jsonStr = JsonConvert.SerializeObject(res);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "text/json")
            };
            return result;
        }

        #endregion

        #region //调式完成
        [HttpPost]
        public HttpResponseMessage Upload_Debugimg([FromBody] DBdebugimg data)
        {
            Result res = new Result();
            if (!string.IsNullOrEmpty(data.orderno))
            {
                //查询订单
                DKX_DDinfoView ordermodel = _IDKX_DDinfoDao.GetDDmodelbyorderno(data.orderno);
                if (ordermodel != null)
                {
                    string[] debugimg = data.debug_img;
                    if(debugimg.Length!=0)
                    {
                    //查询是否有调试单的图片
                    IList<DKX_ZLDataInfoView> DSDModellist = _IDKX_ZLDataInfoDao.GetzljsonbyId(ordermodel.Id,"4");
                    if (DSDModellist != null)
                    {
                        _IDKX_ZLDataInfoDao.NDelete(DSDModellist as List<DKX_ZLDataInfoView>);//删除
                    }
                    }
                    //string[] debugimg = data.debug_img;
                    for (int i = 0,j= debugimg.Length; i < j; i++)
                    {
                        int s = i + 1;
                        DKX_ZLDataInfoView DSDmodel = new DKX_ZLDataInfoView();
                        DSDmodel.url = debugimg[i];
                        DSDmodel.wjName = "图片"+s;
                        DSDmodel.Zl_type = 4;//需求
                        DSDmodel.dd_Id = ordermodel.Id;
                        DSDmodel.Start = 0;
                        DSDmodel.C_name = "";
                        DSDmodel.C_Datetime = DateTime.Now;
                        DSDmodel.Isgl = 0;//附件
                       _IDKX_ZLDataInfoDao.Ninsert(DSDmodel);
                    }
                    string[] photos = data.photos;
                    if(photos.Length!=0)
                    { 
                    //查询是否有产品图片
                    IList<DKX_ZLDataInfoView> CPTPModellist = _IDKX_ZLDataInfoDao.GetzljsonbyId(ordermodel.Id,"3");
                    if (CPTPModellist != null)
                    {
                        _IDKX_ZLDataInfoDao.NDelete(CPTPModellist as List<DKX_ZLDataInfoView>);//删除
                    }
                    }

                    for (int i = 0, j = photos.Length; i < j; i++)
                    {
                        int s = i + 1;
                        DKX_ZLDataInfoView cptpDmodel = new DKX_ZLDataInfoView();
                        cptpDmodel.url = photos[i];
                        cptpDmodel.wjName = "图片" + s;
                        cptpDmodel.Zl_type = 3;//需求
                        cptpDmodel.dd_Id = ordermodel.Id;
                        cptpDmodel.Start = 0;
                        cptpDmodel.C_name = "";
                        cptpDmodel.C_Datetime = DateTime.Now;
                        cptpDmodel.Isgl = 0;//附件
                        _IDKX_ZLDataInfoDao.Ninsert(cptpDmodel);
                    }
                    //不是生产中的不需要验收完成
                    if (ordermodel.DD_ZT == 6)
                    {
                        ordermodel.DD_ZT = -3;//待品审
                        ordermodel.Ysdatetime = DateTime.Now;
                        ordermodel.UP_datetime = DateTime.Now;
                        ordermodel.pbshzt = 0;//品审状态
                        ordermodel.pbshbtgyy = "";
                        ordermodel.scczname = data.worker;
                        ordermodel.scDSname = data.worker;
                        ordermodel.UP_name = "";//小程序同步
                        _IDKX_DDinfoDao.NUpdate(ordermodel);
                    }
                    res.code = "1";
                    res.message = "success";
                }
                else
                {
                    res.code = "0";
                    res.message = "订单不存在";
                }
            }
            else
            {
                res.code = "0";
                res.message = "订单号不为空";
            }
            res.data = "{}";
            var jsonStr = JsonConvert.SerializeObject(res);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "text/json")
            };
            return result;
        }

        #region //修改验收图片
        public class DBdebugimg
        {
            public virtual string orderno { get; set; }

            public virtual string[] photos { get; set; }

            public virtual string[] debug_img { get; set; }

            public virtual string worker { get;set;}
    }
        #endregion

        #region //查询技术小组的产品数据
        public class select_ProductSales
        {
            public virtual string years { get; set; }
        }
        #endregion


        #endregion

        #region //远程发送告警的接口
        [HttpPost]
        public HttpResponseMessage Send_YCNoticeInterface([FromBody] YCNoticeModel data)
        {
            Result res = new Result();
            //查找帐号
            YCAccountbindingInfoView zhmodel = _IYCAccountbindingInfoDao.GetbdzhinfobyId(data.Id);
            if (zhmodel != null)
            {
                string I = MassManager.YCNotice_MB(zhmodel.openId, data.Username, data.NoticeMsg, data.jkdName, data.SId, data.type);
                if (I == "0")
                {
                    res.code = "1";
                    res.message = "调用成功";
                }
                else
                {
                    res.code = "0";
                    res.message = "模板消息发送失败";
                }
            }
            else
            {
                res.code = "0";
                res.message = "串码不存在";
            }
            var jsonStr = JsonConvert.SerializeObject(res);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "text/json")
            };
            return result;
        }
        #endregion

        #region //各技术小组的每个月的产品销售金额
        [HttpGet]
        public HttpResponseMessage Get_TechnicalGroup_ProductSales([FromBody] select_ProductSales yearsM)
        {
            Result res = new Result();
            if (!string.IsNullOrEmpty(yearsM.years))
            {
                string jsonstr = TechnicalGroup_ProductSales(yearsM.years);
                res.code = "1";
                res.message = "success";
                res.data = JsonConvert.DeserializeObject(jsonstr);
            }
            else
            {
                res.code = "0";
                res.message = "参数不为空";
            }
          
            var jsonStr = JsonConvert.SerializeObject(res);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "application/json")
            };
            return result;
        }

        public string TechnicalGroup_ProductSales(string years)
        {
            try
            {
                decimal hzyjine = 0;//何健、林超、周旸一组
                decimal pwjine = 0;//彭菊、王韧一组
                decimal zzsjine = 0;//张骏、张文杰、孙微一组
                decimal yjsjine = 0;//袁静铖、金志浩、孙中原一组
                decimal lzjjine = 0;//吕正佳一组
                decimal llgjine = 0;//老李工 李凤进
                decimal wgjine = 0;//闻工 闻晓成

                decimal hzyjinew = 0;//何健、林超、周旸一组
                decimal pwjinew = 0;//彭菊、王韧一组
                decimal zzsjinew = 0;//张骏、张文杰、孙微一组
                decimal yjsjinew = 0;//袁静铖、金志浩、孙中原一组
                decimal lzjjinew = 0;//吕正佳一组
                decimal llgjinew = 0;//老李工 李凤进
                decimal wgjinew = 0;//闻工 闻晓成

                //销售成本
                decimal hzyjinecb = 0;//何健、林超、周旸一组
                decimal pwjinecb = 0;//彭菊、王韧一组
                decimal zzsjinecb = 0;//张骏、张文杰、孙微一组
                decimal yjsjinecb = 0;//袁静铖、金志浩、孙中原一组
                decimal lzjjinecb = 0;//吕正佳一组
                decimal llgjinecb = 0;//老李工 李凤进
                decimal wgjinecb = 0;//闻工 闻晓成
                //List<object> list = _IERP_SASalAinfoDao.GetsumjineGROUPBY(AbsID);
                string Jsonstr;
                Jsonstr = JsonConvert.SerializeObject(_IERP_SASalAinfoDao.GetsumjineGROUPBY(years));
                JArray ja = (JArray)JsonConvert.DeserializeObject(Jsonstr);
                if (ja != null)
                {

                    for (int i = 0, j = ja.Count(); i < j; i++)
                    {
                        if (ja[i][0].ToString() == "何健")
                        {
                            hzyjine = hzyjine + Convert.ToDecimal(ja[i][1].ToString());
                            hzyjinew = hzyjinew + Convert.ToDecimal(ja[i][2].ToString());
                            hzyjinecb = hzyjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "林超")
                        {
                            hzyjine = hzyjine + Convert.ToDecimal(ja[i][1].ToString());
                            hzyjinew = hzyjinew + Convert.ToDecimal(ja[i][2].ToString());
                            hzyjinecb = hzyjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "周旸")
                        {
                            hzyjine = hzyjine + Convert.ToDecimal(ja[i][1].ToString());
                            hzyjinew = hzyjinew + Convert.ToDecimal(ja[i][2].ToString());
                            hzyjinecb = hzyjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "彭菊")
                        {
                            pwjine = pwjine + Convert.ToDecimal(ja[i][1].ToString());
                            pwjinew = pwjinew + Convert.ToDecimal(ja[i][2].ToString());
                            pwjinecb = pwjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "王韧")
                        {
                            pwjine = pwjine + Convert.ToDecimal(ja[i][1].ToString());
                            pwjinew = pwjinew + Convert.ToDecimal(ja[i][2].ToString());
                            pwjinecb = pwjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "张骏")
                        {
                            zzsjine = zzsjine + Convert.ToDecimal(ja[i][1].ToString());
                            zzsjinew = zzsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            zzsjinecb = zzsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "张文杰")
                        {
                            zzsjine = zzsjine + Convert.ToDecimal(ja[i][1].ToString());
                            zzsjinew = zzsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            zzsjinecb = zzsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "孙微")
                        {
                            zzsjine = zzsjine + Convert.ToDecimal(ja[i][1].ToString());
                            zzsjinew = zzsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            zzsjinecb = zzsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "袁静铖")
                        {
                            yjsjine = yjsjine + Convert.ToDecimal(ja[i][1].ToString());
                            yjsjinew = yjsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            yjsjinecb = yjsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "金志浩")
                        {
                            yjsjine = yjsjine + Convert.ToDecimal(ja[i][1].ToString());
                            yjsjinew = yjsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            yjsjinecb = yjsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "孙中原")
                        {
                            yjsjine = yjsjine + Convert.ToDecimal(ja[i][1].ToString());
                            yjsjinew = yjsjinew + Convert.ToDecimal(ja[i][2].ToString());
                            yjsjinecb = yjsjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "吕正佳")
                        {
                            lzjjine = lzjjine + Convert.ToDecimal(ja[i][1].ToString());
                            lzjjinew = lzjjinew + Convert.ToDecimal(ja[i][2].ToString());
                            lzjjinecb = lzjjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "柳琪")
                        {
                            lzjjine = lzjjine + Convert.ToDecimal(ja[i][1].ToString());
                            lzjjinew = lzjjinew + Convert.ToDecimal(ja[i][2].ToString());
                            lzjjinecb = lzjjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "张国兴")
                        {
                            lzjjine = lzjjine + Convert.ToDecimal(ja[i][1].ToString());
                            lzjjinew = lzjjinew + Convert.ToDecimal(ja[i][2].ToString());
                            lzjjinecb = lzjjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "李凤进")
                        {
                            llgjine = llgjine + Convert.ToDecimal(ja[i][1].ToString());
                            llgjinew = llgjinew + Convert.ToDecimal(ja[i][2].ToString());
                            llgjinecb = llgjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                        if (ja[i][0].ToString() == "闻晓成")
                        {
                            wgjine = wgjine + Convert.ToDecimal(ja[i][1].ToString());
                            wgjinew = wgjinew + Convert.ToDecimal(ja[i][2].ToString());
                            wgjinecb = wgjinecb + Convert.ToDecimal(ja[i][3].ToString());
                        }
                    }

                    List<object> datelist = new List<object>();

                    jsfzmodel model = new jsfzmodel();
                    model.jsfzname = "何健组";
                    model.hzjine = hzyjine;
                    model.wsjine = hzyjinew;
                    model.xscbjine = hzyjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "彭菊组";
                    model.hzjine = pwjine;
                    model.wsjine = pwjinew;
                    model.xscbjine = pwjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "张骏组";
                    model.hzjine = zzsjine;
                    model.wsjine = zzsjinew;
                    model.xscbjine = zzsjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "袁静铖组";
                    model.hzjine = yjsjine;
                    model.wsjine = yjsjinew;
                    model.xscbjine = yjsjinecb;
                    datelist.Add(model);
                    model = new jsfzmodel();
                    model.jsfzname = "柳琪组";
                    model.hzjine = lzjjine;
                    model.wsjine = lzjjinew;
                    model.xscbjine = lzjjinecb;
                    datelist.Add(model);
                    //model = new jsfzmodel();
                    //model.jsfzname = "李凤进组";
                    //model.hzjine = llgjine;
                    //model.wsjine = llgjinew;
                    //model.xscbjine = llgjinecb;
                    //datelist.Add(model);
                    //model = new jsfzmodel();
                    //model.jsfzname = "闻晓成组";
                    //model.hzjine = wgjine;
                    //model.wsjine = wgjinew;
                    //model.xscbjine = wgjinecb;
                    //datelist.Add(model);
                    string datajson = JsonConvert.SerializeObject(datelist);
                    return datajson;

                }
                else
                {
                    return null;

                }

            }
            catch (Exception x)
            {
                return null;
            }
        }

        public class jsfzmodel
        {
            /// <summary>
            /// 技术分组名称
            /// </summary>
            public virtual string jsfzname { get; set; }

            /// <summary>
            /// 金额
            /// </summary>
            public virtual decimal? hzjine { get; set; }

            /// <summary>
            /// 未税金额
            /// </summary>
            public virtual decimal? wsjine { get; set; }

            /// <summary>
            /// 销售成本金额
            /// </summary>
            public virtual decimal? xscbjine { get; set; }
        }
        #endregion

        #region //查询物料信息以及及时库存
        /// <summary>
        /// 查询物料信息以及及时库存
        /// </summary>
        /// <param name="ItmID">物料代码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get_Itmeinfo_ItmID([FromBody] select_ItmID ItmID)
        {
            Result res = new Result();
            //查询普实的物料信息
            string str = zypushsoftHelper.Get_MDItm_by_ItmID(ItmID.ItmID);
            if (str == "[]" || str == "")
            {
                res.code = "0";
                res.message = "物料代码不存在";
                res.data = "";
            }
            else
            {
                List<psItmAzhinfomodel> timemodel = getObjectByJson<psItmAzhinfomodel>(str);
                string datastr = "";
                if (timemodel.Count() > 0)
                {
                    if (timemodel[0].OnHand == null)
                    {
                        timemodel[0].OnHand = "0";
                    }
                    datastr = JsonConvert.SerializeObject(timemodel[0]);
                }
                
                res.code = "1";
                res.message = "success";
                res.data = datastr;
            }
            var jsonStr = JsonConvert.SerializeObject(res);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "application/json")
            };
            return result;
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
        public class select_ItmID
        {
            public virtual string ItmID { get; set; }
        }

        public class psItmAzhinfomodel
        {
            public virtual string ItmID { get; set; }//物料编码

            public virtual string ItmName { get; set; }//物料名称

            public virtual string ItmSpec { get; set; }//物料的规格型号

            public virtual string OnHand { get; set; }//及时库存

       
        }
        #endregion


        #region //测试登录
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Test_login([FromBody] SysPersonView model)
        {

            return null;
        }
        #endregion

    }



}
