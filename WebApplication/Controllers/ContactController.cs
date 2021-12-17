using Model.Dao;
using Model.EF;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(SendContact send)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback();
                feedback.Name = send.Name;
                feedback.Phone = send.Mobile;
                feedback.Email = send.Email;
                feedback.Address = send.Address;
                feedback.Content = send.Content;
                feedback.CreatedDate = DateTime.Now;

                var id = new ContactDao().InsertFeedBack(feedback);
                if (id > 0)
                {
                    TempData["SuccessMessage"] = "Feedback Success!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["SuccessMessage"] = "Feedback Fail!";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ContactUs()
        {
            var list = new ContactDao().GetActiveContact();
            return PartialView(list);
        }

    }

}