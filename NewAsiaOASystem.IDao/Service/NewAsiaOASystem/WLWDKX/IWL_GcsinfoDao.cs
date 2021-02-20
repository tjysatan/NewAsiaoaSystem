using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWL_GcsinfoDao:IBaseDao<WL_GcsinfoView>
    {
        PagerInfo<WL_GcsinfoView> GetGcsinfoList(string Name, SessionUser user);

        /// <summary>
        ///工程商添加时检测是否 有重复的工程商
        /// </summary>
        /// <param name="Gcs_no"></param>
        /// <returns></returns>
        bool JccfbyGcs_no(string Gcs_no);



        WL_GcsinfoView GetGcsinfo(string Gcs_no);
    }
}
