﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IAppPoint_infoDao
    {
        string GisUpload(Point_infoView point);
    }
}
