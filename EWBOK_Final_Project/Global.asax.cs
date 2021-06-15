using EWBOK_Final_Project.Common;
using EWBOK_Final_Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EWBOK_Final_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Response.Clear();
            HttpException httpException = ex as HttpException;
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            if (Session[Constants.ERROR_ADMIN] != null)
            {
                if (Session[Constants.ERROR_ADMIN].ToString() == Constants.ERROR_ADMIN.ToString())
                {
                    routeData.DataTokens.Add("area", "Admin");
                }
            }
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values.Add("action", "Error404");
                        break;
                    case 500:
                        routeData.Values.Add("action", "Error500");
                        break;
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
            }
            if (httpException != null)
            {
                IController errorcontroller = new ErrorController();
                errorcontroller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            }
        }
    }
}
