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
    public class AppPoint_infoDao : ServiceConversion<Point_infoView, Point_info>, IAppPoint_infoDao
    {
        /// <summary>
        /// GIS经纬度坐标上传
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public string GisUpload(Point_infoView point)
        {
            ISysPersonDao _ISysPersonDao = ContextRegistry.GetContext().GetObject("SysPersonDao") as ISysPersonDao;
            Point_info pointModel = GetData(point);
            pointModel.P_Time = DateTime.Now;
            pointModel.sysperson = _ISysPersonDao.NGetModeldataById(pointModel.P_Id);
           if(base.insert(pointModel))
               return "{\"status\":\"true\"}";
           else
               return "{\"status\":\"false\"}";
        }

    }
}
