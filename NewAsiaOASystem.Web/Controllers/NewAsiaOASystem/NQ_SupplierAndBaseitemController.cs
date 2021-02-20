using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.IDao;
using Spring.Context.Support;
using System.Text;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using NewAsiaOASystem.DBModel;
using NHibernate;
using System.Collections.Specialized;
using System.Globalization;

namespace NewAsiaOASystem.Web.Controllers
{
    public class NQ_SupplierAndBaseitemController : Controller
    {
        //
        INQ_BaseitemAttachmentDao _INQ_SupplierDao = ContextRegistry.GetContext().GetObject("NQ_BaseitemAttachmentDao") as INQ_BaseitemAttachmentDao;


        
    }
}
