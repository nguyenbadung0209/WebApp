using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Username available");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email already exists");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Register Success";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Register Fail");
                    }
                }
            }
            return View(model);
        }
    }
}