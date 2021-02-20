using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.IDao
{
    public interface IVote_QuestionDao : IBaseDao<Vote_QuestionView>
    {
        #region //根据标题T_Id 查找该标题下所有的选项
        IList<Vote_QuestionView> Vote_QGetListby_Tid(string id); 
        #endregion

        #region //根据多个主题ID 查找对应的V选项
        IList<Vote_QuestionView> VoteQGetVlistby_Msid(string sid); 
        #endregion



    }
}
