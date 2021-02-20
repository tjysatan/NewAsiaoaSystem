using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface ISysDepartmentDao : IBaseDao<SysDepartmentView> 
    {
       // PagingHelper<SysDepartment> Search(int pageIndex);

        string GetDepTreeData();

        string GetTreeDepartment();
        IList<SysDepartment> NGetList_idData(string id);

    }
}
