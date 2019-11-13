using System.Web.Mvc;
using System.Web.Routing;

namespace BlowOutNew
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Email",
               url: "{controller}/{action}/{firstName}/{lastName}/{emailAddress}/",
               defaults: new { controller = "Contact", action = "Email", firstName = UrlParameter.Optional, lastName = UrlParameter.Optional, emailAddress = UrlParameter.Optional }
           );          

            
        }
    }
}
