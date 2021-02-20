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
    /// <summary>
    /// 不良现象
    /// </summary>
    public class NQ_BlxxinfoDao:ServiceConversion<NQ_BlxxinfoView,NQ_Blxxinfo>,INQ_BlxxinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(NQ_Blxxinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override NQ_BlxxinfoView GetView(NQ_Blxxinfo data)
        {
            NQ_BlxxinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(NQ_BlxxinfoView model)
        {
            NQ_Blxxinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(NQ_BlxxinfoView model)
        {
            NQ_Blxxinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(NQ_BlxxinfoView model)
        {
            NQ_Blxxinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<NQ_BlxxinfoView> model)
        {
            IList<NQ_Blxxinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<NQ_BlxxinfoView> NGetList()
        {
            List<NQ_Blxxinfo> listdata = base.GetList() as List<NQ_Blxxinfo>;
            IList<NQ_BlxxinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<NQ_BlxxinfoView> NGetList_id(string id)
        {
            List<NQ_Blxxinfo> list = base.GetList_id(id) as List<NQ_Blxxinfo>;
            IList<NQ_BlxxinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }




        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<NQ_Blxxinfo> NGetListID(string id)
        {
            IList<NQ_Blxxinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NQ_BlxxinfoView NGetModelById(string id)
        {
            NQ_BlxxinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        public PagerInfo<NQ_BlxxinfoView> GetblxxinfoList(string Name, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and u.Name like '%{0}%' ", Name);
            TempHql.AppendFormat(" order by C_Data desc");
            PagerInfo<NQ_BlxxinfoView> list = Search();
            return list;

        }

        #region //不良现象下拉框数据（查询页面需要）
        /// <summary>
        /// 不良现象下拉框数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string BLXXAlbumDropdown(string Id)
        {
            //获取所有不良现象
           // List<NQ_BlxxinfoView> AllBLXXList = NGetList() as List<NQ_BlxxinfoView>;
            List<NQ_Blxxinfo> AllBLXXList = HibernateTemplate.Find<NQ_Blxxinfo>("from NQ_Blxxinfo where  Status='1'") as List<NQ_Blxxinfo>;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (AllBLXXList != null)
                {
                    foreach (var blxx in AllBLXXList)
                    {
                        if (AllBLXXList != null && blxx != null)
                        {
                            var temp = AllBLXXList.Find(o => o != null && o.Id == blxx.Id);
                            if (temp != null)
                                sb.Append("{id:'" + blxx.Id + "',name:'" + blxx.Name + "',checked:false},");
                            else
                                sb.Append("{id:'" + blxx.Id + "',name:'" + blxx.Name + "',checked:true},");
                        }
                        else
                        {
                            sb.Append("{id:'" + blxx.Id + "',name:'" + blxx.Name + "',checked:false},");
                        }
                    }
                }
                sb.Append("]");
                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region //查找不量现象的在用的全部数据
        /// <summary>
        /// 查找不量现象的在用的全部数据
        /// </summary>
        /// <returns></returns>
        public IList<NQ_BlxxinfoView> Getblxxallzydata()
        {
            List<NQ_Blxxinfo> list = HibernateTemplate.Find<NQ_Blxxinfo>("from NQ_Blxxinfo where  Status='1'") as List<NQ_Blxxinfo>;
            IList<NQ_BlxxinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion
    }
}
