using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                if (dao.CheckProductName(product.Name))
                {
                    ModelState.AddModelError("", "Duplicate product name!");
                }
                else if (dao.CheckProductCode(product.Code))
                {
                    ModelState.AddModelError("", "Duplicate product code!");
                }
                else
                {
                    long id = dao.Insert(product);
                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Product " + product.Name + " Created Successfully!";
                        return RedirectToAction("Index", "Product");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();

                var result = dao.Update(product);

                if (result)
                {
                    //SetAlert("Saved Successfully", "success");
                    TempData["SuccessMessage"] = "Product " + product.Name + " Saved Successfully";
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Update product fail");
                }
            }
            return View();
        }

        [HttpPost]
        public JsonResult DeleteSelectedCheckbox(string[] data)
        {
            foreach (var id in data)
            {
                new ProductDao().Delete(Convert.ToInt32(id));
            }

            return Json(new { status = true });
        }
    }
}