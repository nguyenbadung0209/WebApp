using Model.Dao;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model.EF;
using System.Configuration;
using Common;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var user = (UserLogin)Session[CommonConstanst.USER_SESSION];
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
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            var user = (UserLogin)Session[CommonConstanst.USER_SESSION];
            var cart = new CartDao().ListByCustomerId(user.UserId);
            var delete = new CartDao().DeleteAll(cart);
            return Json(new { status = delete });
        }

        public JsonResult Delete(long id)
        {
            var user = (UserLogin)Session[CommonConstanst.USER_SESSION];
            var cart = new CartDao().ListByCustomerId(user.UserId);

            foreach (var item in cart)
            {
                if (item.ProductID == id)
                {
                    var delete = new CartDao().Delete(item);
                    return Json(new { status = delete });
                }
            }
            return Json(new { status = true });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var user = (UserLogin)Session[CommonConstanst.USER_SESSION];
            var cart = new CartDao().ListByCustomerId(user.UserId);

            foreach (var item in cart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.ProductID);
                item.Quantity = jsonItem.Quantity;
                var result = new CartDao().UpdateQuantity(item);
            }
            return Json(new { status = true });
        }

        public ActionResult AddItem(long customerId, long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = new CartDao().ListByCustomerId(customerId);
            var user = (UserLogin)Session[CommonConstanst.USER_SESSION];

            if (user == null)
            {
                TempData["SuccessMessage"] = "You need to log in to make a purchase !";
                return RedirectToAction("Login", "User");
            }
            else
            {
                if (cart != null)
                {
                    if (cart.Exists(x => x.ProductID == productId))
                    {
                        foreach (var item in cart)
                        {
                            if (item.ProductID == productId)
                            {
                                item.Quantity += quantity;
                                var result = new CartDao().UpdateQuantity(item);
                                if (result) { return RedirectToAction("Index"); }
                            }

                        }
                    }
                    else
                    {
                        var item = new Cart();
                        item.CustomerID = customerId;
                        item.ProductID = product.ID;
                        item.Quantity = quantity;
                        var result = new CartDao().Insert(item);
                        if (result > 0) { return RedirectToAction("Index"); }
                    }
                }
                else
                {
                    var item = new Cart();
                    item.CustomerID = customerId;
                    item.ProductID = product.ID;
                    item.Quantity = quantity;
                    long result = new CartDao().Insert(item);
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult CartItem()
        {
            var user = (UserLogin)Session[CommonConstanst.USER_SESSION];
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

        [HttpPost]
        public ActionResult Payment(PaymentModel payment)
        {
            if (ModelState.IsValid)
            {
                var user = (UserLogin)Session[CommonConstanst.USER_SESSION];
                var order = new Order();

                order.CreatedDate = DateTime.Now;
                order.CustomerID = user.UserId;
                order.ShipName = payment.ShipName;
                order.ShipMobile = payment.ShipMobile;
                order.ShipAddress = payment.ShipAddress;
                order.ShipEmail = payment.ShipEmail;

                var id = new OrderDao().Insert(order);
                var cart = new CartDao().ListByCustomerId(user.UserId);
                var detailDao = new OrderDetailDao();

                foreach (var item in cart)
                {
                    var product = new ProductDao().ViewDetail(item.ProductID);
                    var orderDetail = new OrderDetail();

                    orderDetail.ProductID = item.ProductID;
                    orderDetail.OrderID = id;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Price = product.Price;
                    detailDao.Insert(orderDetail);
                }

                var delete = new CartDao().DeleteAll(cart);
                if (delete == true) { return RedirectToAction("Success"); }

                //string content = System.IO.File.ReadAllText(Server.MapPath("/assets/client/template/neworder.html"));

                //content = content.Replace("{{CustomerName}}", shipName);
                //content = content.Replace("{{Phone}}", mobile);
                //content = content.Replace("{{Email}}", email);
                //content = content.Replace("{{Address}}", address);
                //content = content.Replace("{{Total}}", total.ToString("N0"));
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(email, "New Order from OnlineShop", content);
                //new MailHelper().SendMail(toEmail, "New Order from OnlineShop", content);
            }
            return View();

        }

        public ActionResult Success()
        {
            return View();
        }
    }
}