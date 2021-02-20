using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class NQ_Supplier
    {
        /// <summary>
        /// 编号
        /// </summary>

        public virtual int FItemID { get; set; }

        public virtual int FItemClassID { get; set; }

        public virtual string FAddress { get; set; }

        public virtual string FPhone { get; set; }

        public virtual string FMobilePhone { get; set; }

        public virtual string FNumber { get; set; }

        public virtual int FLevel { get; set; }

        public virtual string FName { get; set; }

        public virtual string FContact { get; set; }

        public virtual string FShortNumber { get; set; }

        public virtual string FFullName { get; set; }

        public virtual string FLicence { get; set; }

        public virtual string FTaxNum { get; set; }

        public virtual string FISO9001 { get; set; }

        public virtual string FISO14000 { get; set; }

        public virtual string FPatent { get; set; }

        public virtual string FOther { get; set; }

        public virtual string FQuestionnaire { get; set; }

        public virtual string FAgent { get; set; }

        public virtual int FIsChecked { get; set; }

        public virtual int FIsDeleted { get; set; }

        public virtual byte[] FModifyTime { get; set; }

        public virtual int FSupplierType { get; set; }


    }
}
