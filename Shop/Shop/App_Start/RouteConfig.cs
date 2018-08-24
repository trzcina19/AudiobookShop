using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AudiobookDetails",
                url: "Audiobook-{id}.html",
                defaults: new { controller = "Audiobooks", action = "Details" }
                );

            routes.MapRoute(
                name: "AudiobooksList",
                url: "categories/{categoryName}.html",
                defaults: new { controller = "Audiobooks", action = "List" }
                );

            routes.MapRoute(
                name: "StaticPages",
                url: "staticpage/{name}.html",
                defaults: new { controller = "Home", action = "StaticPages" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
