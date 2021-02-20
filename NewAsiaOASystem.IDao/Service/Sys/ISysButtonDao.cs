﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface ISysButtonDao : IBaseDao<SysButtonView>
    {
        IList<SysButton> NGetListSysButton(string id);

        SysButtonView NGetModelById(string ID);
    }
}