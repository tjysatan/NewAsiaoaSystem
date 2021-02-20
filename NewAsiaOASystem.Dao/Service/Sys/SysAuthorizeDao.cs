using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;


namespace NewAsiaOASystem.Dao
{

    public class SysAuthorizeDao : ServiceConversion<SysAuthorizeView, SysAuthorize>, ISysAuthorizeDao 
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysAuthorizeView model)
        {
            SysAuthorize listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysAuthorizeView model)
        {
            SysAuthorize listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysAuthorizeView model)
        {
            SysAuthorize listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysAuthorizeView> model)
        {
            IList<SysAuthorize> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysAuthorizeView> NGetList()
        {
            List<SysAuthorize> listdata = base.GetList() as List<SysAuthorize>;
            IList<SysAuthorizeView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysAuthorizeView> NGetList_id(string id)
        {
            List<SysAuthorize> list = base.GetList_id(id) as List<SysAuthorize>;
            IList<SysAuthorizeView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<SysAuthorize> NGetListID(string id)
        {
            IList<SysAuthorize> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysAuthorizeView NGetModelById(string id)
        {
            SysAuthorizeView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public  string GetTreeAuthorize()
        {
            IList<SysAuthorizeView> list = NGetList();
            Base<SysAuthorizeView> _Base = new Base<SysAuthorizeView>();
            string[] pars = new string[] { "Name", "CreatePerson", "CreateTime", "UpdatePerson", "UpdateTime", "Status" };
            if (null == list)
                return "[]";
            string json = _Base.AddNode(list.ToList<SysAuthorizeView>(), "Id", "ParentId", null, 1, pars);
            return json;
        }

        /// <summary>
        /// 获取树形菜单数据
        /// </summary>
        /// <returns></returns>
        public string GetSysAuthorizeTreeData()
        { 
            Base<SysAuthorizeView> _Base = new Base<SysAuthorizeView>();
            IList<SysAuthorizeView> List=NGetList();
            if (List == null)
                return "[]";
            string json = _Base.AddNode(List.ToList<SysAuthorizeView>(), "Id", "ParentId", null, "Name", 1);
            return json;
        }
    }
}
