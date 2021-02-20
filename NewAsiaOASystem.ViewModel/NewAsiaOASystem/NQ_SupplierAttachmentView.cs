using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.ViewModel
{
    public class NQ_SupplierAttachmentView
    {
        public virtual int id { get; set; }

        public virtual int FSupplierid { get; set; }

        

        public virtual int FAttType { get; set; }

        public virtual string FAttURL { get; set; }

        public virtual DateTime FAttDeadline { get; set; }

        public virtual string FAttText { get; set; }

        public virtual int isPermanent { get; set; }

        public virtual int seq { get; set; }

        public virtual string FSuName { get; set; }

        public virtual string FShowdate { get; set; }

    }
}
