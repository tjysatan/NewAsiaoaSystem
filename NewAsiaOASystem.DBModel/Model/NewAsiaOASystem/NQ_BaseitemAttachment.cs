using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewAsiaOASystem.DBModel
{
    public class NQ_BaseitemAttachment
    {
        public virtual int id { get; set; }

        public virtual int FItemid { get; set; }

        public virtual int FSupplierid { get; set; }

        public virtual int FAttType { get; set; }

        public virtual string FAttURL { get; set; }

        public virtual DateTime FAttDeadline { get; set; }

        public virtual string FAttText { get; set; }
        
    }
}
