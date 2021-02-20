using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IBb_UpdateDao
    {
        bool insert(Bb_UpdateView bb_UpdateView);
    }
}
