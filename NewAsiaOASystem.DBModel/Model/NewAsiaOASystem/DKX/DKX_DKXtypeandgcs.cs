using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    /// <summary>
    /// 电控箱类型和工程师的关系实体 
    /// </summary>
    public class DKX_DKXtypeandgcs
    {
        public virtual string Id { get; set; }

        /// <summary>
        /// 电控箱的类型  物联网电控箱  小系统 大系统
        /// </summary>
        public virtual string DkxtypeId { get; set; }

        /// <summary>
        /// 工程师
        /// </summary>
        public virtual string gcsId { get; set; }
    }
}
