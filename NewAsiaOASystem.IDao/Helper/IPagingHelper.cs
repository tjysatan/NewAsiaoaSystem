using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetSystem.DBModel;
using System.Text;

namespace NetSystem.IDao
{
    public interface IPagingHelper<T> 
    {
        /// <summary>
        /// 构造函数,设置分页数据集
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="PageSum">总记录条数</param>
        /// <param name="dataSource">数据集</param>
        void GetPagingHelper(int pageIndex, int PageSum, IEnumerable<T> dataSource);

        //获取分页数据集方法（直接返回实体）
        IEnumerable<T> getPagingData();

        //获取分页数据集方法（返回Json字符串）
        String getPagingDataJson();
        /// <summary>
        /// 输出分页按钮方法
        /// </summary>
        /// <param name="url">点击按钮后的跳转地址</param>
        /// <returns></returns>
        string Table(string url);
    }
}
