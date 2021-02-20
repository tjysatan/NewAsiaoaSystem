using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IWX_MessageDao : IBaseDao<WX_MessageView>
    {

        #region //通过关键词 查找自动回复的内容
        IList<WX_Message> GetWX_MessageList(string A_KeyWord); 
        #endregion

        #region //查找默认回复的消息
        IList<WX_Message> GetWX_MessageMRList(); 
        #endregion

        //WX_Message GetModelById(string id);
        //WX_Message NGetModelById(string id);

        bool WX_Insert(WX_Message model);

        bool WX_Update(WX_Message model);

        WX_Message WX_GetModelById(string id);

        bool Wx_Delete(List<WX_Message> model);
        bool wx_Deletemodel(WX_Message model);
         
        #region //根据ID查找 GetWX_Messageby_id（）
        IList<WX_Message> GetWX_Messageby_id(string id); 
        #endregion

        IList<WX_MessageView> GetWX_MessageViewby_id(string id);

        #region //获取图文的下来框数据
        string MnewsAlbumDropdown(string MessageId); 
        #endregion


        /// <summary>
        /// 关键词分页数据
        /// </summary>
        /// <param name="A_KeyWord">关键词</param>
        /// <param name="MsgType">类型  text  new</param>
        /// <param name="IsDefaultMessage">是否默认  false   true</param>
        /// <param name="user"></param>
        /// <returns></returns>
        PagerInfo<WX_MessageView> GetMseeagaPagelist(string A_KeyWord, string MsgType, string IsDefaultMessage, SessionUser user);

        #region //根据Id查找
        /// <summary>
        /// 根据Id查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<WX_Message> GetWX_MessagebyId(string id); 
        #endregion
    }
}
