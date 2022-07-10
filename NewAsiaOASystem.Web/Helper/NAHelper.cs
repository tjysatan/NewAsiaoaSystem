using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Spring.Context.Support;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using Newtonsoft.Json;

namespace NewAsiaOASystem.Web
{
    public class NAHelper
    {
        IDKX_DDtypeinfoDao _IDKX_DDtypeinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDtypeinfoDao") as IDKX_DDtypeinfoDao;
        IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
      

        public static string Insertczjl(string jlId, string czcon, string czId)
        {
            IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
            DKX_LCCZJLinfoView model = new DKX_LCCZJLinfoView();
            model.dd_Id = jlId;
            model.caozuo = czcon;
            model.c_name = czId;
            model.c_time = DateTime.Now;
            model.caozuotype = -1;//-1其他订单操作 
            _IDKX_LCCZJLinfoDao.Ninsert(model);
            return "1";
        }

        //非标生产订单的操作记录
        /// <summary>
        /// 非标生产订单的操作记录(记录韩有订单编号和订单的责任工程师)
        /// </summary>
        /// <param name="jlId">订单Id</param>
        /// <param name="czcon">操作内容</param>
        /// <param name="czId">操作人Id</param>
        /// <param name="dd_bianhao">订单编号</param>
        /// <param name="gscId">工程师Id</param>
        /// <param name="CBRId">生产退单原因Id</param>
        /// <param name="CBRName">生产退单原因名称</param>
        /// <param name="CBRRemarks">生产退单备注</param>
        /// <returns></returns>
        public static string Insertczjltew(string jlId, string czcon, string czId, string dd_bianhao, string gscId,string CBRId,string CBRName,string CBRRemarks,string type)
        {
            IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
            DKX_LCCZJLinfoView model = new DKX_LCCZJLinfoView();
            model.dd_Id = jlId;
            model.caozuo = czcon;
            model.c_name = czId;
            model.c_time = DateTime.Now;
            model.dd_bianhao = dd_bianhao;
            model.gcs_Id = gscId;

            model.caozuotype = Convert.ToInt32(type);//操作退单
            model.CBRId = CBRId;
            model.CBRName = CBRName;
            model.CBRRemarks = CBRRemarks;
            _IDKX_LCCZJLinfoDao.Ninsert(model);
            return "1";
        }

        //public static string Insrttczjl_pbsh(string jlId, string czcon, string czId, string dd_bianhao, string gscId, string CBRId, string CBRName, string CBRRemarks)

        #region //插入 非标订单生产退单之后工程师和客服的操作记录
        public static string InsertIsPdrefundczjltew(string jlId, string czcon, string czId)
        {
            IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
            DKX_LCCZJLinfoView model = new DKX_LCCZJLinfoView();
            model.dd_Id = jlId;
            model.caozuo = czcon;
            model.c_name = czId;
            model.c_time = DateTime.Now;
            model.caozuotype = -1;//-1其他订单操作 
            model.IsPdrefund = 1;//生产退单的工程师和客服的操作记录
            _IDKX_LCCZJLinfoDao.Ninsert(model);
            return "1";
        }
        #endregion

        #region //查询生产退单原因下拉数据
        public static IList<NewChargebackReasonView> GetSCCBRDATA()
        {
            INewChargebackReasonDao _INewChargebackReasonDao = ContextRegistry.GetContext().GetObject("NewChargebackReasonDao") as INewChargebackReasonDao;
            return _INewChargebackReasonDao.GetSCCBRData();
            //List<NewChargebackReasonView> listView = _INewChargebackReasonDao.GetSCCBRData() as List<NewChargebackReasonView>;
            //List<GetReasonlist> Reasonlist = new List<GetReasonlist>();
            //GetReasonlist Reasonmodel = new GetReasonlist();
            //if (listView != null)
            //{
            //    foreach (var item in listView)
            //    {
            //        Reasonmodel = new GetReasonlist();
            //        Reasonmodel.Id = item.Id.ToString();
            //        Reasonmodel.Name = item.Reason_name;
            //        Reasonlist.Add(Reasonmodel);
            //    }
            //}
            //if (SelectedPID != null)
            //    ViewData["aDTlist"] = new SelectList(Reasonlist, "Id", "Name", SelectedPID);
            //else
            //    ViewData["aDTlist"] = new SelectList(Reasonlist, "Id", "Name");
        }
        #endregion

        #region //根据逗号分割字符串
        public static List<string> GetDivisionstrlist(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))//为空
                    return null;
                else {
                    List<string> list = new List<string>(str.Split(','));
                    return list;
                }    
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //MD5加密
        public static string MD5Encrypt(string input,  Encoding encode)
        {
            MD5 md5 = new  MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(encode.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            return sb.ToString();
        }
        #endregion

        #region //随机生成一个字符串
        public static string RandomNum(int Digit)
        {
            ArrayList MyArray = new ArrayList();
            Random random = new Random();
            string str = null;
            int Nums = Digit;
            while (Nums > 0)
            {
                int i = random.Next(1, 10);// 9>=a>=1
                if (!MyArray.Contains(i))
                {
                    if (MyArray.Count < 6)
                    {
                        MyArray.Add(i);
                    }
                    Nums -= 1;
                }
            }
            for (int j = 0; j <= MyArray.Count - 1; j++)
            {
                str += MyArray[j].ToString();
            }
            return str;
        }
        #endregion

        #region //生产物料编码的流水号
        public static string liushuihao()
        {
            IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
            int a = _IDKX_DDinfoDao.GetDKXBcount();
            a = a + 1;
            string str = a.ToString();
            string pnum = str.PadLeft(5, '0');
            return pnum;
        }
        #endregion

        #region //插入普实的BOM
        public static string JK_Ps_InstarBom(string Id,string Ps_wlBomNO)
        {
            IDKX_DDinfoDao _IDKX_DDinfoDao = ContextRegistry.GetContext().GetObject("DKX_DDinfoDao") as IDKX_DDinfoDao;
            //查询非标订单
            DKX_DDinfoView ddmodel = _IDKX_DDinfoDao.NGetModelById(Id);
            if (ddmodel != null)
            {
                //if (ddmodel.Ps_wlBomNO != "" && ddmodel.Ps_wlBomNO != null)
                //{
                    var bomstr = ddmodel.KBomNo.Substring(0, 3);
                    if (bomstr == "BOM")
                        return "1";
                    if (ddmodel.Ps_BomNO == "" || ddmodel.Ps_BomNO == null)
                    {
                        Ps_Bommodel PsBomnodel = new Ps_Bommodel();
                        PsBomnodel.BomID = Ps_wlBomNO;
                        PsBomnodel.BomName = ddmodel.KBomNo;
                        PsBomnodel.ItmID = Ps_wlBomNO;
                        PsBomnodel.VerNum = "V 1.0";
                        PsBomnodel.RouID = DateTime.Now.ToString();
                        PsBomnodel.Procedures = Getgongxu();
                        PsBomnodel.Items = Getyongliao(ddmodel.Id);
                        string res = pushsoftHelper.InstaerBominfo(PsBomnodel);
                        if (res == "" || res == null) { return "1"; }
                        else
                        {
                            pushsoftErrmodel errmodel = new pushsoftErrmodel();
                            errmodel = JsonConvert.DeserializeObject<pushsoftErrmodel>(res);
                            if (errmodel.ErrCode == "0")
                            {
                                return "0";
                            }
                            else
                            {
                                return "1";
                            }
                        }
                    }
                    return "1";
                //}
                //return "1";
            }
            return "1";
        }

        //工序
        public static IList<Proceduresmodel> Getgongxu()
        {
            List<Proceduresmodel> gxmxlist = new List<Proceduresmodel>();
            Proceduresmodel gongx = new Proceduresmodel();
            gongx.LineNum = "10";
            gongx.PrcID = "DQ01";
            gongx.PrcName = "底板装配";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "0";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "20";
            gongx.PrcID = "DQ02";
            gongx.PrcName = "接控制线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "10";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "30";
            gongx.PrcID = "DQ03";
            gongx.PrcName = "接主回路线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "20";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "40";
            gongx.PrcID = "DQ05";
            gongx.PrcName = "面板装箱";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "30";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "50";
            gongx.PrcID = "DQ06";
            gongx.PrcName = "底板装箱";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "40";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "60";
            gongx.PrcID = "DQ071";
            gongx.PrcName = "面板接线";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "50";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "70";
            gongx.PrcID = "DQ09";
            gongx.PrcName = "调试";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "60";
            gxmxlist.Add(gongx);
            gongx = new Proceduresmodel();
            gongx.LineNum = "80";
            gongx.PrcID = "DQ011";
            gongx.PrcName = "验收";
            gongx.WcnID = "001";
            gongx.WcnName = "电气车间";
            gongx.PreLineNum = "70";
            gxmxlist.Add(gongx);
            return gxmxlist;

        }

        //用料
        public static IList<Itemsmodel> Getyongliao(string Id)
        {
            IDKX_k3BominfoDao _IDKX_k3BominfoDao = ContextRegistry.GetContext().GetObject("DKX_k3BominfoDao") as IDKX_k3BominfoDao;
            IK3_wuliaoinfoDao _IK3_wuliaoinfoDao = ContextRegistry.GetContext().GetObject("K3_wuliaoinfoDao") as IK3_wuliaoinfoDao;
            IDKX_ZLDataInfoDao _IDKX_ZLDataInfoDao = ContextRegistry.GetContext().GetObject("DKX_ZLDataInfoDao") as IDKX_ZLDataInfoDao;
            //查询资料表(料单)
            IList<DKX_ZLDataInfoView> ZLmodellist = _IDKX_ZLDataInfoDao.GetzljsonbyId(Id, "1");
            if (ZLmodellist != null)
            {
                DKX_ZLDataInfoView zlmodel = new DKX_ZLDataInfoView();
                zlmodel = ZLmodellist[0];
                List<Itemsmodel> wuliaolist = new List<Itemsmodel>();
                if (zlmodel.Isgl == 2)//关联K3的料单
                {
                    IList<DKX_k3BominfoView> kemodellist = _IDKX_k3BominfoDao.GetliaodanlistbyIdandbomno(Id, zlmodel.Bjno);
                    foreach (var item in kemodellist)
                    {
                        //查询物料
                        K3_wuliaoinfoView jcwlmodel = _IK3_wuliaoinfoDao.Getwuliaobyfnumber(item.FNumber);
                        Itemsmodel wlmodel = new Itemsmodel();
                        wlmodel.ItmID = item.FNumber;
                        wlmodel.NetQty = item.FAuxQty;
                        wlmodel.ScrapRate = "0";
                        if (jcwlmodel != null)
                        {
                            if (jcwlmodel.SourceID == "制造")
                            {
                                wlmodel.LineType = "M";
                            }
                            else
                            {
                                wlmodel.LineType = "P";
                            }
                        }
                        else
                        {
                            wlmodel.LineType = "P";
                        }
                        wlmodel.OperationLine = "10";
                        wlmodel.Position = item.Beizhu;
                        wlmodel.Position = item.Beizhu;
                        wuliaolist.Add(wlmodel);
                    }
                }

                return wuliaolist;
            }
            return null;
        }
        #endregion

        #region //通过物料规格检查ERP中是否已经存在该规格名称
        /// <summary>
        /// 通过物料规格检查ERP中是否已经存在该规格名称
        /// </summary>
        /// <param name="ItmSpec"></param>
        /// <returns></returns>
        public static bool check_IsYiJingcunzaiguige(string ItmSpec)
        {
            string res = zypushsoftHelper.GetMDItm_ItmSpec(ItmSpec);
            if (res == "[]" || res == "" || res == "null")
                return true;
            else
                return false;
        }
        #endregion



    }
}