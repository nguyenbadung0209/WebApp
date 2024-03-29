﻿using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Common;
using PagedList;
using System.Web.Script.Serialization;


namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            int totalUser = dao.CountUser(searchString); 
            float numberPage = (float)totalUser / pageSize;              
            var model = dao.ListAllPaging(searchString, page ,pageSize);   
            ViewBag.pageCurrent = page;
            ViewBag.numberPage = (int)Math.Ceiling(numberPage);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNewUser()
        {
            return PartialView("CreateUser");
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                if (dao.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "The username is already taken");
                }
                else if (dao.CheckEmail(user.Email))
                {
                    ModelState.AddModelError("", "Email already exists");
                }
                else
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        //SetAlert("Created Successfully", "success");
                        TempData["SuccessMessage"] = "User " + user.Name + " Created Successfully";
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return PartialView("CreateUser", user);
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var user = new UserDao().ViewDatail(id);
            return PartialView("EditUser", user);
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptedMd5Pas;
                }

                var result = dao.Update(user);

                if (result)
                {
                    //SetAlert("Saved Successfully", "success");
                    TempData["SuccessMessage"] = "User " + user.Name + " Saved Successfully";
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("", "Update user fail");
                }
            }
            return PartialView("EditUser", user);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dao = new UserDao();

        //        if (dao.CheckUserName(user.UserName))
        //        {
        //            ModelState.AddModelError("", "The username is already taken");
        //        }
        //        else if (dao.CheckEmail(user.Email))
        //        {
        //            ModelState.AddModelError("", "Email already exists");
        //        }
        //        else
        //        {
        //            var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
        //            user.Password = encryptedMd5Pas;
        //            long id = dao.Insert(user);
        //            if (id > 0)
        //            {
        //                SetAlert("Created Successfully", "success");
        //                TempData["SuccessMessage"] = "User " + user.Name + " Created Successfully";
        //                return RedirectToAction("Index", "User");
        //            }
        //        }

        //    }
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var user = new UserDao().ViewDatail(id);
        //    return View(user);
        //}


        //[HttpPost]
        //public ActionResult Edit(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var dao = new UserDao();

        //        if (!string.IsNullOrEmpty(user.Password))
        //        {
        //            var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
        //            user.Password = encryptedMd5Pas;
        //        }

        //        var result = dao.Update(user);

        //        if (result)
        //        {
        //            SetAlert("Saved Successfully", "success");
        //            TempData["SuccessMessage"] = "User " + user.Name + " Saved Successfully";
        //            return RedirectToAction("Index", "User");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Update user fail");
        //        }
        //    }
        //    return View();
        //}

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteSelectedCheckbox(string[] data)
        {
            if (data != null)
            {
                foreach (var id in data)
                {
                    new UserDao().Delete(Convert.ToInt32(id));
                }
            }
            else { return Json(new { status = false }); }

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new { status = result });
        }


    }
}