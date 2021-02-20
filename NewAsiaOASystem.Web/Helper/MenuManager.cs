using NewAsiaOASystem.IDao;
using NewAsiaOASystem.ViewModel;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NewAsiaOASystem.Web
{
    public class MenuManager
    {
      public static   IWX_MenusDao _IWX_MenusDao = ContextRegistry.GetContext().GetObject("WX_MenusDao") as IWX_MenusDao;
        /// <summary>
        /// 菜单文件路径
        /// </summary>

       public static string GetDataForJs()
        {
            StringBuilder builder = new StringBuilder();
            IList<WX_MenusView> list = new List<WX_MenusView>();
            list = _IWX_MenusDao.Getallist();
            builder.Append("{\"button\":[");
            foreach (WX_MenusView menus in list)
            {
                IList<WX_MenusView> list2 = new List<WX_MenusView>();
                list2 = _IWX_MenusDao.wx_GetejMenu_by(menus.Id);
                int num = 1;
                if (menus.M_ParentID==""||menus.M_ParentID == null)
                {
                    if (list2!=null&&list2.Count!=0)
                    {
                        builder.Append("{\"name\":\"" + menus.M_Name + "\",");
                        builder.Append("\"sub_button\":[");
                        foreach (WX_MenusView menus2 in list2)
                        {
                            builder.Append("{");
                            if (menus2.M_Type == "click")
                            {
                                builder.Append("\"type\":\"" + menus2.M_Type + "\",");
                                builder.Append("\"name\":\"" + menus2.M_Name + "\",");
                                builder.Append("\"key\":\"" + menus2.M_Key + "\"");
                            }
                            else
                            {
                                builder.Append("\"type\":\"" + menus2.M_Type + "\",");
                                builder.Append("\"name\":\"" + menus2.M_Name + "\",");
                                builder.Append("\"url\":\"" + menus2.M_Url + "\"");
                            }
                            if (num < list2.Count)
                            {
                                builder.Append("},");
                                num++;
                            }
                            else
                            {
                                builder.Append("}");
                            }
                        }
                        builder.Append("]");
                        builder.Append("},");
                    }
                    if (list2 == null)
                    {
                       
                        if (menus.M_Type == "click")
                        {
                            if (menus.M_Key == null)
                            {
                                menus.M_Key = "一级菜单";
                            }
                            builder.Append("{");
                            builder.Append("\"type\":\"" + menus.M_Type + "\",");
                            builder.Append("\"name\":\"" + menus.M_Name + "\",");
                            builder.Append("\"key\":\"" + menus.M_Key + "\"");
                            builder.Append("},");
                        }
                        if (menus.M_Type == "view")
                        {
                            builder.Append("{");
                            builder.Append("\"type\":\"" + menus.M_Type + "\",");
                            builder.Append("\"name\":\"" + menus.M_Name + "\",");
                            builder.Append("\"url\":\"" + menus.M_Url + "\"");
                            builder.Append("},");
                        }
                        if (menus.M_Type == "location_select")
                        {
                            builder.Append("{");
                            builder.Append("\"type\":\"" + menus.M_Type + "\",");
                            builder.Append("\"name\":\"" + menus.M_Name + "\",");
                            builder.Append("\"key\":\"" + menus.M_Key + "\"");
                            builder.Append("},");
                        }
                    }
                }
            }
          string aaa=  DelLastComma(builder.ToString());
            //builder.Append("]}");
            aaa = aaa + "]}";
            return  aaa;
        }


        public static string DelLastComma(string str)
        {
            try
            {
                return str.Substring(0, str.LastIndexOf(","));
            }
            catch (Exception ex)
            {
                return str;
            }
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        public static string GetMenu()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", MenuContext.AccessToken);
            return HttpUtility11.GetData(url);
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        public static string CreateMenu(string menu)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", MenuContext.AccessToken);
            //string menu = FileUtility.Read(Menu_Data_Path);
            //return  HttpUtility11.SendHttpRequest(url, menu);
            return HttpUtility11.PostUrl(url,menu);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public static string DeleteMenu()
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", MenuContext.AccessToken);
            return  HttpUtility11.GetData(url);
        }
        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <returns></returns>
        public static string LoadMenu()
        {
            return GetDataForJs();
        }


    }
}