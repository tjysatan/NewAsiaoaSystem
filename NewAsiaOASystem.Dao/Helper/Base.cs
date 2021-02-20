using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.Dao
{
   public class Base<T> where T:class
    {
        #region 无限递归，生成easyui tree可用的json数据

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">数据集合</param>
        /// <param name="kField">主键属性的名称</param>
         /// <param name="pField">父节点属性名称</param>
        /// <param name="pValue">父节点值</param>
        /// <param name="TextField">显示值的属性名称</param>
        /// <param name="deep">开始深度，默认为1</param>
        /// <returns></returns>
        public string AddNode(List<T> list, string kField,string pField, string pValue, string TextField, int deep)
        {
            try
            {
                if (list == null)
                    return "[]";
                StringBuilder sb = new StringBuilder();
                List<T> listNew = new List<T>();
                if (deep == 1)
                {
                    //循环获取根节点
                    foreach (T item in list)
                    {
                        var tempList = list.Where(F => Convert.ToString(F.GetType().GetProperty(kField).GetValue(F, null)) ==
                            Convert.ToString(item.GetType().GetProperty(pField).GetValue(item, null))
                            );
                        if (tempList.Count() <= 0)
                            listNew.Add(item);
                    }
                }
                else
                    listNew = list.Where(F => Convert.ToString(F.GetType().GetProperty(pField).GetValue(F, null)) == pValue).ToList();

                int i = 0;
                int d = deep;
                foreach (T t in listNew)
                {

                    if (i == 0)//如果是某一层的开始，需要“[”开始  
                    {
                        if (d == 1)//如果深度为null或"",即第一层  
                            sb.Append("[");
                        else//否则，为第二层或更深  
                            sb.Append(",\"children\":[");
                    }

                    else
                        sb.Append(",");

                    string KeyVal = t.GetType().GetProperty(kField).GetValue(t, null).ToString();
                    sb.Append("{");
                    sb.Append("\"id\":\"").Append(KeyVal).Append("\",");
                    sb.Append("\"text\":\"").Append(t.GetType().GetProperty(TextField).GetValue(t, null)).Append("\"");
                    sb.Append(AddNode(list, kField, pField, KeyVal, TextField, deep + 1));//递归  
                    sb.Append("}");
                    i = i + 1;
                    if (listNew.Count == i)//如果某一层到了末尾,需要"]"结束  
                        sb.Append("]");
                }

                return sb.ToString();
            }
            catch (Exception)
            {
                return "[]";
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">数据集合</param>
        /// <param name="kField">主键属性的名称</param>
        /// <param name="pField">父节点属性名称</param>
        /// <param name="pValue">父节点值</param>
        /// <param name="TextField">显示值的属性名称</param>
        /// <param name="deep">开始深度，默认为1</param>
        /// <returns></returns>
        public string AddNode(List<T> list, string kField, string pField, string pValue, int deep, string [] pars)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<T> listNew = new List<T>();
               
                if (deep == 1)
                {
                    //循环获取根节点
                    foreach (T item in list)
                    {
                        var tempList= list.Where(F => Convert.ToString(F.GetType().GetProperty(kField).GetValue(F, null))==
                            Convert.ToString(item.GetType().GetProperty(pField).GetValue(item, null))
                            );
                      if (tempList.Count()<= 0)
                          listNew.Add(item);
                    }
                }
                else
                    listNew = list.Where(F => Convert.ToString(F.GetType().GetProperty(pField).GetValue(F, null)) == pValue).ToList();

                int i = 0;
                int d = deep;
                foreach (T t in listNew)
                {

                    if (i == 0)//如果是某一层的开始，需要“[”开始  
                    {
                        if (d == 1)//如果深度为null或"",即第一层  
                            sb.Append("[");
                        else//否则，为第二层或更深  
                            sb.Append(",'children':[");
                    }

                    else
                        sb.Append(",");

                    string KeyVal = t.GetType().GetProperty(kField).GetValue(t, null).ToString();
                    sb.Append("{");
                    //sb.Append("\"state\":\"closed\",");//默认不展开子节点
                    sb.Append("'Id\':'").Append(KeyVal).Append("',");
                    for (int k = 0,j=pars.Length; k < j; k++)
                    {
                        sb.AppendFormat("'{0}':'",pars[k]);
                        sb.Append(t.GetType().GetProperty(pars[k]).GetValue(t, null)).Append("',");
                    }

                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(AddNode(list, kField, pField, KeyVal, deep + 1,pars));//递归  
                    sb.Append("}");
                    i = i + 1;
                    if (listNew.Count == i)//如果某一层到了末尾,需要"]"结束  
                        sb.Append("]");
                }

                return sb.ToString();
            }
            catch (Exception)
            {
                return "[]";
            }
        }
        #endregion

        #region //无限递归，生成layui tree可用的json数据
        public string AddNodelayui(List<T> list, string kField, string pField, string pValue, string TextField,bool spread, int deep)
        {
            try
            {
                if (list == null)
                    return "[]";
                StringBuilder sb = new StringBuilder();
                List<T> listNew = new List<T>();
                if (deep == 1)
                {
                    //循环获取根节点
                    foreach (T item in list)
                    {
                        var tempList = list.Where(F => Convert.ToString(F.GetType().GetProperty(kField).GetValue(F, null)) ==
                            Convert.ToString(item.GetType().GetProperty(pField).GetValue(item, null))
                            );
                        if (tempList.Count() <= 0)
                            listNew.Add(item);
                    }
                }
                else
                    listNew = list.Where(F => Convert.ToString(F.GetType().GetProperty(pField).GetValue(F, null)) == pValue).ToList();

                int i = 0;
                int d = deep;
                foreach (T t in listNew)
                {

                    if (i == 0)//如果是某一层的开始，需要“[”开始  
                    {
                        if (d == 1)//如果深度为null或"",即第一层  
                            sb.Append("[");
                        else//否则，为第二层或更深  
                            sb.Append(",\"children\":[");
                    }

                    else
                        sb.Append(",");

                    string KeyVal = t.GetType().GetProperty(kField).GetValue(t, null).ToString();
                    sb.Append("{");
                    sb.Append("\"id\":\"").Append(KeyVal).Append("\",");
                    sb.Append("\"title\":\"").Append(t.GetType().GetProperty(TextField).GetValue(t, null)).Append("\",");
                    sb.Append("\"spread\":\"").Append(spread).Append("\"");
                    sb.Append(AddNodelayui(list, kField, pField, KeyVal, TextField, spread, deep + 1));//递归  
                    sb.Append("}");
                    i = i + 1;
                    if (listNew.Count == i)//如果某一层到了末尾,需要"]"结束  
                        sb.Append("]");
                }

                return sb.ToString();
            }
            catch (Exception)
            {
                return "[]";
            }
        }

        public string AddNodelayui(List<T> list, string kField, string pField, string pValue,bool spread, int deep, string[] pars)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                List<T> listNew = new List<T>();

                if (deep == 1)
                {
                    //循环获取根节点
                    foreach (T item in list)
                    {
                        var tempList = list.Where(F => Convert.ToString(F.GetType().GetProperty(kField).GetValue(F, null)) ==
                             Convert.ToString(item.GetType().GetProperty(pField).GetValue(item, null))
                            );
                        if (tempList.Count() <= 0)
                            listNew.Add(item);
                    }
                }
                else
                    listNew = list.Where(F => Convert.ToString(F.GetType().GetProperty(pField).GetValue(F, null)) == pValue).ToList();

                int i = 0;
                int d = deep;
                foreach (T t in listNew)
                {

                    if (i == 0)//如果是某一层的开始，需要“[”开始  
                    {
                        if (d == 1)//如果深度为null或"",即第一层  
                            sb.Append("[");
                        else//否则，为第二层或更深  
                            sb.Append(",'children':[");
                    }

                    else
                        sb.Append(",");

                    string KeyVal = t.GetType().GetProperty(kField).GetValue(t, null).ToString();
                    sb.Append("{");
                    //sb.Append("\"state\":\"closed\",");//默认不展开子节点
                    sb.Append("'Id\':'").Append(KeyVal).Append("',");
                    for (int k = 0, j = pars.Length; k < j; k++)
                    {
                        sb.AppendFormat("'{0}':'", pars[k]);
                        sb.Append(t.GetType().GetProperty(pars[k]).GetValue(t, null)).Append("',");
                    }

                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(AddNodelayui(list, kField, pField, KeyVal, spread, deep + 1, pars));//递归  
                    sb.Append("}");
                    i = i + 1;
                    if (listNew.Count == i)//如果某一层到了末尾,需要"]"结束  
                        sb.Append("]");
                }

                return sb.ToString();
            }
            catch (Exception)
            {
                return "[]";
            }
        }
        #endregion

    }
}
