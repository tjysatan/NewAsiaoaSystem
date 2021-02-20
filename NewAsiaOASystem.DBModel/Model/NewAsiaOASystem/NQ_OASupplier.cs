using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NewAsiaOASystem.DBModel
{
    public class NQ_OASupplier
    {
        //id自增
        public virtual int Id { get; set; }

        //地址
        public virtual string FAddress { get; set; }
        //电话
        public virtual string FPhone { get; set; }
        //手机
        public virtual string FMobilePhone { get; set; }
        //编码
        public virtual string FNumber { get; set; }
        //名称
        public virtual string FName { get; set; }
        //联系人
        public virtual string FContact { get; set; }
        //审核状态, 0 待审核, 1 审核, 2 审核未通过
        public virtual int FIsChecked { get; set; }

        public virtual int FIsDeleted { get; set; }
        //供应商类型
        public virtual int FSupplierType { get; set; }
        //创建时间
        public virtual DateTime FCreateDate { get; set; }
        //更新时间
        public virtual DateTime FUpdateDate { get; set; }
        //创建用户
        public virtual string FCreateUser { get; set; }
        //更新用户
        public virtual string FUpdateUser { get; set; }

        private IList<NQ_YJinfo> _baseItems;
        public virtual IList<NQ_YJinfo> baseitems {
            get
            {
                if (_baseItems == null)
                {
                    _baseItems = new List<NQ_YJinfo>();
                    //_baseItems = _INQ_YJinfoDao.GetItemsWithSupplier(this.Id);
                }
                return _baseItems;
            }
            set { _baseItems = value; }
        }
    }

}
