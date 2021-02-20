using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.IDao
{
    public interface IVote_TitleDao : IBaseDao<Vote_TitleView>
    {
        #region 根据主题ID 查找对应的标题
        IList<Vote_TitleView> GetVotetitleby_sid(string sid);
        #endregion

        #region //保存后返回ID
        string InsertID(Vote_TitleView modelView); 
        #endregion

        #region //删除
        bool VotedataDelete(Vote_Title model); 
        #endregion

        #region //批量删除
        bool VotedataDelete(List<Vote_Title> model); 
        #endregion

        #region //批量查询
        IList<Vote_Title> NGetListID(string id); 
        #endregion

        #region //
        IList<Vote_Title> VoteTGetlistby_sid(string sid);
	    #endregion

        #region 根据多个主题ID 查找对应的标题
        IList<Vote_Title> VoteTGetlistby_Msid(string sid); 
        #endregion

        #region 根据多个主题ID 查找对应的V标题
        IList<Vote_TitleView> VoteTGetVlistby_Msid(string sid); 
        #endregion
    }
}
