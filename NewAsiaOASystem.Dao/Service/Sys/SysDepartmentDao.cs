using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using NHibernate;
using Newtonsoft.Json;

namespace NewAsiaOASystem.Dao
{
    public class SysDepartmentDao : ServiceConversion<SysDepartmentView, SysDepartment>, ISysDepartmentDao
    {

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override SysDepartmentView GetView(SysDepartment data)
        {
            SysDepartmentView view = ConvertToDTO(data); 
            return view;
        }

   

        /// <summary>
        /// TDO 转换成 DATA
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public override SysDepartment GetData(SysDepartmentView view)
        {
            SysDepartment data = new SysDepartment();
            data = ConvertToData(view);
            return data;
        } 

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysDepartmentView model)
        {
            SysDepartment listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysDepartmentView model)
        {
            SysDepartment listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysDepartmentView model)
        {
            SysDepartment listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysDepartmentView> model)
        {
            IList<SysDepartment> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysDepartmentView> NGetList()
        {
            List<SysDepartment> listdata = base.GetList() as List<SysDepartment>;
            IList<SysDepartmentView> listmodel = GetViewlist(listdata);
            return listmodel;
        }
 

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysDepartmentView> NGetList_id(string id)
        {
            List<SysDepartment> list = base.GetList_id(id) as List<SysDepartment>;
            IList<SysDepartmentView> listmodel = GetViewlist(list);
            return listmodel;
        }

        //public IList<SysDepartment> NGetListdata_id(string id)
        //{
        //    List<SysDepartment> list = base.GetList_id(id) as List<SysDepartment>;
        //    return list;
        //}

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysDepartment> NGetList_idData(string id)
        {
            List<SysDepartment> list = base.GetList_id(id) as List<SysDepartment>;
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysDepartmentView NGetModelById(string id)
        {
            SysDepartmentView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public string GetTreeDepartment()
        {
            IList<SysDepartmentView> list = NGetList();
            Base<SysDepartmentView> _Base = new Base<SysDepartmentView>();
            string[] pars = new string[] { "Name", "Description", "CreatePerson", "CreateTime", "UpdatePerson", "UpdateTime", "State" };
            if (null == list)
                return "[]";
            string json = _Base.AddNode(list.ToList<SysDepartmentView>(), "Id", "ParentId", null, 1, pars);
            return json;
        }


        /// <summary>
        /// 获取树形菜单数据
        /// </summary>
        /// <returns></returns>
        public string GetDepTreeData()
        {
            List<SysDepartment> list = base.GetList() as List<SysDepartment>;
            List<SysDepartmentView> listView = GetViewlist(list) as List<SysDepartmentView>;
            Base<SysDepartmentView> _Base = new Base<SysDepartmentView>();
            string str = _Base.AddNode(listView, "Id", "ParentId", null, "Name", 1);
            return str;
        }


    }
}
