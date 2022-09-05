using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Work_01
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Roksana1",
            url: "Roksana1",
            defaults: new { controller = "Home", action = "About" }
            );
            routes.MapRoute(
            name: "Roksana2",
            url: "Roksana2",
            defaults: new { controller = "Home", action = "Contact" }
            );


            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
