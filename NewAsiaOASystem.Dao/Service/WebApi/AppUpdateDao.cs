using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate;
using Spring.Context.Support;
using System.IO;

namespace NewAsiaOASystem.Dao
{
    public class AppUpdateDao : DBService<Bb_Update>, IAppUpdateDao
    {
        #region  App下载
        /// <summary>
        /// App下载
        /// </summary>
        /// <param name="Bb_No"></param>
        /// <returns></returns>
        public string AppUpload(string Bb_No)
        {
            try
            {
                //参数为空直接返回或者当字符串中不存在类型时(下划线_后面表示要下载的APP类型包括Android，iphone,wp)也直接返回
                if (string.IsNullOrEmpty(Bb_No) || Bb_No.IndexOf("_") < 0)
                    return "{\"status\":\"false\"}";
                string FileType = Bb_No.Split('_')[1];
                List<Bb_Update> list = base.GetList_HQL(string.Format(" where u.ApkType='{0}' order by u.Bb_Time desc", FileType)) as List<Bb_Update>;
                Bb_Update bb_update = list[0];
                //版本号相等表示服务器无更新
                if (bb_update.Bb_NO.Equals(Bb_No))
                {
                    return "{\"status\":\"no\",\"version\":\"" + bb_update.Bb_NO + "\",\"url\":\"" + bb_update.Bb_Url + "\"}";
                }
                else
                {
                    return "{\"status\":\"yes\",\"version\":\"" + bb_update.Bb_NO + "\",\"url\":\"" + bb_update.Bb_Url + "\"}";
                }
            }

            catch
            {
                return "{\"status\":\"false\"}";
            }
        }

        #endregion

    }
}
