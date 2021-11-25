using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Product Category",
               url: "product/{metatitle}-{cateId}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
               new[] { "OnlineShop.Controllers" }
           );

            routes.MapRoute(
               name: "Product Detail",
               url: "detail/{metatitle}-{Id}",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               new[] { "OnlineShop.Controllers" }
           );

            routes.MapRoute(
              name: "About",
              url: "about",
              defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
              new[] { "OnlineShop.Controllers" }
          );

            routes.MapRoute(
             name: "Shop",
             url: "shop",
             defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Contact",
             url: "contact",
             defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Add Cart",
             url: "addcart",
             defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "OnlineShop.Controllers" }
            );


        }
    }
}
