using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using NewAsiaOASystem.DBModel;

namespace NewAsiaOASystem.Web.WebAPI
{
    public class VoteWebApiController : ApiController
    {
        //
        // GET: /VoteWebApi/
        IVote_SubjectDao _IVote_SubjectDao = ContextRegistry.GetContext().GetObject("Vote_SubjectDao") as IVote_SubjectDao;
        IVote_TitleDao _IVote_TitleDao = ContextRegistry.GetContext().GetObject("Vote_TitleDao") as IVote_TitleDao;
        IVote_ipDao _IVote_ipDao = ContextRegistry.GetContext().GetObject("Vote_ipDao") as IVote_ipDao;
        IVote_QuestionDao _IVote_QuestionDao = ContextRegistry.GetContext().GetObject("Vote_QuestionDao") as IVote_QuestionDao;
        IWX_OpenIDDao _IWX_OpenIDDao = ContextRegistry.GetContext().GetObject("WX_OpenIDDao") as IWX_OpenIDDao;

        #region //根据Id查找主题
        [HttpPost]
        public string GetVoteSnameby_Id([FromBody]string Id)
        {
            Vote_SubjectView pmodel = _IVote_SubjectDao.NGetModelById(Id);

            string json;
            if (pmodel != null)
            {

                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }

            return json;
        } 
        #endregion

        #region //根据Id查找主题
        [HttpPost]
        public string GetdataVoteSnameby_Id([FromBody]string Id)
        {
            Vote_Subject pmodel = _IVote_SubjectDao.GetdataModelbyID(Id);

            string json;
            if (pmodel != null)
            {
                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }

            return json;
        }
        #endregion


        #region //根据Id查找标题
        [HttpPost]
        public string GetVoteTnameby_Id([FromBody]string Id)
        {
            Vote_TitleView pmodel = _IVote_TitleDao.NGetModelById(Id);
            string json;
            if (pmodel != null)
            {

                json = JsonConvert.SerializeObject(pmodel);
            }
            else
            {
                json = null;
            }

            return json;
        }
        #endregion

        #region //根据标题Id查找标题所属的选项
        [HttpPost]
        public string GetVoteQuestionby_tid([FromBody]string Id)
        {
            IList<Vote_QuestionView> listmodel = _IVote_QuestionDao.Vote_QGetListby_Tid(Id);
             string json;
            if(listmodel!=null)
            {
                json=JsonConvert.SerializeObject(listmodel);
            }
            else
            {
                json=null;
            }
            return json;
        } 
        #endregion

        #region //根据OPenId判断是否是绑定用户，返回不一样的主题没有过期的主题问卷列表json
        [HttpPost]
        public string GetVoteSallList([FromBody]string Id)
        {
            bool I=false;
            I=_IWX_OpenIDDao.IsnotBD(Id);
            if (I == true)
            {
                IList<Vote_SubjectView> listmodel = _IVote_SubjectDao.VoteGetListdataby_type(1);//查询内部问卷
                string json;
                if (listmodel != null)
                {
                    json = JsonConvert.SerializeObject(listmodel);
                }
                else
                {
                    json = null;
                }
                return json;
            }
            else
            {
                IList<Vote_SubjectView> listmodel = _IVote_SubjectDao.VoteGetListdataby_type(0);//查询内部问卷
                string json;
                if (listmodel != null)
                {
                    json = JsonConvert.SerializeObject(listmodel);
                }
                else
                {
                    json = null;
                }
                return json;
            }

        } 
        #endregion
    }
}
