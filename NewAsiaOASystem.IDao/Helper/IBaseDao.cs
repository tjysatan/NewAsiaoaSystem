using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.ViewModel;
namespace NewAsiaOASystem.IDao
{
    public interface IBaseDao<T> where T : class
    {
        /// <summary>
        /// 设置页码 
        /// </summary>
        /// <param name="index"></param>
         void SetPagerPageIndex(int index);

         /// <summary>
         /// 设置每页数据行数 ，默认为30
         /// </summary>
         /// <param name="index"></param>
         void SetPagerPageSize(int index);

        PagerInfo<T> Search();

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Ninsert(T model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool NUpdate(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool NDelete(T model);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool NDelete(List<T> model);

        IList<T> NGetList();


        /// <summary>
        /// 获取记录总条数
        /// </summary>
        /// <returns></returns>
        int PageCount(string SqlStr);

        IList<T> NGetList_id(string id);
     

        T NGetModelById(string keyId);
    }
}
