using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.Dao
{
    public class SysButtonDao: ServiceConversion<SysButtonView,SysButton>, ISysButtonDao 
    {
        public override SysButtonView GetView(SysButton data)
        {
            SysButtonView listmodel=ConvertToDTO(data);
            return listmodel;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysButtonView model)
        {
            SysButton listmodel =GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysButtonView model)
        {
            SysButton listmodel =GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysButtonView model)
        {
            SysButton listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysButtonView> model)
        {
            IList<SysButton> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysButtonView> NGetList()
        {
            List<SysButton> listdata = base.GetList() as List<SysButton>;
            IList<SysButtonView> listmodel = GetViewlist(listdata);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysButtonView> NGetList_id(string id)
        {
            List<SysButton> list = base.GetList_id(id) as List<SysButton>;
            IList<SysButtonView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysButton> NGetListSysButton(string id)
        {
            List<SysButton> list = base.GetList_id(id) as List<SysButton>;
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysButtonView NGetModelById(string id)
        {
            SysButtonView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }
    }
}
