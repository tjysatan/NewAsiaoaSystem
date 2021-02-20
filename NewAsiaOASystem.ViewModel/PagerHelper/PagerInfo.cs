using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class PagerInfo<T>
    {
        public PagerInfo()
        {
            CurrentPageIndex = 1;
            PageSize = 30;
        }
        /// <summary>
        /// 数据总条数 
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> DataList { get; set; }
    
        /// <summary>
        /// json格式的数据
        /// </summary>
        public string GetPagingDataJson { get; set; }


    }
}
