using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace NewAsiaOASystem.Dao
{
    public class CommonClass
    {

        #region 验证输入字符串是否为数字和验证输入值是否
        /// <summary>
        /// 验证输入字符串是否为数字并且页码范围是否正确
        /// </summary>
        /// <param name="NumStr">页码</param>
        /// <param name="PageCount">总记录条数</param>
        /// <returns></returns>
        public int checkInput(string NumStr, int PageCount)
        {
            int pageSize = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["PageSize"]);
            NumStr = string.IsNullOrEmpty(NumStr) ? "1" : NumStr.Trim();//判断传入参数是否为空
            System.Text.RegularExpressions.Regex rex =
                new System.Text.RegularExpressions.Regex(@"^\d+$");//使用正则表达式判断传入参数是否是数字字符串
            if (!rex.IsMatch(NumStr))//如果不是数字则显示第一页
            {
                NumStr = "1";
            }

            int Num = Convert.ToInt32(NumStr);
            //判断输入值是否大于总页数
            PageCount = (int)Math.Ceiling(PageCount / (double)pageSize);//
            Num = Num > PageCount ? PageCount : Num;
            //判断输入值是否小于1
            Num = Num > 1 ? Num : 1;
            return Num;
        }
        #endregion

        #region 无限递归，生成easyui tree可用的json数据
        /// <summary>
        /// 无限递归，生成easyui tree可用的json数据 
        /// </summary>
        /// <param name="ds">数据源DataSet</param>
        /// <param name="ParentID">父id</param>
        /// <param name="deep">开始深度，默认为1</param>
        /// <returns></returns>
        public string AddNode(DataSet ds, string ParentID, int deep)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                DataView dvTree = new DataView(ds.Tables[0]);
                if (string.IsNullOrEmpty(ParentID))
                    dvTree.RowFilter = " isnull(parentId,'')='' ";
                else
                    dvTree.RowFilter = "[ParentId]   ='" + ParentID + "'";//过滤出ParentId  
                int i = 0;
                int d = deep;
                foreach (DataRowView drv in dvTree)
                {

                    if (i == 0)//如果是某一层的开始，需要“[”开始  
                    {
                        if (d == 1)//如果深度为null,即第一层  
                            sb.Append("[");
                        else//否则，为第二层或更深  
                            sb.Append(",\"children\":[");
                    }

                    else
                        sb.Append(",");

                    sb.Append("{");
                    sb.Append("\"ID\":\"").Append(drv["Id"]).Append("\",");
                    sb.Append("\"text\":\"").Append(drv["Name"]).Append("\"");
                    sb.Append(AddNode(ds, drv["Id"].ToString(), deep + 1));//递归  
                    sb.Append("}");
                    i = i + 1;
                    if (dvTree.Count == i)//如果某一层到了末尾,需要"]"结束  
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

        #region 执行存储过程
        /// <summary>
        /// 从XML获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        private string GetConnStr(string DaoXmlUrl)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            //忽略文档里面的注释
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(DaoXmlUrl, settings);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(xmlDoc.NameTable);
            xmlnsManager.AddNamespace("NameSpace", "http://www.springframework.net");
            xmlnsManager.AddNamespace("db", "http://www.springframework.net/database");
            //XmlNode node = xmlDoc.SelectSingleNode("/db:objects", xmlnsManager);
            XmlNode node = xmlDoc.SelectSingleNode("/NameSpace:objects/db:provider", xmlnsManager);
            // 将节点转换为元素，便于得到节点的属性值
            XmlElement xe = (XmlElement)node;
            string ConnStr = xe.GetAttribute("connectionString");
            return ConnStr;
        }

       
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="DaoXmlUrl">数据库连接字符串xml所在路径</param>
        /// <param name="PName">存储过程名称</param>
        /// <param name="pars">存储过程参数数组</param>
        /// <returns></returns>
        public DataSet ExecProceDure(string DaoXmlUrl,string PName,SqlParameter [] pars)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr(DaoXmlUrl)))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(PName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(pars);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }

                catch
                {
                    return new DataSet();
                }

                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        #endregion

    }

}

