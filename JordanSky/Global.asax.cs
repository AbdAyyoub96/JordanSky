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
                        action = "error_404.html";
                        break;
                    case 403:
                        action = "error_403.html";
                        break;
                    case 500:
                        action = "error_500.html";
                        break;
                    case 405:
                        action = "error_405.html";
                        break;
                    default:
                        action = "error_503.html";
                        break;
                }

                Server.ClearError();

                Response.Redirect(String.Format("~/Errors/{0}", action));
            }
            else
            {
                Response.Redirect(String.Format("~/Errors/error_503.html"));
            }
        }
    }
}
