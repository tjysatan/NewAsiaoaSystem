using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Context.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.Dao;
using System.IO;

namespace NewAsiaOASystem.Web.Controllers
{
    public class Vote_SubjectController : Controller
    {
        //
        // GET: /Vote_Subject/
        IVote_QuestionDao _IVote_QuestionDao = ContextRegistry.GetContext().GetObject("Vote_QuestionDao") as IVote_QuestionDao;
        IVote_TitleDao _IVote_TitleDao = ContextRegistry.GetContext().GetObject("Vote_TitleDao") as IVote_TitleDao;
        IVote_SubjectDao _IVote_SubjectDao = ContextRegistry.GetContext().GetObject("Vote_SubjectDao") as IVote_SubjectDao;
        IVote_ConfigDao _IVote_ConfigDao = ContextRegistry.GetContext().GetObject("Vote_ConfigDao") as IVote_ConfigDao;
        IVote_ipDao _IVote_ipDao = ContextRegistry.GetContext().GetObject("Vote_ipDao") as IVote_ipDao;

        public ActionResult Index(int? pageIndex)
        {
            int CurrentPageIndex = Convert.ToInt32(pageIndex);
            if (CurrentPageIndex == 0)
                CurrentPageIndex = 1;
            _IVote_SubjectDao.SetPagerPageIndex(CurrentPageIndex);
            _IVote_SubjectDao.SetPagerPageSize(15);
            PagerInfo<Vote_SubjectView> listmodel = _IVote_SubjectDao.Search();
            return View(listmodel);
        }


        #region 保存文本的处理方法
        /// <summary>
        /// 保存处理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Vote_Subject model)
        {
            try
            {
                bool flay = false;
                //添加
                if (string.IsNullOrEmpty(model.S_Id))
                {
                    model.S_time = DateTime.Now;
                    model.S_person = Convert.ToString(Session["user"]);
                    flay = _IVote_SubjectDao.DataInsert(model);
                }
                //修改
                else
                {
                    model.S_Id = model.S_Id;
                    flay = _IVote_SubjectDao.DataUpdate(model);
                }

                if (flay)
                    return Json(new { result = "success" });
                else
                    return Json(new { result = "error" });
            }

            catch
            {
                return Json(new { result = "error" });
            }
        }
        #endregion

        #region //添加跳转页面
        public ActionResult addPage()
        {
            return View("Edit", new Vote_Subject());
        }
        #endregion

        #region //编辑跳转页面
        public ActionResult EditPage(string id)
        {
            Vote_Subject sys = new Vote_Subject();
            if (!string.IsNullOrEmpty(id))
                sys = _IVote_SubjectDao.GetdataModelbyID(id);
            return View("Edit", sys);
        }
        #endregion

        #region //删除
        public ActionResult Delete(string id)
        {
            try
            {
                //操作是否成功 
                List<Vote_SubjectView> sys = _IVote_SubjectDao.NGetListdata_id(id) as List<Vote_SubjectView>;
                List<Vote_Subject> sysv = _IVote_SubjectDao.VoteGetListdata_id(id) as List<Vote_Subject>;
                if (null != sys)
                {
                    string s_id = gets_id(sys);
                    if (VoteQDelete(s_id) == true)
                    {
                        if (VoteTDelete(s_id) == true)
                        {
                            if (_IVote_SubjectDao.NDelete(sys) == true)
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                return Content("<script>alert('删除失败');window.history.back();</script>");
                            }
                        }
                        else
                        {
                            return Content("<script>alert('删除失败');window.history.back();</script>");
                        }
                    }
                    else
                    {
                        return Content("<script>alert('删除失败');window.history.back();</script>");
                    }
                }
            }
            catch
            {
                return Content("<script>alert('删除失败');window.history.back();</script>");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region //删除该主题下的所有题目
        public bool VoteTDelete(string s_id)
        {
            bool i = false;
            List<Vote_TitleView> t_sys = _IVote_TitleDao.VoteTGetVlistby_Msid(s_id) as List<Vote_TitleView>;
            if (null != t_sys)
            {
                i = _IVote_TitleDao.NDelete(t_sys);
            }
            else
            {
                i = true;
            }
            return i;
        } 
        #endregion

        #region //删除该主题下的所有选项
        public bool VoteQDelete(string S_Id)
        {
            bool i = false;
            List<Vote_QuestionView> sys = _IVote_QuestionDao.VoteQGetVlistby_Msid(S_Id) as List<Vote_QuestionView>;
            if (null != sys)
            {
                i = _IVote_QuestionDao.NDelete(sys);
            }
            else
            {
                i = true;
            }
            return i;
        } 
        #endregion

        #region //拼接主题ID
        public string gets_id(List<Vote_SubjectView> sys)
        {
            string s_id = "";
            if (sys != null)
            {
                for (int i = 0, j = sys.Count; i < j; i++)
                {
                    s_id = s_id + "'" + sys[i].S_Id + "',";
                }
                s_id = s_id.TrimEnd(',');//拼接主题Id
            }
            return s_id;
        } 
        #endregion


        #region //问卷列表页面
        public ActionResult Votelist(string Code)
        {
            string OpenId = MenuContext.GetOAuthOpenID(Code);
          //  string OpenId = "oZaEquE5kSu8HR1t_tFIKoriY8VU";
            if (OpenId != null)
            {
                ViewData["OpenId"] = OpenId;//oZaEquE5kSu8HR1t_tFIKoriY8VU
            }
            return View();
        } 
        #endregion

        #region //投票页面
        public ActionResult VoteView(string Id, string openId)
        {
            ViewData["Id"] = Id;
            ViewData["openId"] = openId;
          //  string i=MenuContext.GetOAuthOpenID(Code);
            return View();
        }
        #endregion

        [HttpPost]
        public JsonResult VoteEdit(FormCollection from)
        {
            bool i = false;
            string RestrictIP;
            int refuseTime;
            string sid = Request.QueryString["sid"];
            string openid = Request.QueryString["openid"];
        
            if (sid == "" || sid == null)
            {
                return Json(new { result = "error1" });//主题Id为空
            }
            if (openid == "" || openid == null)
            {
                return Json(new { result = "error4" });//主题Id为空
            }
            Vote_Subject smodel = new Vote_Subject();
            smodel = _IVote_SubjectDao.GetdataModelbyID(sid);
            if (smodel == null)
            {
                return Json(new { result = "error2" });//不存在该主题
            }
            DateTime? sqx = smodel.S_QX;
            if (sqx <= DateTime.Now)
            {
                return Json(new { result = "error5" });//该主题调查问卷已经过期
            }
            Vote_ConfigView cmodel = _IVote_ConfigDao.NGetone();//基础配置信息
            if (cmodel != null)
            {
                RestrictIP = Convert.ToString(cmodel.RestrictIP);//是否限制IP 重复提交 0 限制  1不限制
                refuseTime = Convert.ToInt32(cmodel.refuseTime);//同一个IP 在多久内不能重复提交
                if (RestrictIP == "0")//限制IP
                {
                    string fIP = openid;//获取IP 地址 微信用户的OPenID
                    if (_IVote_ipDao.JcIP(sid, fIP)!=false)
                    {
                        Vote_ipView Imodel = _IVote_ipDao.JcIPmodel(sid,fIP);
                        DateTime Itime = Imodel.I_time;

                        int DateHours= DateDiff(DateTime.Now,Itime);
                        if (DateHours < refuseTime)
                        {
                            return Json(new { result = "error2" });//你已经投过票了
                        }
                        else
                        {
                            if (!_IVote_ipDao.NDelete(Imodel))
                            {
                                return Json(new { result = "error4" });//投票不成功
                            }
                            else
                            {
                                Vote_ipView Imodelnew = new Vote_ipView();
                                Imodelnew.S_Id = sid;
                                Imodelnew.I_IP = fIP;
                                Imodelnew.I_time = DateTime.Now;
                                if (!_IVote_ipDao.Ninsert(Imodelnew))
                                {
                                    return Json(new { result = "error4" });//投票不成功
                                }
                            }
                        }
                    }  
                }

                //遍历FORM表单
                for (int ii = 0; ii < Request.Form.Count; ii++)
                {
                    if (Request.Form[ii].ToString().Trim() != "" && Request.Form.Keys[ii].ToString().Trim() != "submit")
                    {
                        string T_id = Request.Form.Keys[ii].ToString();
                        Vote_TitleView Tmodel = _IVote_TitleDao.NGetModelById(T_id);
                        Tmodel.T_Acount = ++Tmodel.T_Acount;
                        i = _IVote_TitleDao.NUpdate(Tmodel);//更新一个题目的参与人数
                        if (!i)
                        {
                            return Json(new { result = "error4" });//投票不成功
                        }

                        string Q_id = Request.Form[ii].ToString().Trim();
                        if (Tmodel.T_type == 0)
                        {
                            Vote_QuestionView qmodel = _IVote_QuestionDao.NGetModelById(Q_id);
                            qmodel.Q_Count = ++qmodel.Q_Count;
                            qmodel.T_Id = T_id;
                           i=  _IVote_QuestionDao.NUpdate(qmodel);
                          if (!i)
                          {
                              return Json(new { result = "error4" });//投票不成功
                          }
                        }
                        else
                        {
                            Q_id = "'" + Q_id.Replace(",", "','") + "'";
                            IList<Vote_QuestionView> qlist = _IVote_QuestionDao.NGetList_id(Q_id);
                            for (int z = 0, j = qlist.Count(); z < j; z++)
                            {
                                Vote_QuestionView qmodel = qlist[z];
                                qmodel.Q_Count = ++qmodel.Q_Count;
                                qmodel.T_Id = T_id;
                                i=_IVote_QuestionDao.NUpdate(qmodel);
                                if (!i)
                                {
                                    return Json(new { result = "error4" });//投票不成功
                                }
                            }
                        }
                    }
                }
                return Json(new { result = "error3" });//提交成功
            }
            else
            {
                return Json(new { result = "error4" });//投票不成功
            }
        }



        #region //计算俩个时间的间隔了多少小时
        private int DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            int dateDiff = 0;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = Convert.ToInt32(ts.TotalHours);
            return dateDiff;
        } 
        #endregion

        #region //统计页面
        public ActionResult Votecount(string Id)
        {
            ViewData["Id"] = Id;
            return View();
        } 
        #endregion


        
    }
}
