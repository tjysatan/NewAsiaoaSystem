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
    public class DKX_DDinfoDao : ServiceConversion<DKX_DDinfoView, DKX_DDinfo>,IDKX_DDinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_DDinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_DDinfoView GetView(DKX_DDinfo data)
        {
            DKX_DDinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_DDinfoView model)
        {
            DKX_DDinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_DDinfoView model)
        {
            DKX_DDinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_DDinfoView model)
        {
            DKX_DDinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_DDinfoView> model)
        {
            IList<DKX_DDinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_DDinfoView> NGetList()
        {
            List<DKX_DDinfo> listdata = base.GetList() as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_DDinfoView> NGetList_id(string id)
        {
            List<DKX_DDinfo> list = base.GetList_id(id) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_DDinfo> NGetListID(string id)
        {
            IList<DKX_DDinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_DDinfoView NGetModelById(string id)
        {
            DKX_DDinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //电控箱生产单分页数据（客服）
        /// <summary>
        /// 电控箱生产单数量分页数据
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">需求编号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="Gcs_nameId">工程师Id</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 验收入库 6 待发货 7 完成 9 缺料</param>
        /// <param name="start">是否禁用</param>
        /// <param name="DHtype">订货型号</param>
        /// <param name="YQtype">逾期类型 0 发货逾期 1 生产中（生产逾期） 2 可生产（生产接单逾期）  3待生产和缺料（箱体确认逾期） 4待生产和缺料（其他物料确认逾期） 5制图中（制图逾期） 6 待制图（制图接单逾期）</param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> Getdkxtypelistpage(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz, string gnjs, string kfId,string POWER, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(BJno))
                TempHql.AppendFormat(" and BJno like '%{0}%'", BJno);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(Lxname))
                TempHql.AppendFormat(" and Lxname like '%{0}%'", Lxname);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(Gcs_nameId))
                TempHql.AppendFormat(" and Gcs_nameId='{0}'", Gcs_nameId);
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType like '%{0}%'", DHtype);
            if (!string.IsNullOrEmpty(cpph))
                TempHql.AppendFormat(" and u.cpph like '%{0}%'", cpph);
            if (!string.IsNullOrEmpty(beizhu1))
                TempHql.AppendFormat(" and u.REMARK like '%{0}%'", beizhu1);
            if (!string.IsNullOrEmpty(beizhu2))
                TempHql.AppendFormat(" and u.REMARK2 like '{0}'", beizhu2);
            if (user.RoleType != "0")
                TempHql.AppendFormat(" and u.C_name='{0}'", user.Id);
            if (!string.IsNullOrEmpty(POWER))
                TempHql.AppendFormat(" and u.POWER like '%{0}%'", POWER);
            if (!string.IsNullOrEmpty(YQtype))
            {
                if (YQtype == "0")//发货逾期
                    TempHql.AppendFormat(" and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2");
                if (YQtype == "1")//生产逾期 (生产中的)
                    TempHql.AppendFormat(" and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3");
                if (YQtype == "2")//生产接单逾期（可生产状态）
                    TempHql.AppendFormat(" and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1");
                // TempHql.AppendFormat(" and DD_ZT='3' and  DATEDIFF(day,Gscwctime, getdate()) > 1");
                if (YQtype == "3")//箱体库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "4")//其他库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "5")//制图逾期
                    TempHql.AppendFormat(" and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2");
                if (YQtype == "6")//制图接单逾期
                    TempHql.AppendFormat(" and DD_ZT='1' and  DATEDIFF(day,C_time, getdate())> 1");
                if (YQtype == "7")//待审核审核逾期
                    TempHql.AppendFormat(" and DD_ZT='-1' and DATEDIFF(day,Gstjshtime,getdate())>1");
            }
            if (!string.IsNullOrEmpty(Isdqpb))//电器排布图状态
                TempHql.AppendFormat(" and Bnote2='{0}'", Isdqpb);
            if (!string.IsNullOrEmpty(Isqttz))//电器原理图状态（原其他图）
                TempHql.AppendFormat(" and Bnote1='{0}'", Isqttz);
            if (!string.IsNullOrEmpty(gnjs))
                TempHql.AppendFormat(" and gnjsstr like '%{0}%'", gnjs);
            if (!string.IsNullOrEmpty(kfId))
            {
                if (kfId == "1")
                {

                }
                else
                {
                    TempHql.AppendFormat(" and C_name='{0}'", kfId);
                }
            }
            else
            {
                if (user.RoleType == "0")
                {

                }
                else
                {
                    TempHql.AppendFormat(" and C_name='{0}'", user.Id);
                }
            }
            TempHql.AppendFormat("order by u.DD_ZT asc,u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;

        }
        #endregion

        #region //电控箱生产列表分页数据（工程师）
        /// <summary>
        /// 电控箱生产列表分页数据（工程师）
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="KBomNo">关联编号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 验收入库 6 待发货 7 完成 9 缺料</param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start">是否禁用</param>
        /// <param name="start">订货型号</param>
        /// <param name="YQtype">逾期类型 逾期类型 0 发货逾期 1 生产中（生产逾期） 2 可生产（生产接单逾期）  3待生产和缺料（箱体确认逾期） 4待生产和缺料（其他物料确认逾期） 5制图中（制图逾期） 6 待制图（制图接单逾期）</param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> GetdkxlistpageGCS(string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start, string GCSId, string DHtype, string YQtype, string Isdqpb, string Isqttz, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(KBomNo))
                TempHql.AppendFormat(" and KBomNo like '%{0}%'", KBomNo);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(Lxname))
                TempHql.AppendFormat(" and Lxname like '%{0}%'", Lxname);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(DD_ZT))
                //TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
                TempHql.AppendFormat(" and DD_ZT in ({0})", DD_ZT);
            else
                TempHql.AppendFormat(" and DD_ZT not in(-1)");//
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(GCSId))
            {
                TempHql.AppendFormat(" and u.Gcs_nameId='{0}'", GCSId);
            }
            else
            {
                if (user.RoleType == "0" || user.RoleType == "5")
                {

                }
                else
                {
                    TempHql.AppendFormat(" and u.Gcs_nameId in (select Id from  DKX_GCSinfo where ZH_Id='{0}')", user.Id);
                }
            }
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType like '%{0}%'", DHtype);
            if (!string.IsNullOrEmpty(YQtype))
            {
                if (YQtype == "0")//发货逾期
                    TempHql.AppendFormat(" and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2");
                if (YQtype == "1")//生产逾期 (生产中的)
                    TempHql.AppendFormat(" and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3");
                if (YQtype == "2")//生产接单逾期（可生产状态）
                    TempHql.AppendFormat(" and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1");
                // TempHql.AppendFormat(" and DD_ZT='3' and  DATEDIFF(day,Gscwctime, getdate()) > 1");
                if (YQtype == "3")//箱体库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "4")//其他库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "5")//制图逾期
                    TempHql.AppendFormat(" and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2");
                if (YQtype == "6")//制图接单逾期
                    TempHql.AppendFormat(" and DD_ZT='1' and  DATEDIFF(day,C_time, getdate()) > 1");
                if (YQtype == "7")//待审核审核逾期
                    TempHql.AppendFormat(" and DD_ZT='-1' and DATEDIFF(day,Gstjshtime,getdate())>1");
            }
            if (!string.IsNullOrEmpty(Isdqpb))//电器排布图状态
                TempHql.AppendFormat(" and Bnote2='{0}'", Isdqpb);
            if (!string.IsNullOrEmpty(Isqttz))//电器原理图状态（原其他图）
                TempHql.AppendFormat(" and Bnote1='{0}'", Isqttz);
            TempHql.AppendFormat("order by u.DD_ZT asc,u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;
        }
        #endregion

        #region //电控箱生产列表分页数据（电气资料审核）
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="DD_Type"></param>
        /// <param name="KHname"></param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="start"></param>
        /// <param name="GCSId"></param>
        /// <param name="DHtype"></param>
        /// <param name="dqshjd"></param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> Getdkxlistpagedqsh(string DD_Bianhao, string DD_Type, string KHname, string startctime, string endctiome,
         string DD_ZT, string start, string GCSId, string DHtype, string dqshjd, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT in ({0})", DD_ZT);
            else
                TempHql.AppendFormat(" and DD_ZT not in(-1)");//
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(GCSId))
            {
                TempHql.AppendFormat(" and u.Gcs_nameId='{0}'", GCSId);
            }
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType like '%{0}%'", DHtype);
            if (!string.IsNullOrEmpty(dqshjd))
                TempHql.AppendFormat(" and u.dqshres='{0}'", dqshjd);
            TempHql.AppendFormat(" and u.Isdqsh='1'");
            TempHql.AppendFormat("order by u.dqshres asc,u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;

        }
        #endregion

        #region //电控箱生产列表分页数据（品保）
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="BJno"></param>
        /// <param name="DD_Type"></param>
        /// <param name="KHname"></param>
        /// <param name="Lxname"></param>
        /// <param name="Tel"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start"></param>
        /// <param name="DHtype"></param>
        /// <param name="YQtype"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> GetdkxlistpagePb(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
             string startctime, string endctiome, string start, string DHtype, string YQtype, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(BJno))
                TempHql.AppendFormat(" and BJno like '%{0}%'", BJno);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(Lxname))
                TempHql.AppendFormat(" and Lxname like '%{0}%'", Lxname);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(DD_ZT))
            {
                TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
                TempHql.AppendFormat(" and Bnote='1'");
            }
            else
            {
                TempHql.AppendFormat(" and DD_ZT in(7,8,-3)");
                TempHql.AppendFormat(" and Bnote='1'");
            }
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType like '%{0}%'", DHtype);
            if (!string.IsNullOrEmpty(YQtype))
            {
                if (YQtype == "0")//发货逾期
                    TempHql.AppendFormat(" and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2");
                if (YQtype == "1")//生产逾期 (生产中的)
                    TempHql.AppendFormat(" and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3");
                if (YQtype == "2")//生产接单逾期（可生产状态）
                    TempHql.AppendFormat(" and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1");
                // TempHql.AppendFormat(" and DD_ZT='3' and  DATEDIFF(day,Gscwctime, getdate()) > 1");
                if (YQtype == "3")//箱体库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "4")//其他库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "5")//制图逾期
                    TempHql.AppendFormat(" and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2");
                if (YQtype == "6")//制图接单逾期
                    TempHql.AppendFormat(" and DD_ZT='1' and  DATEDIFF(day,C_time, getdate()) > 1");
            }
            TempHql.AppendFormat("order by u.DD_ZT asc,u.Ysdatetime desc,u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;
        }
        #endregion

        #region //电控箱生产列表分页数据（生产）
        /// <summary>
        /// 电控箱生产列表分页数据（生产）
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">需求编号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 缺料  6 待发货 7 完成  </param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start">是否禁用</param>
        /// <param name="DHtype">订货型号</param>
        /// <param name="YQtype">逾期类型 0 发货逾期 1 生产中（生产逾期） 2 缺料 3 待生产（生产接单逾期）  4制图中（制图逾期） </param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> GetdkxlistpageSC(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start, string DHtype, string YQtype, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(BJno))
                TempHql.AppendFormat(" and BJno like '%{0}%'", BJno);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(Lxname))
                TempHql.AppendFormat(" and Lxname like '%{0}%'", Lxname);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(DD_ZT))
            {
                TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
                TempHql.AppendFormat(" and Bnote='1'");
            }
            else
            {
                TempHql.AppendFormat(" and DD_ZT in(2,3,4,5,6,7,8,-2,-3)");
                TempHql.AppendFormat(" and Bnote='1'");
            }
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType='{0}'", DHtype);
            if (!string.IsNullOrEmpty(YQtype))
            {
                if (YQtype == "0")//发货逾期
                    TempHql.AppendFormat(" and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2");
                if (YQtype == "1")//生产逾期 (生产中的)
                    TempHql.AppendFormat(" and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3");
                if (YQtype == "2")//生产接单逾期（可生产状态）
                    TempHql.AppendFormat(" and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1");
                // TempHql.AppendFormat(" and DD_ZT='3' and  DATEDIFF(day,Gscwctime, getdate()) > 1");
                if (YQtype == "3")//箱体库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "4")//其他库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "5")//制图逾期
                    TempHql.AppendFormat(" and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2");
                if (YQtype == "6")//制图接单逾期
                    TempHql.AppendFormat(" and DD_ZT='1' and  DATEDIFF(day,C_time, getdate()) > 1");
            }
            TempHql.AppendFormat("order by u.DD_ZT asc,u.Ysdatetime desc,u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;
        }
        #endregion

        #region //电控箱生产列表分页数据（仓库发货）注：只显示待发货和完成发货状态的数据
        /// <summary>
        /// 电控箱生产列表分页数据（仓库发货）注：只显示待发货和完成发货状态的数据
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="BJno"></param>
        /// <param name="DD_Type"></param>
        /// <param name="KHname"></param>
        /// <param name="Lxname"></param>
        /// <param name="Tel"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="startctime"></param>
        /// <param name="endctiome"></param>
        /// <param name="start"></param>
        /// <param name="DHtype"></param>
        /// <param name="user"></param>
        /// <param name="YQtype">逾期类型 0 发货逾期 1 生产中（生产逾期） 2 缺料 3 待生产（生产接单逾期）  4制图中（制图逾期） </param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> GetDKXDDlistpageCK(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start, string DHtype, string YQtype, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(BJno))
                TempHql.AppendFormat(" and BJno like '%{0}%'", BJno);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(Lxname))
                TempHql.AppendFormat(" and Lxname like '%{0}%'", Lxname);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
            else
                TempHql.AppendFormat(" and DD_ZT in(7,8)");
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType='{0}'", DHtype);
            if (!string.IsNullOrEmpty(YQtype))
            {
                if (YQtype == "0")//发货逾期
                    TempHql.AppendFormat(" and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2");
                if (YQtype == "1")//生产逾期 (生产中的)
                    TempHql.AppendFormat(" and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3");
                if (YQtype == "2")//生产接单逾期（可生产状态）
                    TempHql.AppendFormat(" and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1");
                // TempHql.AppendFormat(" and DD_ZT='3' and  DATEDIFF(day,Gscwctime, getdate()) > 1");
                if (YQtype == "3")//箱体库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "4")//其他库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "5")//制图逾期
                    TempHql.AppendFormat(" and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2");
                if (YQtype == "6")//制图接单逾期
                    TempHql.AppendFormat(" and DD_ZT='1' and  DATEDIFF(day,C_time, getdate()) > 1");
            }
            TempHql.AppendFormat("order by u.DD_ZT asc,u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;
        }
        #endregion

        #region //返回当天的电控箱的下单数量
        /// <summary>
        /// 返回当天的电控箱的下单数量
        /// </summary>
        /// <returns></returns>
        public int GetDKXcount()
        {
            try
            {
                string temHql = string.Format(" from DKX_DDinfo where DateDiff(dd,C_time,getdate())=0 ");
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region //返回所有B的订单数量
        /// <summary>
        /// 返回所有B的订单没有物料代码的数量
        /// </summary>
        /// <returns></returns>
        public int GetDKXBcount()
        {
            try
            {
                string temHql = string.Format("  from DKX_DDinfo where liushuitype='B' and Ps_wlBomNO !=''");
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        #region //电控箱生产单数量分页数据 汇总
        /// <summary>
        /// 电控箱生产单数量分页数据
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="BJno">需求编号</param>
        /// <param name="DD_Type">订单类型 0 小系统 1大系统 3 物联网</param>
        /// <param name="KHname">客户名称</param>
        /// <param name="Lxname">联系方式</param>
        /// <param name="Tel">电话</param>
        /// <param name="Gcs_nameId">工程师Id</param>
        /// <param name="DD_ZT">订单状态 -1 未提交 0已提交 1 待制图 2 制图中 3 待生产 4生产中 5 验收入库 6 待发货 7 完成 9 缺料</param>
        /// <param name="start">是否禁用</param>
        /// <param name="YQtype">逾期类型 0 发货逾期 1 生产中（生产逾期） 2 可生产（生产接单逾期）  3待生产和缺料（箱体确认逾期） 4待生产和缺料（其他物料确认逾期） 5制图中（制图逾期） 6 待制图（制图接单逾期）</param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> Getdkxhzlistpage(string DD_Bianhao, string BJno, string DD_Type, string KHname, string Lxname, string Tel, string Gcs_nameId,
            string DD_ZT, string startctime, string endctiome, string start, string DHtype, string cpph, string beizhu1, string beizhu2, string YQtype, string Isdqpb, string Isqttz, string gnjs,
            string wcstarttime, string wcendtime,string POWER)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(BJno))
                TempHql.AppendFormat(" and BJno like '%{0}%'", BJno);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(Lxname))
                TempHql.AppendFormat(" and Lxname like '%{0}%'", Lxname);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(Gcs_nameId))
                TempHql.AppendFormat(" and Gcs_nameId='{0}'", Gcs_nameId);
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat(" and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            if (!string.IsNullOrEmpty(wcstarttime))
                TempHql.AppendFormat(" and u.Ysdatetime>='{0}' and Ysdatetime<='{1}'", wcstarttime + " 00:00:00", wcendtime + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType like '%{0}%'", DHtype);
            if (!string.IsNullOrEmpty(cpph))
                TempHql.AppendFormat(" and u.cpph like '%{0}%'", cpph);
            if (!string.IsNullOrEmpty(beizhu1))
                TempHql.AppendFormat(" and u.REMARK like '%{0}%'", beizhu1);
            if (!string.IsNullOrEmpty(beizhu2))
                TempHql.AppendFormat(" and u.REMARK2 like '{0}'", beizhu2);
            if (!string.IsNullOrEmpty(YQtype))
            {
                if (YQtype == "0")//发货逾期
                    TempHql.AppendFormat(" and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2");
                if (YQtype == "1")//生产逾期 (生产中的)
                    TempHql.AppendFormat(" and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3");
                if (YQtype == "2")//生产接单逾期（可生产状态）
                    TempHql.AppendFormat(" and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1");
                // TempHql.AppendFormat(" and DD_ZT='3' and  DATEDIFF(day,Gscwctime, getdate()) > 1");
                if (YQtype == "3")//箱体库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "4")//其他库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "5")//制图逾期
                    TempHql.AppendFormat(" and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2");
                if (YQtype == "6")//制图接单逾期
                    TempHql.AppendFormat(" and DD_ZT='1' and  DATEDIFF(day,C_time, getdate()) > 1");
                if (YQtype == "7")//待审核审核逾期
                    TempHql.AppendFormat(" and DD_ZT='-2' and DATEDIFF(day,Gstjshtime,getdate())>1");
            }
            if (!string.IsNullOrEmpty(Isdqpb))//电器排布图状态
                TempHql.AppendFormat(" and Bnote2='{0}'", Isdqpb);
            if (!string.IsNullOrEmpty(Isqttz))//电器原理图状态（原其他图）
                TempHql.AppendFormat(" and Bnote1='{0}'", Isqttz);
            if (!string.IsNullOrEmpty(gnjs))
                TempHql.AppendFormat(" and gnjsstr like '%{0}%'", gnjs);
            if (!string.IsNullOrEmpty(POWER))
                TempHql.AppendFormat(" and u.POWER like '%{0}%'", POWER);
            TempHql.AppendFormat("order by u.DD_ZT asc,u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;

        }
        #endregion

        #region //保存后返回ID
        public string InsertID(DKX_DDinfoView modelView)
        {
            DKX_DDinfo model = GetData(modelView);
            try
            {
                HibernateTemplate.SaveOrUpdate(model);
                string Id = model.Id;
                log4net.LogManager.GetLogger("ApplicationInfoLog");
                return Id;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //操作逾期数据
        /// <summary>
        /// 操作逾期数据
        /// </summary>
        /// <param name="type">逾期类型  0 制图接单逾期 1 制图逾期 2 箱体库存确认逾期 3 其他物料库存确认逾期 4 生产接单逾期 5 生产逾期 6 发货逾期</param>
        /// <returns></returns>
        public IList<DKX_DDinfoView> CZYQDATAList(string type)
        {
            string Hqlstr = "";
            if (type == "0")//工程师接单逾期
            {
                Hqlstr = string.Format(" from DKX_DDinfo  where Start='0' and DD_ZT='1' and  DATEDIFF(day,C_time, getdate()) >1 ");
            }
            if (type == "1")//制图逾期
            {
                Hqlstr = string.Format(" from DKX_DDinfo  where Start='0' and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2 ");
            }
            if (type == "2")//箱体确认逾期
            {
                Hqlstr = string.Format(" from DKX_DDinfo  where Start='0' and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1 ");
            }
            if (type == "3")//其他确认逾期
            {
                Hqlstr = string.Format(" from DKX_DDinfo  where Start='0' and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1 ");
            }
            if (type == "4")//生产接单逾期
            {
                Hqlstr = string.Format(" from DKX_DDinfo  where Start='0' and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1 ");
            }
            if (type == "5")//生产逾期
            {
                Hqlstr = string.Format("from DKX_DDinfo  where Start='0' and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3 ");
            }
            if (type == "6")
            {
                Hqlstr = string.Format("from DKX_DDinfo  where Start='0' and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2 ");
            }
            if (type == "7")
            {
                Hqlstr = string.Format("from DKX_DDinfo  where Start='0' and DD_ZT='-2' and  DATEDIFF(day,Gstjshtime, getdate()) > 1 ");
            }
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(Hqlstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //按照时间顺序查询全部的订单
        /// <summary>
        /// 按照时间顺序查询全部的订单
        /// </summary>
        /// <returns></returns>
        public IList<DKX_DDinfoView> GetallzcDDlist(string DD_Bianhao, string DHtype, string ddtype, string gcsname, string khname, string xdstrattime, string xdendtime, string ysstrattime, string ysendtime)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and DD_DHType like '%{0}%'", DHtype);
            if (!string.IsNullOrEmpty(ddtype))
                TempHql.AppendFormat(" and DD_Type='{0}'", ddtype);
            if (!string.IsNullOrEmpty(gcsname))
                TempHql.AppendFormat(" and Gcs_nameId='{0}'", gcsname);
            if (!string.IsNullOrEmpty(khname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", khname);
            if (!string.IsNullOrEmpty(xdstrattime))
                TempHql.AppendFormat("and C_time>='{0}' and C_time<='{1}'", xdstrattime + " 00:00:00", xdendtime + " 23:59:59");
            if (!string.IsNullOrEmpty(ysstrattime))
                TempHql.AppendFormat(" and Ysdatetime>='{0}' and Ysdatetime<='{1}'", ysstrattime + " 00:00:00", ysendtime + " 23:59:59");
            TempHql.AppendFormat(" and Start='0'");
            TempHql.AppendFormat(" order by C_time asc");
            string HQLstr = string.Format("from DKX_DDinfo u where 1=1 {0}", TempHql.ToString());
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(HQLstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //生产订单的统计情况
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DD_Bianhao"></param>
        /// <param name="KHname"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="Isyq">是否逾期分俩种 0 进行中逾期  1 完成的订单 逾期</param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> Getscddinfopagerlist(string DD_Bianhao, string KHname, string DD_ZT, string Isyq, string starttime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
            else
                TempHql.AppendFormat(" and DD_ZT not in(-1)");
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                TempHql.AppendFormat(" and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            else
                TempHql.AppendFormat(" and C_time>='{0}' and C_time<='{1}'", ksdate + " 00:00:00", jsdate + " 23:59:59");
            if (!string.IsNullOrEmpty(Isyq))
            {
                if (Isyq == "0")//进行中 逾期
                {
                    TempHql.AppendFormat(" and DD_ZT!='8' and DATEDIFF(day,C_time, getdate())>12");
                }
                else//完成的订单逾期
                {
                    TempHql.AppendFormat(" and DD_ZT='8' and DATEDIFF(day,C_time, Fhdatetime)>12");
                }
            }
            TempHql.AppendFormat("order by  u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;
        }

        #region //电控箱总下单量各个数量
        public decimal Getzxdsum(string starttime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            string Hqlstr = "";
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                Hqlstr = string.Format("from DKX_DDinfo where DD_ZT not in(-1) and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            }
            else
            {
                Hqlstr = string.Format("from DKX_DDinfo where DD_ZT not in(-1) and C_time>='{0}' and C_time<='{1}'", ksdate + " 00:00:00", jsdate + " 23:59:59");
            }
            IQuery queryCount = Session.CreateSQLQuery(string.Format("select count(*)  {0} ", Hqlstr));
            //IQuery queryCount = Session.CreateQuery(string.Format("select sum(kdje)  {0} ", Hqlstr));
            decimal count = Convert.ToDecimal(queryCount.UniqueResult());
            return count;
        }
        //各个状态的数量
        public decimal GetZTdsumbyddzt(string ddzt, string starttime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            string Hqlstr = "";
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                Hqlstr = string.Format("from DKX_DDinfo where DD_ZT not in(-1) and DD_ZT='{0}' and C_time>='{1}' and C_time<='{2}'", ddzt, starttime + " 00:00:00", endtime + " 23:59:59");
            }
            else
            {
                Hqlstr = string.Format("from DKX_DDinfo where DD_ZT not in(-1) and DD_ZT='{0}' and C_time>='{1}' and C_time<='{2}'", ddzt, ksdate + " 00:00:00", jsdate + " 23:59:59");
            }
            IQuery queryCount = Session.CreateSQLQuery(string.Format("select count(*)  {0} ", Hqlstr));
            //IQuery queryCount = Session.CreateQuery(string.Format("select sum(kdje)  {0} ", Hqlstr));
            decimal count = Convert.ToDecimal(queryCount.UniqueResult());
            return count;
        }

        //在途的数量
        public decimal Getzaitusum(string starttime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            string Hqlstr = "";
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
            {
                Hqlstr = string.Format("from DKX_DDinfo where DD_ZT not in(-1,8) and C_time>='{0}' and C_time<='{1}' ", starttime + " 00:00:00", endtime + " 23:59:59");
            }
            else
            {
                Hqlstr = string.Format("from DKX_DDinfo where DD_ZT not in(-1,8) and C_time>='{0}' and C_time<='{1}' ", ksdate + " 00:00:00", jsdate + " 23:59:59");
            }
            IQuery queryCount = Session.CreateSQLQuery(string.Format("select count(*)  {0} ", Hqlstr));
            decimal count = Convert.ToDecimal(queryCount.UniqueResult());
            return count;
        }

        //逾期完成和正常完成
        public decimal Getyqwcandzcwcsum(string type, string starttime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            string Hqlstr = "";
            if (type == "0")//逾期完成
            {
                if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                {
                    Hqlstr = string.Format("from DKX_DDinfo where DD_ZT='8' and  DATEDIFF(day,C_time, Fhdatetime)>12  and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
                }
                else
                {
                    Hqlstr = string.Format("from DKX_DDinfo where DD_ZT='8' and  DATEDIFF(day,C_time, Fhdatetime)>12  and C_time>='{0}' and C_time<='{1}'", ksdate + " 00:00:00", jsdate + " 23:59:59");
                }
            }
            else//正常完成
            {
                if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                {
                    Hqlstr = string.Format("from DKX_DDinfo where DD_ZT='8' and  DATEDIFF(day,C_time, Fhdatetime)<=12  and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
                }
                else
                {
                    Hqlstr = string.Format("from DKX_DDinfo where DD_ZT='8' and  DATEDIFF(day,C_time, Fhdatetime)<=12  and C_time>='{0}' and C_time<='{1}'", ksdate + " 00:00:00", jsdate + " 23:59:59");
                }
            }
            IQuery queryCount = Session.CreateSQLQuery(string.Format("select count(*)  {0} ", Hqlstr));
            decimal count = Convert.ToDecimal(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //导出数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dd_no"></param>
        /// <param name="khname"></param>
        /// <param name="DD_ZT"></param>
        /// <param name="Isyq"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<DKX_DDinfoView> GetddinfoExportJson(string dd_no, string khname, string DD_ZT, string Isyq, string starttime, string endtime)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(dd_no))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", dd_no);
            if (!string.IsNullOrEmpty(khname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", khname);
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT='{0}'", DD_ZT);
            else
                TempHql.AppendFormat(" and DD_ZT not in(-1)");
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                TempHql.AppendFormat(" and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            else
                TempHql.AppendFormat(" and C_time>='{0}' and C_time<='{1}'", ksdate + " 00:00:00", jsdate + " 23:59:59");
            if (!string.IsNullOrEmpty(Isyq))
            {
                if (Isyq == "0")//进行中 逾期
                {
                    TempHql.AppendFormat(" and DD_ZT!='8' and DATEDIFF(day,C_time, getdate())>12");
                }
                else//完成的订单逾期
                {
                    TempHql.AppendFormat(" and DD_ZT='8' and DATEDIFF(day,C_time, Fhdatetime)>12");
                }
            }
            string HQLstr = string.Format("from DKX_DDinfo u where 1=1 {0}", TempHql.ToString());
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(HQLstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
        #endregion


        #region //根据产品名称 功率单位查询时间内已经完成的订单产品数量
        /// <summary>
        /// 功率单位查询时间内已经完成的订单产品数量
        /// </summary>
        /// <param name="cpname">产品名称</param>
        /// <param name="gl">功率</param>
        /// <param name="dw">单位</param>
        /// <param name="starttime">完成时间</param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public int GetSCCPSUM(string cpname, string gl, string dw, string starttime, string endtime)
        {
            try
            {
                string temHql = "";
                if (!string.IsNullOrEmpty(starttime))//完成时间不为空
                    temHql = string.Format(" from DKX_DDinfo where Fhdatetime>='{0}' and Fhdatetime<='{1}' and DD_DHType='{2}' and POWER='{3}' and dw='{4}' and DD_ZT='8'", starttime + " 00:00:00", endtime + " 23:59:59", cpname, gl, dw);
                else
                    temHql = string.Format(" from DKX_DDinfo where  DD_DHType='{0}' and POWER='{1}' and dw='{2}' and DD_ZT='8'", cpname, gl, dw);
                IQuery queryCount = Session.CreateQuery(string.Format("select sum(NUM)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return 0;
            }
        }
        #endregion


        #region //查询需要工程师助力检查的订单数据的数量
        /// <summary>
        /// 查询需要工程师助力检查的订单数据
        /// </summary>
        /// <param name="dragstart">检查助力状态 0 需要检查 1 待检查 2 完成检查 3 驳回检查</param>
        /// <param name="user">当前登陆的账号</param>
        /// <returns></returns>
        public int Getdragsumbyzt(string dragstart, SessionUser user)
        {
            try
            {
                string temHql = "";
                if (!string.IsNullOrEmpty(dragstart))
                {
                    temHql = string.Format(" from DKX_DDinfo where DD_ZT='2' and dragstart='{0}' and Gcs_nameId !='{1}'", dragstart, user.Id);
                    IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                    int count = Convert.ToInt32(queryCount.UniqueResult());
                    return count;
                }

                else { return -1; }

            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region //查询需要工程师助力检查的订单分页数据
        /// <summary>
        /// 查询需要工程师助力检查的订单分页数据
        /// </summary>
        /// <param name="DD_Bianhao">订单编号</param>
        /// <param name="user">当前登陆的账号</param>
        /// <returns></returns>
        public PagerInfo<DKX_DDinfoView> GetdragddinfoPagerlist(string DD_Bianhao, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            TempHql.AppendFormat(" and DD_ZT='2'");
            TempHql.AppendFormat(" and dragstart='1'");
            TempHql.AppendFormat(" and Gcs_nameId !='{0}'", user.Id);
            TempHql.AppendFormat("order by  u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;
        }
        #endregion

        #region //查询工程师需要处理的标记异常的数据的数量
        public int GetgczlycsumDate(SessionUser user)
        {
            try
            {
                string temHql = "";

                if (user.RoleType == "0" || user.RoleType == "5")
                {
                    temHql = string.Format(" from DKX_DDinfo where gczl_Isyc='1'");
                }
                else
                {
                    temHql = string.Format(" from DKX_DDinfo where gczl_Isyc='1' and Gcs_nameId ='{0}'", user.Id);
                }
                IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", temHql));
                int count = Convert.ToInt32(queryCount.UniqueResult());
                return count;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region //查询需要工程师修改的工程资料异常分页数据
        public PagerInfo<DKX_DDinfoView> GetgczlycPagerlistdate(string DD_Bianhao, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            TempHql.AppendFormat(" and gczl_Isyc='1'");
            //if (user.RoleType == "0" || user.RoleType == "5")
            //{
            //}
            //else
            //{
            //    TempHql.AppendFormat(" and Gcs_nameId ='{0}'", user.Id);
            //}
            TempHql.AppendFormat("order by  u.C_time desc");
            PagerInfo<DKX_DDinfoView> list = Search();
            return list;
        }
        #endregion

        #region //导出订单数据
        public IList<DKX_DDinfoView> GetExcelExportDDdata(string DD_Bianhao, string KBomNo, string DD_Type, string KHname, string Lxname, string Tel, string DD_ZT,
            string startctime, string endctiome, string start, string GCSId, string DHtype, string YQtype, string Isdqpb, string Isqttz, SessionUser user)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            string ksdate = d1.ToString("yyyyMMdd");
            string jsdate = d2.ToString("yyyyMMdd");
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Bianhao like '%{0}%'", DD_Bianhao);
            if (!string.IsNullOrEmpty(KBomNo))
                TempHql.AppendFormat(" and KBomNo like '%{0}%'", KBomNo);
            if (!string.IsNullOrEmpty(DD_Type))
                TempHql.AppendFormat(" and DD_Type='{0}'", DD_Type);
            if (!string.IsNullOrEmpty(KHname))
                TempHql.AppendFormat(" and KHname like '%{0}%'", KHname);
            if (!string.IsNullOrEmpty(Lxname))
                TempHql.AppendFormat(" and Lxname like '%{0}%'", Lxname);
            if (!string.IsNullOrEmpty(Tel))
                TempHql.AppendFormat(" and Tel like '%{0}%'", Tel);
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT in ({0})", DD_ZT);
            else
                TempHql.AppendFormat(" and DD_ZT not in(-1)");//
            if (!string.IsNullOrEmpty(startctime))
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            else
                TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", ksdate + " 00:00:00", jsdate + " 23:59:59");
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and u.Start='{0}'", start);
            if (!string.IsNullOrEmpty(GCSId))
            {
                TempHql.AppendFormat(" and u.Gcs_nameId='{0}'", GCSId);
            }
            else
            {
                if (user.RoleType == "0" || user.RoleType == "5")
                {

                }
                else
                {
                    TempHql.AppendFormat(" and u.Gcs_nameId in (select Id from  DKX_GCSinfo where ZH_Id='{0}')", user.Id);
                }
            }
            if (!string.IsNullOrEmpty(DHtype))
                TempHql.AppendFormat(" and u.DD_DHType like '%{0}%'", DHtype);
            if (!string.IsNullOrEmpty(YQtype))
            {
                if (YQtype == "0")//发货逾期
                    TempHql.AppendFormat(" and DD_ZT='7' and  DATEDIFF(day,Ysdatetime, getdate()) > 2");
                if (YQtype == "1")//生产逾期 (生产中的)
                    TempHql.AppendFormat(" and DD_ZT='6' and  DATEDIFF(day,Scjdtime, getdate()) > 3");
                if (YQtype == "2")//生产接单逾期（可生产状态）
                    TempHql.AppendFormat(" and DD_ZT='4' and DATEDIFF(day,wlsqtime,getdate())>1");
                // TempHql.AppendFormat(" and DD_ZT='3' and  DATEDIFF(day,Gscwctime, getdate()) > 1");
                if (YQtype == "3")//箱体库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and xtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "4")//其他库存确认逾期
                    TempHql.AppendFormat(" and DD_ZT in (3,5) and qtIsq='0' and DATEDIFF(day,Gscwctime,getdate())>1");
                if (YQtype == "5")//制图逾期
                    TempHql.AppendFormat(" and DD_ZT='2' and  DATEDIFF(day,Gscjdtime, getdate()) > 2");
                if (YQtype == "6")//制图接单逾期
                    TempHql.AppendFormat(" and DD_ZT='1' and  DATEDIFF(day,C_time, getdate()) > 1");
                if (YQtype == "7")//待审核审核逾期
                    TempHql.AppendFormat(" and DD_ZT='-1' and DATEDIFF(day,Gstjshtime,getdate())>1");
            }
            if (!string.IsNullOrEmpty(Isdqpb))//电器排布图状态
                TempHql.AppendFormat(" and Bnote2='{0}'", Isdqpb);
            if (!string.IsNullOrEmpty(Isqttz))//电器原理图状态（原其他图）
                TempHql.AppendFormat(" and Bnote1='{0}'", Isqttz);
            TempHql.AppendFormat("order by u.DD_ZT asc,u.C_time desc");
            string HQLstr = string.Format("from DKX_DDinfo u where 1=1 {0}", TempHql.ToString());
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(HQLstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;

        }
        #endregion

        #region //查询2020和2021年有效订单数据
        public IList<DKX_DDinfoView> GetorderdataToyear()
        {
            string Hqlstr = string.Format("from  DKX_DDinfo where  DD_ZT not in(-1) and  DATEDIFF(YYYY,C_time,getdate())<=1");
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(Hqlstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //通过订单编号查询订单的详情数据
        /// <summary>
        /// 通过订单编号查询订单的详情数据
        /// </summary>
        /// <param name="orderno">订单编号</param>
        /// <returns></returns>
        public DKX_DDinfoView GetDDmodelbyorderno(string orderno)
        {
            string temHql = string.Format(" from DKX_DDinfo where   DD_Bianhao='{0}' and Start='0'", orderno);
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(temHql) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //通过订单编号查询订单的详情数据（包括包括被删除的）
        /// <summary>
        /// 通过订单编号查询订单的详情数据（包括被删除的）
        /// </summary>
        /// <param name="orderno">订单编号</param>
        /// <returns></returns>
        public DKX_DDinfoView GetALLDDmodelbyorderno(string orderno)
        {
            string temHql = string.Format(" from DKX_DDinfo where   DD_Bianhao='{0}'", orderno);
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(temHql) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion

        #region //查询当天的完成发料的数据数量
        /// <summary>
        /// 查询当天的完成发料的数据数量
        /// </summary>
        /// <returns></returns>
        public int GetTodayFLWCordercunot()
        {
            string tempHql = "from  DKX_DDinfo where DateDiff(dd,Flwxtime,getdate())=0";
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //查询同一个大类中的产品数据
        /// <summary>
        /// 查询同一个大类的产品数据
        /// </summary>
        /// <param name="sanduanno">查询同一个大类的产品数据</param>
        /// <returns></returns>
        public int Getdaleordercount(string sanduanno)
        {
            string Hqlstr = string.Format(" from DKX_DDinfo where Ps_sanduanno='{0}' and Start='0'  ", sanduanno);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", Hqlstr));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //通过自动生成的销售订单号查询所有的明细
        /// <summary>
        /// 通过自动生成的销售订单号查询所有的明细
        /// </summary>
        /// <param name="Ps_orderno">自动生成的销售明细单</param>
        /// <returns></returns>
        public IList<DKX_DDinfoView> GetAllmxbyPs_orderno(string Ps_orderno)
        {
            string Hqlstr = string.Format("from  DKX_DDinfo where  Ps_orderno='{0}'", Ps_orderno);
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(Hqlstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //通过客户的编码查询没有生成销售单号的数据
        /// <summary>
        /// 通过客户的编码查询没有生成销售单号的数据
        /// </summary>
        /// <param name="K3CODEL">客户编码</param>
        /// <returns></returns>
        public IList<DKX_DDinfoView> Getqtmxbyk3code(string K3CODEL)
        {
            string Hqlstr = string.Format("from  DKX_DDinfo where  khkecode='{0}' and Ps_orderno is null and Start='0' ORDER BY C_time DESC", K3CODEL);

            IQuery query = Session.CreateQuery(Hqlstr);
            query.SetFirstResult(0);
            query.SetMaxResults(40);
            List<DKX_DDinfo> list= query.List<DKX_DDinfo>() as List<DKX_DDinfo>;
           // List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(Hqlstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion


        #region //通过下单时间和订单状态查询订单数据
        /// <summary>
        /// 通过下单时间和订单状态查询订单数据
        /// </summary>
        /// <param name="startctime">下单时间</param>
        /// <param name="endctiome"></param>
        /// <param name="DD_ZT">状态</param>
        /// <returns></returns>
        public IList<DKX_DDinfoView> Getorderlistbyxdtimeandddzt(string startctime, string endctiome, string DD_ZT)
        {
            TempHql = new StringBuilder();
            TempHql.AppendFormat("and u.C_time>='{0}' and C_time<='{1}'", startctime + " 00:00:00", endctiome + " 23:59:59");
            TempHql.AppendFormat(" and u.Start='0'");
            if (!string.IsNullOrEmpty(DD_ZT))
                TempHql.AppendFormat(" and DD_ZT in ({0})", DD_ZT);
            TempHql.AppendFormat("order by u.DD_ZT asc,u.C_time desc");
            string HQLstr = string.Format("from DKX_DDinfo u where 1=1 {0}", TempHql.ToString());
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(HQLstr) as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //

        #endregion

        #region //通过新生成的普实料号查询是否重复
        /// <summary>
        /// 检查普实料号是否重复
        /// </summary>
        /// <param name="pswlno"></param>
        /// <returns></returns>
        public bool checkpswlno(string pswlno)
        {
            string Hqlstr = string.Format(" from DKX_DDinfo  where Start='0' and  Ps_wlBomNO='{0}' ",pswlno);
            List<DKX_DDinfo> list = HibernateTemplate.Find<DKX_DDinfo>(Hqlstr) as List<DKX_DDinfo>;
            if (list != null)
                return true;
            else
                return false;
        }
        #endregion

        #region //根据物料号查询下单的数量
        /// <summary>
        /// 根据物料号查询下单的数量
        /// </summary>
        /// <param name="ItemNo">物料号</param>
        /// <returns></returns>
        public int Get_Place_Order_Cunot(string ItemNo)
        {
            string tempHql =string.Format(" from DKX_DDinfo where Start='0' and DD_ZT!='-1' and Ps_wlBomNO='{0}'",ItemNo);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //根据物料号查询最近20次下单的记录
        /// <summary>
        /// 根据物料号查询最近20次下单的记录
        /// </summary>
        /// <param name="itemno">物料代码</param>
        /// <param name="count">查询的条数</param>
        /// <returns></returns>
        public IList<DKX_DDinfoView> Get_order_by_itemno(string itemno, int count)
        {
            string Hqlstr = string.Format(" from DKX_DDinfo where  Start='0' and DD_ZT!='-1' and Ps_wlBomNO='{0}' ORDER BY C_time DESC", itemno);
            IQuery query = Session.CreateQuery(Hqlstr);
            query.SetFirstResult(0);
            query.SetMaxResults(count);
            List<DKX_DDinfo> list = query.List<DKX_DDinfo>() as List<DKX_DDinfo>;
            IList<DKX_DDinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
