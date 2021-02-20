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
    public class CG_DetailinfoDao:ServiceConversion<CG_DetailinfoView,CG_Detailinfo> ,ICG_DetailinfoDao
    {
        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(CG_Detailinfo).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override CG_DetailinfoView GetView(CG_Detailinfo data)
        {
            CG_DetailinfoView view = ConvertToDTO(data);
            return view;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(CG_DetailinfoView model)
        {
            CG_Detailinfo listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(CG_DetailinfoView model)
        {
            CG_Detailinfo listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(CG_DetailinfoView model)
        {
            CG_Detailinfo listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<CG_DetailinfoView> model)
        {
            IList<CG_Detailinfo> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<CG_DetailinfoView> NGetList()
        {
            List<CG_Detailinfo> listdata = base.GetList() as List<CG_Detailinfo>;
            IList<CG_DetailinfoView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<CG_DetailinfoView> NGetList_id(string id)
        {
            List<CG_Detailinfo> list = base.GetList_id(id) as List<CG_Detailinfo>;
            IList<CG_DetailinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }



        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<CG_Detailinfo> NGetListID(string id)
        {
            IList<CG_Detailinfo> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CG_DetailinfoView NGetModelById(string id)
        {
            CG_DetailinfoView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        //根据采购单查找采购明细
        public IList<CG_DetailinfoView> Getcgmxlist(string id)
        {
            List<CG_Detailinfo> list = HibernateTemplate.Find<CG_Detailinfo>("from CG_Detailinfo where cgId='"+id+"'") as List<CG_Detailinfo>;
            IList<CG_DetailinfoView> listmodel = GetViewlist(list);
            return listmodel;
        }

    }
}
