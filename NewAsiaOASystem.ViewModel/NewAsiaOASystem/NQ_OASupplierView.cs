using NewAsiaOASystem.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NQ_OASupplierView
    {
        public virtual int Id { get; set; }

        public virtual string FAddress { get; set; }

        public virtual string FPhone { get; set; }

        public virtual string FMobilePhone { get; set; }

        public virtual string FNumber { get; set; }

        public virtual string FName { get; set; }

        public virtual string FContact { get; set; }

        public virtual int FIsChecked { get; set; }

        public virtual int FIsDeleted { get; set; }

        public virtual int FSupplierType { get; set; }

        public virtual DateTime FCreateDate { get; set; }

        public virtual DateTime FUpdateDate { get; set; }

        public virtual string FCreateUser { get; set; }

        public virtual string FUpdateUser { get; set; }

        public virtual suStatus supplierStatus { get; set; }

        public string baseitems { get; set; }

        public virtual int itemsCount{get;set;}

    }

    public struct suStatus
    {
        //0:待审核,1:审核通过,2:审核未通过,3正常,4即将过期,5已经过期
        public int iStatus; 
        public string strStatusName;
        public string strStatusColor;

    }
}
