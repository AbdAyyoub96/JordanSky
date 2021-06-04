using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JordanSky
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            var httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "error_404.html";
                        break;
                    case 403:
                        // forbidden
                        action = "error_403.html";
                        break;
                    case 500:
                        // server error
                        action = "error_500.html";
                        break;
                    case 405:
                        // server error
                        action = "error_405.html";
                        break;
                    default:
                        action = "error_503.html";
                        break;
                }

                // clear error on server
                Server.ClearError();

                Response.Redirect(String.Format("~/Errors/{0}", action));
            }
            else
            {
                // this is my modification, which handles any type of an exception.
                Response.Redirect(String.Format("~/Errors/error_503.html"));
            }
        }
    }
}
