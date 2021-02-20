using NewAsiaOASystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;

namespace NewAsiaOASystem.IDao
{
    public interface IVote_SubjectDao : IBaseDao<Vote_SubjectView>
    {
           #region //根据多个Id 查询多条记录
        IList<Vote_SubjectView> NGetListdata_id(string id);
	     #endregion

        #region //查找数据
        Vote_Subject GetdataModelbyID(string id); 
        #endregion

        #region  //插入
        bool DataInsert(Vote_Subject model); 
        #endregion

        
        #region //更新
        bool DataUpdate(Vote_Subject model); 
        #endregion

        #region //批量删除
        bool DataDelete(List<Vote_Subject> model); 
        #endregion

        #region //批量查询
        IList<Vote_Subject> NGetListID(string id); 
        #endregion

        #region //根据ID批量查询
        IList<Vote_Subject> VoteGetListdata_id(string id); 
        #endregion

        #region //根据问卷的类型查选问卷的
        IList<Vote_SubjectView> VoteGetListdataby_type(int type); 
        #endregion

 
    }
}
