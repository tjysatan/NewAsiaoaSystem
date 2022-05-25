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
    public class WL_DkxInfoDao:ServiceConversion<WL_DkxInfoView,WL_DkxInfo>,IWL_DkxInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WL_DkxInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WL_DkxInfoView GetView(WL_DkxInfo data)
        {
            WL_DkxInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WL_DkxInfoView model)
        {
            WL_DkxInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WL_DkxInfoView model)
        {
            WL_DkxInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WL_DkxInfoView model)
        {
            WL_DkxInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WL_DkxInfoView> model)
        {
            IList<WL_DkxInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WL_DkxInfoView> NGetList()
        {
            List<WL_DkxInfo> listdata = base.GetList() as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WL_DkxInfoView> NGetList_id(string id)
        {
            List<WL_DkxInfo> list = base.GetList_id(id) as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WL_DkxInfo> NGetListID(string id)
        {
            IList<WL_DkxInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WL_DkxInfoView NGetModelById(string id)
        {
            WL_DkxInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region // 物联网电控箱 分页列表数据 +GetQyinfoList（）
        /// <summary>
        /// 物联网电控箱 分页列表数据
        /// </summary>
        /// <param name="Name">区域名称</param>
        /// <param name="user">当前登录用户</param>
        /// <returns></returns>
        public PagerInfo<WL_DkxInfoView> GetWLdkxinfoList(string Name,string Name2, string Is_sx,string Is_xs, string wl_zt, string wl_sid,string Is_qf, string startdate, string enddate, string sxstartdate, string sxenddate, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
            {
                TempHql.AppendFormat(" and u.Xs_jxsId ='{0}'", Name);
                if (!string.IsNullOrEmpty(Name2))
                {
                    TempHql.AppendFormat(" or u.Xs_jxsId ='{0}'", Name2);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Name2))
                    TempHql.AppendFormat(" and u.Xs_jxsId ='{0}'", Name2);
            }
            if (!string.IsNullOrEmpty(wl_zt))
            {
                if (wl_zt == "1")
                {
                    wl_zt = "'1'," + "'2'," + "'3'";
                }
                else if (wl_zt == "2") {
                    wl_zt = "'2'," + "'3'";
                }
                TempHql.AppendFormat(" and u.WL_zt in ({0})", wl_zt);
            }
            if (!string.IsNullOrEmpty(Is_sx))//是否上线
                TempHql.AppendFormat(" and u.Is_sx='{0}'",Is_sx);
            if (!string.IsNullOrEmpty(Is_xs))//是否销售
                TempHql.AppendFormat(" and u.Is_xs='{0}'",Is_xs);
            if (!string.IsNullOrEmpty(wl_sid))
                TempHql.AppendFormat(" and u.WL_SSD='{0}'",wl_sid);
            if (!string.IsNullOrEmpty(Is_qf))
            {
                string Newdate = DateTime.Now.ToString();//获取当前服务器时间
                if (Is_qf == "0")//欠费的 到期时间小于等于当前时间的
                    TempHql.AppendFormat("and Jf_endtime<='{0}'", Newdate);
                else
                    TempHql.AppendFormat(" and Jf_endtime>'{0}'", Newdate);
            }
                 
            if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
                TempHql.AppendFormat("and Xs_datetime>='{0}' and Xs_datetime<='{1}'", startdate + " 00:00:00", enddate + " 23:59:59");
            if (!string.IsNullOrEmpty(sxstartdate) && !string.IsNullOrEmpty(sxenddate))
                TempHql.AppendFormat("and Sx_time>='{0}' and Sx_time<='{1}'", sxstartdate + " 00:00:00", sxenddate + " 23:59:59");
            TempHql.AppendFormat(" and u.States='{0}'", "0");
            TempHql.AppendFormat("order by WL_SSD asc");
            PagerInfo<WL_DkxInfoView> list = Search();
            return list;
        }
        #endregion


        /// <summary>
        /// 工程回访 SID 页面
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Name2"></param>
        /// <param name="RVtype"></param>
        /// <param name="isrv"></param>
        /// <param name="SId"></param>
        /// <returns></returns>
        public PagerInfo<WL_DkxInfoView> GetWLdkxinfolistbyRVgcs(string Name, string Name2, string RVtype,string isrv, string SId,string sxstartdate,string sxenddate)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
            {
                TempHql.AppendFormat(" and u.Xs_jxsId ='{0}'", Name);
                if (!string.IsNullOrEmpty(Name2))
                {
                    TempHql.AppendFormat(" or u.Xs_jxsId ='{0}'", Name2);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Name2))
                    TempHql.AppendFormat(" and u.Xs_jxsId ='{0}'", Name2);
            }
            if (!string.IsNullOrEmpty(SId))
                TempHql.AppendFormat(" and u.WL_SSD='{0}'", SId);
            if (isrv=="0")
                TempHql.AppendFormat(" and u.WL_zt='{0}' ","2");
            if (isrv == "1")
                TempHql.AppendFormat(" and u.WL_zt='{0}' and u.Id not in(select DKXId from _20150510WL_ReturnVisit where RVtype='{1}' )","2",RVtype);
            if(isrv=="2")
                TempHql.AppendFormat(" and u.WL_zt='{0}' and u.Id in(select DKXId from _20150510WL_ReturnVisit where RVtype='{1}' )", "2", RVtype);
            if (!string.IsNullOrEmpty(sxstartdate))
                TempHql.AppendFormat("and Sx_time>='{0}' and Sx_time<='{1}'", sxstartdate + " 00:00:00", sxenddate + " 23:59:59");
            TempHql.AppendFormat(" and u.States='{0}'", "0");
            TempHql.AppendFormat("order by Sx_time asc");
            PagerInfo<WL_DkxInfoView> list = Search();
            return list;
        }




        #region //根据SID码 查找电控箱信息
        public WL_DkxInfoView GetDkxinfo(string Sid)
        {
            List<WL_DkxInfo> list = HibernateTemplate.Find<WL_DkxInfo>("from WL_DkxInfo where WL_SSD='"+Sid +"'") as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            return null;
        } 
        #endregion

        #region //根据sid查询sid信息
        /// <summary>
        /// SID
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public WL_DkxInfoView GetDkxinfobySID(string SID)
        {
            string hqlstr = string.Format("from WL_DkxInfo where WL_SSD='{0}'",SID);
            List<WL_DkxInfo> list = HibernateTemplate.Find<WL_DkxInfo>(hqlstr) as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            return null;
        }
        #endregion

        #region //查找全部的未上线已售的电控箱数据
        /// <summary>
        /// 查找全部的未上线已售的电控箱数据
        /// </summary>
        /// <returns></returns>
        public IList<WL_DkxInfoView> Getscwsxinfo()
        {
            string TmpHQL = "from WL_DkxInfo where WL_zt='1'";
            List<WL_DkxInfo> list = HibernateTemplate.Find<WL_DkxInfo>(TmpHQL) as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            return listmodel;
        } 
        #endregion

        #region //查找已经扫码出售的电控箱数据
        /// <summary>
        /// 查找已经扫码出售的电控箱数据
        /// </summary>
        /// <returns></returns>
        public IList<WL_DkxInfoView> GetYSsidinfo()
        {
            string TmpHQL = "from WL_DkxInfo where Is_xs='1'";
            List<WL_DkxInfo> list = HibernateTemplate.Find<WL_DkxInfo>(TmpHQL) as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        /// <summary>
        /// 查询没有上线的数据根据
        /// </summary>
        /// <param name="startsum">起始条数</param>
        /// <param name="endsum"></param>
        /// <returns></returns>
        public IList<WL_DkxInfoView> Getwl_dkxbysum(string startsum, string endsum)
        {
            string temHql;
            temHql = string.Format(" from WL_DkxInfo where Is_sx='0'");//查询全部没有上线的数据
            IQuery query = Session.CreateQuery(temHql);
            query.SetFirstResult(Convert.ToInt32(startsum));
            query.SetMaxResults(Convert.ToInt32(endsum));
            List<WL_DkxInfo> list = query.List<WL_DkxInfo>() as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }



        #region //根据订单明细Id 查找出货数量
        public int GetChcunotbyorderId(string OrdermxId)
        {
            string tempHql = "from  WL_DkxInfo where OrdermxId='"+OrdermxId+"'";
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //根据ERP拣配单单号查找出SID扫码数量
        public int GetGetChcunotbyjpno(string jpno)
        {
            string tempHql = "from  WL_DkxInfo where erp_JPorderno='" + jpno + "'";
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //根据ERP拣配单单号查询出SID明细
        /// <summary>
        /// sid 的明细
        /// </summary>
        /// <param name="jpno">拣配单单号</param>
        /// <returns></returns>
        public IList<WL_DkxInfoView> GetChSIDinfotbyjpno(string jpno)
        {
            string TmpHQL = "from  WL_DkxInfo where erp_JPorderno='" + jpno + "'";
           
            List<WL_DkxInfo> list = HibernateTemplate.Find<WL_DkxInfo>(TmpHQL) as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion 

        #region //省份销售数量查询 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfId">省份Id</param>
        /// <param name="month">年月开始</param>
        /// <param name="type">0 销售  1上线  2累计销售  3累计上线</param>
        /// <returns></returns>
        public int GetdkxxscountbySFIdandsj(string sfId, string month,string Enddate, int type)
        {
            string tempHql;
            if (type == 0)//销售情况
            {
                if (!string.IsNullOrEmpty(month))
                {
                    tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Id from NACustomerinfo where qyId='{0}') and Xs_datetime>='{1}' and Xs_datetime<='{2}'", sfId, month + " 00:00:00", Enddate + " 23:59:59");
                }
                else
                {
                    tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Id from NACustomerinfo where qyId='{0}')", sfId);
                }
            }
            else if(type==1)//上线情况
            {
                if (!string.IsNullOrEmpty(month))
                {
                    tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Id from NACustomerinfo where qyId='{0}') and Sx_time>='{1}' and Sx_time<='{2}' and WL_zt='2'", sfId, month + " 00:00:00", Enddate + " 23:59:59");
                }
                else
                {
                    tempHql = string.Format("from WL_DkxInfo where  Xs_jxsId in (select Id from NACustomerinfo where qyId='{0}') and WL_zt='2'", sfId);
                }
            }
            else if (type == 2)//累计销售情况
            {
                tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Id from NACustomerinfo where qyId='{0}')", sfId);
            }
            else//累计上线情况
            {
                tempHql = string.Format("from WL_DkxInfo where  Xs_jxsId in (select Id from NACustomerinfo where qyId='{0}') and WL_zt='2'", sfId);
            }
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        } 
        #endregion

        #region //
        /// <summary>
        /// 查询经销商该时间内 买了多少套以及上线了多少套
        /// </summary>
        /// <param name="KhId">经销商Id</param>
        /// <param name="month">年月</param>
        /// <param name="type">经销权和只买一送一</param>
        /// <returns></returns>
        public int GetdkxxscounybyKHIdandsj(string KhId, string month,string endtime, int type)
        {
            string tempHql;
            if (type == 0)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    tempHql = string.Format("from WL_DkxInfo where Xs_jxsId ='{0}' and Xs_datetime>='{1}' and Xs_datetime<='{2}'", KhId, month + " 00:00:00", endtime + " 23:59:59");
                }
                else
                {
                    tempHql = string.Format("from WL_DkxInfo where  Xs_jxsId ='{0}'",KhId);
                }
            }
            else if(type ==1)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    tempHql = string.Format("from WL_DkxInfo where Xs_jxsId ='{0}' and Sx_time>='{1}' and  Sx_time<='{2}' and WL_zt='2'", KhId, month + " 00:00:00", endtime + " 23:59:59");
                }
                else
                {
                    tempHql = string.Format("from WL_DkxInfo where  Xs_jxsId ='{0}' and WL_zt='2' ",KhId);
                }
            }
            else if (type ==2)
            {
                tempHql = string.Format("from WL_DkxInfo where  Xs_jxsId ='{0}'", KhId);
            }
            else
            {
                tempHql = string.Format("from WL_DkxInfo where  Xs_jxsId ='{0}' and WL_zt='2' ", KhId);
            }

            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        } 
        #endregion

        #region //根据订单Id 查询SID
        public IList<WL_DkxInfoView> GetSIDbyOrderId(string orderId)
        {
            string tempHql = "from WL_DkxInfo where OrdermxId in ('" + orderId + "')";
            List<WL_DkxInfo> list = HibernateTemplate.Find<WL_DkxInfo>(tempHql) as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            return null;
        } 
        #endregion

        #region //通过电商平台 Uid 查找客户ID 查找全部的Id查找电控箱信息
        /// <summary>
        /// 统计上线电控箱
        /// </summary>
        /// <param name="UId"></param>
        /// <returns></returns>
        public IList<WL_DkxInfoView> GetSIDbyKhId(string UId,int SetFrist,int SetMax)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Id from NACustomerinfo where DsUid='{0}') and Is_sx='1'", UId);
            IQuery query = Session.CreateQuery(tempHql);
            query.SetFirstResult(SetFrist);
            query.SetMaxResults(SetMax);
            List<WL_DkxInfo> list = query.List<WL_DkxInfo>() as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            return null;
        }

        /// <summary>
        /// 统计上线数量
        /// </summary>
        /// <param name="UId"></param>
        /// <returns></returns>
        public int GetcountdkxbyUId(string UId)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Id from NACustomerinfo where DsUid='{0}') and Is_sx='1'", UId);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        //微信统计数据
        #region //微信统计
        /// <summary>
        /// 经销商全部的电控箱
        /// </summary>
        /// <param name="OpenID">微信公众号ID</param>
        /// <returns></returns>
        public int WXGetcountdkxbyCusId(string OpenID)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Person_Id from WX_OpenID where OpenId='{0}') and Sort='0'", OpenID);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }

        /// <summary>
        /// 经销商本月上线数量
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <param name="datetime">当前时间年月</param>
        /// <returns></returns>
        public int WXdkxsxcountbyCusIdM(string OpenID, string datetime,string Enddatetime)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Person_Id from WX_OpenID where OpenId='{0}') and Sort='0' and WL_zt='2' and Sx_time>='{1}' and  Sx_time<='{2}'", OpenID, datetime + " 00:00:00", Enddatetime + " 23:59:59");
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}",tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }

        /// <summary>
        /// 全部上线的数量
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <returns></returns>
        public int WXdkxallsxbyCusId(string OpenID)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Person_Id from WX_OpenID where OpenId='{0}') and Sort='0' and  WL_zt='2' ", OpenID);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }

        /// <summary>
        /// 本月收益
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <param name="datetime">当前年月</param>
        /// <returns></returns>
        public int WXdkxjfbyCusIdM(string OpenID, string datetime,string Enddatetime)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Person_Id from WX_OpenID where OpenId='{0}') and Sort='0' and  WL_zt='2' and Jf_starttime>='{1}' and Jf_starttime<='{2}'", OpenID, datetime + "  00:00:00", Enddatetime + " 23:59:59");
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            int sy=count*50;
            return sy;
        }


        /// <summary>
        /// 次月预计收益
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <param name="datetime">当前年月</param>
        /// <returns></returns>
        public int WXdkxjfbyCusIdCM(string OpenID, string datetime)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Person_Id from WX_OpenID where OpenId='{0}') and Sort='0' and  WL_zt='2'  and  Jf_endtime<='{1}'", OpenID, datetime + " 23:59:59");
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*) {0}", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            int sy = count * 50;
            return sy;
        }


        /// <summary>
        /// 累计收益
        /// </summary>
        /// <param name="OpenID">微信公众号</param>
        /// <returns></returns>
        public int WXdkxcountsybyCusId(string OpenID)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId in (select Person_Id from WX_OpenID where OpenId='{0}') and Sort='0' and  WL_zt='2' ", OpenID);
            IQuery queryCount = Session.CreateQuery(string.Format("select SUM(Jf_cs) {0}", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            int sy = count * 50;
            return sy;
        }
        #endregion

        #region //物联网电控箱的销售上线统计
        //各省份物联网销售上线情况
        public string DkxsaleOnlineJson(string month, string Enddate)
        {
            //查处所有省份信息
            string Jsondate;
            string p_HQLstr = "from NA_Qyinfo where Pid='' order by Sort desc";
            List<NA_Qyinfo> list = HibernateTemplate.Find<NA_Qyinfo>(p_HQLstr) as List<NA_Qyinfo>;
            //
            List<saleonline> solist = new List<saleonline>();
            //根据省份查询各个省份的销售上线情况
            foreach (var item in list)
            {
                saleonline somodel = new saleonline();
                somodel.Id = item.Id;//省份Id
                somodel.Pname = item.Qyname;//省份名称
                somodel.salecount = GetdkxxscountbySFIdandsj(item.Id, month, Enddate, 0).ToString();//销售情况
                somodel.onlinecount = GetdkxxscountbySFIdandsj(item.Id, month, Enddate, 1).ToString();//当前时间内的上线情况
                somodel.LJsalecount = GetdkxxscountbySFIdandsj(item.Id, month, Enddate, 2).ToString();//累计销售情况
                somodel.LJonlinecount = GetdkxxscountbySFIdandsj(item.Id, month, Enddate, 3).ToString();//累计上线情况
                solist.Add(somodel);
            }
            Jsondate = JsonConvert.SerializeObject(solist);
            return Jsondate;
        }

        //各省销售上线情况临时变量model
        public class saleonline
        {
            public virtual string Id { get; set; }//区域Id

            public virtual string Pname { get; set; }//省份名称

            public virtual string salecount { get; set; }//销售数量

            public virtual string onlinecount { get; set; }//上线数量

            public virtual string LJsalecount { get; set; }//累计销售

            public virtual string LJonlinecount { get; set; }//累计上线
        } 


        //各经销商的销售上线数据统计(通过省份顺序)
        public string CusDkxsaleOnlineJson(string month, string Enddate)
        {
            //查处所有省份信息
            string Jsondate;
            string p_HQLstr = "from NA_Qyinfo where Pid='' order by Sort desc";
            List<NA_Qyinfo> list = HibernateTemplate.Find<NA_Qyinfo>(p_HQLstr) as List<NA_Qyinfo>;

            List<Cussaleonline> csolist = new List<Cussaleonline>();
            foreach (var item in list)
            {
                //临时实体
              
                //初始化客户list
                List<NACustomerinfo> cuslist = new List<NACustomerinfo>();
                cuslist = CuslistmodelbyP_Id(item.Id);
                if (cuslist != null) {
                    foreach (var Cusitem in cuslist)
                    {
                        Cussaleonline csomodel = new Cussaleonline();
                        csomodel.Id = item.Id;//区域Id
                        csomodel.Pname = item.Qyname;//区域名称
                        csomodel.CusId = Cusitem.Id;//客户Id
                        csomodel.CusName = Cusitem.Name;//客户名称
                        csomodel.CusType = Cusitem.isjxs.ToString();//客户类型
                        csomodel.salecount = GetdkxxscounybyKHIdandsj(Cusitem.Id, month, Enddate, 0).ToString();//销售数量
                        csomodel.onlinecount = GetdkxxscounybyKHIdandsj(Cusitem.Id, month, Enddate, 1).ToString();//上线数量
                        csomodel.LJsalecount = GetdkxxscounybyKHIdandsj(Cusitem.Id, month, Enddate, 2).ToString();//累计销售
                        csomodel.LJonlinecount = GetdkxxscounybyKHIdandsj(Cusitem.Id, month, Enddate, 3).ToString();//累计上线
                        csolist.Add(csomodel);
                    }
                }
            }
            Jsondate = JsonConvert.SerializeObject(csolist);
            return Jsondate;
        }

        //各个经销商销售上线情况临时变量model
        public class Cussaleonline 
        {
            public virtual string Id { get; set; }//省份Id

            public virtual string Pname { get; set; }//省份名称

            public virtual string CusId { get; set; }//客户Id

            public virtual string CusName { get; set; }//客户名称

            public virtual string CusType { get; set; }//客户类型（经销权，一般经销商）

            public virtual string salecount { get; set; }//销售数量

            public virtual string onlinecount { get; set; }//上线数量

            public virtual string LJsalecount { get; set; }//累计销售

            public virtual string LJonlinecount { get; set; }//累计上线
                 
        }


        //根据省份查找物联网经销商和一般经销商
        public List<NACustomerinfo> CuslistmodelbyP_Id(string P_Id)
        {
            string tmpHQL = string.Format("from NACustomerinfo where qyId='{0}' and isjxs in('1','2')",P_Id);
            List<NACustomerinfo> list = HibernateTemplate.Find<NACustomerinfo>(tmpHQL) as List<NACustomerinfo>;
            return list;
        }

        #endregion

        #region //按照查找最新更新的SID
        public WL_DkxInfoView Getwldkxinfonewdate()
        {
            string tempHql = "from  WL_DkxInfo order by sidxh desc";
            List<WL_DkxInfo> list = Session.CreateQuery(tempHql).SetFirstResult(0).SetMaxResults(1).List<WL_DkxInfo>() as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
            {
                return listmodel[0];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region //查询已经上线 监控点为空的数据
        public IList<WL_DkxInfoView> Getwldkxinfosxjkdnull()
        {
            string tempHql = "from WL_DkxInfo where Is_sx='1' and monitor_name is null";
            List<WL_DkxInfo> list = Session.CreateQuery(tempHql).List<WL_DkxInfo>() as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion


        #region //根据经销商Id 查找经销商名下的sid数量
        public int jxqGetcountYGdkxsumbyjxsId(string jxsId)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId='{0}'",jxsId);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0}", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //根据经销商Id 查找已经上线的sid数量
        public int jxqGetcountSXdkxsumbyjxsId(string jxsId)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId='{0}' and  WL_zt='2'", jxsId);
            IQuery queryCount = Session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            int count = Convert.ToInt32(queryCount.UniqueResult());
            return count;
        }
        #endregion

        #region //根据经销商Id 查找已经SID数量
        /// <summary>
        /// 根据经销商Id 查找已经SID数量
        /// </summary>
        /// <param name="jxsId">经销商Id</param>
        /// <returns></returns>
        public IList<WL_DkxInfoView> GetSIDlistbyjxsId(string jxsId)
        {
            string tempHql;
            tempHql = string.Format("from WL_DkxInfo where Xs_jxsId='{0}' ", jxsId);
            List<WL_DkxInfo> list = Session.CreateQuery(tempHql).List<WL_DkxInfo>() as List<WL_DkxInfo>;
            IList<WL_DkxInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

      

    }
}
