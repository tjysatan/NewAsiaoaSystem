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
        public static string Insertczjltew(string jlId, string czcon, string czId, string dd_bianhao, string gscId,string CBRId,string CBRName,string CBRRemarks)
        {
            IDKX_LCCZJLinfoDao _IDKX_LCCZJLinfoDao = ContextRegistry.GetContext().GetObject("DKX_LCCZJLinfoDao") as IDKX_LCCZJLinfoDao;
            DKX_LCCZJLinfoView model = new DKX_LCCZJLinfoView();
            model.dd_Id = jlId;
            model.caozuo = czcon;
            model.c_name = czId;
            model.c_time = DateTime.Now;
            model.dd_bianhao = dd_bianhao;
            model.gcs_Id = gscId;
            model.caozuotype = 0;//操作退单
            model.CBRId = CBRId;
            model.CBRName = CBRName;
            model.CBRRemarks = CBRRemarks;
            _IDKX_LCCZJLinfoDao.Ninsert(model);
            return "1";
        }

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

 
    }
}