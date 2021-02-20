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

namespace NewAsiaOASystem.Dao
{
    public class WX_MessageDao : ServiceConversion<WX_MessageView, WX_Message>, IWX_MessageDao
    {

        //重写sql语句
        private StringBuilder TempHql = null;
        private List<string> TempList = null;
        public override String GetSearchHql()
        {
            return string.Format(" from {0} u where 1=1 {1}", typeof(WX_Message).Name, TempHql.ToString());
        }

        /// <summary>
        /// DATA 转换成 TDO  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override WX_MessageView GetView(WX_Message data)
        {
            WX_MessageView view = ConvertToDTO(data);
            if (data.wx_Message_News != null)
            {
                List<WX_Message_News> dept = data.wx_Message_News.ToList();
                dept = dept.Where(x => x != null).ToList<WX_Message_News>();
                //&& x.SysMenu.Count >0&& x.SysFun.Count>0&&x.SysDept.Count>0&&x.SysAuth.Count>0
                view.wx_Message_News = JsonConvert.SerializeObject(dept);
            }
            return view;
        }


        /// <summary>
        /// TDO 转换成 DATA
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public override WX_Message GetData(WX_MessageView view)
        {
            try
            {
                WX_Message data = new WX_Message();
                data = ConvertToData(view);
    
                if (view.wx_Message_News != null)
                {
                    IWX_Message_NewsDao _IWX_Message_NewsDao = ContextRegistry.GetContext().GetObject("WX_Message_NewsDao") as IWX_Message_NewsDao;
                    data.wx_Message_News = _IWX_Message_NewsDao.NGetListdata_id(view.wx_Message_News);
                }

                return data;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(WX_MessageView model)
        {
            WX_Message listmodel = GetData(model);
            return base.insert(listmodel);
        }

        public bool WX_Insert(WX_Message model)
        {
            return base.insert(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(WX_MessageView model)
        {
            WX_Message listmodel = GetData(model);
            return base.Update(listmodel);
        }

        public bool WX_Update(WX_Message model)
        {
            return base.Update(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(WX_MessageView model)
        {
            WX_Message listmodel = GetData(model);
            return base.Delete(listmodel);
        }

        public bool wx_Deletemodel(WX_Message model)
        {
            return base.Delete(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<WX_MessageView> model)
        {
            IList<WX_Message> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        public bool Wx_Delete(List<WX_Message> model)
        {
            IList<WX_Message> listmodel = model;
            return base.NDelete(model);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<WX_MessageView> NGetList()
        {
            List<WX_Message> listdata = base.GetList() as List<WX_Message>;
            IList<WX_MessageView> listmodel = GetViewlist(listdata);
            return listmodel;
        }


        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<WX_MessageView> NGetList_id(string id)
        {
            List<WX_Message> list = base.GetList_id(id) as List<WX_Message>;
            IList<WX_MessageView> listmodel = GetViewlist(list);
            return listmodel;
        }


       

        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回list数据</returns>
        public IList<WX_Message> NGetListID(string id)
        {
            IList<WX_Message> list = base.GetList_id(id);
            return list;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WX_MessageView NGetModelById(string id)
        {
            WX_MessageView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        public WX_Message WX_GetModelById(string id)
        {
            base.HibernateTemplate.Clear();
            return base.GetModelById(id);
        }
        //public WX_Message NGetModelById(string id)
        //{
        //    WX_Message listmodel = base.GetModelById(id);
        //    return listmodel;
        //}

        #region //根据关键字查找 对应回复的内容
        public IList<WX_Message> GetWX_MessageList(string A_KeyWord)
        {
           // string tempHql = " from WX_Message  where A_KeyWord like '%" + A_KeyWord + " %'";
            string tempHql = string.Format(" from  WX_Message  where  A_KeyWord  = '{0}'", A_KeyWord);
            try
            {
                IList<WX_Message> list = Session.CreateQuery(tempHql).List<WX_Message>();
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        #region //查找默认回复的消息
        public IList<WX_Message> GetWX_MessageMRList()
        {
            string tempHql = " from WX_Message where IsDefaultMessage ='True'";
            try
            {
                IList<WX_Message> list = Session.CreateQuery(tempHql).List<WX_Message>();
              //  IList<WX_MessageView> listmodel = GetViewlist(list);
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }  
        #endregion


        #region //根据ID查找 GetWX_Messageby_id（）
        public IList<WX_MessageView> GetWX_MessageViewby_id(string id)
        {
            string tempHql = string.Format(" from  WX_Message  where  A_id = '{0}'", id);
            try
            {
                List<WX_Message> list = Session.CreateQuery(tempHql).List<WX_Message>() as List<WX_Message>;
                IList<WX_MessageView> listmodel = GetViewlist(list);
                return listmodel;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        } 
        #endregion

        #region //根据Id查找
        /// <summary>
        /// 根据Id查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<WX_Message> GetWX_MessagebyId(string id)
        {
            string tempHql = string.Format(" from  WX_Message  where  A_id = '{0}'", id);
            try
            {
                List<WX_Message> list = Session.CreateQuery(tempHql).List<WX_Message>() as List<WX_Message>;
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //根据ID查找 GetWX_Messageby_id（）
        public IList<WX_Message> GetWX_Messageby_id(string id)
        {
            //  string tempHql = " from WX_Message where A_id in (" + id + ")";
            string tempHql = string.Format(" from  WX_Message  where  A_id in ({0})", id);
            try
            {
                // List<WX_Message> list = Session.CreateQuery(tempHql) as List<WX_Message>;
                IList<WX_Message> list = Session.CreateQuery(tempHql).List<WX_Message>();
                //  IList<WX_MessageView> listmodel = GetViewlist(list);
                return list;
            }
            catch (Exception e)
            {
                log4net.LogManager.GetLogger("ApplicationInfoLog").Error(e);
                return null;
            }
        }
        #endregion

        #region //获取图文下来框数据
        public string MnewsAlbumDropdown(string MessageId)
        {
            IWX_Message_NewsDao _IWX_Message_NewsDao = ContextRegistry.GetContext().GetObject("WX_Message_NewsDao") as IWX_Message_NewsDao;
            WX_Message Message = null;
            // SysPerson person = null;
            //获取关键词对应的回复图文
            List<WX_Message_News> Mnewlist = null;
            if (!string.IsNullOrEmpty(MessageId))
            {
                Message = base.GetModelById(MessageId);
                if (Message != null && Message.wx_Message_News != null)
                    Mnewlist = Message.wx_Message_News.ToList<WX_Message_News>();
            }
            //获取所有的图文消息
            List<WX_Message_NewsView> AllMNewList = _IWX_Message_NewsDao.GetWX_Message_newlistby_type(0).ToList();


            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                if (AllMNewList != null)
                {

                    foreach (var mnew in AllMNewList)
                    {
                        if (Mnewlist != null && mnew != null)
                        {
                            var temp = Mnewlist.Find(o => o != null && o.N_Id == mnew.N_Id);
                            if (temp != null)
                                sb.Append("{id:'" + mnew.N_Id + "',pId:'" + null + "',name:'" + mnew.Title + "',checked:true},");
                            else
                                sb.Append("{id:'" + mnew.N_Id + "',pId:'" + null + "',name:'" + mnew.Title + "',checked:false},");
                        }
                        else
                        {
                            sb.Append("{id:'" + mnew.N_Id + "',pId:'" + null + "',name:'" + mnew.Title + "',checked:false},");
                        }
                    }
                    if (sb.Length > 1)
                        sb.Remove(sb.Length - 1, 1);
                }
                sb.Append("]");
                return sb.ToString();
            }

            catch
            {
                return "";
            }
        } 
        #endregion

        #region //关键词分页数据 +GetMseeagaPagelist()
        /// <summary>
        /// 关键词分页数据
        /// </summary>
        /// <param name="A_KeyWord">关键词</param>
        /// <param name="MsgType">类型  text  new</param>
        /// <param name="IsDefaultMessage">是否默认  false   true</param>
        /// <param name="user"></param>
        /// <returns></returns>
        public PagerInfo<WX_MessageView> GetMseeagaPagelist(string A_KeyWord, string MsgType, string IsDefaultMessage, SessionUser user)
        {
            TempList = new List<string>();
            TempHql = new StringBuilder();
            if (!string.IsNullOrEmpty(A_KeyWord))
                TempHql.AppendFormat(" and u.A_KeyWord like '%{0}%' ", A_KeyWord);
            if (!string.IsNullOrEmpty(MsgType))
                TempHql.AppendFormat(" and u.MsgType = '{0}' ", MsgType);
            if (!string.IsNullOrEmpty(IsDefaultMessage))
                TempHql.AppendFormat(" and u.IsDefaultMessage='{0}'", IsDefaultMessage);
            TempHql.AppendFormat(" order by u.A_CreateTime desc");
            PagerInfo<WX_MessageView> list = Search();
            return list;
        }
        #endregion
    }
}
