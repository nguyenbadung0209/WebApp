using BotDetect.Web.Mvc;
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove(CommonConstanst.USER_SESSION);
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserId = user.ID;
                    userSession.UserName = user.UserName;

                    Session.Add(CommonConstanst.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account does not exist");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Account is locked");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Incorrect password");
                }
                else
                {
                    ModelState.AddModelError("", "Login fail");
                }

            }
            return View();
        }

        [HttpPost]

        [CaptchaValidation("CaptchaCode", "registerCapcha", "Incorrect code!")]
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
                        //return RedirectToAction("Index", "Home");
                        ViewBag.Success = "Register Success";
                    }
                    else
                    {
                        ModelState.AddModelError("", "Register Fail");
                    }
                }
            }
            return View();
        }
    }
}