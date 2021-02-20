using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IDKX_DKXtypeandgcsDao:IBaseDao<DKX_DKXtypeandgcsView>
    {
                /// <summary>
        /// 根据工程师的Id 查找对应的关系
        /// </summary>
        /// <param name="gcsId">工程师Id</param>
        /// <returns></returns>
        List<DKX_DKXtypeandgcs> GetdkxtypelistbygcsId(string gcsId);

               /// <summary>
        /// 根据工程师的Id 查找对应的关系
        /// </summary>
        /// <param name="gcsId"></param>
        /// <returns></returns>
        IList<DKX_DKXtypeandgcsView> GetdkxtypeandgcslistViewbygcsId(string gcsId);
    }
}
