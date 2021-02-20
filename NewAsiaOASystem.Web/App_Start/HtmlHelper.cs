using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class HtmlHelperComm
    {
        #region 分页方法
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalCount">总记录条数</param>
        /// <returns></returns>
        public static HtmlString ShowPageNavigate(this HtmlHelper htmlHelper, int currentPage, int pageSize, int totalCount)
        {
            var redirectTo = htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            pageSize = pageSize == 0 ? 1 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            output.AppendFormat("<div class='pagination'><div class='pagination-c'>");
            output.AppendFormat("<ol><span class='fanye'><a href='{0}?pageIndex=1&pageSize={1}'>首页</a></span></ol>", redirectTo, pageSize);
            output.AppendFormat("<ol><span>{0}</span> <span>/</span><span>{1}</span></ol>", currentPage, totalPages);
            if (totalPages > 1)
            {
                if (currentPage > 1)
                {
                    //处理上一页的连接
                    output.AppendFormat("<ol><span class='fanye'><a href='{0}?pageIndex={1}&pageSize={2}' style='disabled:false'>上一页</a></span></ol>", redirectTo, currentPage - 1, pageSize);
                }
                else
                {
                    output.AppendFormat("<ol><span class='fanye'><a  style='cursor:default;color:#C0C0C0'>上一页</a></span></ol>");
                }

                //output.AppendFormat("<ol><span><input type='text' id='getPage'/></span></ol>");

                if (currentPage < totalPages)
                {
                    //处理下一页的链接
                    output.AppendFormat("<ol><span class='fanye'><a href='{0}?pageIndex={1}&pageSize={2}'>下一页</a></span></ol>", redirectTo, currentPage + 1, pageSize);
                }
                else
                {
                    output.AppendFormat("<ol><span class='fanye'><a  style='cursor:default;color:#C0C0C0'>下一页</a></span></ol>");
                }
                
            }
            output.AppendFormat("<ol><span class='fanye'><a href='{0}?pageIndex={1}&pageSize={2}'>最后一页</a></span></ol>", redirectTo, totalPages, pageSize);   
            output.AppendFormat("<ol><span>共</span><span>{0}</span><span>条</span></ol>", totalCount);
            output.AppendFormat("<ol><span><input type='text' id='getPage'/></span></ol>");
            if (totalPages > 1)
            output.AppendFormat("<ol style='cursor:pointer;' class='go' onclick='goClick()'><span><a>GO</a></span></ol></div>");
            output.Append("<script>function goClick(){location.href='").Append(redirectTo).Append("?pageIndex='+ document.getElementById(\"getPage\").value+'&pageSize=")
                .Append(pageSize).Append("'").Append("}</script>");
            return new HtmlString(output.ToString());
        }

        #endregion

        #region 输出分页字符串(使用ajax作查询时调用)
        /// <summary>
        /// 输出分页字符串
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalCount">总记录条数</param>
        /// <returns></returns>
        public static string OutPutPageNavigate(int currentPage, int pageSize, int totalCount)
        {
            pageSize = pageSize == 0 ? 1 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            output.AppendFormat("<div class='pagination'><div class='pagination-c'>");
            output.AppendFormat("<ol><span class='fanye'><a href='#' onclick='SerchList(1)'>首页</a></span></ol>");
            output.AppendFormat("<ol><span>{0}</span> <span>/</span><span>{1}</span></ol>", currentPage, totalPages);
            if (totalPages > 1)
            {
                if (currentPage > 1)
                {
                    //处理上一页的连接
                    output.AppendFormat("<ol><span class='fanye'><a href='#'  onclick='SerchList({0})' style='disabled:false'>上一页</a></span></ol>", currentPage - 1);
                }
                else
                {
                    output.AppendFormat("<ol><span class='fanye'><a  style='cursor:default;color:#C0C0C0'>上一页</a></span></ol>");
                }

                output.AppendFormat("<ol><span><input type='text' id='getPage'/></span></ol>");

                if (currentPage < totalPages)
                {
                    //处理下一页的链接
                    output.AppendFormat("<ol><span class='fanye'><a href='#' onclick='SerchList({0})'>下一页</a></span></ol>",currentPage + 1);
                }
                else
                {
                    output.AppendFormat("<ol><span class='fanye'><a  style='cursor:default;color:#C0C0C0'>下一页</a></span></ol>");
                }

            }
            output.AppendFormat("<ol><span class='fanye'><a href='#' onclick='SerchList({0})'>最后一页</a></span></ol>", totalPages);
            output.AppendFormat("<ol><span>共</span><span>{0}</span><span>条</span></ol>", totalCount);
            if (totalPages > 1)
                output.AppendFormat("<ol style='cursor:pointer;' class='go' onclick='goClick()'><span><a>GO</a></span></ol></div>");
            output.Append("<script>function goClick(){SerchList(document.getElementById('getPage').value)}</script>");
            //output.Append("<script>function goClick(){location.href='").Append(redirectTo).Append("?pageIndex='+ document.getElementById(\"getPage\").value+'&pageSize=")
            //    .Append(pageSize).Append("'").Append("}</script>");
            return output.ToString();
        }

        #endregion

    }
}