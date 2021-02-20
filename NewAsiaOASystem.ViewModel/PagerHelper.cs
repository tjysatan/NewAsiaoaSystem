using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSystem.ViewModel
{
    public class PagerHelper
    {
        private int listCount = 30;

        public int ListCount
        {
            get { return listCount; }
            set { listCount = value; }
        }

        private int indexPage = 0;

        public int IndexPage
        {
            get { return indexPage; }
            set { indexPage = value; }
        }

        private string strHql = "";

        public string StrHql
        {
            get { return strHql; }
            set { strHql = value; }
        }

    }
}
