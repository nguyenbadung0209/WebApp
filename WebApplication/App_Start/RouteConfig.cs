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

            routes.IgnoreRoute("{*botdetect}",
     new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

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
             name: "Cart",
             url: "cart",
             defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Payment",
             url: "payment",
             defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Add Cart",
             url: "addcart",
             defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Payment Sussess",
             url: "complete",
             defaults: new { controller = "Cart", action = "Sussess", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Login",
             url: "login",
             defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Register",
             url: "register",
             defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
             new[] { "OnlineShop.Controllers" }
         );

            routes.MapRoute(
             name: "Search",
             url: "search",
             defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
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
