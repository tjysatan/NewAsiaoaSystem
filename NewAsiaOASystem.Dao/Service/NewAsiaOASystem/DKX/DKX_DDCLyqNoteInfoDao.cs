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
    public class DKX_DDCLyqNoteInfoDao:ServiceConversion<DKX_DDCLyqNoteInfoView,DKX_DDCLyqNoteInfo>,IDKX_DDCLyqNoteInfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_DDCLyqNoteInfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_DDCLyqNoteInfoView GetView(DKX_DDCLyqNoteInfo data)
        {
            DKX_DDCLyqNoteInfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_DDCLyqNoteInfoView model)
        {
            DKX_DDCLyqNoteInfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_DDCLyqNoteInfoView model)
        {
            DKX_DDCLyqNoteInfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_DDCLyqNoteInfoView model)
        {
            DKX_DDCLyqNoteInfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_DDCLyqNoteInfoView> model)
        {
            IList<DKX_DDCLyqNoteInfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_DDCLyqNoteInfoView> NGetList()
        {
            List<DKX_DDCLyqNoteInfo> listdata = base.GetList() as List<DKX_DDCLyqNoteInfo>;
            IList<DKX_DDCLyqNoteInfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_DDCLyqNoteInfoView> NGetList_id(string id)
        {
            List<DKX_DDCLyqNoteInfo> list = base.GetList_id(id) as List<DKX_DDCLyqNoteInfo>;
            IList<DKX_DDCLyqNoteInfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_DDCLyqNoteInfo> NGetListID(string id)
        {
            IList<DKX_DDCLyqNoteInfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_DDCLyqNoteInfoView NGetModelById(string id)
        {
            DKX_DDCLyqNoteInfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        #region //查询今天是否有该类型的提醒数据
        /// <summary>
        /// 查询今天是否有该类型的提醒数据
        /// </summary>
        /// <param name="type">提醒类型</param>
        /// <param name="fstype">发送时段 0 上午 1 下午</param>
        /// <returns></returns>
        public bool JCFSshujubytypeandfstypw(string type, string fstype)
        {
            string Hqlstr = string.Format(" from DKX_DDCLyqNoteInfo where Type='{0}' and fstype='{1}' and   DateDiff(dd,C_time,getdate())=0", type, fstype);
            List<DKX_DDCLyqNoteInfo> list = HibernateTemplate.Find<DKX_DDCLyqNoteInfo>(Hqlstr) as List<DKX_DDCLyqNoteInfo>;
            IList<DKX_DDCLyqNoteInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return false;
            else
                return true;
        }
        #endregion


        #region //逾期处理提醒通知的数据分页列表
        /// <summary>
        /// 逾期处理提醒通知的数据分页列表
        /// </summary>
        /// <param name="DD_Bianhao">订单号</param>
        /// <param name="khname">客户名称</param>
        /// <param name="yqtype">逾期类型</param>
        /// <param name="jsname">通知人</param>
        /// <param name="tztimetype">发送时间断 0 上网 1下午</param>
        /// <param name="starttime">通知时间</param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public PagerInfo<DKX_DDCLyqNoteInfoView> GetddclyqNotedataPagerlist(string DD_Bianhao, string khname, string yqtype, string jsname,string tztimetype, string starttime, string endtime)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(DD_Bianhao))
                TempHql.AppendFormat(" and DD_Id in (select Id from DKX_DDinfo where DD_Bianhao = '{0}')", DD_Bianhao);
            if (!string.IsNullOrEmpty(khname))
                TempHql.AppendFormat(" and DD_Id in (select Id from DKX_DDinfo where KHname like '%{0}%')",khname);
            if (!string.IsNullOrEmpty(yqtype))
                TempHql.AppendFormat(" and Type='{0}'",yqtype);
            if (!string.IsNullOrEmpty(jsname))
                TempHql.AppendFormat(" and Jsname in(select Id from SysPerson where UserName='{0}')",jsname);
            if (!string.IsNullOrEmpty(tztimetype))
                TempHql.AppendFormat(" and fstype='{0}'",tztimetype);
            if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
                TempHql.AppendFormat("and C_time>='{0}' and C_time<='{1}'", starttime + " 00:00:00", endtime + " 23:59:59");
            TempHql.AppendFormat("and fsdxtype='0'");
            TempHql.AppendFormat("order by u.C_time desc");
            PagerInfo<DKX_DDCLyqNoteInfoView> list = Search();
            return list;
        } 
        #endregion

        #region //根据订单Id查询逾期通知数据
        /// <summary>
        /// 根据订单Id查询逾期通知数据
        /// </summary>
        /// <param name="DDid">订单Id</param>
        /// <returns></returns>
        public IList<DKX_DDCLyqNoteInfoView> GetyqtzinfobyddId(string DDid)
        {
            string Hqlstr = string.Format(" from DKX_DDCLyqNoteInfo where DD_Id='{0}' and fsdxtype='0' order by C_time desc", DDid);
            List<DKX_DDCLyqNoteInfo> list = HibernateTemplate.Find<DKX_DDCLyqNoteInfo>(Hqlstr) as List<DKX_DDCLyqNoteInfo>;
            IList<DKX_DDCLyqNoteInfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel;
            else
                return null;
        }
        #endregion
    }
}
