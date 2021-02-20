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
    public class DKX_DDtypeinfoDao:ServiceConversion<DKX_DDtypeinfoView,DKX_DDtypeinfo>,IDKX_DDtypeinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(DKX_DDtypeinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DKX_DDtypeinfoView GetView(DKX_DDtypeinfo data)
        {
            DKX_DDtypeinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(DKX_DDtypeinfoView model)
        {
            DKX_DDtypeinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(DKX_DDtypeinfoView model)
        {
            DKX_DDtypeinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(DKX_DDtypeinfoView model)
        {
            DKX_DDtypeinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<DKX_DDtypeinfoView> model)
        {
            IList<DKX_DDtypeinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<DKX_DDtypeinfoView> NGetList()
        {
            List<DKX_DDtypeinfo> listdata = base.GetList() as List<DKX_DDtypeinfo>;
            IList<DKX_DDtypeinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<DKX_DDtypeinfoView> NGetList_id(string id)
        {
            List<DKX_DDtypeinfo> list = base.GetList_id(id) as List<DKX_DDtypeinfo>;
            IList<DKX_DDtypeinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<DKX_DDtypeinfo> NGetListID(string id)
        {
            IList<DKX_DDtypeinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DKX_DDtypeinfoView NGetModelById(string id)
        {
            DKX_DDtypeinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        #region //电控箱种类管理分页列表数据
        /// <summary>
        /// 电控箱种类管理分页列表数据
        /// </summary>
        /// <param name="Name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="type">是否禁用</param>
        /// <returns></returns>
        public PagerInfo<DKX_DDtypeinfoView> Getdkxtypelistpage(string Name, string start)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
                TempHql.AppendFormat(" and Name like '%{0}%'", Name);
            if (!string.IsNullOrEmpty(start))
                TempHql.AppendFormat(" and start='{0}'",start);
            else
                TempHql.AppendFormat(" and start='0'");
            TempHql.AppendFormat("order by u.Sort asc");
            PagerInfo<DKX_DDtypeinfoView> list = Search();
            return list;
        }
        #endregion


        /// <summary>
        /// 设置电控箱类型下拉框值(编辑页面时)
        /// </summary>
        /// <param name="gcsId">需要选中的Value值</param>
        /// <returns></returns>
        public string DKXtypeAlbumDropdown(string gcsId)
        {
             IDKX_DKXtypeandgcsDao _IDKX_DKXtypeandgcsDao = ContextRegistry.GetContext().GetObject("DKX_DKXtypeandgcsDao") as IDKX_DKXtypeandgcsDao;
 //获取工程师所对应的电控箱的类型
            List<DKX_DKXtypeandgcs> DKXtypeList = null;
            if (!string.IsNullOrEmpty(gcsId))
            {
                DKXtypeList = _IDKX_DKXtypeandgcsDao.GetdkxtypelistbygcsId(gcsId);
            }
            //获取系统所有的电控箱类型
            List<DKX_DDtypeinfo> AllDKXList = base.GetList() as List<DKX_DDtypeinfo>;
            try 
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (AllDKXList != null)
                {
                    foreach (var Role in AllDKXList)
                    {
                        if (DKXtypeList != null && Role != null)
                        {
                            var temp = DKXtypeList.Find(o => o != null && o.DkxtypeId == Role.Id);
                            if (temp != null)
                                sb.Append("{id:'" + Role.Id + "',name:'" + Role.Name + "',checked:true},");
                            else
                                sb.Append("{id:'" + Role.Id + "',name:'" + Role.Name + "',checked:false},");
                        }
                        else
                        {
                            sb.Append("{id:'" + Role.Id + "',name:'" + Role.Name + "',checked:false},");
                        }
                    }
                    if (sb.Length > 1)
                        sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        #region //查询电控箱类型的全部数据
        /// <summary>
        /// 按序查找全部电控箱类型的数据
        /// </summary>
        /// <returns></returns>
        public IList<DKX_DDtypeinfoView> GetALLdkxtypejson()
        {
            string Hqlstr = string.Format(" from DKX_DDtypeinfo order by Sort asc");
            List<DKX_DDtypeinfo> list = HibernateTemplate.Find<DKX_DDtypeinfo>(Hqlstr) as List<DKX_DDtypeinfo>;
            IList<DKX_DDtypeinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //查找全部启用的电控箱类型的全部数据
        /// <summary>
        /// 按序查找全部电控箱类型的数据
        /// </summary>
        /// <returns></returns>
        public IList<DKX_DDtypeinfoView> GetALLQYdkxtypejson()
        {
            string Hqlstr = string.Format(" from DKX_DDtypeinfo where Start=0 order by Sort asc");
            List<DKX_DDtypeinfo> list = HibernateTemplate.Find<DKX_DDtypeinfo>(Hqlstr) as List<DKX_DDtypeinfo>;
            IList<DKX_DDtypeinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }
        #endregion

        #region //通过类型的编号查找数据
        public DKX_DDtypeinfoView Getdkxtypebytype(string type)
        {
            string Hqlstr = string.Format(" from DKX_DDtypeinfo where Type='{0}'",type);
            List<DKX_DDtypeinfo> list = HibernateTemplate.Find<DKX_DDtypeinfo>(Hqlstr) as List<DKX_DDtypeinfo>;
            IList<DKX_DDtypeinfoView> listmodel = GetViewlist(list);
            if (listmodel != null)
                return listmodel[0];
            else
                return null;
        }
        #endregion
    }
}
