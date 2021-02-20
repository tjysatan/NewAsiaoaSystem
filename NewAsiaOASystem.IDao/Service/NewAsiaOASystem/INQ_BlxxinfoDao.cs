using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface INQ_BlxxinfoDao:IBaseDao<NQ_BlxxinfoView>
    {
        PagerInfo<NQ_BlxxinfoView> GetblxxinfoList(string Name, SessionUser user);

        /// <summary>
        /// 不良现象下拉框数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string BLXXAlbumDropdown(string Id);
       
        #region //查找不量现象的在用的全部数据
        /// <summary>
        /// 查找不量现象的在用的全部数据
        /// </summary>
        /// <returns></returns>
        IList<NQ_BlxxinfoView> Getblxxallzydata(); 
        #endregion
    }
}
