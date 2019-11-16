using System.Web.Mvc;
using System.Web.Routing;

namespace BlowOutNew
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //had to switch the routes around because there were problems with the "Email" route.  I did some troubleshooting
            //but was unsuccessful in finding the error.  I realize that by setting the "Default" route first, I an actually negating
            //the "Email" route altogether.

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Email",
               url: "{controller}/{action}/{firstName}/{lastName}/{emailAddress}/",
               defaults: new { controller = "Contact", action = "Contact", firstName = UrlParameter.Optional, lastName = UrlParameter.Optional, emailAddress = UrlParameter.Optional }
           );

            


        }
    }
}
