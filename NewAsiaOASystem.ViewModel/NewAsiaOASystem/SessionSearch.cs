using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 用Session存储未免儿童搜索条件
    /// </summary>
   public class SessionSearch
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public  string Search_Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public  string Search_Sex { get; set; }

        /// <summary>
        /// 免疫点ID
        /// </summary>
        public  string Search_ImmuneID { get; set; }

        /// <summary>
        /// 处理结果（未联系上，禁忌症等）
        /// </summary>
        public  string Search_FK_State { get; set; }

        /// <summary>
        /// 免疫区域（本地/外地免疫）
        /// </summary>
        public  string Search_FeedOne { get; set; }

        /// <summary>
        /// 反馈状态（待种、漏种、种全）
        /// </summary>
        public  string Search_FeedTow { get; set; }

       /// <summary>
       /// 免疫状态名称
       /// </summary>
        public string Search_MYZTname { get; set; }

        /// <summary>
        /// 查询年龄类型(month表示按月数查询，year表示按年份查询)
        /// </summary>
        public  string AgeType { get; set; }

        /// <summary>
        /// 开始岁数或月数
        /// </summary>
        public  string Search_StartAge { get; set; }

        /// <summary>
        /// 截止岁数或月数
        /// </summary>
        public  string Search_EndAge { get; set; }

        /// <summary>
        /// 流入开始时间
        /// </summary>
        public  string Search_LrStartDate { get; set; }

        /// <summary>
        /// 流入结束时间
        /// </summary>
        public  string Search_LrEndDate { get; set; }

        /// <summary>
        /// 已处理儿童
        /// </summary>
        public  string YesDealWithChild { get; set; }

        /// <summary>
        /// 未处理儿童
        /// </summary>
        public  string NoDealWithChild { get; set; }

        /// <summary>
        /// 新增儿童
        /// </summary>
        public  string NewChild { get; set; }

        /// <summary>
        /// 开始创建时间/入库时间
        /// </summary>
        public  string StartDisCreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束创建时间/入库时间
        /// </summary>
        public  string EndDisCreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 社区名称
        /// </summary>
        public  string CommName
        {
            get;
            set;
        }

    }
}
