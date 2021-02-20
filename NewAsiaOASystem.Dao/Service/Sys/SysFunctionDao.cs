using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;
using Newtonsoft.Json;
using Spring.Context.Support;


namespace NewAsiaOASystem.Dao
{

    public class SysFunctionDao : ServiceConversion<SysFunctionView, SysFunction>, ISysFunctionDao
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysFunctionView model)
        {
            SysFunction listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysFunctionView model)
        {
            SysFunction listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysFunctionView model)
        {
            SysFunction listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysFunctionView> model)
        {
            IList<SysFunction> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysFunctionView> NGetList()
        {
            List<SysFunction> listdata = base.GetList() as List<SysFunction>;
            IList<SysFunctionView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 查询返回json
        /// </summary>
        /// <returns></returns>
        public string NGetListJson()
        {
            List<SysFunction> listdata = base.GetList() as List<SysFunction>;
            IList<SysFunctionView> listmodel = GetViewlist(listdata);
            return JsonConvert.SerializeObject(listmodel);
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysFunctionView> NGetList_id(string id)
        {
            List<SysFunction> list = base.GetList_id(id) as List<SysFunction>;
            IList<SysFunctionView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据直接返回数据库实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysFunction> NGetListModel(string id)
        {
            IList<SysFunction> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysFunctionView NGetModelById(string id)
        {
            SysFunctionView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckAction(string action)
        {
            IList<SysFunction> list = base.GetList_HQL(" where u.ActionUrl='" + action + "'");
            if (list.Count() > 0)
                return false;
            return true;
        }


        #region 根据用户ID 获取用户角色对应的 功能菜单
         public List<string> GetRole_Fun(string Id)
        {
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            SysPerson sysp = _ISysPersonDao.NGetModeldataById(Id);
            IList<SysRole> RoleList = sysp.Role;
            List<SysFunction> FunctionList = new List<SysFunction>();
            if (RoleList != null)
                foreach (var Role in RoleList)
                {
                    if (Role != null)
                        FunctionList.AddRange(Role.SysFun);
                }

            List<string> list = new List<string>();
            if (FunctionList != null)
            {
                foreach (var Function in FunctionList)
                {
                    list.Add(Function.ActionUrl);
                }
            }
            return list;
        }
        #endregion


    }
}
