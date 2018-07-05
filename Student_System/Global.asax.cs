using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Student_System
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Server.ClearError();
            Response.Redirect("/ErrorPage/ErrorMessage");
        }
    }
}
