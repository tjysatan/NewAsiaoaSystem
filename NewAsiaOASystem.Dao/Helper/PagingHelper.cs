using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using System.Text;
using Spring.Context.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace NetSystem.Dao
{
    public class PagingHelper<T> : IPagingHelper<T> where T : class
    {
        //分页数据源
        public IEnumerable<T> DataSource { get; set; }
        //分页显示条数
        public int PageSize { get; set; }
        //当前页
        public int PageIndex { get; set; }
        //总记录条数
        public int PageSum { get; set; }
        //总页数
        public int PageCount { get; set; }
        //是否有上一页
        public bool HasPrev { get { return PageIndex > 1; } }
        //是否有下一页
        public bool HasNext { get { return PageIndex < PageCount; } }

        #region 构造函数,设置分页数据集  PagingHelper(int pageIndex, int PageSum, IEnumerable<T> dataSource)

        /// <summary>
        /// 构造函数,设置分页数据集
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="PageSum">总记录条数</param>
        /// <param name="dataSource">数据集</param>
        public void GetPagingHelper(int pageIndex, int PageSum, IEnumerable<T> dataSource)
        {
            //PagingHelper<T> PagingHelper =;
            //从配置文件获取分页条数
            int pageSize = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["PageSize"]);
            this.PageSize = pageSize > 1 ? pageSize : 1;

            //设置总记录条数
            this.PageSum = PageSum;
            //设置总页数
            PageCount = (int)Math.Ceiling(PageSum / (double)pageSize);
            //先验证传入参数是否为数字和输入数字范围再赋值给当前页面
            this.PageIndex = pageIndex;
            this.DataSource = dataSource;
        }

        #endregion

        #region  获取分页数据集
        //获取分页数据集方法（直接返回实体）
        public IEnumerable<T> getPagingData()
        {
            return this.DataSource;
        }

        //获取分页数据集方法（返回Json字符串）
        public String getPagingDataJson()
        {
            if (null != this.DataSource)
            {
                return JsonConvert.SerializeObject(this.DataSource);
            }

            else
            {
                return JsonConvert.SerializeObject("");
            }

        }
        #endregion

        #region 输出分页按钮方法 Table(string url)
        /// <summary>
        /// 输出分页按钮方法
        /// </summary>
        /// <param name="url">点击按钮后的跳转地址</param>
        /// <returns></returns>
        public string Table(string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='message'>共<i class='blue'>")
            .Append(PageSum)
            .Append("</i>条记录，当前显示第&nbsp;<i class='blue'>")
            .Append(PageIndex)
            .Append("</i>页，共&nbsp;<i class='blue'>")
            .Append(PageCount)
            .Append("</i>页</div>");

            //首页
            sb.Append("<ul class='paginList'><li class='paginItem'><a href=")
           .Append(url)
           .Append("?pageIndex=1>首页</a> </li>");

            //上一页
            sb.Append("<li class='paginItem'>");
            //判断是否有上一页
            if (HasPrev)
            {
                sb.Append("<a href=").Append(url).Append("?pageIndex=").Append(PageIndex - 1).Append(">上一页</a>");
            }
            else
            {
                sb.Append("<span style='color:Gray;'>上一页</span>");
            }
            sb.Append("</li>");

            //下一页
            sb.Append("<li class='paginItem'>");
            //判断是否有下一页
            if (HasNext)
            {
                sb.Append("<a href=").Append(url).Append("?pageIndex=").Append(PageIndex + 1).Append(">下一页</a>");
            }
            else
            {
                sb.Append("<span  style='color:Gray;'>下一页</span>");
            }
            sb.Append("</li>");

            //尾页
            sb.Append("<li class='paginItem'><a href=").Append(url).Append("?pageIndex=").Append(PageCount).Append(">尾页</a> </li>");
            //跳转
            sb.Append(" <li class='paginItem' style='width:80px;'><input type='text' class='go' id='go'/>")
            .Append("<span style='display:block; float:right;cursor:pointer;'>")
            .Append("<a  onclick='goClick()'").Append(">跳转</a></span></li>");
            sb.Append("</ul>");
            sb.Append("<script>function goClick(){location.href='").Append(url)
            .Append("?pageIndex='+ document.getElementById(\"go\").value}</script>");
            return sb.ToString();
        }

        #endregion

    }
}
