using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    /// <summary>
    /// 元器件请购单model
    /// </summary>
    public  class Flow_PleasepurchaseinfoView
    {
        /// <summary>
        /// 编号
        /// </summary>		
        private string _id;
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 生产计划单
        /// </summary>		
        private string _p_id;
        public virtual string P_Id
        {
            get { return _p_id; }
            set { _p_id = value; }
        }
        /// <summary>
        /// 元器件Id
        /// </summary>		
        private string _yqj_id;
        public virtual string Yqj_Id
        {
            get { return _yqj_id; }
            set { _yqj_id = value; }
        }
        /// <summary>
        /// 元器件物料代码
        /// </summary>		
        private string _yqj_bianhao;
        public virtual string Yqj_bianhao
        {
            get { return _yqj_bianhao; }
            set { _yqj_bianhao = value; }
        }
        /// <summary>
        /// 元器件名称
        /// </summary>		
        private string _yqj_name;
        public virtual string Yqj_Name
        {
            get { return _yqj_name; }
            set { _yqj_name = value; }
        }

        /// <summary>
        /// 元器件型号
        /// </summary>
        public virtual string Yqj_Namexh { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>		
        private decimal _cg_shuliang;
        public virtual decimal Cg_shuliang
        {
            get { return _cg_shuliang; }
            set { _cg_shuliang = value; }
        }
        /// <summary>
        /// 采购交期
        /// </summary>		
        private DateTime? _jqtime;
        public virtual DateTime? Jqtime
        {
            get { return _jqtime; }
            set { _jqtime = value; }
        }
        /// <summary>
        /// 0 待回复  1 采购中 2 采购完成 
        /// </summary>		
        private int _type;
        public virtual int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        private DateTime _c_time;
        public virtual DateTime C_time
        {
            get { return _c_time; }
            set { _c_time = value; }
        }
        /// <summary>
        /// 创建人
        /// </summary>		
        private string _c_name;
        public virtual string C_Name
        {
            get { return _c_name; }
            set { _c_name = value; }
        }

        /// <summary>
        /// 采购完成时间
        /// </summary>
        public virtual DateTime? wc_datetime { get; set; }

        /// <summary>
        /// 采购人
        /// </summary>
        public virtual int? cgy { get; set; }

        /// <summary>
        /// 实际采购数量
        /// </summary>
        public virtual decimal? sjcgsl { get; set; }
    }
}
