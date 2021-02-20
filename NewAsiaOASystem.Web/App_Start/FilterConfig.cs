using System.Web;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserAuthorizeAttribute());
            //filters.Add(new AuthorFilter());
        }
    }
}