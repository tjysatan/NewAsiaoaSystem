using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSystem.ViewModel
{
    public class PagerQuery<TPager, TEntityList> 
    {
        public PagerQuery(TPager pager, TEntityList entityList)
        {
            this.Pager = pager;
            this.EntityList = entityList;
        }
        public TPager Pager { get; set; }
        public TEntityList EntityList { get; set; }
    }
}
