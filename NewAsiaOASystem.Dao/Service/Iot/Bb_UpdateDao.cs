using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NHibernate;
using Spring.Context.Support;

namespace NewAsiaOASystem.Dao
{
    public class Bb_UpdateDao : ServiceConversion<Bb_UpdateView, Bb_Update>, IBb_UpdateDao
    {
        public bool insert(Bb_UpdateView bb_UpdateView)
        {
            Bb_Update model = GetData(bb_UpdateView);
            model.Bb_Time = DateTime.Now;
            return base.insert(model);
        }
    }
}
