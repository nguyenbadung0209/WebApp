using Model.Dao;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //ViewBag.Slides = new SlideDao().ListAll();
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(3);
            ViewBag.FeatureProducts = productDao.ListFeatureProduct(3);
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);

            return PartialView(model);
        }

        [ChildActionOnly]      
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);

            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var user = (UserLogin)Session[CommonConstanst.USER_SESSION];
            if (user != null) {
                var cart = new CartDao().ListByCustomerId(user.UserId);
                var list = new List<CartItem>();

                if (cart != null)
                {
                    foreach (var item in cart)
                    {
                        var product = new ProductDao().ViewDetail(item.ProductID);
                        var items = new CartItem();
                        items.Product = product;
                        items.Quantity = item.Quantity;
                        list.Add(items);
                    }
                }
                return PartialView(list);
            }
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();

            return PartialView(model);
        }
    }
}