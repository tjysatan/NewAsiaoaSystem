using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWX_Message_NewsDao:IBaseDao<WX_Message_NewsView>
    {
        IList<WX_Message_News> GetWX_Message_newby_id(string id);

        bool wx_MNdelete(List<WX_Message_News> mode);

        string InsertID(WX_Message_NewsView modelView);

        IList<WX_Message_NewsView> GetWX_Message_newby_Nid(string id);

        #region //回复地图时修改URL
        bool UpdateUrl(WX_Message_News model); 
        #endregion

          #region //根据图文的类型查找 单条
        WX_Message_NewsView GetWX_Message_newby_type(int type);
           #endregion

           #region //根据图文的类型查找多条
        IList<WX_Message_NewsView> GetWX_Message_newlistby_type(int type);
             #endregion

         #region //根据多个Id 查询多条记录
        IList<WX_Message_News> NGetListdata_id(string id);
        #endregion

        
        #region //查询分页数据
        PagerInfo<WX_Message_NewsView> GetCinfoList(string Title, string Description); 
        #endregion
    }
}
