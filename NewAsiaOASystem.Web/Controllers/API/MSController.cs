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

namespace NewAsiaOASystem.Web.Controllers
{
    [ApiAuthorize]
    
    public class MSController : ApiController
    {
        IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
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
    }



}
